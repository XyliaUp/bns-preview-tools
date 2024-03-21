using Xylia.Preview.Data.Engine.BinData.Helpers;

namespace Xylia.Preview.Data.Common.DataStruct;
public readonly struct Sub
{
	public readonly short Subclass;


	public static bool operator ==(Sub a, Sub b) => a.Subclass == b.Subclass;

	public static bool operator !=(Sub a, Sub b) => !(a == b);

	public bool Equals(Sub other) => this == other;

	public override bool Equals(object obj) => obj is Sub other && Equals(other);

	public override int GetHashCode() => Subclass.GetHashCode();
}