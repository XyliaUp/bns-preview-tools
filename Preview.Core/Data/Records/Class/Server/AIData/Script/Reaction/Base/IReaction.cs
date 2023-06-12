using System.ComponentModel;

using Xylia.Preview.Data.Table.XmlRecord;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	public abstract class IReaction : TypeBaseRecord<ReactionType>
	{
		[DefaultValue(0)]
		public byte Probability;
	}
}