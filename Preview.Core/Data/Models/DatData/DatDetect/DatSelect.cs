using Xylia.Configure;
using Xylia.Extension;
using Xylia.Preview.Data.Models.DatData.DatDetect;

namespace Xylia.Preview.Data.Models.DatData;
public partial class DatSelect : Form
{
	#region Fields
	private readonly IEnumerable<FileInfo> list_xml;
	private readonly IEnumerable<FileInfo> list_local;


	public string XML_Select;

	public string Local_Select;
	#endregion

	#region Constructor
	public DatSelect(IEnumerable<FileInfo> Xml, IEnumerable<FileInfo> Local)
	{
		InitializeComponent();

		this.list_xml = Xml;
		this.list_local = Local;
		this.Chk_64bit.Checked = Ini.ReadValue("DatSelect", "64bit").ToBool();

		#region 判断是否禁用切换
		if (!Xml.Has32bit() && !Local.Has32bit())
		{
			Chk_64bit.Enabled = false;
			Chk_64bit.Checked = true;
		}
		else if (!Xml.Has64bit() && !Local.Has64bit())
		{
			Chk_64bit.Enabled = false;
			Chk_64bit.Checked = false;
		}
		#endregion
	}
	#endregion

	#region Functions (UI)
	private void Select_Load(object sender, EventArgs e)
	{
		Chk_HidenBpFiles_CheckedChanged(null, null);
	}

	private void Btn_Confirm_Click(object sender, EventArgs e)
	{
		XML_Select = comboBox1.Text.Replace("...", @"contents\Local");
		Local_Select = comboBox2.Text.Replace("...", @"contents\Local");

		this.DialogResult = DialogResult.OK;
		this.TimeInfo.Enabled = this.NoResponse.Enabled = false;
	}

	private void Btn_Cancel_Click(object sender, EventArgs e)
	{
		this.DialogResult = DialogResult.Cancel;
	}

	private void DataSelect_MouseEnter(object sender, EventArgs e)
	{
		StopCountDown();
		LastActTime = DateTime.Now;
	}

	private void TimeInfo_VisibleChanged(object sender, EventArgs e)
	{
		this.Chk_HidenBpFiles.Visible = !this.TimeInfo.Visible;
	}

	private void Chk_HidenBpFiles_CheckedChanged(object sender, EventArgs e)
	{
		Load_Cmb(comboBox1, list_xml);
		Load_Cmb(comboBox2, list_local);
	}

	private void Chk_64bit_CheckedChanged(object sender, EventArgs e)
	{
		Ini.WriteValue("DatSelect", "64bit", this.Chk_64bit.Checked);

		Chk_HidenBpFiles_CheckedChanged(null, null);
	}

	private void Load_Cmb(ComboBox Cmb, IEnumerable<FileInfo> FileCollection)
	{
		Cmb.Items.Clear();

		//item sort
		var Pathes = FileCollection.GetFiles(this.Chk_64bit.Checked).Select(f => f.FullName).ToList();
		Pathes.ForEach(w =>
		{
			//hide back files
			if (Chk_HidenBpFiles.Checked)
			{
				if (!(w.Contains("backup") || w.Contains("备份")))
					Cmb.Items.Add(w.Replace(@"contents\Local", "..."));
			}
			else Cmb.Items.Add(w.Replace(@"contents\Local", "..."));
		});


		if (Cmb.Items.Count > 0) Cmb.Text = Cmb.Items[0].ToString();

		Cmb.Enabled = Cmb.Items.Count != 1;
	}
	#endregion



	#region 倒计时控制块
	/// <summary>
	/// 最后活动时间
	/// </summary>
	DateTime LastActTime = DateTime.Now;

	/// <summary>
	/// 倒计时启动时间
	/// </summary>
	DateTime dt = DateTime.Now;

	/// <summary>
	/// 倒计时总秒数
	/// </summary>
	readonly int CountDownSec = 10;

	/// <summary>
	/// 无响应上限时长
	/// </summary>
	readonly int NoResponseSec = 15;


	/// <summary>
	/// 开始倒计时
	/// </summary>
	private void StartCountDown()
	{
		TimeInfo.Text = null;

		dt = DateTime.Now;
		this.CountDown.Enabled = true;
		this.TimeInfo.Visible = true;
	}

	private void StopCountDown()
	{
		this.CountDown.Enabled = false;
		this.TimeInfo.Visible = false;
	}

	private void DataSelect_Shown(object sender, EventArgs e)
	{
		StartCountDown();
		LastActTime = DateTime.Now;
		this.NoResponse.Enabled = true;
	}

	/// <summary>
	/// 长时间无响应控制器
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void NoResponse_Tick(object sender, EventArgs e)
	{
		int CurNoResponseSec = (int)DateTime.Now.Subtract(LastActTime).TotalSeconds;
		if (CurNoResponseSec >= NoResponseSec)
		{
			StartCountDown();
			LastActTime = DateTime.Now;
		}
	}

	/// <summary>
	/// 控制器
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void Timer_Tick(object sender, EventArgs e)
	{
		int RemainSec = CountDownSec - (int)DateTime.Now.Subtract(dt).TotalSeconds;
		TimeInfo.Text = $"将在 {RemainSec} 秒后自动选择";

		//自动选择
		if (RemainSec <= 0) Btn_Confirm_Click(null, null);
	}
	#endregion
}