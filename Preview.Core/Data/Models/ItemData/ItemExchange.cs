using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public sealed class ItemExchange : Record
{
	public string Alias;



	[Name("rule-usage")]
	public RuleUsageSeq RuleUsage;
	public enum RuleUsageSeq
	{
		[Name("antique-exchange")]
		AntiqueExchange,

		[Name("crystallization")]
		Crystallization,
	}



	[Name("required-item"), Repeat(4)]
	public Ref<Record>[] RequiredItem;

	[Name("required-item-min-level"), Repeat(4)]
	public sbyte[] RequiredItemMinLevel;

	[Name("required-item-stack-count"), Repeat(4)]
	public short[] RequiredItemStackCount;



	[Name("normal-item"), Repeat(4)]
	public Ref<Item>[] NormalItem;

	[Name("normal-item-stack-count"), Repeat(4)]
	public short[] NormalItemStackCount;
}