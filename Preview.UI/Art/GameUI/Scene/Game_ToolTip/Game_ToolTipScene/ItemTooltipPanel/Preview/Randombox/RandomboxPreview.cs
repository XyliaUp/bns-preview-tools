using Xylia.Extension;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.BinData.Table.Record;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Scene.Game_ToolTip.Game_ToolTipScene;
using Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Randombox;
using Xylia.Preview.UI.Custom.Controls;
using Xylia.Preview.UI.Resources;

using static Xylia.Preview.Data.Record.Item;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel;
public partial class RandomboxPreview : TitlePanel
{
	#region Constructor
	public RandomboxPreview()
	{
		InitializeComponent();
		this.AutoSize = true;
	}
	#endregion

	#region Fields
	public bool HasReward => this.RewardPages != null && this.RewardPages.Count != 0;


	private readonly static Bitmap IniImage = Resource_Common.Circle.ChangeColor(Color.White);

	private readonly static Bitmap SelImage = Resource_Common.Circle.ChangeColor(Color.BlueViolet);


	/// <summary>
	/// 显示奖励组信息
	/// </summary>
	public bool ShowGroup { get; set; } = false;

	/// <summary>
	/// 显示奖励职业信息
	/// </summary>
	public bool ShowJob { get; set; } = true;
	#endregion

	#region Fields (Page Control)
	private List<RewardPage> RewardPages;

	private readonly Dictionary<Control, RewardPage> RewardPageLink = new();

	/// <summary>
	/// 当前选择的奖励选择 按钮图标
	/// </summary>
	private ItemIconCell SelPicBox { get; set; }

	private List<ItemIconCell> ItemIconCells { get; set; }

	/// <summary>
	/// 钥匙图标大小
	/// </summary>
	readonly int PicScale = 32;
	#endregion


	#region Functions
	public new void CreateControl()
	{
		#region Initialize
		if (!HasReward) return;

		bool ShowPicBox = false;
		this.ItemIconCells = new();

		bool OnlyOnePage = this.RewardPages.Count == 1;
		#endregion

		#region create select button
		foreach (var page in this.RewardPages)
		{
			if (!OnlyOnePage || page.HasOpenItem2)
			{
				ShowPicBox = true;
				var OpenItem2 = FileCache.Data.Item[page.OpenItem2?.Item];

				#region 控件处理
				Bitmap Icon = OpenItem2?.Icon();

				var IconBtn = new ItemIconCell()
				{
					ObjectRef = OpenItem2,
					Image = Icon,
					Scale = PicScale,
					SizeMode = PictureBoxSizeMode.Zoom,

					//显示所需钥匙数量
					ShowStackCount = Icon is not null,
					StackCount = (page.OpenItem2?.StackCount) ?? 1,
				};

				if (Icon is null)
				{
					IconBtn.Scale = 20;
					IconBtn.Image = IniImage;
				}
				#endregion

				if (!OnlyOnePage)
				{
					IconBtn.Click += (s, e) => this.SelectReward((ItemIconCell)s);
					IconBtn.BringToFront();

					this.Controls.Add(IconBtn);

					this.ItemIconCells.Add(IconBtn);
					this.RewardPageLink.Add(IconBtn, page);
				}
			}
		}
		#endregion


		if (ShowPicBox) this.RewardPreview_SizeChanged(null, null);


		if (!OnlyOnePage) this.SelectReward(this.ItemIconCells.First());
		else this.SelectReward(this.RewardPages.First());
	}

	private void RewardPreview_SizeChanged(object sender, EventArgs e)
	{
		if (this.ItemIconCells != null)
		{
			int StartX = this.Width - 30;
			int PicPadding = 3;

			int DiffVal = PicScale * this.ItemIconCells.Count + PicPadding * (this.ItemIconCells.Count - 1);

			this.ItemIconCells.ForEach(box =>
			{
				box.Location = new Point(StartX - DiffVal, 8);
				DiffVal -= PicScale + PicPadding;
			});
		}
	}

	private void SelectReward(ItemIconCell CurPic)
	{
		// check same button
		if (this.SelPicBox != null && CurPic == this.SelPicBox) return;
		else this.SelPicBox = CurPic;


		// change color
		if (this.SelPicBox != null && this.SelPicBox.Image is null)
			this.SelPicBox.Image = IniImage;

		if (CurPic is null) throw new InvalidOperationException();
		else CurPic.Image ??= SelImage;


		SelectReward(this.RewardPageLink[CurPic]);
	}

	private void SelectReward(RewardPage page)
	{
		this.SuspendLayout();
		this.Controls.OfType<ContentPanel>().ForEach(this.Controls.Remove);

		// event invoke
		this.RewardSelected(page);

		#region show 
		int Y = 21;
		foreach (var c in new List<ContentPanel>(page.Preview))
		{
			//if (page.RewardInfo is DecomposeJobRewardInfo)
			//	c.IsJobReward = true;

			c.Location = new Point(5, Y);
			c.Refresh();
			c.BringToFront();

			this.Controls.Add(c);


			this.Height = Math.Max(Y = c.Bottom, this.Height);
		}
		#endregion

		this.ResumeLayout();
	}
	#endregion


	#region Interface Functions
	public override bool INVALID() => !this.HasReward;

	public override void LoadData(BaseRecord record)
	{
		this.RewardPages?.Clear();
		this.RewardPageLink?.Clear();

		var Item = record as Item;

		this.Title = (Item is Grocery grocery && string.IsNullOrEmpty(grocery.UnsealAcquireItem1)) ?
		   "UI.ItemTooltip.RandomboxPreview.Title".GetText() :
		   "UI.ItemTooltip.Decompose.Title".GetText();


		this.RewardPages = RewardPage.LoadFrom(new DecomposeInfo(Item));
		this.CreateControl();
	}


	internal void RewardSelected(RewardPage e)
	{
		if (this.ParentForm is not ItemTooltipPanel parent) 
			return;


		parent.MainInfo.RemoveAll(Info => Info.Tag == "RewardPreview");
		if (e.HasOpenItem2)
		{
			var param = new ContentParams();
			param[2] = FileCache.Data.Item[e.OpenItem2.Item];
			param[3] = e.OpenItem2.StackCount;

			parent.MainInfo.Insert(0, new MyInfo(
				param.Handle(e.OpenItem2.StackCount > 1 ?
				"UI.ItemTooltip.DecomposeByItem2.ConsumtItem.Count" :
				"UI.ItemTooltip.DecomposeByItem2.ConsumtItem"),
				"RewardPreview"));
		}


		var AssuredCount = e.Attrs["selected-item-assured-count"].ToInt();
		if (AssuredCount != 0)
		{
			parent.MainInfo.Add(new(
				new ContentParams(null, AssuredCount).Handle("UI.ItemTooltip.ChoiceReward"), "RewardPreview"));
		}

		parent.Refresh();
	}
	#endregion
}