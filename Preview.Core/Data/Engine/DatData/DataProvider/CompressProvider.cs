using CUE4Parse.Compression;

namespace Xylia.Preview.Data.Engine.DatData;
public class CompressProvider : FolderProvider
{
	private readonly BNSDat Package;

	public CompressProvider(string filepath) : base(filepath)
	{
		if (!File.Exists(filepath)) 
			throw new FileNotFoundException();

		Package = new BNSDat(new PackageParam(filepath)
		{
			BinaryXmlVersion = BinaryXmlVersion.None,
			CompressionMethod = CompressionMethod.Oodle,
		});
	}

	public override Stream[] GetFiles(string pattern) => Package!.SearchFiles(pattern).Select(x => new MemoryStream(x.Data)).ToArray();
}