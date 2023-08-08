namespace Xylia.Preview.Data.Models.DatData.DatDetect;
public class DataCollection
{
	#region Constructor
	private readonly Dictionary<DatType, List<FileInfo>> DataPathMenu = new();

	public DataCollection(string Folder) => Init(Folder);
	#endregion

	#region Functions
	private void Init(string Folder)
	{
		List<FileInfo> files = new();

		var DirInfo = new DirectoryInfo(Folder);
		files.AddRange(DirInfo.GetFiles("*.dat", SearchOption.AllDirectories));
		files.AddRange(DirInfo.GetFiles("*.bin", SearchOption.AllDirectories));

		foreach (var file in files)
		{
			DatType datType;
			switch (Path.GetFileNameWithoutExtension(file.Name).ToLower())
			{
				case "xml":
				case "datafile":
				case "xml64":
				case "datafile64":
					datType = DatType.xml;
					break;

				case "config":
				case "config64":
					datType = DatType.config;
					break;

				case "local":
				case "localfile":
				case "local64":
				case "localfile64":
					datType = DatType.local;
					break;

				default: continue;
			}


			//add
			if (!DataPathMenu.ContainsKey(datType))
				DataPathMenu.Add(datType, new());

			DataPathMenu[datType].Add(file);
		}
	}

	public List<FileInfo> GetFiles(DatType type, ResultMode mode)
	{
		var result = new List<FileInfo>();
		if (type == DatType.xml && DataPathMenu.TryGetValue(DatType.xml, out var fs)) result.AddRange(fs);
		else if (type == DatType.local && DataPathMenu.TryGetValue(DatType.local, out fs)) result.AddRange(fs);
		else if (type == DatType.config && DataPathMenu.TryGetValue(DatType.config, out fs)) result.AddRange(fs);

		// *
		if (mode == ResultMode.SelectBin) return result.Where(r => r.Extension == ".bin").ToList();
		if (mode == ResultMode.SelectDat) return result.Where(r => r.Extension == ".dat").ToList();

		return result;
	}
	#endregion
}


[Flags]
public enum DatType
{
	bit32 = 0x00000000,
	bit64 = 0x00000001,

	local = 0x10000000,
	xml = 0x20000000,
	config = 0x30000000,
}

public enum ResultMode
{
	All,
	SelectBin,
	SelectDat,
}