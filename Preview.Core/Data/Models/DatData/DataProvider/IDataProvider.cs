using Xylia.Preview.Data.Models.DatData.DatDetect;

namespace Xylia.Preview.Data.Models.DatData.DataProvider;
public interface IDataProvider
{
	FileInfo[] GetFiles(string pattern);

	bool is64Bit() => true;
}


public class DefaultProvider : DataDetector , IDataProvider
{
	public DefaultProvider(string FolderPath) : base(FolderPath)
	{
		
	}


	FileInfo[] IDataProvider.GetFiles(string pattern) => throw new NotImplementedException();

	bool IDataProvider.is64Bit() => base.is64Bit;
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


	FileInfo[] IDataProvider.GetFiles(string pattern) => directory.GetFiles(pattern, SearchOption.AllDirectories);
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


	FileInfo[] IDataProvider.GetFiles(string pattern) => files.ToArray();
}