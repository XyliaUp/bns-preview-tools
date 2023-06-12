using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using CSCore.SoundOut;

using Xylia.Preview.Data.Record;
using Xylia.Preview.Data.Record.QuestData;
using Xylia.Preview.Data.Record.QuestData.Case;
using Xylia.Preview.GameUI.Controls;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal
{
	[DesignTimeVisible(false)]
	public partial class MissionPanel : UserControl
	{
		#region Constructor
		public MissionPanel() => InitializeComponent();
		#endregion


		#region Functions
		public void LoadData(Mission Mission, WaveOut SoundOut)
		{
			#region Initialize
			if (Mission is null) return;

			this.Tag = Mission.id;
			this.MissionText.Text = Mission.Name2.GetText();

			//书信对话类型可能不会有步骤描述
			if (Mission.Name2 is null)
			{
				if (Mission.Case.Find(c => c is CaseBase o && o.Type == CaseType.TalkToSelf) != null)
					this.MissionText.Text = "<font name=\"00008130.UI.Label_LightYellow_12\">完成书信对话</font>";
			}

			//如果课题需求值不为1, 则显示进度需求信息
			if (Mission.RequiredRegisterValue != 1) this.MissionText.Text = $"0/{ Mission.RequiredRegisterValue} {this.MissionText.Text}";
			#endregion

			#region 处理课题信息
			List<CaseTest> Cases = new();
			foreach (var o in Mission.Case)
			{
				var Case = new CaseTest(o);
				Cases.Add(Case);


				//测试模式
				if (Game_QuestJournalScene.TestMode)
				{
					Case.Controls.Add(new ContentPanel($"{o.GetType()}"));
				}

				//如果是对话类型
				if (o is TalkToSelf talktoself)
				{
					Case.Controls.AddRange(LoadTalkMessage(talktoself.Msg, SoundOut));
				}
				else if (o is NpcTalkBase talk)
				{
					if (talk.NpcResponse != null) Case.Controls.AddRange(LoadTalkMessage(talk.NpcResponse, SoundOut));
				}
			}
			#endregion


			#region	控件排序
			var height = this.MissionText.Bottom;
			foreach (var Case in Cases)
			{
				if (Case.Controls.Count == 0) continue;

				#region 用于区分多个实例
				var signal = new ContentPanel("▼")
				{
					Tag = "#signal",

					Location = new Point(GroupBase.ContentStartX - 10, height),
					ForeColor = Color.BlueViolet,
				};

				this.Controls.Add(signal);
				ToolTip.SetToolTip(signal, Case.ToString());
				#endregion

				#region 界面处理
				foreach (var o in Case.Controls)
				{
					if (!this.Controls.Contains(o)) this.Controls.Add(o);

					o.Location = new Point(GroupBase.ContentStartX + 10, height);
					height = o.Bottom;
				}
				height += 30;
				#endregion
			}

			this.Height = height;
			#endregion
		}



		public static List<Control> LoadTalkMessage(NpcResponse Response, WaveOut SoundOut)
		{
			List<Control> result = new();

			var NpcResponse = FileCache.Data.NpcResponse[Response];
			if (NpcResponse is null || NpcResponse.TalkMessage is null) return result;

			return LoadTalkMessage(NpcResponse.TalkMessage, SoundOut);
		}

		/// <summary>
		/// Load 对话消息
		/// </summary>
		/// <param name="Message"></param>
		/// <param name="SoundOut"></param>
		/// <returns></returns>
		public static List<Control> LoadTalkMessage(NpcTalkMessage Message, WaveOut SoundOut)
		{
			//Load Data
			List<Control> result = new();

			var TalkMessage = FileCache.Data.NpcTalkMessage[Message];
			if (TalkMessage is null) return result;

			if (TalkMessage.EndTalkSocial != null)
				System.Diagnostics.Debug.WriteLine("EndTalkSocial: " + TalkMessage.EndTalkSocial);

			for (int i = 1; i <= 30; i++)
			{
				#region Load Data
				var StepText = TalkMessage.Attributes["step-text-" + i];
				if (StepText is null) break;

				var StepSubtext = TalkMessage.Attributes["step-subtext-" + i];
				var StepNext = TalkMessage.Attributes["step-next-" + i];
				var StepKismet = TalkMessage.Attributes["step-kismet-" + i];
				var StepCinematic = TalkMessage.Attributes["step-cinematic-" + i];
				var StepShow = TalkMessage.Attributes["step-show-" + i];
				var StepCameraShow = TalkMessage.Attributes["step-camera-show-" + i];
				#endregion

				#region 生成控件
				string Text = StepText.GetText();
				if (StepSubtext != null) Text += $"\n           [内心] " + StepSubtext.GetText();
				if (StepNext != null) Text += $"\n           [自己] " + StepNext.GetText();

				CaseInfoPanel test = new();
				test.Text = Text;
				test.NpcTalkMessage = TalkMessage;
				test.StepIdx = i;
				test.SoundOut = SoundOut;

				result.Add(test);
				#endregion
			}

			return result;
		}
		#endregion
	}
}