using Xylia.Preview.Common.Attributes;

namespace Xylia.Preview.Data.Models;
public sealed class ItemExchange : ModelElement
{
	public RuleUsageSeq RuleUsage { get; set; }

	public enum RuleUsageSeq
	{
		[Name("antique-exchange")]
		AntiqueExchange,

		[Name("crystallization")]
		Crystallization,
	}
}