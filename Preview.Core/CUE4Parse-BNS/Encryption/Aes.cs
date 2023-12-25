using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using CUE4Parse.Utils;

using AesProvider = System.Security.Cryptography.Aes;

namespace CUE4Parse.Encryption.Aes;
public static partial class AesEncrypt
{
	private static readonly AesProvider Provider;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static byte[] Encrypt(this byte[] decrypted, FAesKey key)
	{
		return Provider.CreateEncryptor(key.Key, null).TransformFinalBlock(decrypted, 0, decrypted.Length);
	}

	public static void Align(ref byte[] bytes)
	{
		var size = bytes.Length.Align(Aes.ALIGN);
		if (size != bytes.Length)
		{
			var index = 0;
			var temp = new byte[size];
			while (index < size)
			{
				Buffer.BlockCopy(bytes, 0, temp, index, Math.Min(bytes.Length, size - index));
				index += bytes.Length;
			}

			bytes = temp;
		}
	}


	static AesEncrypt()
	{
		Provider = AesProvider.Create();
		Provider.Mode = CipherMode.ECB;
		Provider.Padding = PaddingMode.None;
		Provider.BlockSize = Aes.BLOCK_SIZE;
	}
}