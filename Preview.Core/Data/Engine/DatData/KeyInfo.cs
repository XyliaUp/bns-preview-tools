using System.Text;

namespace Xylia.Preview.Data.Engine.DatData;
public class KeyInfo
{
	#region Keys
	public static byte[] AES_2014 => Encoding.ASCII.GetBytes("bns_obt_kr_2014#");

	public static byte[] AES_2020_01 => Encoding.ASCII.GetBytes("ja#n_2@020_compl");

	public static byte[] AES_2020_02 => Encoding.ASCII.GetBytes("jan_2#0_cpl_bns!");

	public static byte[] AES_2020_03 => new byte[] { 166, 228, 20, 193, 142, 29, 181, 184, 107, 21, 47, 88, 66, 181, 193, 49 };

	public static byte[] AES_2020_04 => new byte[] { 56, 136, 117, 31, 170, 26, 76, 33, 186, 192, 59, 119, 197, 84, 103, 183 };

	public static byte[] AES_2020_05 => new byte[] { 23, 81, 170, 213, 30, 54, 74, 27, 254, 96, 116, 231, 208, 133, 7, 104 };


	public static byte[] XOR_KEY_2014 => new byte[] { 240, 200, 186, 170, 18, 31, 130, 159, 172, 24, 84, 33, 138, 58 };

	public static byte[] XOR_KEY_2021 => new byte[] { 164, 159, 216, 179, 246, 142, 57, 194, 45, 224, 97, 117, 92, 75, 26, 7 };
	#endregion


	public byte[] XOR_KEY { get; set; } = XOR_KEY_2021;

	public byte[] AES_KEY { get; set; } = AES_2020_05;
}