using Xylia.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls;
using Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Preview.Reward;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
	/// <summary>
	/// 封印解印提示工具
	/// </summary>
	public partial class SealPreview : TitleContentPanel
	{
		public SealPreview() : base() => InitializeComponent();

		public override void LoadData(BaseRecord Record)
		{
			#region 封印数据
			//新封印Functions
			var SealRenewalAuctionable = Record.Attributes["seal-renewal-auctionable"].ToBool();
			var SealConsumeItem1 = Record.Attributes["seal-consume-item-1"];
			var SealConsumeItem2 = Record.Attributes["seal-consume-item-2"];
			var SealConsumeItemCount1 = (short)Record.Attributes["seal-consume-item-count-1"].ToInt();
			var SealConsumeItemCount2 = (short)Record.Attributes["seal-consume-item-count-2"].ToInt();
			var SealAcquireItem = Record.Attributes["seal-acquire-item"];
			var UnsealResultPreviewItem = Record.Attributes["unseal-result-preview-item"];
			#endregion

			#region 解印数据
			List<string> UnsealAcquireItem = new();
			List<DecomposeByItem2> UnsealConsumeItem2 = new();

			for (int i = 0; i <= 20; i++)
			{
				var item = Record.Attributes[$"unseal-acquire-item-{i}"];
				if (item != null) UnsealAcquireItem.Add(item);
			}

			for (int i = 0; i <= 4; i++)
			{
				var item = new DecomposeByItem2(
					Record.Attributes[$"unseal-consume-item2-{i}"], 
					Record.Attributes[$"unseal-consume-item2-stack-count-{i}"].ToInt());

				if (!item.Item.INVALID) UnsealConsumeItem2.Add(item);
			}
			#endregion


			#region Load 信息
			string Info = null;

			//封印信息
			if (SealRenewalAuctionable || SealAcquireItem != null)
			{
				this.Title = "封印";

				if (SealConsumeItem1 != null)
				{
					var ConsumeItem = FileCache.Data.Item[SealConsumeItem1];
					Info = $"{ SealConsumeItemCount1 }个{ ConsumeItem.ItemName }";
				}
				if (SealConsumeItem2 != null)
				{
					var ConsumeItem = FileCache.Data.Item[SealConsumeItem2];
					Info += $"或者 { SealConsumeItemCount2 }个{ ConsumeItem.ItemName }";
				}

				//显示封印信息
				if (SealRenewalAuctionable) Info = $"可通过{ Info }进行封印, 封印后保留强化效果";
				else Info = $"可通过{ Info }重新封印为{  FileCache.Data.Item[SealAcquireItem]?.ItemName }";
			}

			//解印信息
			else if (UnsealResultPreviewItem != null || UnsealAcquireItem.Any())
			{
				this.Title = "解印";

				//获取可使用的解印符
				if (UnsealConsumeItem2.Any())
				{
					var CurItemInfo = UnsealConsumeItem2.First();
					var ConsumeItem = FileCache.Data.Item[CurItemInfo.Item];
					Info += $"{ CurItemInfo.StackCount }个{ ConsumeItem.ItemName }";
				}

				#region 获取解印获得道具
				Item UnsealResultItem = null;
				if (UnsealResultPreviewItem != null) UnsealResultItem = FileCache.Data.Item[UnsealResultPreviewItem];
				else if (UnsealAcquireItem.Any()) UnsealResultItem = FileCache.Data.Item[UnsealAcquireItem.First()];

				if (UnsealResultItem != null) Info = $"可通过{ Info } 解印为 { UnsealResultItem.ItemName }";
				#endregion
			}
			#endregion


			this.Visible = Info != null;
			this.Text = Info;
		}
	}
}