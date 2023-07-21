using System.ComponentModel;
using System.Data;

using CSCore.SoundOut;

using Xylia.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record;
using Xylia.Preview.Data.Record.QuestData;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal;

[DesignTimeVisible(false)]
public partial class MissionStepPanel : UserControl
{
	#region Constructor
	public MissionStepPanel() => InitializeComponent();
	#endregion


	#region Functions
	public void LoadData(MissionStep MissionStep, WaveOut SoundOut)
	{
		this.Content_StepID.Text = "● ";

		#region Desc
		this.Panel_TaskDesc.Visible = false;
		if (MissionStep.Desc != null)
		{
			this.Panel_TaskDesc.Visible = true;
			this.Panel_TaskDesc.Text = MissionStep.Desc.GetText();
		}
		#endregion

		#region Demand
		if (this.MissionDemand.Visible = MissionStep.Mission.Count > 1)
		{
			if (MissionStep.CompletionType == OpCheck.or) this.MissionDemand.Text = $"完成下列任一课题";
			else if (MissionStep.CompletionType == OpCheck.and)
			{
				this.MissionDemand.Text = $"完成下列所有课题";
				this.MissionDemand.SetToolTip("对于同步骤中的不同课题, 进度同时计算");
			}
		}
		#endregion

		#region Missions
		foreach (var Mission in MissionStep.Mission)
		{
			var MissionPanel = new MissionPanel();
			MissionPanel.LoadData(Mission, SoundOut);

			this.Controls.Add(MissionPanel);
		}
		#endregion
	}

	public override void Refresh()
	{
		base.Refresh();

		int y = 0;
		if (this.MissionDemand.Visible) y = this.MissionDemand.Bottom;

		foreach (var MissionCtl in this.Controls.OfType<MissionPanel>().OrderBy(c => (byte)c.Tag))
		{
			MissionCtl.Location = new Point(15, y);
			MissionCtl.Refresh();

			y = MissionCtl.Bottom;
		}

		if (this.Panel_TaskDesc.Visible)
		{
			this.Panel_TaskDesc.Location = new Point(0, y);
			y = this.Panel_TaskDesc.Bottom;
		}

		this.Height = y + 5;
	}
	#endregion
}