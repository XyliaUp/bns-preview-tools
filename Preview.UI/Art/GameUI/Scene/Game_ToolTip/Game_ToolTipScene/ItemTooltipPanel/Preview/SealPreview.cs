using Xylia.Extension;
using Xylia.Preview.Common.Cast;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.BinData.Table.Record;
using Xylia.Preview.Data.Record;
using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel;
public partial class SealPreview : TitleContentPanel
{
	public SealPreview() : base() => InitializeComponent();

	public override void LoadData(BaseRecord Record)
	{
		string Info = null;

		#region Seal
		var SealRenewal = Record.Attributes["seal-renewal-auctionable"].ToBool();
		if (Record.Contains("seal-acquire-item", out var SealAcquireItem) || SealRenewal)
		{
			this.Title = "封印";

			var item1 = Record.Attributes["seal-consume-item-1"].CastObject<Item>();
			var item2 = Record.Attributes["seal-consume-item-2"].CastObject<Item>();
			var count1 = Record.Attributes["seal-consume-item-count-1"].ToShort();
			var count2 = Record.Attributes["seal-consume-item-count-2"].ToShort();

			if (item1 != null) Info = $"{count1}个{item1.ItemName}";
			if (item2 != null) Info += $"或者 {count2}个{item2.ItemName}";


			if (SealRenewal) Info = $"可通过{Info}进行封印, 封印后保留强化效果";
			else Info = $"可通过{Info}重新封印为{FileCache.Data.Item[SealAcquireItem]?.ItemName}";
		}
		#endregion

		#region UnSeal
		List<string> UnsealAcquireItem = new();
		for (int i = 0; i <= 20; i++)
		{
			if (Record.Contains($"unseal-acquire-item-{i}", out var item))
				UnsealAcquireItem.Add(item);
		}


		var UnsealResultPreviewItem = Record.Attributes["unseal-result-preview-item"];
		if (!string.IsNullOrEmpty(UnsealResultPreviewItem) || UnsealAcquireItem.Any())
		{
			this.Title = "解印";

			for (int i = 1; i <= 4; i++)
			{
				var item = Record.Attributes[$"unseal-consume-item2", i].CastObject<Item>();
				var count = Record.Attributes[$"unseal-consume-item2-stack-count", i].ToShort();

				if (item != null)
				{
					Info += $"{count}个{item.ItemName}";
					break;
				}
			}


			#region 获取解印获得道具
			Item UnsealResultItem = null;
			if (!string.IsNullOrEmpty(UnsealResultPreviewItem)) UnsealResultItem = FileCache.Data.Item[UnsealResultPreviewItem];
			else if (UnsealAcquireItem.Any()) UnsealResultItem = FileCache.Data.Item[UnsealAcquireItem.First()];

			if (UnsealResultItem != null) Info = $"可通过{Info} 解印为 {UnsealResultItem.ItemName}";
			#endregion
		}
		#endregion


		this.Visible = Info != null;
		this.Text = Info;
	}
}