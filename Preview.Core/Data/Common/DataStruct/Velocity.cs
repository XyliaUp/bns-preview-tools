namespace Xylia.Preview.Data.Common.DataStruct;
public struct Velocity
{
	private ushort source;
	public Velocity(ushort value) => this.value = value;


	public ushort value { get => (ushort)(source / 4); set => source = (ushort)(value * 4); }
	public override string ToString() => value.ToString();


	#region Operator

	public static implicit operator Velocity(ushort value) => new(value);
	#endregion
}