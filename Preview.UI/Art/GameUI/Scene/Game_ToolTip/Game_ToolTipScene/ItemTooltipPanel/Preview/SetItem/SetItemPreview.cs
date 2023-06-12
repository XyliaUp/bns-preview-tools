using System.Collections.Generic;
using System.Drawing;

using Xylia.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Common.Interface;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls;
using Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell;
using Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Preview;

using static Xylia.Extension.String;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
	public partial class SetItemPreview : PreviewControl
	{
		#region Constructor
		public SetItemPreview()
		{
			InitializeComponent();

			this.BackColor = Color.Transparent;
			this.AutoSize = false;
		}
		#endregion

		#region Fields
		readonly List<ItemShowCell> ItemShowCells = new();

		readonly List<SetItemEffect> SetItemEffects = new();
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

			//获取职业派系信息
			this.JobStyleSelect.Visible = false;

			ItemShowCells.Clear();
			SetItemEffects.Clear();

			//Load 套装类型
			var SlotTagIcon1 = Record.Attributes[$"slot-tag-icon-1"];
			var UseGemPreview = this.GemPreview.Visible = SlotTagIcon1 == "TagIcon_Alpha_01_gray,1";

			this.lbl_Title.Text = Record.Attributes["name2"].GetText();
			#endregion


			#region	Load 套装构成品数据
			for (int idx = 1; idx <= 10; idx++)
			{
				var SlotName = Record.Attributes[$"slot-name-{idx}"];
				var SlotTagIcon = Record.Attributes[$"slot-tag-icon-{idx}"];
				var SlotEquipType = Record.Attributes[$"slot-equip-type-{idx}"].ToEnum<EquipType>();
				if (SlotTagIcon is null && SlotEquipType == EquipType.None)
				{
					this.lbl_Title.Text += $" ({ idx - 1 })";
					break;
				}

				if (!UseGemPreview)
				{
					ItemShowCells.Add(new ItemShowCell()
					{
						ItemIcon = SlotTagIcon.GetIcon(),
						ItemName = SlotName.GetText(),

						Scale = 32,
					});
				}
			}
			#endregion

			#region	Load 套装效果
			for (int idx = 1; idx <= 10; idx++)
			{
				SetItemEffect SetItemEffect = new() { Count = idx };
				bool UseSetItemEffect = false;

				#region 获取效果信息
				string OriginalText = null;
				if (Record.Attributes[$"count-{idx}-tooltip-1"]?.ToBool() ?? false)
				{
					UseSetItemEffect = true;

					var Effect1 = FileCache.Data.Effect[Record.Attributes[$"count-{idx}-effect-1"]];
					SetItemEffect.Text = OriginalText = Effect1?.Description2.GetText();
				}

				if (Record.Attributes[$"count-{idx}-tooltip-2"]?.ToBool() ?? false)
				{
					UseSetItemEffect = true;

					var Effect2 = FileCache.Data.Effect[Record.Attributes[$"count-{idx}-effect-2"]];
					SetItemEffect.Text = OriginalText = Effect2?.Description2.GetText();
				}
				#endregion

				#region 获取技能变更信息
				var SkillModifyInfoGroup = new Dictionary<byte, SkillModifyInfoGroup>();
				for (byte idx2 = 1; idx2 <= 10; idx2++)
				{
					SkillModifyInfoGroup[idx2] = FileCache.Data.SkillModifyInfoGroup[Record.Attributes[$"count-{idx}-skill-modify-info-group-{idx2}"]];
					if (SkillModifyInfoGroup[idx2] != null) this.JobStyleSelect.Visible = UseSetItemEffect = true;
				}

				if (UseSetItemEffect)
				{
					this.JobStyleSelect.JobStyleChanged += new JobStyleSelect.JobStyleChangedHandle(e =>
					{
						//当前派系对应的技能变更信息
						var skillModifyInfoGroup = SkillModifyInfoGroup[(byte)(1 + (byte)e)];
						if (skillModifyInfoGroup != null) SetItemEffect.Text = OriginalText.JudgeLineFeed() + skillModifyInfoGroup.ToString();
					});
				}
				#endregion

				//处理信息
				if (!UseSetItemEffect) continue;
				SetItemEffects.Add(SetItemEffect);
			}
			#endregion


			#region 处理界面刷新
			if (this.JobStyleSelect.Visible)
			{
				this.JobStyleSelect.LoadStyleIcon(Item.EquipJobCheck1);
				this.JobStyleSelect.JobStyleChanged += new JobStyleSelect.JobStyleChangedHandle(e =>
			    {
				   this.Refresh();
				   //this.Parent?.Refresh();
			    });

				this.JobStyleSelect.SelectDefault();
			}

			this.Refresh();
			#endregion
		}

		public override void Refresh()
		{
			base.Refresh();

			int LoY;
			if (this.GemPreview.Visible) LoY = this.GemPreview.Bottom;
			else LoY = this.lbl_Title.Bottom + 2;

			#region 套装构成品
			foreach (var c in ItemShowCells)
			{
				this.Controls.Add(c);
				c.Location = new Point(7, LoY);

				LoY = c.Bottom + 1;
			}
			#endregion

			#region 计算套装效果部分的位置 
			this.SetItemEffect_Title.Location = new Point(SetItemEffect_Title.Location.X, LoY);
			this.JobStyleSelect.Location = new Point(JobStyleSelect.Location.X, LoY - 10);

			LoY = this.SetItemEffect_Title.Bottom;
			#endregion

			#region 套装效果
			foreach (var c in SetItemEffects)
			{
				this.Controls.Add(c);
				c.Location = new Point(2, LoY);
				LoY = c.Bottom + 1;
			}
			#endregion

			this.Height = LoY;
		}
		#endregion
	}
}