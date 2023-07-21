using CSCore.SoundOut;

using Xylia.Preview.Data.Record;
using Xylia.Preview.UI.Custom.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal;
public partial class TaskPanel : GroupBase
{
	#region Constructor
	public TaskPanel() => InitializeComponent();
	#endregion

	#region Functions
	public void LoadData(Quest Quest, WaveOut SoundOut)
	{
		int LocY = 30;
		if (!Quest.MissionStep.Value.Any())
		{
			this.Controls.Add(new ContentPanel
			{
				Text = "无内容, 可能是废弃任务。",
				Location = new Point(ContentStartX, LocY),
			});

			return;
		}

		foreach (var MissionStep in Quest.MissionStep.Value.OrderBy(step => step.id))
		{
			if (MissionStep.Retired) continue;								

			var panel = new MissionStepPanel();
			panel.LoadData(MissionStep, SoundOut);
			this.Controls.Add(panel);
		}
	}

	protected override void OnAutoSizeChanged(EventArgs e)
	{
		foreach (var c in this.Controls.OfType<MissionStepPanel>())
		{
			c.Width = this.Width;
		}
	}

	public override void Refresh()
	{
		base.Refresh();

		int LocY = 30;
		foreach (var panel in this.Controls.OfType<MissionStepPanel>())
		{
			panel.Location = new Point(ContentStartX, LocY);
			panel.Refresh();

			LocY = panel.Bottom;
		}
	}
	#endregion
}
