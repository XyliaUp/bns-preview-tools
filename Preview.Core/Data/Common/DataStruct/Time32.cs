using Xylia.Preview.Data.Engine.DatData;

namespace Xylia.Preview.Data.Common.DataStruct;
/// <summary>
///  Represents an instant in time
/// </summary>
/// <param name="Ticks"></param>
/// <param name="Publisher"></param>
public struct Time32(int Ticks, Publisher Publisher)
{
	public int Ticks = Ticks;

	public Publisher Publisher { get; set; } = Publisher;
}