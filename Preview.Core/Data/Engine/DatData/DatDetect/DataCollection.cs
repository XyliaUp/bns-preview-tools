namespace Xylia.Preview.Data.Engine.DatData;
public class DataCollection
{
	#region Constructor
	private readonly Dictionary<DatType, List<FileInfo>> DataPathMenu = new();

	public DataCollection(string Folder) => Init(Folder);
	#endregion

	#region Methods
	private void Init(string Folder)
	{
		List<FileInfo> files = [];

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
					datType = DatType.Xml;
					break;

				case "config":
				case "config64":
					datType = DatType.Config;
					break;

				case "local":
				case "localfile":
				case "local64":
				case "localfile64":
					datType = DatType.Local;
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
		if (type == DatType.Xml && DataPathMenu.TryGetValue(DatType.Xml, out var fs)) result.AddRange(fs);
		else if (type == DatType.Local && DataPathMenu.TryGetValue(DatType.Local, out fs)) result.AddRange(fs);
		else if (type == DatType.Config && DataPathMenu.TryGetValue(DatType.Config, out fs)) result.AddRange(fs);

		// *
		if (mode == ResultMode.SelectBin) return result.Where(r => r.Extension == ".bin").ToList();
		if (mode == ResultMode.SelectDat) return result.Where(r => r.Extension == ".dat").ToList();

		return result;
	}
	#endregion
}


public enum DatType
{
	Local,
	Xml,
	Config,
}

public enum ResultMode
{
	All,
	SelectBin,
	SelectDat,
}