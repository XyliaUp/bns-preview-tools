using System.Runtime.InteropServices;

namespace Xylia.Preview.Data.Common.DataStruct;

[StructLayout(LayoutKind.Sequential)]
public struct Box(Vector16 l, Vector16 u)
{
	public Vector16 L = l;
	public Vector16 U = u;

	public static Box Parse(string input)
	{
		var items = input.Split([',', '/']);
		if (items.Length != 6)
			throw new ArgumentException("Invalid Box string input");

		short x = Convert.ToInt16(items[0]);
		short y = Convert.ToInt16(items[1]);
		short z = Convert.ToInt16(items[2]);
		short x2 = Convert.ToInt16(items[3]);
		short y2 = Convert.ToInt16(items[4]);
		short z2 = Convert.ToInt16(items[5]);
		return new Box(new Vector16(x, y, z), new Vector16(x2, y2, z2));
	}

	public override readonly int GetHashCode() => HashCode.Combine(L, U);

	public override readonly string ToString() => $"{L},{U}";
}