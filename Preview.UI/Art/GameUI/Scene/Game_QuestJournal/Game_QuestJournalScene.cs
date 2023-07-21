using CSCore.SoundOut;

using Xylia.Extension;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Record;
using Xylia.Preview.Data.Record.QuestData.Enums;
using Xylia.Preview.UI.Custom.Controls.Forms;

namespace Xylia.Preview.GameUI.Scene.Game_QuestJournal;
public partial class Game_QuestJournalScene : PreviewFrm
{
	#region Constructor
	public Game_QuestJournalScene(Quest QuestData) : this() => this.LoadData(QuestData);

	public Game_QuestJournalScene()
	{
		InitializeComponent();
		this.MaximumSize = new(2000, 1000);

		this.Title = "UI.QuestJournal.Title".GetText();
	}
	#endregion

	#region Fields
	public WaveOut SoundOut = new() { Latency = 100 };

	/// <summary>
	/// 测试模式
	/// </summary>
	public static bool TestMode = false;
	#endregion


	#region Functions (UI)
	private void SwitchTestMode_Click(object sender, EventArgs e) => TestMode = this.SwitchTestMode.Checked;

	private void QuestPreview_Shown(object sender, EventArgs e)
	{
		this.SwitchTestMode.Checked = TestMode;
		this.Refresh();
	}

	private void QuestPreview_FormClosing(object sender, FormClosingEventArgs e)
	{
		try
		{
			if (this.SoundOut.PlaybackState != PlaybackState.Stopped)
				this.SoundOut.Stop();

			this.SoundOut.Dispose();
			this.SoundOut = null;
		}
		catch
		{

		}
	}

	public override void Refresh()
	{
		base.Refresh();

		int y = QuestName.Bottom + 10;
		foreach (var s in this.Controls.OfType<GroupBase>())
		{
			if (!s.Visible) continue;
			
			s.Location = new Point(0, y);
			s.Refresh();

			y = s.Bottom;
		}

		this.Height = y + 45;
	}
	#endregion




	#region Functions
	Quest _data;

	public void LoadData(Quest value)
	{
		_data = value;
		if (value is null) return;


		this.QuestName.Text = value.Name2.GetText();
		this.QuestName.ForeColor = value.ForeColor;
		this.Quest_ICON.Image = value.FrontIcon;
		this.Quest_Group.Text = value.Group2.GetText();

		if (value.Desc is null) this.DescInfo.Visible = false;
		else this.DescInfo.Text = value.Desc.GetText();


		this.TaskInfo.LoadData(value, SoundOut);
		this.RewardInfo.LoadData(value);

		if (value.Category == Category.Dungeon)
		{
			this.Quest_ICON.SetToolTip("此类型为副本进度任务");

			List<string> DifficultyType = new();
			if (value.ProgressDifficultyType1) DifficultyType.Add("入门");
			if (value.ProgressDifficultyType2) DifficultyType.Add("普通");
			if (value.ProgressDifficultyType3) DifficultyType.Add("熟练");


			if (DifficultyType.Any())
				this.QuestName.Text += $" [{DifficultyType.Aggregate((sum, now) => sum + "/" + now)}]";
		}
	}

	private void OpenFileData_Click(object sender, EventArgs e)
	{
		string path = FileCache.Data.Quest.XmlDataPath.Replace("*", "." + this._data.id);
		var content = FileCache.Data.DataPath.XmlData.FileTable.Find(o => o.FilePath == path)?.Xml;
		if (content is null) return;


		var Editor = new UI.Custom.Editor.MainForm();
		Editor.Content = content.Nodes.OuterXml;
		Editor.SaveContent += (o, a) =>
		{
			//ReadTest.Dat.XML_Content[filePath].Data = Encoding.UTF8.GetBytes(Editor.Content);

			////存储内容
			//new BNSDat().CompressFiles(DataPath, filePath, Encoding.UTF8.GetBytes(Editor.Content), false);

			//FrmTips.ShowTipsSuccess("修改成功");
		};

		Editor.Show();
	}
	#endregion
}