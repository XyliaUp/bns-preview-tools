using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models;
public sealed class ItemExchange : ModelElement
{
	[Name("rule-usage")]
	public RuleUsageSeq RuleUsage { get; set; }
	public enum RuleUsageSeq
	{
		[Name("antique-exchange")]
		AntiqueExchange,

		[Name("crystallization")]
		Crystallization,
	}



	[Name("required-item"), Repeat(4)]
	public Ref<ModelElement>[] RequiredItem { get; set; }

	[Name("required-item-min-level"), Repeat(4)]
	public sbyte[] RequiredItemMinLevel { get; set; }

	[Name("required-item-stack-count"), Repeat(4)]
	public short[] RequiredItemStackCount { get; set; }



	[Name("normal-item"), Repeat(4)]
	public Ref<Item>[] NormalItem { get; set; }

	[Name("normal-item-stack-count"), Repeat(4)]
	public short[] NormalItemStackCount { get; set; }
}