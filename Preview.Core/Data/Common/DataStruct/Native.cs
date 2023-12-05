using System.Runtime.InteropServices;

namespace Xylia.Preview.Data.Common.DataStruct;

[StructLayout(LayoutKind.Sequential)]
public struct Native
{
	public int StringSize;
	public int Offset;

	public Native(int stringSize = 0, int offset = 0)
	{
		StringSize = stringSize;
		Offset = offset;
	}



	public static bool operator ==(Native a, Native b)
	{
		return
			a.StringSize == b.StringSize &&
			a.Offset == b.Offset;
	}

	public static bool operator !=(Native a, Native b) => !(a == b);

	public bool Equals(Native other)
	{
		return StringSize == other.StringSize && Offset == other.Offset;
	}

	public override bool Equals(object obj) => obj is Native other && Equals(other);

	public override int GetHashCode() => HashCode.Combine(StringSize, Offset);
}