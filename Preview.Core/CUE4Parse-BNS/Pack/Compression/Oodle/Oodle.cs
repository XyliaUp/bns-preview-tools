using System.Runtime.InteropServices;

using Xylia.Extension;

using static CUE4Parse.Compression.Oodle;

namespace CUE4Parse.Compression;
public static class OodleCompress
{
	public static byte[] Compress(byte[] buffer, int size, OodleFormat format, OodleCompressionLevel level = OodleCompressionLevel.Fast)
	{
		LoadOodleDll();

		uint compressedBufferSize = GetCompressionBound((uint)size);
		byte[] compressedBuffer = new byte[compressedBufferSize];
		long compressedCount = OodleLZ_Compress(format, buffer, size, compressedBuffer, level, 0L, 0L, 0L, 0L, 0L);

		byte[] outputBuffer = new byte[compressedCount];
		Buffer.BlockCopy(compressedBuffer, 0, outputBuffer, 0, (int)compressedCount);

		return outputBuffer;
	}



	[DllImport(OODLE_DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
	private static extern long OodleLZ_Compress(OodleFormat format, byte[] buffer, long bufferSize, byte[] result, OodleCompressionLevel level, long unused1, long unused2, long unused3, long unused4, long unused5);

	private static uint GetCompressionBound(uint bufferSize)
	{
		return bufferSize + 274 * ((bufferSize + 0x3FFFF) / 0x40000);
	}
}
