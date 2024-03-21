using CUE4Parse.UE4.Exceptions;

using Ionic.Zlib;
using K4os.Compression.LZ4;

namespace CUE4Parse.Compression;
public static partial class Compression2
{
	public static byte[] Compress(byte[] uncompressed, int uncompressedSize, CompressionMethod method, int compressionLevel)
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

			case CompressionMethod.Gzip:
			{
				var srcStream = new MemoryStream();
				var gzip = new GZipStream(srcStream, CompressionMode.Compress);
				gzip.Write(uncompressed, 0, uncompressedSize);
				gzip.Flush();
				gzip.Dispose();

				return srcStream.ToArray();
			}

			case CompressionMethod.Oodle:
				return OodleCompress.Compress(uncompressed, uncompressedSize, OodleFormat.Kraken, (OodleCompressionLevel)compressionLevel);

			case CompressionMethod.LZ4:
			{
				var compressedBuffer = new byte[0];

				LZ4Codec.Encode(uncompressed, compressedBuffer, (LZ4Level)compressionLevel);
				return compressedBuffer;
			}

			default: throw new UnknownCompressionMethodException($"Compression method \"{method}\" is unknown");
		}
	}
}