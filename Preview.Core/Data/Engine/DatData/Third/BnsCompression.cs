using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace Xylia.Preview.Data.Engine.DatData.Third;
public static class BnsCompression
{
	public unsafe static byte[] GetRSAKeyBlob(RSAParameters param) => GetRSAKeyBlob(param.Exponent, param.Modulus, param.P, param.Q);

	public unsafe static byte[] GetRSAKeyBlob(byte[] exp, byte[] mod, byte[] p, byte[] q)
	{
		if (exp == null || mod == null || p == null || q == null)
		{
			throw new CryptographicException();
		}

		byte[] array = new byte[Marshal.SizeOf<BCRYPT_RSAKEY_BLOB>() + exp.Length + mod.Length + p.Length + q.Length];
		fixed (byte* ptr = array)
		{
			var ptr2 = (BCRYPT_RSAKEY_BLOB*)ptr;
			ptr2->Magic = KeyBlobMagicNumber.BCRYPT_RSAPRIVATE_MAGIC;
			ptr2->BitLength = mod.Length * 8;
			ptr2->cbPublicExp = exp.Length;
			ptr2->cbModulus = mod.Length;
			ptr2->cbPrime1 = p.Length;
			ptr2->cbPrime2 = q.Length;

			int num = Marshal.SizeOf<BCRYPT_RSAKEY_BLOB>();
			Emit(array, ref num, exp);
			Emit(array, ref num, mod);
			Emit(array, ref num, p);
			Emit(array, ref num, q);
		}

		return array;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void Emit(byte[] rsaBlob, ref int offset, byte[] value)
	{
		Buffer.BlockCopy(value, 0, rsaBlob, offset, value.Length);
		offset += value.Length;
	}


	[DllImport("bnscompression.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
	public static extern bool GetCreateParams([MarshalAs(UnmanagedType.LPWStr)] string fileName, [MarshalAs(UnmanagedType.U1)] out bool use64Bit, out CompressionLevel compressionLevel, out IntPtr encryptionKey, out uint encryptionKeySize, out IntPtr privateKeyBlob, out uint privateKeyBlobSize, out BinaryXmlVersion binaryXmlVersion);

	[DllImport("bnscompression.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
	public static extern double ExtractToDirectory([MarshalAs(UnmanagedType.LPWStr)] string sourceFileName, [MarshalAs(UnmanagedType.LPWStr)] string destinationDirectoryName, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 3)] byte[] encryptionKey, uint encryptionKeySize, Delegate d);

	[DllImport("bnscompression.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
	public static extern double CreateFromDirectory([MarshalAs(UnmanagedType.LPWStr)] string sourceDirectoryName, [MarshalAs(UnmanagedType.LPWStr)] string destinationFileName, [MarshalAs(UnmanagedType.U1)] bool use64Bit, CompressionLevel compressionLevel, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 5)] byte[] encryptionKey, uint encryptionKeySize, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 7)] byte[] privateKeyBlob, uint privateKeyBlobSize, BinaryXmlVersion binaryXmlVersion, Delegate d);

	public enum DelegateResult
	{
		Continue,
		Skip,
		Cancel
	}

	[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
	public delegate DelegateResult Delegate([MarshalAs(UnmanagedType.LPWStr)] string fileName, ulong fileSize);
}