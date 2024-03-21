using System.Diagnostics;
using System.Windows;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Extensions;

namespace Xylia.Preview.UI.GameUI.Scene.Game_Tooltip;
public partial class Skill3ToolTipPanel_1
{
	#region Constructors
	public Skill3ToolTipPanel_1()
	{
		InitializeComponent();

		this.PreviewMouseDown += (s, e) =>
		{
			Trace.WriteLine("loading data");
			DataContext = FileCache.Data.Provider.GetTable<Skill3>()[Clipboard.GetText() ?? "SwordMaster_S1_3_turning_strike"];
			OnLoaded(e);
		};
	}
	#endregion


	#region Methods
	protected override void OnLoaded(RoutedEventArgs e)
	{
		if (DataContext is not Skill3 record) return;

		Skill3ToolTipPanel_1_Name.Text = record.Name2.GetText();
		Skill3ToolTipPanel_1_Main_Icon.ExpansionComponentList["IconImage"]?.SetValue(record.Icon);
		Skill3ToolTipPanel_1_Main_Icon.Expansion = ["IconImage,KEYCOMMAND,Slot"];

		Skill3ToolTipPanel_1_ItemProbability.String.LabelText =
			"UI.Skill.ItemProbability.PrevTag".GetText() + "<br/>" +
			"UI.Skill.ItemProbability".GetText([record.RevisedEventProbabilityInExec[0]]);

		Skill3ToolTipPanel_1_Main_Description.String.LabelText =
			string.Join("<br/>", record.MainTooltip1.SelectNotNull(x => x.Instance)) +
			string.Join("<br/>", record.MainTooltip2.SelectNotNull(x => x.Instance));

		Skill3ToolTipPanel_1_Sub_Description.String.LabelText = string.Join("<br/>", record.SubTooltip.SelectNotNull(x => x.Instance));

		#region DamageInfoHolder
		Skill3ToolTipPanel_1_DamageInfoHolder.Top = Skill3ToolTipPanel_1_Sub_Description.Bottom + 5;
		Skill3ToolTipPanel_1_DamageInfo_PvEInfo.ExpansionComponentList["Info"]?.SetValue($"<image enablescale='true' imagesetpath='00009076.CharInfo_AttackPower' scalerate='1.2'/> x {record.DamageRatePvp * 10000:0.00}");
		Skill3ToolTipPanel_1_DamageInfo_PvPInfo.ExpansionComponentList["Info"]?.SetValue($"<image enablescale='true' imagesetpath='00009076.CharInfo_PcAttackPower' scalerate='1.2'/> x {record.DamageRateStandardStats * 10000:0.00}");
		#endregion

		#region Sub_Holder
		//Skill3ToolTipPanel_1_CastingTime.Arguments = [null, record];
		//Skill3ToolTipPanel_1_RecycleTime.Arguments = [null, record];
		#endregion

		#region Tooltip
		var ConditionTooltips = record.ConditionTooltip.SelectNotNull(x => x.Instance);
		Skill3ToolTipPanel_1_ConditionTitle.Visibility = ConditionTooltips.Any().ToVisibility();
		Skill3ToolTipPanel_1_ConditionText.String.LabelText = string.Join("<br/>", ConditionTooltips);

		var StanceTooltips = record.StanceTooltip.SelectNotNull(x => x.Instance);
		Skill3ToolTipPanel_1_StanceTitle.Visibility = StanceTooltips.Any().ToVisibility();
		Skill3ToolTipPanel_1_StanceText.String.LabelText = string.Join("<br/>", StanceTooltips);

		Skill3ToolTipPanel_1_ItemTitle.Visibility = Skill3ToolTipPanel_1_ItemName.Visibility = Visibility.Collapsed;
		Skill3ToolTipPanel_1_SkillSkinDescription_Title.Visibility = Skill3ToolTipPanel_1_SkillSkinDescription.Visibility = Visibility.Collapsed;
		#endregion


		Skill3ToolTipPanel_1_Sub_Holder.Top = Skill3ToolTipPanel_1_DamageInfoHolder.Bottom + 10;
		Skill3ToolTipPanel_1_ConditionTitle.Top = Skill3ToolTipPanel_1_Sub_Holder.Bottom + 10;
		Skill3ToolTipPanel_1_ConditionText.Top = Skill3ToolTipPanel_1_ConditionTitle.Bottom;
		Skill3ToolTipPanel_1_StanceTitle.Top = Skill3ToolTipPanel_1_ConditionText.Bottom;
		Skill3ToolTipPanel_1_StanceText.Top = Skill3ToolTipPanel_1_StanceTitle.Bottom;
	}
	#endregion
}