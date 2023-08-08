using Xylia.Attribute;
using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.BinData.Table.Record;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Preview;
using Xylia.Preview.UI.Interface;
using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel;
public partial class SetItemTooltip : PreviewControl
{
	#region Constructor
	public SetItemTooltip()
	{
		InitializeComponent();

		this.BackColor = Color.Transparent;
		this.AutoSize = false;


		SetItemEffect_Title.Text = "UI.ItemTooltip.SetItemEffect.Title".GetText();
	}
	#endregion


	#region Interface Functions
	public override void LoadData(BaseRecord record)
	{
		#region Initialize
		var Item = record as Item;
		var Record = FileCache.Data.SetItem[record.Attributes["set-item"]];
		if (Record is null)
		{
			this.Visible = false;
			return;
		}

		this.lbl_Title.Text = Record.Attributes["name2"].GetText();
		var SlotTagIcon1 = Record.Attributes[$"slot-tag-icon-1"];
		var UseGemPreview = this.GemPreview.Visible = SlotTagIcon1 == "TagIcon_Alpha_01_gray,1";
		#endregion


		#region	item
		int ItemIdx = 0;
		for (ItemIdx = 1; ItemIdx <= 10; ItemIdx++)
		{
			var SlotName = Record.Attributes[$"slot-name", ItemIdx];
			var SlotTagIcon = Record.Attributes[$"slot-tag-icon", ItemIdx];
			var SlotEquipType = Record.Attributes[$"slot-equip-type", ItemIdx].ToEnum<EquipType>();
			if (SlotEquipType == EquipType.None) break;

			if (!UseGemPreview)
			{
				//"UI.ItemTooltip.SetItem.EquipedSlot"
				//"UI.ItemTooltip.SetItem.EmptySlot.IconView"
				//"UI.ItemTooltip.SetItem.EmptySlot"

				Item Equiped = null;
				Equiped ??= RecordExtension.CreateNew<Item>(
					new IAttribute("item-grade", "7"),
					new IAttribute("name2", SlotName),
					new IAttribute("icon", SlotTagIcon));


				var slot = new ContentPanel("UI.ItemTooltip.SetItem.EquipedSlot".GetText()) { Tag = "slot" };
				slot.Params[2] = Equiped;
				slot.Params[3] = Equiped;

				this.Controls.Add(slot);
			}
		}

		this.lbl_Title.Text += $" ({ItemIdx - 1})";
		#endregion

		#region	effect
		for (int idx = 1; idx <= 10; idx++)
		{
			//if (!Record.Attributes[$"count-{idx}-tooltip-1"].ToBool())
			//	continue;


			//"UI.ItemTooltip.SetItemIndex.Blank"
			//"UI.ItemTooltip.SetItemIndex.1.Disable"
			var SetItemIndex = $"UI.ItemTooltip.SetItemIndex.{idx}.Enable".GetText();
			//"UI.ItemTooltip.SetItemEffect.Effect.Disable"
			var SetItemEffect = "UI.ItemTooltip.SetItemEffect.Effect".GetText();


			var effect = new ContentPanel(SetItemIndex + SetItemEffect) { Tag = "effect" };
			effect.Params[2] = FileCache.Data.Effect[Record.Attributes[$"count-{idx}-effect-1"]];
			effect.Params[3] = null;



			var groups = new Dictionary<JobStyleSeq, SkillModifyInfoGroup>();
			for (byte style = 1; style <= 10; style++)
			{
				var group = FileCache.Data.SkillModifyInfoGroup[Record.Attributes[$"count-{idx}-skill-modify-info-group", style]];
				if (group != null) groups[(JobStyleSeq)(style - 1)] = group;
			}


			if (effect.Params[2] != null)
				this.Controls.Add(effect);

			if (groups.Any())
			{
				this.Controls.Add(effect);

				this.JobStyleSelect.Visible = true;

				//"UI.ItemTooltip.SetItemEffect.SkillModifyByEquipment.Enahnce"
				//"UI.ItemTooltip.SetItemEffect.SkillModifyByEquipment.Penalty"
				//"UI.ItemTooltip.SetItemEffect.SkillModifyByEquipment.Disable"
				this.JobStyleSelect.JobStyleChanged += new JobStyleSelect.JobStyleChangedHandle(e =>
				{
					effect.Params[3] = groups.TryGetValue(e, out var group) ? $"{group}<br/>" : null;
				});
			}
		}

		if (this.JobStyleSelect.Visible)
		{
			this.JobStyleSelect.LoadStyleIcon(Item.EquipJobCheck[0]);
			this.JobStyleSelect.JobStyleChanged += new JobStyleSelect.JobStyleChangedHandle(e =>
			{
				this.Refresh();
				this.ParentForm?.Refresh();
			});
			this.JobStyleSelect.SelectDefault();
		}
		#endregion
	}

	public override void Refresh()
	{
		int y = this.GemPreview.Visible ? this.GemPreview.Bottom : this.lbl_Title.Bottom + 2;
		foreach (var c in this.Controls.OfType<ContentPanel>().Where(o => o.Tag == "slot"))
		{
			c.Location = new Point(5, y);
			c.BringToFront();
			c.Refresh();

			y = c.Bottom;
		}

		this.SetItemEffect_Title.Location = new Point(SetItemEffect_Title.Location.X, y); 
		y = this.SetItemEffect_Title.Bottom;
		this.JobStyleSelect.Location = new Point(this.Width - JobStyleSelect.Width, this.SetItemEffect_Title.Top - 10);

		foreach (var c in this.Controls.OfType<ContentPanel>().Where(o => o.Tag == "effect"))
		{
			c.Location = new Point(5, y);
			c.Refresh();

			y = c.Bottom + 2;
		}

		this.Height = y;
	}
	#endregion
}