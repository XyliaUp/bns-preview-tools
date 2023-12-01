namespace Xylia.Preview.Data.Common.DataStruct;
public struct Time32
{
	public int Ticks;

	public Time32(int Ticks) => this.Ticks = Ticks;


	public static implicit operator Time32(int ticks) => new(ticks);
}