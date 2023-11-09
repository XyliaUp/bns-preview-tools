using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public sealed class ZoneArea : Record
{
	public int Zone;
	public short Id;

	public string Alias;
	public Box Box;
	public string Description;
	public IColor Areacolor;
	public Vector32 Pos;
	public float ScaleX;
	public float ScaleY;
	public float ScaleZ;
}