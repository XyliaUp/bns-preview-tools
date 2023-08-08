using System.ComponentModel;
using System.Diagnostics;
using System.IO;

using HZH_Controls.Forms;

using Xylia.Configure;
using Xylia.Match.Util.ItemList;
using Xylia.Match.Windows.Forms;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Helper.Output;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Scene.Searcher;
using Xylia.Preview.Helper.Output;
using Xylia.Preview.Properties;
using Xylia.Preview.UI.Extension;

using GameUI = Xylia.Preview.GameUI.Scene;

namespace Xylia.Match.Windows.Panel;

[DesignTimeVisible(false)]
public partial class ItemPage : UserControl
{
	#region Constructor
	public ItemPage()
	{
		InitializeComponent();
		this.TabControl.SelectedIndex = 0;
		this.GRoot_Path.Text = CommonPath.GameFolder;
		this.ItemPreview_Search.InputText = Ini.ReadValue("Preview", "item#searchrule");
	}
	#endregion


	#region Functions (UI)
	public string CacheList { get => Ini.ReadValue(this.GetType(), "CacheList"); set => Ini.WriteValue(this.GetType(), "CacheList", value); }

	private void ucBtnFillet1_BtnClick(object sender, EventArgs e)
	{
		var SettingsForm = new SettingsForm();
		if (SettingsForm.ShowDialog() == DialogResult.OK)
			this.GRoot_Path.Text = CommonPath.GameFolder;
	}

	private void File_Searcher_BtnClick(object sender, EventArgs e)
	{
		Open.Filter = @"|*.chv|All files|*.*";
		Open.RestoreDirectory = false;
		Open.InitialDirectory = ItemMatch.OUTDIR;
		if (Open.ShowDialog() == DialogResult.OK)
		{
			Chv_Path.Text = Path.GetFullPath(Open.FileName);
			CacheList = Chv_Path.Text;
		}
	}



	private void Btn_StartMatch_BtnClick(object sender, EventArgs e)
	{
		#region Initialize
		if (!Directory.Exists(GRoot_Path.Text))
		{
			SendMessage("请先设置游戏目录");
			return;
		}
		else if (!Directory.Exists(CommonPath.OutputFolder))
		{
			SendMessage("请先设置输出目录");
			ucBtnFillet1_BtnClick(null, null);
			return;
		}
		else if (Chk_OnlyNew.Checked && !Chv_Path.Text.Contains("云端资源") && !File.Exists(Chv_Path.Text))
		{
			SendMessage(".Chv配置文件未选择或不存在\n如无配置文件, 请使用云资源或者取消下方\"仅更新\"勾选");
			return;
		}

		Btn_StartMatch.Enabled = File_Searcher.Enabled = GRoot_Path.Enabled = Chv_Path.Enabled = Chk_OnlyNew.Enabled = false;
		// Program.Taskbar.SetProgressState(TaskbarProgressBarState.Indeterminate, this.Handle);
		#endregion

		#region Select Mode
		ModeSelect select2 = new();
		select2.ShowDialog();

		if (select2.Result == ModeSelect.State.None)
		{
			ToEnd(true);
			return;
		}
		#endregion


		var StartTime = DateTime.Now;
		this.Timer.Start();

		var thread = new Thread(() =>
		{
			#region Load
			var match = new ItemMatch(Str => SendMessage(Str)) { Chk_OnlyNew = this.Chk_OnlyNew.Checked, };
			Step1.StepIndex = 1;

			match.LoadCache(CacheList);
			var noEmpty = match.GetData();
			if (!noEmpty)
			{
				SendMessage("已激活仅新增道具功能, 对比后无新增。");
				ToEnd();
				return;
			}
			Step1.StepIndex = 2;
			#endregion

			#region Output
			Step1.StepIndex = 3;
			match.Start(StartTime, select2.Result == ModeSelect.State.Xlsx);
			match = null;
			#endregion


			ToEnd();
		});

		thread.SetApartmentState(ApartmentState.STA);
		thread.Start();
	}

	private void ToEnd(bool UseError = false)
	{
		Timer.Stop();
		Step1.StepIndex = 4;

		Btn_StartMatch.Enabled = ucBtnFillet1.Enabled = File_Searcher.Enabled = GRoot_Path.Enabled = Chv_Path.Enabled = Chk_OnlyNew.Enabled = true;
		GC.Collect();
	}






	private void Chk_OnlyNew_CheckedChanged(object sender, EventArgs e) => Note_Chv.Visible = Chv_Path.Visible = /*Online_Searcher.Visible = */File_Searcher.Visible = Chk_OnlyNew.Checked;

	private void File_Searcher_MouseEnter(object sender, EventArgs e) => FrmAnchorTips.ShowTips(File_Searcher, "选择本地文件\n\n如无, 请选择云端资源。", AnchorTipsLocation.BOTTOM, Color.MediumOrchid, Color.FloralWhite, null, 12, 3500, false);


	private void SendMessage(string Msg, bool IsError = false) => this.Invoke(() =>
	{
		if (IsError) FrmTips.ShowTipsError(Msg);
		else FrmTips.ShowTipsSuccess(Msg);
	});

	private void TabControl_KeyDown(object sender, KeyEventArgs e)
	{
		switch (e.KeyCode)
		{
			case Keys.Enter:
			{
				var SelectdPage = this.TabControl.SelectedTab;

				if (SelectdPage == this.PreviewPage_Item) ItemPreview_Search_SearchClick(null, null);
				else if (SelectdPage == this.PreviewPage_Else) ucBtnExt11_BtnClick(null, null);
				else Debug.WriteLine("enter event not defined：" + SelectdPage);
			}
			break;

			case Keys.Up:
			{
				if (int.TryParse(ItemPreview_Search.InputText, out int r)) ItemPreview_Search.InputText = (++r).ToString();
			}
			break;

			case Keys.Down:
			{
				if (int.TryParse(ItemPreview_Search.InputText, out int r)) ItemPreview_Search.InputText = (--r).ToString();
			}
			break;
		}
	}

	private void TimeInfo_TextChanged(object sender, EventArgs e)
	{
		if (TimeInfo.Text == "TimeInfo") TimeInfo.Visible = false;
		else if (!TimeInfo.Visible) TimeInfo.Visible = true;
	}
	#endregion



	#region Item
	private void ItemPreview_Search_SearchClick(object sender, EventArgs e)
	{
		var rule = ItemPreview_Search.InputText;
		var thread = new Thread(() =>
		{
			if (string.IsNullOrWhiteSpace(rule)) new GameUI.Game_Auction.Game_AuctionScene().ShowDialog();
			else
			{
				var records = rule.GetItemInfo(true);
				if (records.Count == 0) SendMessage("所查找的道具不存在", true);
				else if (records.Count == 1) records.First().PreviewShow();
				else new GameUI.Game_Auction.Game_AuctionScene(rule).ShowDialog();
			}
		});

		thread.SetApartmentState(ApartmentState.STA);
		thread.Start();
	}

	private void ucBtnExt5_MouseEnter(object sender, EventArgs e)
	{
		FrmAnchorTips.ShowTips((Control)sender, "初始化已载入的数据", AnchorTipsLocation.BOTTOM, Color.MediumOrchid, Color.FloralWhite, null, 12, 3500, false);
	}

	private void ucBtnExt5_BtnClick(object sender, EventArgs e)
	{
		new Thread(o =>
		{
			try
			{
				FileCache.Clear();
				ProcessEx.ClearMemory();

				SendMessage("刷新完成");
			}
			catch (Exception ee)
			{
				SendMessage(ee.Message, true);
				//LoggerFrm.Instance.Write(ee, MsgInfo.MsgLevel.错误);
			}

		}).Start();
	}

	private void ItemPreview_Search_TextChanged(object sender, EventArgs e) => Ini.WriteValue("Preview", "item#searchrule", this.ItemPreview_Search.InputText);
	#endregion

	#region Else
	public static void ShowDialog<T>() where T : Form, new() => Task.Run(() => new T().ShowDialog());
	

	private void ucBtnExt1_BtnClick(object sender, EventArgs e) => ShowDialog<GameUI.Game_RandomStore.Game_RandomStoreExhibitionScene>();
	private void ucBtnExt4_Click(object sender, EventArgs e) => Task.Run(() => new SearcherResult(FileCache.Data.Npc).ShowDialog());
	private void ucBtnExt8_BtnClick(object sender, EventArgs e) => ShowDialog<GameUI.Game_RandomStore.Game_RandomStoreScene>();
	private void ucBtnExt9_BtnClick(object sender, EventArgs e) => ShowDialog<GameUI.Game_ChallengeToday.Game_ChallengeTodayScene>();
	private void ucBtnExt11_BtnClick(object sender, EventArgs e) => ShowDialog<GameUI.Game_ItemStore.Game_ItemStoreScene>();
	private void ucBtnExt13_BtnClick(object sender, EventArgs e) => ShowDialog<GameUI.Game_Map.Game_MapScene>();
	private void ucBtnExt20_BtnClick(object sender, EventArgs e) => ShowDialog<GameUI.Skill.SkillTraitPreview>();

	private void ucBtnExt12_BtnClick(object sender, EventArgs e) => OutBase.StartOutput<ItemCloset_Main>();
	private void ucBtnExt18_BtnClick(object sender, EventArgs e) => OutBase.StartOutput<ItemCloset_Type>();
	private void ucBtnExt14_BtnClick(object sender, EventArgs e) => OutBase.StartOutput<ItemBuyPriceOut>();
	private void ucBtnExt16_BtnClick(object sender, EventArgs e) => OutBase.StartOutput<ItemTransformRecipeOut>();
	private void ucBtnExt10_BtnClick(object sender, EventArgs e) => OutBase.StartOutput<OutSet<WorldAccountExpedition>>();
	private void ucBtnExt2_BtnClick(object sender, EventArgs e) => OutBase.StartOutput<OutSet<WorldAccountMuseum>>();
	private void ucBtnExt3_BtnClick(object sender, EventArgs e) => OutBase.StartOutput<CollectingOut>();

	private void btn_SetOutput_Click(object sender, EventArgs e)
	{
		Open.Filter = @"Xml Files|*.xml|All files|*.*";
		if (Open.ShowDialog() == DialogResult.OK)
		{
			OutputTable(Open.FileName);
		}
		else if (MessageBox.Show("是否需要创建默认配置文件？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
		{
			var save = new SaveFileDialog
			{
				FileName = $"WorldAccountMuseum.xml",
				Filter = "Xml Files|*.xml",

				InitialDirectory = PathDefine.Desktop,
			};
			save.ShowDialog();

			var resources = new ComponentResourceManager(typeof(ItemPage));
			File.WriteAllText(save.FileName, resources.GetString("OutputSet_default"));
		}
	}


	public static void OutputTable(string Path)
	{
		var thread = new Thread(act => new OutSet<FileConfigOut>(File.ReadAllText(Path)).Output());
		thread.SetApartmentState(ApartmentState.STA);
		thread.Start();
	}
	#endregion
}