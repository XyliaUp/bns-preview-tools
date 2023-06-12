using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls;
using Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell;
using Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Preview.Reward;
using Xylia.Preview.Resources;


namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel
{
	/// <summary>
	/// 奖励预览控件
	/// </summary>
	public partial class RewardPreview : TitlePanel
	{
		#region Constructor
		public RewardPreview()
		{
			InitializeComponent();

			this.AutoSize = true;
		}

		public RewardPreview(string Title) : this()
		{
			this.Title = Title;
		}
		#endregion

		#region Delegate & Event
		public delegate void RewardChangedHandle(object sender, RewardChangedEvent e);

		public event RewardChangedHandle SelRewardChanged;
		#endregion

		#region Fields
		/// <summary>
		/// 是否有奖励
		/// </summary>
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
		/// <summary>
		/// 奖励分页数据集合
		/// </summary>
		private List<RewardPage> RewardPages;

		/// <summary>
		/// 记录分页对应的奖励控件
		/// </summary>
		private Dictionary<Control, RewardPage> RewardPageLink = new();

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
		/// <summary>
		/// 创建界面
		/// </summary>
		public new void CreateControl()
		{
			#region Initialize
			if (!HasReward) return;

			bool ShowPicBox = false;      //显示道具图标
			this.ItemIconCells = new();   //图片按键列表

			bool OnlyOnePage = this.RewardPages.Count == 1;  //指示是否只有一个开启内容
			#endregion


			#region 遍历奖励分页
			foreach (var page in this.RewardPages)
			{
				//Initialize
				int LoY = 21;

				#region 处理 RewardCell 
				foreach (var c in page.RewardInfo.Preview)
				{
					c.Location = new Point(5, LoY);

					if (page.RewardInfo is DecomposeJobRewardInfo)
						c.IsJobReward = true;

					this.ExecuteExtra(c);
					LoY += c.Height;

					//有效奖励组数量为1时, 直接添加
					if (OnlyOnePage) this.Controls.Add(c);
				}
				#endregion

				#region 当存在多个分页或者开启所需道具不为空时显示 PictureBox
				if (!OnlyOnePage || page.HasOpenItem2)
				{
					ShowPicBox = true;

					var OpenItem2 = FileCache.Data.Item[page.OpenItem2?.Item];

					#region 控件处理
					Bitmap Icon = OpenItem2?.Icon();

					//图片按键
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

					//有效奖励组数量为1时, 不需要处理点击Event
					if (!OnlyOnePage)
					{
						//绑定点击Event
						IconBtn.Click += (s, e) => this.SelectReward((ItemIconCell)s);
						IconBtn.BringToFront();

						this.Controls.Add(IconBtn);

						this.ItemIconCells.Add(IconBtn);
						this.RewardPageLink.Add(IconBtn, page);
					}
				}
				#endregion
			}
			#endregion


			//排序奖励分页,处理显示位置
			if (ShowPicBox) this.RewardPreview_SizeChanged(null, null);


			#region 仅有单页时, 直接触发点击Event
			if (!OnlyOnePage) this.SelectReward(this.ItemIconCells.First());
			else SelRewardChanged?.Invoke(null, new RewardChangedEvent(this.RewardPages.First()));
			#endregion
		}

		/// <summary>
		/// 处理显示位置
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RewardPreview_SizeChanged(object sender, EventArgs e)
		{
			if (this.ItemIconCells != null)
			{
				int StartX = this.Width - 30;
				int PicPadding = 3;

				//变动生成位置
				int DiffVal = 0;

				//计算最大差值
				DiffVal = PicScale * this.ItemIconCells.Count + PicPadding * (this.ItemIconCells.Count - 1);

				this.ItemIconCells.ForEach(box =>
				{
					//减去常量是因为Width会多出一部分
					box.Location = new Point(StartX - DiffVal, 8);

					DiffVal -= PicScale + PicPadding;
				});
			}
		}

		/// <summary>
		/// 选择奖励内容
		/// </summary>
		/// <param name="CurPic"></param>
		private void SelectReward(ItemIconCell CurPic)
		{
			#region Initialize
			this.SuspendLayout();

			//如果选择相同奖励, 直接返回
			if (this.SelPicBox != null && CurPic == this.SelPicBox) return;
			else this.SelPicBox = CurPic;

			//清除目前的控件
			this.Controls.OfType<RewardCell>().ForEach(r => this.Controls.Remove(r));


			//将原来的选择图框颜色恢复
			if (this.SelPicBox != null && this.SelPicBox.Image is null) this.SelPicBox.Image = IniImage;

			//变更选择后的颜色
			if (CurPic is null) throw new Exception("CurPic点击Event中发生控件为空错误");
			else if (CurPic.Image is null) CurPic.Image = SelImage;
			#endregion

			#region 记录控件内容
			var SelData = this.RewardPageLink[CurPic];
			SelRewardChanged?.Invoke(CurPic, new RewardChangedEvent(SelData));

			foreach (var c in new List<RewardCell>(SelData.RewardInfo.Preview))
			{
				this.Controls.Add(c);
				c.BringToFront();

				//应用新的高度
				this.Height = Math.Max(c.Bottom, this.Height);
			}

			this.ResumeLayout();
			#endregion
		}



		/// <summary>
		/// 额外处理
		/// </summary>
		/// <param name="c"></param>
		public void ExecuteExtra(RewardCell c = null)
		{
			#region 获取处理对象
			var Cells = new List<RewardCell>();

			if (c is null) Cells.AddRange(this.RewardPages.SelectMany(page => page.RewardInfo.Preview));
			else Cells.Add(c);
			#endregion

			Cells.ForEach(cell =>
			{
				if (cell.IsJobReward && this.ShowJob) cell.ShowExtra = true;
				else if (ShowGroup) cell.ShowExtra = true;
				else if (cell.ShowExtra) cell.ShowExtra = false;

				cell.Refresh();
			});
		}
		#endregion

		#region Interface Functions
		public override bool INVALID() => !this.HasReward;

		public override void LoadData(BaseRecord record)
		{
			//Initialize
			this.RewardPages?.Clear();
			this.RewardPageLink?.Clear();

			var Item = record as Item;
			var DecomposeInfo = new DecomposeInfo(Item);
			this.RewardPages = RewardPage.GetPages(DecomposeInfo);

			this.CreateControl();
		}
		#endregion
	}
}