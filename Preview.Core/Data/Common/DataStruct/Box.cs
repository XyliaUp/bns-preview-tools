using System.Runtime.InteropServices;

namespace Xylia.Preview.Data.Common.DataStruct;

[StructLayout(LayoutKind.Sequential)]
public struct Box
{
	public Vector32 Start;
	public Vector32 End;

	public Box(short x1, short y1, short z1, short x2, short y2, short z2)
	{
		Start.X = x1;
		Start.Y = y1;
		Start.Z = z1;
		End.X = x2;
		End.Y = y2;
		End.Z = z2;
	}

	public static bool operator ==(Box a, Box b)
	{
		return a.Start == b.Start && a.End == b.End;
	}

	public static bool operator !=(Box a, Box b)
	{
		return !(a == b);
	}

	public static Box Parse(string input)
	{
		var items = input.Split(',');

		if (items.Length != 6)
			throw new ArgumentException("Invalid Box string input");

		return new Box(
			short.Parse(items[0]),
			short.Parse(items[1]),
			short.Parse(items[2]),
			short.Parse(items[3]),
			short.Parse(items[4]),
			short.Parse(items[5])
		);
	}

	public bool Equals(Box other) => this == other;

	public override bool Equals(object obj)
	{
		return obj is Box other && Equals(other);
	}

	public override readonly int GetHashCode() => HashCode.Combine(Start, End);

	public override readonly string ToString() => $"{Start},{End}";
}