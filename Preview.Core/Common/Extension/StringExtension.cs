using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Xylia.Preview.Common.Extension;
public static partial class String
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string GetDescription(this Enum value)
	{
		var fi = value.GetType().GetField(value.ToString());
		if (fi == null) return $"{value} ({value:D})";
		var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
		return attributes.Length > 0 ? attributes[0].Description : $"{value} ({value:D})";
	}



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
}