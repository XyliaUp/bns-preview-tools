using System.Runtime.InteropServices;

namespace Xylia.Preview.Data.Common.DataStruct;
[StructLayout(LayoutKind.Sequential)]
public struct Vector16
{
    public short X;
    public short Y;
    public short Z;

    public Vector16(short x, short y, short z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static bool operator ==(Vector16 a, Vector16 b)
    {
        return
            a.X == b.X &&
            a.Y == b.Y &&
            a.Z == b.Z;
    }

    public static bool operator !=(Vector16 a, Vector16 b)
    {
        return !(a == b);
    }

    public static Vector16 Parse(string input)
    {
        var items = input.Split(',');

        if (items.Length != 3)
            throw new ArgumentException("Invalid Vector16 string input");

        return new Vector16(
            short.Parse(items[0]),
            short.Parse(items[1]),
            short.Parse(items[2])
        );
    }

    public bool Equals(Vector16 other)
    {
        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public override bool Equals(object obj)
    {
        return obj is Vector16 other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public override string ToString() => $"{X},{Y},{Z}";


	public static double Distance(Vector16 v1, Vector16 v2)
	{
		return Math.Sqrt(SquareDistance(v1, v2));
	}

	public static long SquareDistance(Vector16 v1, Vector16 v2)
	{
		var num = v1.X - v2.X;
		var num2 = v1.Y - v2.Y;
		var num3 = v1.Z - v2.Z;
		return Math.BigMul(num, num) + Math.BigMul(num2, num2) + Math.BigMul(num3, num3);
	}
}