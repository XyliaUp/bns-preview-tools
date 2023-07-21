using System.ComponentModel;

using CSCore.SoundOut;

using Xylia.Preview.Data.Record;
using Xylia.Preview.Data.Record.QuestData;
using Xylia.Preview.UI.Custom.Controls;

using static Xylia.Preview.Data.Record.QuestData.Case;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal;

[DesignTimeVisible(false)]
public partial class MissionPanel : UserControl
{
	#region Constructor
	public MissionPanel() => InitializeComponent();
	#endregion


	#region Functions
	List<CaseTest> Cases = new();

	public void LoadData(Mission Mission, WaveOut SoundOut)
	{
		#region Initialize
		if (Mission is null) return;

		this.Tag = Mission.id;
		this.MissionText.Text = Mission.Name2.GetText();

		//书信对话类型可能不会有步骤描述
		if (Mission.Name2 is null)
		{
			if (Mission.Case.Find(c => c is TalkToSelf) != null)
				this.MissionText.Text = "<font name=\"00008130.UI.Label_LightYellow_12\">完成书信对话</font>";
		}

		//如果课题需求值不为1, 则显示进度需求信息
		if (Mission.RequiredRegisterValue != 1) this.MissionText.Text = $"0/{Mission.RequiredRegisterValue} {this.MissionText.Text}";
		#endregion

		#region Case
		foreach (var o in Mission.Case)
		{
			var Case = new CaseTest(o);
			Cases.Add(Case);

			if (Game_QuestJournalScene.TestMode)
			{
				Case.Controls.Add(new ContentPanel($"{o.GetType()}"));
			}

			// 如果是对话类型
			if (o is TalkToSelf talktoself)
			{
				Case.Controls.AddRange(LoadTalkMessage(talktoself.Msg, SoundOut));
			}
			else if (o is NpcTalkBase talk)
			{
				if (talk.NpcResponse != null) Case.Controls.AddRange(LoadTalkMessage(talk.NpcResponse, SoundOut));
			}


			if (Case.Controls.Count > 0)
			{
				var signal = new ContentPanel("▼") { Tag = "#signal", ForeColor = Color.BlueViolet, };
				ToolTip.SetToolTip(signal, Case.ToString());

				Case.Controls.Insert(0, signal);
			}
		}
		#endregion
	}

	private static List<Control> LoadTalkMessage(NpcResponse Response, WaveOut SoundOut)
	{
		List<Control> result = new();
		if (Response is null || Response.TalkMessage is null) return result;

		return LoadTalkMessage(Response.TalkMessage, SoundOut);
	}

	private static List<Control> LoadTalkMessage(NpcTalkMessage TalkMessage, WaveOut SoundOut)
	{
		List<Control> result = new();
		if (TalkMessage is null) return result;

		if (TalkMessage.EndTalkSocial != null)
			Debug.WriteLine("EndTalkSocial: " + TalkMessage.EndTalkSocial);

		for (int i = 0; i < 30; i++)
		{
			var StepText = TalkMessage.StepText[i];
			if (StepText is null) break;

			string Text = StepText.GetText();
			if (TalkMessage.StepSubtext[i] != null) Text += $"\n           [内心] " + TalkMessage.StepSubtext[i].GetText();
			if (TalkMessage.StepNext[i] != null) Text += $"\n           [自己] " + TalkMessage.StepNext[i].GetText();

			result.Add(new CaseInfoPanel()
			{
				StepIdx = i,
				Text = Text,
				NpcTalkMessage = TalkMessage,
				SoundOut = SoundOut
			});
		}

		return result;
	}




	public override void Refresh()
	{
		base.Refresh();

		var height = this.MissionText.Bottom;
		foreach (var Case in Cases)
		{
			if (Case.Controls.Count == 0) continue;

			foreach (var o in Case.Controls)
			{
				this.Controls.Add(o);

				if (o.Tag == "#signal")
				{
					o.Location = new Point(GroupBase.ContentStartX - 10, height);
				}
				else
				{
					o.Location = new Point(GroupBase.ContentStartX + 10, height);
					o.Refresh();

					height = o.Bottom;
				}
			}

			height += 30;
		}

		this.Height = height;
	}
	#endregion
}