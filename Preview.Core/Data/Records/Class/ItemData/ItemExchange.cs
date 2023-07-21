using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public sealed class ItemExchange : BaseRecord
{
	[Signal("rule-usage")]
	public RuleUsageSeq RuleUsage;
	public enum RuleUsageSeq
	{
		[Signal("antique-exchange")]
		AntiqueExchange,

		[Signal("crystallization")]
		Crystallization,
	}



	[Signal("required-item"), Repeat(4)]
	public BaseRecord[] RequiredItem;

	[Signal("required-item-min-level"), Repeat(4)]
	public byte[] RequiredItemMinLevel;

	[Signal("required-item-stack-count"), Repeat(4)]
	public short[] RequiredItemStackCount;



	[Signal("normal-item"), Repeat(4)]
	public Item[] NormalItem;

	[Signal("normal-item-stack-count"), Repeat(4)]
	public short[] NormalItemStackCount;
}