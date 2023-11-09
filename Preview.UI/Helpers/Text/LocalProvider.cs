using System.IO;

using Xylia.Preview.Data.Engine.BinData.Definitions;
using Xylia.Preview.Data.Engine.BinData.Helpers;
using Xylia.Preview.Data.Engine.DatData;

namespace Xylia.Preview.UI.Helpers;
public class LocalProvider : DefaultProvider
{
	#region Constructor
	private readonly string path;

	public LocalProvider(string path)
	{
		this.path = path;
		this.Name = Path.GetFileName(path);
	}
	#endregion

	#region Methods
	public override Stream[] GetFiles(string pattern) => new Stream[] { File.Open(path, FileMode.Open) };

	public override void LoadData(List<TableDefinition> definitions)
	{
		var ext = Path.GetExtension(path);
		switch (ext)
		{
			case ".dat":
			{
				LocalData = new BNSDat(path);
				is64Bit = LocalData.Bit64;
				ReadFrom(LocalData.EnumerateFiles(is64Bit ? "localfile64.bin" : "localfile.bin").FirstOrDefault()?.Data, is64Bit);

				// detect text table type
				Detect = new DatafileDetect();
				Detect.Read(this);
				Detect.Convert(definitions);
				break;
			}

			case ".xml":
			{
				var text = definitions.First(x => x.Name.Equals("text"));
				Tables.Add(new() { Name = text.Name, XmlPath = path });

				break;
			}
		}
	}
	#endregion
}