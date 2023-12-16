using System.IO;
using System.Xml;
using Xylia.Preview.Data.Engine.BinData.Helpers;
using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.Data.Engine.DatData.Third;
using Xylia.Preview.Data.Engine.Definitions;

namespace Xylia.Preview.UI.Helpers;
public class LocalProvider(string Source) : DefaultProvider
{
	public override string Name => Path.GetFileName(Source);

	public override Stream[] GetFiles(string pattern) => new Stream[] { File.OpenRead(pattern) };

	public override void LoadData(DatafileDefinition definitions)
	{
		this.Tables = [];

		// invalid path
		if (string.IsNullOrWhiteSpace(Source)) return;

		var ext = Path.GetExtension(Source);
		switch (ext)
		{
			case ".xml" or ".x16":
				Tables.Add(new() { Owner = this, Name = "text", XmlPath = Source });
				break;

			case ".dat":
			{
				LocalData = new BNSDat(Source);
				Is64Bit = LocalData.Bit64;
				ReadFrom(LocalData.EnumerateFiles(Is64Bit ? "localfile64.bin" : "localfile.bin").FirstOrDefault()?.Data, Is64Bit);

				// detect text table type
				if (definitions.HasHeader) Detect = new DatafileDirect(definitions.Header);
				else Detect = new DatafileDetect(this);
				Detect.ParseType(definitions);
			}
			break;

		}
	}


	/// <summary>
	/// Replace existed text
	/// </summary>
	/// <param name="files">x16 file path</param>
	public void ReplaceText(FileInfo[] files)
	{
		var table = this.Tables["text"];
		ArgumentNullException.ThrowIfNull(table);

		foreach (var file in files)
		{
			XmlDocument xml = new();
			xml.Load(file.FullName);

			foreach (XmlElement element in xml.DocumentElement.SelectNodes($"./" + table.Definition.ElRecord.Name))
			{
				var alias = element.Attributes["alias"]?.Value;
				var text = element.InnerXml;

				var record = table[alias];
				if (record != null) record.StringLookup.Strings = [alias, text];
			}
		}
	}

	/// <summary>
	/// Save as dat
	/// </summary>
	/// <remarks>
	/// <see langword="Source"/> must be a dat file
	/// </remarks>
	/// <param name="text"></param>
	public void Save(byte[] data)
	{
		var table = this.Tables["text"];
		ArgumentNullException.ThrowIfNull(table);

		using var stream = new MemoryStream(data);
		var actions = table.LoadXml(stream);
		actions.ForEach(a => a.Invoke());

		WriteData(Source, Is64Bit);
	}


	public override void WriteData(string folder, bool is64bit)
	{
		var replaces = new Dictionary<string, byte[]>();
		replaces.Add(Is64Bit ? "localfile64.bin" : "localfile.bin", WriteTo([.. this.Tables], is64bit));

		MySpport.PackParam param = new() { PackagePath = folder, Bit64 = true };
		MySpport.Extract(param);
		MySpport.Pack(param, replaces);
	}
}