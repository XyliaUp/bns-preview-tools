using System.ComponentModel;

using CUE4Parse.BNS.Conversion;

using Xylia.Extension;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Scene.Game_ToolTipScene.Skill3ToolTipPanel_1.SkillPreview;
using Xylia.Preview.UI.Extension;

using static Xylia.Preview.Data.Record.Skill3;

namespace Xylia.Preview.GameUI.Scene.Skill;

[DesignTimeVisible(false)]
public partial class SkillPreview : UserControl
{
	#region Constructor
	public SkillPreview()
	{
		InitializeComponent();

		this.UI_Tooltip_Scale.Text = "UI.Tooltip.Scale".GetText();
		this.UI_Tooltip_Casting.Text = "UI.Tooltip.Casting".GetText();
		this.UI_Tooltip_Reuse.Text = "UI.Tooltip.Reuse".GetText();

		//Distance.Text = "Name.Skill.CastingRange.Default".GetText();
		//"Name.Skill.CastingRange".GetText();
		//"Name.Skill.CastingRange.MinMax".GetText();
		//"Name.Skill.ScaleRange".GetText();
		//"Name.Skill.ScaleRange.WidthHeight".GetText();
		//"Name.Skill.ScaleRange.Default".GetText();
	}
	#endregion


	#region Functions
	public void LoadData(Skill3 skill3)
	{
		#region Initialize
		this.M1_Panel.Controls.Clear();
		this.M2_Panel.Controls.Clear();
		this.SUB_Panel.Controls.Clear();
		this.CONDITION_Panel.Controls.Clear();

		if (skill3 is null)
		{
			this.SkillName.Text = "#INVALID";
			this.SkillIcon.Image = null;

			return;
		}
		#endregion

		#region Info
		Debug.WriteLine(skill3.Attributes);

		this.SkillName.Text = skill3.Name2.GetText();
		this.SkillIcon.Image = skill3.Icon();
		//this.SkillIcon.ExtraTopLeft = skill3.CurrentShortCutKey.GetIcon();


		this.DamageRateStandardStats.Text = ((float)skill3.DamageRateStandardStats / 1000).ToString("F3");
		this.DamageRatePvp.Text = ((float)skill3.DamageRatePvp / 1000).ToString("F3");

		//this.Casting.Text = skill3.CastDuration.GetDuration();
		//this.reuse.Text = skill3.RecycleGroupDuration.GetDuration();
		#endregion

		#region Tooltip
		foreach (var Tooltip in skill3.GetSkillTooltips())
		{
			#region Attributes 
			Bitmap icon = null;
			string text = Tooltip.DefaultText?.GetText();

			if (Tooltip.ConditionAttribute != null)
			{
				icon = Tooltip.ConditionAttribute.Icon?.GetIcon();

				ContentParams Params = new();
				Linq.For(2, (i) => Params.LoadArg(Tooltip.ConditionAttribute.Arg_Type[i], Tooltip.ConditionArg[i], Tooltip));
				text += Params.HandleText(Tooltip.ConditionAttribute.Text.GetText()) + " ";
			}

			if (Tooltip.TargetAttribute != null)
			{
				icon = Tooltip.TargetAttribute.Icon?.GetIcon();
				text += Tooltip.TargetAttribute.Text.GetText() + " ";
			}

			if (Tooltip.AfterStanceAttribute != null)
			{
				icon = Tooltip.AfterStanceAttribute.Icon?.GetIcon();
				text += Tooltip.AfterStanceAttribute.Text.GetText() + " ";
			}

			if (Tooltip.BeforeStanceAttribute != null)
			{
				icon = Tooltip.BeforeStanceAttribute.Icon?.GetIcon();
				text += Tooltip.BeforeStanceAttribute.Text.GetText() + " ";
			}

			if (Tooltip.EffectAttribute != null)
			{
				icon = Tooltip.EffectAttribute.Icon?.GetIcon();

				ContentParams Params = new();
				Linq.For(4, (i) => Params.LoadArg(Tooltip.EffectAttribute.Arg_Type[i], Tooltip.EffectArg[i], Tooltip));
				text += Params.HandleText(Tooltip.EffectAttribute.Text.GetText()) + " ";
			}
			#endregion

			#region Group
			SkillTooltipPanel group = null;
			switch (Tooltip.tooltipGroup)
			{
				case SkillTooltip.TooltipGroup.M1: group = M1_Panel; break;
				case SkillTooltip.TooltipGroup.M2: group = M2_Panel; break;
				case SkillTooltip.TooltipGroup.SUB: group = SUB_Panel; break;
				case SkillTooltip.TooltipGroup.STANCE: group = STANCE_Panel; break;
				case SkillTooltip.TooltipGroup.CONDITION: group = CONDITION_Panel; break;
				default: continue;
			}

			group.Controls.Add(new IconContentPanel()
			{
				Font = Tooltip.tooltipGroup == SkillTooltip.TooltipGroup.M1 ? new Font(this.Font.FontFamily, this.Font.Size + 3, FontStyle.Regular) : this.Font,

				icon = icon,
				Text = (icon is null ? null : $"<image object='icon'/> ") + text,
			});
			#endregion
		}
		#endregion


		if (skill3 is ActiveSkill activeSkill)
		{
			this.Casting.Text = activeSkill.CastDuration.GetDuration();
			this.reuse.Text = activeSkill.RecycleGroupDuration.GetDuration();


			var GatherRange = FileCache.Data.SkillGatherRange3[activeSkill.GatherRange];
			if (GatherRange != null)
			{
				Debug.WriteLine($"GatherRange: 距离 {GatherRange.CastMax}  范围 {GatherRange.RadiusMax}\n" + GatherRange.Attributes);

				Distance.Text = GatherRange.CastMax / 100 + "米";
			}

			Debug.WriteLine($"FlowType: " + activeSkill.FlowType);
			Debug.WriteLine($"FlowRepeat: " + activeSkill.FlowRepeat);

			if (activeSkill.FlowRepeat >= 1) Debug.WriteLine($"ExecGatherType1: " + activeSkill.ExecGatherType1);
			if (activeSkill.FlowRepeat >= 2) Debug.WriteLine($"ExecGatherType2: " + activeSkill.ExecGatherType2);
			if (activeSkill.FlowRepeat >= 3) Debug.WriteLine($"ExecGatherType3: " + activeSkill.ExecGatherType3);
			if (activeSkill.FlowRepeat >= 4) Debug.WriteLine($"ExecGatherType4: " + activeSkill.ExecGatherType4);
			if (activeSkill.FlowRepeat >= 5) Debug.WriteLine($"ExecGatherType5: " + activeSkill.ExecGatherType5);


			#region GatherType
			GatherType GatherType = activeSkill.ExecGatherType1;
			if (GatherType == GatherType.Target && activeSkill.FlowRepeat >= 2) GatherType = activeSkill.ExecGatherType2;
			if (GatherType == GatherType.Target && activeSkill.FlowRepeat >= 3) GatherType = activeSkill.ExecGatherType3;
			if (GatherType == GatherType.Target && activeSkill.FlowRepeat >= 4) GatherType = activeSkill.ExecGatherType4;
			if (GatherType == GatherType.Target && activeSkill.FlowRepeat >= 5) GatherType = activeSkill.ExecGatherType5;


			if (GatherType == GatherType.Target)
			{
				this.scale.Text = "Name.ActiveSkill.ScaleRange.Default".GetText();
				this.SkillGatherType.Image = null;
			}
			else
			{
				this.scale.Text = GatherRange.RadiusMax / 100 + "米";

				string res = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_ImageSet/SkillGatherType/" + GatherType.GetSignal().Replace("-", "_");
				this.SkillGatherType.Image = FileCache.Provider.LoadObject(res)?.GetImage();
			}
			#endregion
		}
	}


	public override void Refresh()
	{
		this.SuspendLayout();
		base.Refresh();

		//this.M1_Panel.Location = M1_Panel.Location;
		this.M1_Panel.Refresh();

		this.M2_Panel.Location = new Point(this.M2_Panel.Location.X, this.M1_Panel.Bottom);
		this.M2_Panel.Refresh();

		this.SUB_Panel.Refresh();
		this.SUB_Panel.Location = new Point(this.SUB_Panel.Location.X, Math.Max(this.SkillIcon.Bottom, this.M2_Panel.Bottom) + 5);
		this.SUB_Panel.Height = this.SUB_Panel.Bottom - this.SUB_Panel.Top;

		this.CONDITION_Panel.Refresh();

		this.STANCE_Panel.Location = new Point(this.STANCE_Panel.Location.X, this.CONDITION_Panel.Bottom + 10);
		this.STANCE_Panel.Refresh();


		this.ResumeLayout();
	}
	#endregion
}
