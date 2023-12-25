namespace Xylia.Preview.Data.Common.DataStruct;
public struct Sub
{
	public readonly short Type;


	public static bool operator ==(Sub a, Sub b) => a.Type == b.Type;

	public static bool operator !=(Sub a, Sub b) => !(a == b);

	public bool Equals(Sub other) => this == other;

	public override bool Equals(object obj) => obj is Sub other && Equals(other);
}