using System.Diagnostics;

using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Preview.Reward
{
	/// <summary>
	/// 奖励分页数据
	/// </summary>
	public class RewardPage
	{
		#region Fields
		public RewardPage(DecomposeRewardInfo RewardInfo, DecomposeByItem2 OpenItem2)
		{
			this.RewardInfo = RewardInfo;
			this.OpenItem2 = OpenItem2;
		}


		/// <summary>
		/// 奖励数据
		/// </summary>
		public DecomposeRewardInfo RewardInfo;

		/// <summary>
		/// 开启花费
		/// </summary>
		public DecomposeByItem2 OpenItem2;

		/// <summary>
		/// 存在开启物品
		/// </summary>
		public bool HasOpenItem2 => !this.OpenItem2?.Item?.INVALID ?? false;
		#endregion


		#region Functions
		public static List<RewardPage> GetPages(DecomposeInfo DecomposeInfo)
		{
			var result = new List<RewardPage>();


			#region normal reward
			void GetPage(DecomposeRewardInfo RewardInfo, DecomposeByItem2 OpenItem)
			{
				if (RewardInfo is null) return;


				var Cells = RewardInfo.Preview;
				if (!Cells.Any()) Debug.WriteLine($"empty reward");

				result.Add(new RewardPage(RewardInfo, OpenItem));
			}

			GetPage(DecomposeInfo.DecomposeReward1, DecomposeInfo.Decompose_By_Item2_1);
			GetPage(DecomposeInfo.DecomposeReward2, DecomposeInfo.Decompose_By_Item2_2);
			GetPage(DecomposeInfo.DecomposeReward3, DecomposeInfo.Decompose_By_Item2_3);
			GetPage(DecomposeInfo.DecomposeReward4, DecomposeInfo.Decompose_By_Item2_4);
			GetPage(DecomposeInfo.DecomposeReward5, DecomposeInfo.Decompose_By_Item2_5);
			GetPage(DecomposeInfo.DecomposeReward6, DecomposeInfo.Decompose_By_Item2_6);
			GetPage(DecomposeInfo.DecomposeReward7, DecomposeInfo.Decompose_By_Item2_7);
			#endregion

			#region job reward
			var RewardGroup_Job = DecomposeInfo.DecomposeJobRewards;
			if (RewardGroup_Job != null && RewardGroup_Job.Any())
			{
				//数量大于一定值时, 仍然分页显示
				int CellSum = RewardGroup_Job.Sum(group => group.Preview.Count);
				if (CellSum >= 30)
				{
					foreach (var group in RewardGroup_Job)
						result.Add(new RewardPage(group, null));
				}
				else
				{
					//创建一个临时组, 以实现合并显示
					var tempReward = new DecomposeJobRewardInfo(JobSeq.JobNone, null)
					{
						_preview = RewardGroup_Job.SelectMany(group => group.Preview).ToList()
					};

					result.Add(new RewardPage(tempReward, DecomposeInfo.Job_Decompose_By_Item2_1));
				}
			}
			#endregion

			return result;
		}
		#endregion
	}
}