using BnsBinTool.Core.DataStructs;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public sealed class ZoneArea : BaseRecord
{
	public int Zone;
	public short Id;

	//public string Alias;
	public Box Box;
	public string Description;
	public IColor Areacolor;
	public Vector32 Pos;
	public float ScaleX;
	public float ScaleY;
	public float ScaleZ;
}