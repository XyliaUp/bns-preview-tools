using Xylia.Extension;
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Record;
using Xylia.Preview.UI.Resources;

using static Xylia.Preview.Data.Record.Item;
using static Xylia.Preview.Data.Record.Item.Grocery;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTipScene.ItemTooltipPanel.Preview.Randombox;
public sealed class DecomposeInfo
{
	#region Fields
	/// <summary>
	/// 根据钥匙类型适配奖励
	/// 否则只能获得第一个奖励
	/// </summary>
	[Signal("decompose-reward-by-consume-index")]
	public bool DecomposeRewardByConsumeIndex;

	public Reward[] DecomposeReward;

	[Signal("decompose-event-reward")]
	public Reward DecomposeEventReward;

	public Dictionary<JobSeq, Reward> DecomposeJobRewards;

	[Signal("decompose-max")]
	public int DecomposeMax = 1;

	[Signal("decompose-money-cost")]
	public int DecomposeMoneyCost;

	public DecomposeByItem2[] Decompose_By_Item2;
	public DecomposeByItem2[] Job_Decompose_By_Item2;
	#endregion

	#region Constructor
	public DecomposeInfo(Item ItemInfo)
	{
		var attr = ItemInfo.Attributes;
		DecomposeJobRewards = new();


		DecomposeRewardByConsumeIndex = attr["decompose-reward-by-consume-index"].ToBool();
		DecomposeMax = attr["decompose-max"].ToInt32();
		DecomposeMoneyCost = attr["decompose-money-cost"].ToInt32();

		DecomposeReward = Linq.For(7, (id) => FileCache.Data.Reward[attr["decompose-reward", id]]);
		Job.GetPcJob().ForEach(job => DecomposeJobRewards[job] = FileCache.Data.Reward[attr[$"decompose-job-reward-{job.GetSignal()}"]]);

		Decompose_By_Item2 = Linq.For(7, (id) => new DecomposeByItem2(attr["decompose-by-item2", id], attr["decompose-by-item2-stack-count", id].ToInt32()));
		Job_Decompose_By_Item2 = Linq.For(7, (id) => new DecomposeByItem2(attr["job-decompose-by-item2", id], attr["job-decompose-by-item2-stack-count", id].ToInt32()));
	}
	#endregion


	#region Functions
	public Bitmap GetExtra()
	{
		var result = GetExtra(Decompose_By_Item2[0].Item);
		result ??= GetExtra(Job_Decompose_By_Item2[0].Item);
		result ??= DecomposeMoneyCost == 0 ? null : Resource_BNSR.Weapon_Lock_04;

		return result;
	}

	private static Bitmap GetExtra(Item item2)
	{
		if (item2.INVALID())
			return null;

		var Item2Info = FileCache.Data.Item[item2];
		if (Item2Info != null && Item2Info is Grocery grocery && grocery.GroceryType == GroceryTypeSeq.Key) return Resource_BNSR.unuseable_KeyLock;
		else return Resource_BNSR.Weapon_Lock_04;
	}
	#endregion
}