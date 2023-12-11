using System.Runtime.CompilerServices;
using System.Text;

namespace Xylia.Preview.Common.Extension;
public static partial class String
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string TitleCase(this string line)
	{
		StringBuilder sb = new();
		for (int i = 0; i < line.Length; i++)
		{
			if ((i == 0 || (line[i - 1] == '-')) && line[i] >= 'a' && line[i] <= 'z') sb.Append((char)(line[i] - 32));
			else if (line[i] != '-') sb.Append(line[i]);
		}

		return sb.ToString();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string TitleLowerCase(this string line)
	{
		StringBuilder sb = new();
		for (int i = 0; i < line.Length; i++)
		{
			if (line[i] >= 'A' && line[i] <= 'Z')
			{
				if (i != 0) sb.Append('-');

			   sb.Append((char)(line[i] + 32));
			}
			else sb.Append(line[i]);
		}

		return sb.ToString();
	}




	public static byte ToByte(this string s) => byte.TryParse(s, out var result) ? result : (byte)0;
	public static sbyte ToInt8(this string s) => sbyte.TryParse(s, out var result) ? result : (sbyte)0;
	public static short ToInt16(this string s) => short.TryParse(s, out var result) ? result : (short)0;
	public static int ToInt32(this string s) => int.TryParse(s, out var result) ? result : 0;
	public static long ToInt64(this string s) => long.TryParse(s, out var result) ? result : 0;
	public static float ToFloat32(this string s) => float.TryParse(s, out var result) ? result : 0;
	public static double ToDouble(this string s) => double.TryParse(s, out var result) ? result : 0;


	public static bool ToBool(this string s, out bool Result)
	{
		Result = false;
		if (string.IsNullOrWhiteSpace(s)) return false;
		else if (int.TryParse(s, out int result))
		{
			Result = result != 0;
			return true;
		}


		s = s.ToLower();
		if (s == "y" || s == "true" || s == "t")
		{
			Result = true;
			return true;
		}
		else if (s == "n" || s == "false" || s == "f")
		{
			Result = false;
			return true;
		}
		else return false;
	}

	public static bool ToBool(this string s) => ToBool(s, out bool Result) && Result;
}