namespace Xylia.Preview.Data.Record.AIData.ActSequence.Action
{
	public abstract class IAction : TypeBaseRecord<ActionType>
	{
		/// <summary>
		/// 仅当上级节点是 Select 时才有意义
		/// </summary>
		public byte Prob;
	}
}