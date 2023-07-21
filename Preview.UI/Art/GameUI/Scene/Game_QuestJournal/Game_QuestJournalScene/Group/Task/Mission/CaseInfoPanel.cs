using System.ComponentModel;

using CSCore;
using CSCore.SoundOut;

using CUE4Parse.BNS.Conversion;

using FModel.Views.Resources.Controls.Aup;

using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Record;
using Xylia.Preview.UI.Resources;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal;

[DesignTimeVisible(false)]
public partial class CaseInfoPanel : UserControl
{
	#region Constructor
	public CaseInfoPanel()
	{
		InitializeComponent();
	}
	#endregion

	#region Fields
	public WaveOut SoundOut;

	public NpcTalkMessage NpcTalkMessage;

	public int StepIdx;

	public override string Text { get => ContentPanel.Text; set => ContentPanel.Text = value; }
	#endregion

	#region Functions
	private void CaseInfoPanel_Load(object sender, EventArgs e)
	{
		if (NpcTalkMessage.StepShow[StepIdx].Path != null)
			this.pictureBox1.Visible = true;
	}



	CancellationTokenSource cts = new();

	private async void pictureBox1_Click(object sender, EventArgs e)
	{
		cts.Cancel();
		await Task.Run(() =>
		{
			try
			{
				// single or queue 
				if (StepIdx > 0)
				{
					var data = GetWave(NpcTalkMessage.StepShow[StepIdx].Path, StepIdx);
					if (data is null) DisableBtn();
					else Play(data);
				}
				else
				{
					List<byte[]> datas = new();
					for (int idx = 1; idx <= 30; idx++)
					{
						var StepShow = NpcTalkMessage.StepShow[idx];
						if (StepShow.Path is null) break;

						datas.Add(GetWave(StepShow.Path, idx));
					}


					var Valid = datas.Where(data => data != null);
					if (!Valid.Any()) DisableBtn();

					foreach (var data in Valid)
					{
						Thread.Sleep(Play(data));
						Thread.Sleep(800);
					}
				}
			}
			catch
			{

			}
		});
	}

	private void DisableBtn()
	{
		this.pictureBox1.Enabled = false;
		this.pictureBox1.Image = Resource_Common.Image2;
	}

	public static byte[] GetWave(string StepShow, int StepIdx) => StepShow.GetUObject().GetWave(StepIdx);

	public TimeSpan Play(byte[] data)
	{
		if (this.SoundOut.PlaybackState != PlaybackState.Stopped)
			this.SoundOut.Stop();

		var _waveSource = new CustomCodecFactory().GetCodec(data, "ogg");
		this.SoundOut.Initialize(_waveSource);
		this.SoundOut.Play();

		return this.SoundOut.WaveSource.GetLength();
	}
	#endregion
}