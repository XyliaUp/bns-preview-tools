using System.ComponentModel;

using Xylia.Configure;
using Xylia.Extension;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Scene.Game_QuestJournal;
using Xylia.Preview.UI.Resources;

namespace Xylia.Match.Windows;
public partial class QuestSelector : Form
{
	#region Constructor
	public QuestSelector()
	{
		InitializeComponent();
		this.LoadData();
	}
	#endregion


	#region Functions (UI)
	private void ListBox1_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		Quest SelItem;
		if (listBox1.SelectedItem != null) SelItem = listBox1.SelectedItem as Quest;
		else
		{
			Tip.Warning(new ComponentResourceManager(typeof(QuestSelector)).GetString("Tip_SelectOne"));
			return;
		}


		LastRule = SelItem.id;

		var thread = new Thread(act => new Game_QuestJournalScene(SelItem).ShowDialog());
		thread.SetApartmentState(ApartmentState.STA);
		thread.Start();
	}

	private void QuestSelect_TextChanged(object sender, EventArgs e)
	{
		this.Refresh();
	}



	bool inprogress = false;

	public static string LastSearchRule { get; set; }

	private void textBoxEx1_TextChanged(object sender, EventArgs e)
	{
		if (inprogress) return;
		inprogress = true;


		LastSearchRule = textBoxEx1.Text;
		this.listBox1.Items.Clear();
		FileCache.Data.Quest
			.Where(info => info.Name2.GetText()?.Contains(textBoxEx1.Text) ?? false)
			.OrderBy(o => o.id)
			.ForEach(o => listBox1.Items.Add(o));

		inprogress = false;
	}
	#endregion

	#region Functions
	public static int LastRule
	{
		get => Ini.ReadValue("Preview", "quest#last").ToInt();
		set => Ini.WriteValue("Preview", "quest#last", value);
	}

	public void LoadData()
	{
		FileCache.Data.Text.TryLoad();

		this.RefreshList();
	}


	public void RefreshList()
	{
		this.listBox1.Items.Clear();
		this.listBox1.Items.AddRange(FileCache.Data.Quest.OrderBy(o => o.id).ToArray());

		this.textBoxEx1.Visible = true;
		this.textBoxEx1.Text = LastSearchRule;

		//恢复上次选择任务的位置
		var Quest = FileCache.Data.Quest[LastRule];
		if (Quest is null) return;

		for (int idx = 0; idx < this.listBox1.Items.Count; idx++)
		{
			var tmp = this.listBox1.Items[idx] as Quest;
			if (tmp.id == LastRule)
			{
				this.listBox1.SelectedIndex = idx;
				break;
			}
		}
	}

	private void ListBox1_DrawItem(object sender, DrawItemEventArgs e)
	{
		#region Initialize
		if (e.Index == -1 || e.Index >= listBox1.Items.Count) return;
		if (listBox1.Items[e.Index] is not Quest CurQuest) return;


		Brush myBrush;
		if ((e.State & DrawItemState.Selected) == DrawItemState.Selected) myBrush = new SolidBrush(Color.DeepSkyBlue);
		else if (e.Index % 2 == 0) myBrush = new SolidBrush(Color.White);
		else myBrush = new SolidBrush(Color.White);

		e.Graphics.FillRectangle(myBrush, e.Bounds);
		e.DrawFocusRectangle();
		#endregion


		#region 图标
		Graphics g = e.Graphics;
		Rectangle bounds = e.Bounds;
		Rectangle imageRect = new(bounds.X, bounds.Y, bounds.Height, bounds.Height);

		Image image = CurQuest.FrontIcon;
		if (image != null) g.DrawImage(image, imageRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
		#endregion

		#region 文本
		//如果颜色未赋值, 则使用前景色
		Color StrColor = CurQuest.ForeColor;
		if (StrColor == default) StrColor = this.ForeColor;

		//获取任务名称
		string SourceText = CurQuest.Name2.GetText();
		string QuestName = $"[{CurQuest.id}] " + SourceText.CutText();

		var Font = new Font(e.Font.FontFamily, e.Font.Size, FontStyle.Bold);
		Rectangle textRect = new(imageRect.Right, bounds.Y, bounds.Width - imageRect.Right, bounds.Height);
		e.Graphics.DrawString(QuestName, Font, new SolidBrush(StrColor), textRect, new StringFormat { LineAlignment = StringAlignment.Center });
		#endregion

		#region 额外附加图标
		if (SourceText is null) return;

		List<Image> ExtraImage = new();

		if (SourceText.Contains("\"00015590.Tag_Contents_Daily\"")) ExtraImage.Add(Resource_BNSR.Tag_024);

		if (SourceText.Contains("_Superior")) ExtraImage.Add(Resource_BNSR.Tag_138);
		if (SourceText.Contains("_Prime")) ExtraImage.Add(Resource_BNSR.Tag_138);
		if (SourceText.Contains("_Hero")) ExtraImage.Add(Resource_BNSR.Tag_139);

		if (SourceText.Contains("\"00015590.Tag_Dungeon_Two\"")) ExtraImage.Add(Resource_BNSR.Tag_164);
		if (SourceText.Contains("\"00015590.Tag_Dungeon_Six\"")) ExtraImage.Add(Resource_BNSR.Tag_165);
		if (SourceText.Contains("\"00015590.Tag_Dungeon_Four\"")) ExtraImage.Add(Resource_BNSR.Tag_166);
		//if (SourceText.Contains("EventMarker")) ExtraImage.Add(BnsCommon.EventMarker);

		if (ExtraImage.Any())
		{
			int StartX = textRect.Left + (int)QuestName.MeasureString(Font).Width;
			foreach (var eg in ExtraImage)
			{
				Rectangle rect = new(StartX += eg.Width, bounds.Y, eg.Width, eg.Height);
				g.DrawImage(eg, rect, 0, 0, eg.Width, eg.Height, GraphicsUnit.Pixel);
			}
		}

		ExtraImage.Clear();
		#endregion
	}
	#endregion
}