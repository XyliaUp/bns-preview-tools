using Xylia.Preview.Data.Common.DataStruct;
namespace Xylia.Preview.Data.Engine.BinData.Serialization;
public class AliasTableArchiveEntry
{
	public string String;
	public long StringOffset;
	public uint Begin;
	public uint End;

	public bool IsLeaf => (Begin & 1) == 0;

	public Ref ToRef() => Ref.From((Begin | (ulong)End << 32) >> 1);

	public override string ToString() => $"{Begin >> 1}-{End} IsLeaf:{IsLeaf}";
}