using System.ComponentModel;

using HZH_Controls.Forms;

using Xylia.Extension;
using Xylia.Match.Util;
using Xylia.Match.Windows.Attribute;
using Xylia.Match.Windows.Forms;
using Xylia.Match.Windows.Panel;
using Xylia.Match.Windows.Panel.TextInfo;
using Xylia.Preview.Properties;

namespace Xylia.Match.Windows;
public partial class MainFrm : FrmWithTitle
{
	#region Constructor
	readonly TipMessage TipMessage = new();

	readonly List<ControlPage> pages = new();

	public MainFrm()
	{
		this.InitializeComponent();
		this.Title = $"{AssemblyEx.Title} ({AssemblyEx.BuildTime:yyyyMMdd})";

#if DEBUG
		this.Title += " (DEBUG)";
#endif

		#region Pages
		var resources = new ComponentResourceManager(typeof(MainFrm));
		pages.Add(new(resources.GetString("ItemPage"), new ItemPage()));
		pages.Add(new(resources.GetString("QuestPage"), new QuestPage()));
		pages.Add(new(resources.GetString("IconPage"), new IconPage()));
		pages.Add(new(resources.GetString("TextPage"), new TextPage()));
		pages.Add(new(resources.GetString("UE4Page"), new UE4Page()));
		pages.Add(new(resources.GetString("AttributePage"), new AttributePage()));
		//pages.Add(new(null, new Youdao()));

		pages.ForEach(p => this.tvMenu.Nodes.Add(p.Key, p.Text));
		this.tvMenu.AfterSelect += new TreeViewEventHandler((o, e) =>
		{
			var page = pages.Find(page => page.Key == e.Node.Name.Trim());
			ArgumentNullException.ThrowIfNull(page);

			this.Panel.Controls.Clear();
			this.Panel.Controls.Add(page.Control);
		});

		//this.tvMenu.SelectedNode = this.tvMenu.Nodes.Find("AttributePage", false).FirstOrDefault();
		#endregion


		System.Windows.Forms.Timer Tips = new();
		Tips.Interval = 6000;
		Tips.Enabled = true;
		Tips.Tick += Tips_Tick;

		System.Windows.Forms.Timer UsedMemory = new();
		UsedMemory.Interval = 300;
		UsedMemory.Enabled = true;
		UsedMemory.Tick += GetUsedMemory_Tick;
	}
#endregion


	#region Form
	private void MainForm_Shown(object sender, EventArgs e)
	{
		Task.Run(() => new Update().CheckForUpdates());
	}

	private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		var result = MessageBox.Show("您正在关闭应用程序, 是否确认这么做吗？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
		if (result != DialogResult.OK)
		{
			e.Cancel = true;
			return;
		}

		this.Hide();
		Environment.Exit(0);
	}

	private void MainForm_SizeChanged(object sender, EventArgs e)
	{
		this.Panel.Height = this.Height - 100;
	}
	#endregion

	#region Functions
	private void Tips_Tick(object sender, EventArgs e)
	{
		//保证文本不会重复
		string Tip = "";
		while (Tip == Footer.Text)
			Tip = TipMessage.GetNext;

		Footer.Text = Tip;
	}

	private void GetUsedMemory_Tick(object sender, EventArgs e)
	{
		string Msg = ProcessEx.GetProcessUsedMemory();
		Memory.ForeColor = Msg.Contains("GB") ? Color.Red : Color.MediumAquamarine;
		Memory.Text = $"   内存 {Msg}";
	}

	private void Set_Click(object sender, EventArgs e)
	{
		var Set = new SettingsForm();
		if (Set.ShowDialog() != DialogResult.OK) return;

		// 同步显示
		var page = (ItemPage)pages.Find(page => page.Control is ItemPage).Control;
		page.GRoot_Path.Text = CommonPath.GameFolder;
	}
	#endregion

	#region Open Files / Pages
	private void OpenFolder_Click(object sender, EventArgs e) => System.Diagnostics.Process.Start("explorer", CommonPath.OutputFolder);

	private void pictureBox1_Click(object sender, EventArgs e) => LoggerFrm.Instance.Show();
	private void Btn_AboutUs_Click(object sender, EventArgs e) => System.Diagnostics.Process.Start("explorer", "https://github.com/XyliaUp/bns-preview-tools");
	#endregion
}