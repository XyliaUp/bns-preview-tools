using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using System.Xml;
using CUE4Parse.Utils;
using K4os.Hash.xxHash;
using Serilog;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.Data.Engine.BinData.Serialization;
public class ProviderSerialize(IDataProvider Provider)
{
	private const string hash = "hashes.txt";

	private event Action DataSaved;


	public async Task ExportAsync(string folder, Action<int, int> progress, params Table[] tables) => await Task.Run(() =>
	{
		var hashes = new ConcurrentBag<HashInfo>();

		int current = 0;
		progress(current, tables.Length);

		Parallel.ForEach(tables, table =>
		{
			try
			{
				var hash = table.WriteXml(folder);
				hash.ForEach(x => hashes.Add(x));

				progress(++current, tables.Length);
			}
			catch(Exception ex)
			{
				++current;
				Log.Error($"output failed, table: `{table.Name}` error: {ex.Message}");
			}
		});

		// save hashes
		if (hashes.Count > 1)
		{
			File.WriteAllLines(Path.Combine(folder, hash), hashes.OrderBy(x => x.Path).Select(x => $"{x.Path}={x.Hash}"));
		}
	});

	public async Task ImportAsync(string folder) => await Task.Run(() =>
	{
		// load cached hash
		var root = new DirectoryInfo(folder);
		var hashes = new Dictionary<string, ulong>();
		var modifiedHashes = new ConcurrentBag<HashInfo>();

		var hashpath = Path.Combine(folder, hash);
		if (File.Exists(hashpath))
		{
			hashes = File.ReadAllLines(hashpath)
				.Where(x => !string.IsNullOrWhiteSpace(x) && x.IndexOf('=') > 0)
				.Select(x => x.Split('=', 2))
				.ToDictionary(x => x[0], x => ulong.Parse(x[1]));
		}

		// load xml table
		var buildActions = new ConcurrentBag<List<Action>>();
		foreach (var table in Provider.Tables)
		{
			Stream[] files = null;
			string pattern = table.SearchPattern ?? $"{table.Name.TitleCase()}Data*.xml";

			try
			{
				bool modified = false;
				files = root.GetFiles(pattern, SearchOption.AllDirectories).Select(file =>
				{
					var stream = file.OpenRead();

					// Hash the xml file
					byte[] data = new byte[stream.Length];
					stream.Read(data, 0, data.Length);
					stream.Seek(0, SeekOrigin.Begin);
					var hash = XXH64.DigestOf(data);

					// Check if hash changed
					var name = file.FullName.SubstringAfter(folder).Trim('\\');
					if (!hashes.TryGetValue(name, out var originalHash) || originalHash != hash) modified = true;

					modifiedHashes.Add(new HashInfo(name, hash));
					return stream;
				}).ToArray();

				// return if no modification
				if (modified)
				{
					var actions = table.LoadXml(files);
					buildActions.Add(actions);
				}
			}
			catch (DirectoryNotFoundException)
			{
				// Miss quest will cause DirectoryNotFoundException, no need to throw.
			}
			finally
			{
				// close file 
				files?.ForEach(x => x.Close());
			}
		}

		// build after all tables are loaded
		foreach (var actions in buildActions)
		{
			actions.ForEach(a => a.Invoke());
		}


		// load replace information
		var Replace = root.GetDirectories("Replace").FirstOrDefault();
		if (Replace != null)
		{
			foreach (var xml in Replace.GetFiles("*.xml"))
			{
				XmlDocument doc = new();
				doc.Load(xml.FullName);

				foreach (XmlElement _table in doc.SelectNodes("table"))
				{
					var type = _table.GetAttribute("type");
					var table = Provider.Tables[type];
					if (table is null) continue;

					foreach (XmlElement _record in _table.SelectNodes("./record"))
					{
						var alias = new Regex(_record.GetAttribute("alias"));
						var attributes =
							from element in _record.SelectNodes("./attribute").OfType<XmlElement>()
							select (element.GetAttribute("name"), element.GetAttribute("value"));

						// HACK: 
						Parallel.ForEach(table.Records, record =>
						{
							if (!alias.IsMatch(record.ToString())) return;

							foreach (var attr in attributes)
							{
								record.Attributes.Set(record.Definition[attr.Item1], attr.Item2);
							}
						});
					}
				}
			}
		}


		// invoke save action
		DataSaved = new Action(() =>
		{
			foreach (var item in modifiedHashes)
				hashes[item.Path] = item.Hash;

			File.WriteAllLines(hashpath, hashes.OrderBy(x => x.Key).Select(x => $"{x.Key}={x.Value}"));
		});
	});

	public async Task SaveAsync(string savePath) => await Task.Run(() =>
	{
		Provider.WriteData(savePath, new() { Is64bit = true, Mode = Mode.Datafile });
		DataSaved?.Invoke();
	});
}