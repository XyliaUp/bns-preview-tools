using CUE4Parse.UE4.Exceptions;
using CUE4Parse.UE4.Readers;

using Ionic.Zlib;

namespace CUE4Parse.Compression;
public static partial class Compression2
{
	public static byte[] Compress(byte[] uncompressed, int uncompressedSize, CompressionMethod method, int compressionLevel, FArchive reader = null)
	{
		switch (method)
		{
			case CompressionMethod.None:
				return uncompressed;

			case CompressionMethod.Zlib:
			{
				var srcStream = new MemoryStream();
				var zlib = new ZlibStream(srcStream, CompressionMode.Compress, (CompressionLevel)compressionLevel, true);
				zlib.Write(uncompressed, 0, uncompressedSize);
				zlib.Flush();
				zlib.Close();

				return srcStream.ToArray();
			}

			//case CompressionMethod.Gzip:
			//{
			//	var gzip = new GZipStream(srcStream, CompressionMode.Compress);
			//	var compressed = new byte[compressedSize];

			//	gzip.Read(compressed, compressedOffset, compressedSize);
			//	gzip.Dispose();
			//	return compressed;
			//}

			case CompressionMethod.Oodle:
				return OodleCompress.Compress(uncompressed, uncompressedSize, OodleFormat.Kraken, OodleCompressionLevel.Fast);


			//case CompressionMethod.LZ4:
			//{
			//	var compressed = new byte[compressedSize];
			//	var compressedBuffer = new byte[compressedSize + 4];

			//	var result = LZ4Codec.Encode(uncompressed, uncompressedOffset, uncompressedSize, compressedBuffer, 0, compressedBuffer.Length);
			//	Buffer.BlockCopy(compressedBuffer, 0, compressed, compressedOffset, compressedSize);
			//	if (result != compressedSize) throw new FileLoadException($"Failed to decompress LZ4 data (Expected: {compressedSize}, Result: {result})");
			//	//var lz4 = LZ4Stream.Decode(srcStream);
			//	//lz4.Read(compressed, compressedOffset, compressedSize);
			//	//lz4.Dispose();
			//	return compressed;
			//}

			default:
				if (reader != null) throw new UnknownCompressionMethodException(reader, $"Compression method \"{method}\" is unknown");
				else throw new UnknownCompressionMethodException($"未知的压缩模式：\"{method}\"");
		}
	}
}