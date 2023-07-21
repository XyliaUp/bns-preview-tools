namespace Xylia.Preview.Data.Models.DatData.DataProvider;
public interface IDataProvider
{
	FileInfo[] GetFiles(string pattern);
}


public class FolderProvider : IDataProvider
{
	#region Constructor
	private readonly DirectoryInfo directory;

	public FolderProvider(string path)
	{
		directory = new(path);
	}
	#endregion


	public FileInfo[] GetFiles(string pattern) => directory.GetFiles(pattern, SearchOption.AllDirectories);
}

public class FileProvider : IDataProvider
{
	#region Constructor
	public List<FileInfo> files;

	public FileProvider(params FileInfo[] file)
	{
		this.files.AddRange(file);
	}
	#endregion
	

	public FileInfo[] GetFiles(string pattern) => files.ToArray();
}

