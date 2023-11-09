using System.Runtime.InteropServices;

namespace Xylia.Preview.Data.Common.DataStruct;

[StructLayout(LayoutKind.Sequential)]
public struct Distance
{
	private short source;
	public Distance(short value) => this.value = value;

	public short value { get => (short)(source / 4); set => source = (short)(value * 4); }
	public override string ToString() => value.ToString();


	#region Operator

	public static implicit operator Distance(short value) => new(value);

	public static Distance operator *(Distance distance, int value) => (Distance)(distance.source * value);

	public static Distance operator /(Distance distance, int value) => (Distance)(distance.source / value);

	public static Distance operator +(Distance distance1, Distance distance2) => (Distance)(distance1.source + distance2.source);
	#endregion
}