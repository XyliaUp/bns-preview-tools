using System.Security.Cryptography;
using System.Text;

namespace Xylia.Preview.Data.Engine.DatData;
public static class PackageKey
{
	#region AES
	public static byte[] AES_2014 => Encoding.ASCII.GetBytes("bns_obt_kr_2014#");

	public static byte[] AES_2020_01 => Encoding.ASCII.GetBytes("ja#n_2@020_compl");

	public static byte[] AES_2020_02 => Encoding.ASCII.GetBytes("jan_2#0_cpl_bns!");

	public static byte[] AES_2020_03 => new byte[] { 166, 228, 20, 193, 142, 29, 181, 184, 107, 21, 47, 88, 66, 181, 193, 49 };

	public static byte[] AES_2020_04 => new byte[] { 56, 136, 117, 31, 170, 26, 76, 33, 186, 192, 59, 119, 197, 84, 103, 183 };

	public static byte[] AES_2020_05 => new byte[] { 23, 81, 170, 213, 30, 54, 74, 27, 254, 96, 116, 231, 208, 133, 7, 104 };
	#endregion

	#region XOR
	public static byte[] XOR_KEY_2014 => new byte[] { 164, 159, 216, 179, 246, 142, 57, 194, 45, 224, 97, 117, 92, 75, 26, 7 };

	public static byte[] XOR_KEY_2021 => new byte[] { 15, 19, 93, 85, 72, 248, 65, 249, 53, 24, 42, 132, 81, 92 };
	#endregion

	#region RSA
	public static RSAParameters RSA2 => new()
	{
		Exponent = Convert.FromBase64String("AQAB"),
		Modulus = Convert.FromBase64String("6frEEJqRXEuy/ttKNKxRZZdvqAgeSi0yDwMzMu4lZhtq4/sbojbQH2zkcsEUz6PJ7Ab9Zty2EuBDO1ZJoYN2Y0i1Pvi+avGGJbwTuHuPag352hxHwVPbBXZ//koxlL4J1J9FQKtEWHBCRkDM7UYVBkCQb5I6k9fEtyJejrzdmgk="),
		P = Convert.FromBase64String("8mJvLTFUS3w15GT+//Ok7xSZlO2SRtypmhcCrtAFUGDbmjmIT9Wg8Ll353yDGzFxZIVmiMblgdMrRnc1d7pf6Q=="),
		Q = Convert.FromBase64String("9x93N77SSk7vsZdzuS9eutc+zMKxk5fBYqndgK6gm8+mSfKWBm4CCKVWXNeuhIYtsgSOBeix5nYvjkymVpw1IQ=="),
		D = new byte[128],
		DP = new byte[64],
		DQ = new byte[64],
		InverseQ = new byte[64],
	};

	public static RSAParameters RSA3 => new()
	{
		Exponent = Convert.FromBase64String("AQAB"),
		Modulus = Convert.FromBase64String("4n/9xPwCpn2/TGXY0bCc23xXKdU9iobCl2RCMWTDgz17uh+Jl8W+Jvci+apyTyXDYdQH8nh2SKkUpAYsQy8bA9v8k+ZbYDytp/DAcHKBfY/1ccknJQrWStbzxQwRXSGsmWmY0vwCW2K7iTkWGQbxo0qRG/L10/qDXQLxf7bmyE8="),
		P = Convert.FromBase64String("6fXfeI2dhsT173QVhtOpYLqEQazrsf9opL8cps8j6XH5AzGpRDh0PePoXzhWTZA36nbyEJY0yqDrrBVBEQwRnQ=="),
		Q = Convert.FromBase64String("99Y0G0QkA62/hFBFmg5fI4vdsesCYGMZw+QwhdSJW87Z5fTZ8r8PamYzNudQPeiJhgQgAVjpFBG7K6Um6JRj2w=="),
		D = new byte[128],
		DP = new byte[64],
		DQ = new byte[64],
		InverseQ = new byte[64],
	};
	#endregion
}