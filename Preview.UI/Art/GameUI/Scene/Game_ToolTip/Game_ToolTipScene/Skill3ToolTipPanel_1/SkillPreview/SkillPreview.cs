using System.ComponentModel;

using CUE4Parse.BNS;

using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls;

namespace Xylia.Preview.GameUI.Scene.Skill
{
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
		public void LoadData(Data.Record.Skill Skill)
		{
			#region Initialize
			this.M1_Panel.Tooltips.Clear();
			this.M2_Panel.Tooltips.Clear();
			this.SUB_Panel.Tooltips.Clear();
			this.CONDITION_Panel.Tooltips.Clear();

			if (Skill is null)
			{
				this.SkillName.Text = "无效技能";
				this.SkillIcon.Image = null;

				return;
			}
			#endregion

			#region 获取基本信息
			System.Diagnostics.Debug.WriteLine(Skill.Attributes);

			this.SkillName.Text = Skill.Name2.GetText(); 
			this.SkillIcon.Image = Skill.Icon(); 
			//this.SkillIcon.ExtraTopLeft = Skill.CurrentShortCutKey.GetIcon();


			this.DamageRateStandardStats.Text = ((float)Skill.DamageRateStandardStats / 1000).ToString("F3");
			this.DamageRatePvp.Text = ((float)Skill.DamageRatePvp / 1000).ToString("F3");

			this.Casting.Text = Skill.CastDuration.GetDuration();
			this.reuse.Text = Skill.RecycleGroupDuration.GetDuration();
			#endregion



			var GatherRange = FileCache.Data.SkillGatherRange3[Skill.GatherRange];
			if (GatherRange != null)
			{
				System.Diagnostics.Debug.WriteLine($"GatherRange: 距离 {GatherRange.CastMax}  范围 {GatherRange.RadiusMax}\n" + GatherRange.Attributes);

				Distance.Text = GatherRange.CastMax / 100 + "米";
			}

			System.Diagnostics.Debug.WriteLine($"FlowType: " + Skill.FlowType);
			System.Diagnostics.Debug.WriteLine($"FlowRepeat: " + Skill.FlowRepeat);

			if (Skill.FlowRepeat >= 1) System.Diagnostics.Debug.WriteLine($"ExecGatherType1: " + Skill.ExecGatherType1);
			if (Skill.FlowRepeat >= 2) System.Diagnostics.Debug.WriteLine($"ExecGatherType2: " + Skill.ExecGatherType2);
			if (Skill.FlowRepeat >= 3) System.Diagnostics.Debug.WriteLine($"ExecGatherType3: " + Skill.ExecGatherType3);
			if (Skill.FlowRepeat >= 4) System.Diagnostics.Debug.WriteLine($"ExecGatherType4: " + Skill.ExecGatherType4);
			if (Skill.FlowRepeat >= 5) System.Diagnostics.Debug.WriteLine($"ExecGatherType5: " + Skill.ExecGatherType5);


			#region GatherType
			GatherType GatherType = Skill.ExecGatherType1;
			if (GatherType == GatherType.Target && Skill.FlowRepeat >= 2) GatherType = Skill.ExecGatherType2;
			if (GatherType == GatherType.Target && Skill.FlowRepeat >= 3) GatherType = Skill.ExecGatherType3;
			if (GatherType == GatherType.Target && Skill.FlowRepeat >= 4) GatherType = Skill.ExecGatherType4;
			if (GatherType == GatherType.Target && Skill.FlowRepeat >= 5) GatherType = Skill.ExecGatherType5;


			if (GatherType == GatherType.Target)
			{
				this.scale.Text = "Name.Skill.ScaleRange.Default".GetText();
				this.SkillGatherType.Image = null;
			}
			else
			{
				this.scale.Text = GatherRange.RadiusMax / 100 + "米";

				string res = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_ImageSet/SkillGatherType/" + GatherType.GetSignal().Replace("-", "_");
				this.SkillGatherType.Image = res.GetUObject().GetImage();
			}
			#endregion


			#region 获取提示信息
			foreach (var Tooltip in Skill.GetSkillTooltips())
			{
				#region 获取组信息
				var group = Tooltip.tooltipGroup switch
				{
					SkillTooltip.TooltipGroup.M1 => this.M1_Panel,
					SkillTooltip.TooltipGroup.M2 => this.M2_Panel,
					SkillTooltip.TooltipGroup.SUB => this.SUB_Panel,
					SkillTooltip.TooltipGroup.STANCE => null,
					SkillTooltip.TooltipGroup.CONDITION => this.CONDITION_Panel,

					_ => null,
				};

				if (group is null) continue;
				#endregion


				#region 获取Property 
				Bitmap icon = null;
				string text = Tooltip.DefaultText?.GetText();
				
				var ConditionAttribute = FileCache.Data.SkillTooltipAttribute[Tooltip.ConditionAttribute];
				if (ConditionAttribute != null)
				{
					icon = ConditionAttribute.Icon?.GetIcon();

					ContentParams Params = new();
					Params.LoadArg(ConditionAttribute.ArgType1, Tooltip.ConditionArg1, Tooltip);
					Params.LoadArg(ConditionAttribute.ArgType2, Tooltip.ConditionArg2, Tooltip);

					text += Params.Handle(ConditionAttribute.Text.GetText()) + " ";
				}

				var TargetAttribute = FileCache.Data.SkillTooltipAttribute[Tooltip.TargetAttribute];
				if (TargetAttribute != null)
				{
					icon = TargetAttribute.Icon?.GetIcon();
					text += TargetAttribute.Text.GetText() + " ";
				}

				var AfterStanceAttribute = FileCache.Data.SkillTooltipAttribute[Tooltip.AfterStanceAttribute];
				if (AfterStanceAttribute != null)
				{
					icon = AfterStanceAttribute.Icon?.GetIcon();
					text += AfterStanceAttribute.Text.GetText() + " ";
				}

				var BeforeStanceAttribute = FileCache.Data.SkillTooltipAttribute[Tooltip.BeforeStanceAttribute];
				if (BeforeStanceAttribute != null)
				{
					icon = BeforeStanceAttribute.Icon?.GetIcon();
					text += BeforeStanceAttribute.Text.GetText() + " ";
				}

				var EffectAttribute = FileCache.Data.SkillTooltipAttribute[Tooltip.EffectAttribute];
				if (EffectAttribute != null)
				{
					icon = EffectAttribute.Icon?.GetIcon();

					ContentParams Params = new();
					Params.LoadArg(EffectAttribute.ArgType1, Tooltip.EffectArg1, Tooltip);
					Params.LoadArg(EffectAttribute.ArgType2, Tooltip.EffectArg2, Tooltip);
					Params.LoadArg(EffectAttribute.ArgType3, Tooltip.EffectArg3, Tooltip);
					Params.LoadArg(EffectAttribute.ArgType4, Tooltip.EffectArg4, Tooltip);

					text += Params.Handle(EffectAttribute.Text.GetText()) + " ";
				}
				#endregion


				group.Tooltips.Add(new ContentPanel()
				{
					Font = Tooltip.tooltipGroup == SkillTooltip.TooltipGroup.M1 ? new Font(this.Font.FontFamily, this.Font.Size + 3, FontStyle.Regular) : this.Font,

					Icon = icon,
					Text = text,
				});
			}
			#endregion
		}




		public override void Refresh()
		{
			this.SuspendLayout();
			base.Refresh();


			this.M1_Panel.Refresh();

			this.M2_Panel.Location = new Point(this.M2_Panel.Location.X, this.M1_Panel.Bottom);
			this.M2_Panel.Refresh();

			this.SUB_Panel.Refresh();
			this.SUB_Panel.Location = new Point(this.SUB_Panel.Location.X, Math.Max(this.SkillIcon.Bottom, this.M2_Panel.Bottom) + 5);
			this.SUB_Panel.Height = this.SUB_Panel.Bottom - this.SUB_Panel.Top;

			this.CONDITION_Panel.Refresh();

			this.ResumeLayout();
		}
		#endregion
	}
}