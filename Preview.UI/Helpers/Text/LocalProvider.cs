using System.IO;
using System.Net;
using System.Xml;

using Xylia.Preview.Data.Engine.BinData.Definitions;
using Xylia.Preview.Data.Engine.BinData.Helpers;
using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.Data.Engine.DatData.Third;

namespace Xylia.Preview.UI.Helpers;
public class LocalProvider(string Source) : DefaultProvider
{
	public override string Name => Path.GetFileName(Source);

	public override Stream[] GetFiles(string pattern) => new Stream[] { File.Open(pattern, FileMode.Open) };

	public override void LoadData(List<TableDefinition> definitions)
	{
		this.Tables = new();

		// invalid path
		if (string.IsNullOrWhiteSpace(Source)) return;

		var ext = Path.GetExtension(Source);
		switch (ext)
		{
			case ".dat":
			{
				LocalData = new BNSDat(Source);
				is64Bit = LocalData.Bit64;
				ReadFrom(LocalData.EnumerateFiles(is64Bit ? "localfile64.bin" : "localfile.bin").FirstOrDefault()?.Data, is64Bit);

				// detect text table type
				Detect = new DatafileDetect(this);
				Detect.ParseType(definitions);
				break;
			}

			case ".xml":
			{
				var text = definitions.First(x => x.Name.Equals("text"));
				Tables.Add(new() { Name = text.Name, XmlPath = Source });

				break;
			}
		}
	}


	/// <summary>
	/// <see langword="Source"/> must be a dat file
	/// </summary>
	/// <param name="file">replace data path</param>
	/// <param name="full">determin full-replace mode</param>
	public void Rewrite(string file, bool full = false)
	{
		var table = this.Tables["text"];

		if (full)
		{
			// Reload records
			table.XmlPath = file;
			table.LoadAsync(true);
		}
		else
		{
			XmlDocument xml = new();
			xml.Load(file);

			foreach (XmlElement element in xml.DocumentElement.SelectNodes($"./" + table.Definition.ElRecord.Name))
			{
				var alias = element.Attributes["alias"]?.Value;
				var text =  element.Attributes["text"]?.Value ?? element.InnerXml;

				// HACK: reslove html tags, transcoding should not be done in output
				text = WebUtility.HtmlDecode(text);

				var record = table[alias];
				if (record != null) record.StringLookup.Strings = [alias, text];
			}
		}

		#region Repack
		var replaces = new Dictionary<string, byte[]>();
		replaces.Add("localfile64.bin", this.WriteTo(true));

		MySpport.PackParam param = new() { PackagePath = Source, Bit64 = true };
		MySpport.Extract(param);
		MySpport.Pack(param, replaces);
		#endregion
	}
}