namespace Xylia.Preview.Data.Engine.DatData;
public static partial class Utils
{
	public static bool Judge64Bit(this FileInfo FileInfo) => FileInfo.Name.Judge64Bit();

	public static bool Judge64Bit(this string FilePath)
	{
		if (string.IsNullOrWhiteSpace(FilePath))
			return false;

		return Path.GetFileNameWithoutExtension(FilePath).Contains("64");
	}

	public static IEnumerable<FileInfo> GetFiles(this IEnumerable<FileInfo> files, bool? is64 = null)
	{
		if (is64 is null) return files;
		else if (is64.Value) return files.Where(f => f.Judge64Bit());
		else return files.Where(f => !f.Judge64Bit());
	}
}