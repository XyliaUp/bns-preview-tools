using System.ComponentModel;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ChallengeToday
{
	[DesignTimeVisible(false)]
	public partial class ChallengeListRewardPreview : UserControl
	{
		#region Constructor
		public ChallengeListRewardPreview() => InitializeComponent();
		#endregion

		#region Events & Delegates
		public event EventHandler PrevSeleted;

		public event EventHandler NextSeleted;

		private void Btn_Prev_Click(object sender, EventArgs e) => this.PrevSeleted?.Invoke(this, e);

		private void Btn_Next_Click(object sender, EventArgs e) => this.NextSeleted?.Invoke(this, e);
		#endregion


		#region Functions
		public void LoadData(ChallengeListReward Reward)
		{
			this.Controls.Remove<ItemIconCell>();

			#region Load 控件
			int LocX = this.Btn_Prev.Right + 20;
			for (int i = 1; i <= 8; i++)
			{
				var RewardItem = FileCache.Data.Item[Reward.Attributes["reward-item-" + i]];
				if (RewardItem is null) continue;

				var RewardItemCount = Reward.Attributes["reward-item-count-" + i].ToShort();


				var ItemIcon = new ItemIconCell()
				{
					ObjectRef = RewardItem,
					Image = RewardItem.IconExtra(),
					ShowStackCount = true,
					StackCount = RewardItemCount,

					Location = new Point(LocX, 0),
					Scale = 60,
				};

				this.Controls.Add(ItemIcon);
				LocX = ItemIcon.Right + 10;
			}

			this.Btn_Next.Location = new Point(LocX + 10, this.Btn_Next.Location.Y);
			this.Width = this.Btn_Next.Right;
			#endregion
		}
		#endregion
	}
}