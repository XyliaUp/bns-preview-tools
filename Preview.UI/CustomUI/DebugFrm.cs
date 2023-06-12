using CUE4Parse.BNS;

using Xylia.Preview.GameUI.Controls;

namespace Xylia.Preview.Tests
{
	public partial class DebugFrm : Form
	{
		#region Constructor
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.SetCompatibleTextRenderingDefault(false);

			Application.Run(new GameUI.Scene.Game_Auction.Game_AuctionScene());
			Application.Run(new DebugFrm());
		}

		public DebugFrm() => InitializeComponent();
		#endregion


		private async void DebugFrm_Load(object sender, EventArgs e)
		{
			PakData PakData = new();
			PakData.Initialize();

			pictureBox1.Image = "/Game/Art/UI/GameUI/Resource/GameUI_Icon5th/Weapon_BA_180028_col1.Weapon_BA_180028_col1".GetUObject().GetImage();

			TestTooltip2.SetTooltip(this.contentPanel2, "<p justification=\"true\" justificationtype=\"linefeedbywidgetarea\"><link id=\"none\"/> </p><p horizontalalignment=\"center\"><br/><image enablescale=\"false\" imagesetpath=\"00027918.InterD_ChungGakjiBu\"/><br/><image enablescale=\"true\" imagesetpath=\"00009499.Field_Boss\" scalerate=\"1.4\"/>铁傀王<br/><br/>中原的海盗组织——冲角团的平南舰队支部。<br/>支部长是啸四海。</p>");


			//this.contentPanel1.Params.Add("2080002"]);
			//this.contentPanel1.Params.Add(1);
			//this.contentPanel1.Params.Add(2);
			//this.contentPanel1.Text = "UI.Tooltip.BonusRewardItem.Random.MinMax".GetText() + "UI.Tooltip.BonusRewardItem.Random.Tag".GetText();
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			//Debug.WriteLine(FileCache.Data.TextData[this.textBox1.Text]);


			//var data = "/Game/FaceFXImporter/220622/q_2209_2_voice_1_show.q_2209_2_voice_1_show".GetUObject().GetWave();
			//if (data is null) return;
			//var _waveSource = new CustomCodecFactory().GetCodec(data, "ogg");
			//var _soundOut = new WaveOut() { Latency = 100 };
			//_soundOut.Initialize(_waveSource);
			//_soundOut.Play();


			//this.Controls.Remove<CaseInfoPanel>();
			//int height = 25;
			//var cs = MissionPanel.LoadTalkMessage(new Xylia.Preview.Data.Record.CommonTable.NpcResponse(this.textBox1.Text), new WaveOut());

			//foreach (var o in cs)
			//{
			//	if (!this.Controls.Contains(o)) this.Controls.Add(o);

			//	o.Location = new Point(GroupBase.ContentStartX + 10, height);
			//	height = o.Bottom;
			//}
		}
	}
}