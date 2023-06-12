using System;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Preview.Reward
{
	public class RewardChangedEvent : EventArgs
	{
		public RewardChangedEvent(RewardPage RewardPage) => this.Page = RewardPage;


		public RewardPage Page;
	}
}