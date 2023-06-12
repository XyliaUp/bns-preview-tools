using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using CSCore.SoundOut;


namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal
{
	public partial class TaskPanel : GroupBase
	{
		#region Constructor
		public TaskPanel()
		{
			InitializeComponent();
		}
		#endregion

		#region Functions
		private void ContentInfo_SizeChanged(object sender, EventArgs e)
		{
			this.Refresh();
		}

		public override void Refresh()
		{
			foreach (Control c in this.Controls)
			{
				if (c is MissionStepPanel) c.Width = this.Width;
			}

			base.Refresh();
		}

		public void LoadData(Data.Record.Quest Quest, WaveOut SoundOut)
		{
			int LocY = 30;
			if (!Quest.MissionStep.Value.Any())
			{
				this.Controls.Add(new Controls.ContentPanel
				{
					Text = "无内容, 可能是废弃任务。",
					Location = new Point(ContentStartX, LocY),
				});

				return;
			}

			foreach (var MissionStep in Quest.MissionStep.Value.OrderBy(step => step.id))
			{
				if (MissionStep.Retired) continue;

				var MSP = new MissionStepPanel { Location = new Point(ContentStartX, LocY) };
				MSP.LoadData(MissionStep, SoundOut);

				this.Controls.Add(MSP);
				LocY = MSP.Bottom;
			}
		}
		#endregion
	}
}
