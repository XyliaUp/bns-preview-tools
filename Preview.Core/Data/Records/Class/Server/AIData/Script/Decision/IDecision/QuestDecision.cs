using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.ScriptData.Decision
{
	[Signal("quest-decision")]
	public sealed class QuestDecision : IDecision
	{
		public string Alias;


		//recation  mission-step-completed 	mission-step-skipped  mission-step-failed	 
	}
}