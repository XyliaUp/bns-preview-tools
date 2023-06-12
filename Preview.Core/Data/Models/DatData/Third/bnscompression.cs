using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

// Token: 0x02000004 RID: 4
public static class bnscompression
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void Emit(byte[] rsaBlob, ref int offset, byte[] value)
	{
		Buffer.BlockCopy(value, 0, rsaBlob, offset, value.Length);
		offset += value.Length;
	}

	public unsafe static byte[] GetRSAKeyBlob(byte[] exp, byte[] mod, byte[] p, byte[] q)
	{
		if (exp == null || mod == null || p == null || q == null)
		{
			throw new CryptographicException();
		}

		byte[] array2;
		byte[] array = array2 = new byte[Marshal.SizeOf<BCRYPT_RSAKEY_BLOB>() + exp.Length + mod.Length + p.Length + q.Length];
		fixed (byte* ptr = array2) {
			BCRYPT_RSAKEY_BLOB* ptr2 = (BCRYPT_RSAKEY_BLOB*)ptr;
			ptr2->Magic = KeyBlobMagicNumber.BCRYPT_RSAPRIVATE_MAGIC;
			ptr2->BitLength = mod.Length * 8;
			ptr2->cbPublicExp = exp.Length;
			ptr2->cbModulus = mod.Length;
			ptr2->cbPrime1 = p.Length;
			ptr2->cbPrime2 = q.Length;
			int num = Marshal.SizeOf<BCRYPT_RSAKEY_BLOB>();
			bnscompression.Emit(array, ref num, exp);
			bnscompression.Emit(array, ref num, mod);
			bnscompression.Emit(array, ref num, p);
			bnscompression.Emit(array, ref num, q);
		}
		array2 = null;
		return array;
	}

	[DllImport("bnscompression.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
	public static extern bool GetCreateParams([MarshalAs(UnmanagedType.LPWStr)] string fileName, [MarshalAs(UnmanagedType.U1)] out bool use64Bit, out bnscompression.CompressionLevel compressionLevel, out IntPtr encryptionKey, out uint encryptionKeySize, out IntPtr privateKeyBlob, out uint privateKeyBlobSize, out bnscompression.BinaryXmlVersion binaryXmlVersion);

	[DllImport("bnscompression.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
	public static extern double ExtractToDirectory([MarshalAs(UnmanagedType.LPWStr)] string sourceFileName, [MarshalAs(UnmanagedType.LPWStr)] string destinationDirectoryName, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 3)] byte[] encryptionKey, uint encryptionKeySize, bnscompression.Delegate d);

	[DllImport("bnscompression.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
	public static extern double CreateFromDirectory([MarshalAs(UnmanagedType.LPWStr)] string sourceDirectoryName, [MarshalAs(UnmanagedType.LPWStr)] string destinationFileName, [MarshalAs(UnmanagedType.U1)] bool use64Bit, bnscompression.CompressionLevel compressionLevel, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 5)] byte[] encryptionKey, uint encryptionKeySize, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 7)] byte[] privateKeyBlob, uint privateKeyBlobSize, bnscompression.BinaryXmlVersion binaryXmlVersion, bnscompression.Delegate d);

	public enum CompressionLevel
	{
		// Token: 0x04000035 RID: 53
		None = -1,
		// Token: 0x04000036 RID: 54
		Fastest,
		// Token: 0x04000037 RID: 55
		Fast,
		// Token: 0x04000038 RID: 56
		Normal,
		// Token: 0x04000039 RID: 57
		Maximum
	}

	public enum BinaryXmlVersion
	{
		// Token: 0x0400003B RID: 59
		None = -1,
		// Token: 0x0400003C RID: 60
		Version3,
		// Token: 0x0400003D RID: 61
		Version4
	}

	public enum DelegateResult
	{
		// Token: 0x0400003F RID: 63
		Continue,
		// Token: 0x04000040 RID: 64
		Skip,
		// Token: 0x04000041 RID: 65
		Cancel
	}

	[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
	public delegate DelegateResult Delegate([MarshalAs(UnmanagedType.LPWStr)] string fileName, ulong fileSize);
}
