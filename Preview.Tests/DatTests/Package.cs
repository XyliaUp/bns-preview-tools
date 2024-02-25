using CUE4Parse.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xylia.Preview.Data.Engine.DatData;

namespace Xylia.Preview.Tests.DatTests;

[TestClass]
public partial class DatTests
{
	[TestMethod]
	public void Package()
	{
		var param = new PackageParam(@"D:\资源\客户端相关\Auto\data.pak")
		{
			FolderPath = @"D:\资源\客户端相关\Auto\data - 副本",
			CompressionLevel = CompressionLevel.Fast,
			BinaryXmlVersion = BinaryXmlVersion.None,
			CompressionMethod = CompressionMethod.Oodle,
		};

		BNSDat.CreateFromDirectory(param);
	}
}