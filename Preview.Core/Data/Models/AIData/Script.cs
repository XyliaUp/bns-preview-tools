using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class Script : Record
{
	public List<Decision.Default> Decision = new();

	public List<Decision.QuestDecision> QuestDecision = new();



	public string Parent;

	public HangerSeq Hanger;
	public enum HangerSeq
	{
		None,

		Object,
	}
}