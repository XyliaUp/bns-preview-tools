using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Xylia.Extension;
using Xylia.Preview.Resources;

namespace Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel.Cell
{
	/// <summary>
	/// 奖励单元
	/// </summary>
	[DesignTimeVisible(false)]
	public partial class RewardCell : UserControl, IComparable<RewardCell>
	{
		#region 类型声明
		/// <summary>
		/// 组信息
		/// </summary>
		public enum CellGroup
		{
			Fixed,
			Selected,
			Random,

			g1,
			g2,

			rare,

			g3,
			g4,
			g5,
		}
		#endregion

		#region Constructor
		public RewardCell()
		{
			InitializeComponent();
		}
		#endregion



		#region Fields
		public bool IsJobReward;

		/// <summary>
		/// 物品名称, 用于显示切换额外信息
		/// </summary>
		private string RealName { get; set; }

		public int CellIdx { get; set; }

		[Category("Data"), Description("额外信息")]
		public string ItemExtra { get; set; }

		[Category("Data"), Description("显示额外信息")]
		public bool ShowExtra { get; set; }


		private CellGroup m_Group;

		[Category("Data"), Description("奖励分组")]
		public CellGroup Group
		{
			get => this.m_Group;
			set
			{
				this.m_Group = value;

				if (value == CellGroup.Fixed)
				{
					this.m_Type.Visible = false;
					return;
				}

				this.m_Type.Visible = true;

				if (value == CellGroup.Selected) this.m_Type.Image = Resource_BNSR.Tag_199.Thumbnail(0.9);
				else this.m_Type.Image = Resource_BNSR.Tag_090.Thumbnail(0.9);
			}
		}
		#endregion

		public int CompareTo(RewardCell other)
		{
			#region Fixed 组
			if (this.Group == CellGroup.Fixed && other.Group != CellGroup.Fixed) return -1;
			else if (other.Group == CellGroup.Fixed && this.Group != CellGroup.Fixed) return 1;
			#endregion

			#region Selected 组
			else if (this.Group == CellGroup.Selected && other.Group != CellGroup.Selected) return -1;
			else if (other.Group == CellGroup.Selected && this.Group != CellGroup.Selected) return 1;
			#endregion


			#region 同类型道具再按品质排序
			var GradeA = this.ItemGrade;
			var GradeB = other.ItemGrade;

			if (GradeA == GradeB) return this.CellIdx - other.CellIdx;
			return GradeA - GradeB;
			#endregion
		}






		#region 数量
		private int m_countmin;
		private int m_countmax;

		[Category("Data"), Description("最小数量")]
		public int Count_Min
		{
			get => m_countmin;
			set
			{
				m_countmin = value;
				RenderCount();
			}
		}

		[Category("Data"), Description("最大数量")]
		public int Count_Max
		{
			get => m_countmax;
			set
			{
				m_countmax = value;
				RenderCount();
			}
		}
		#endregion


		#region 物品信息
		/// <summary>
		/// 物品别名
		/// </summary>
		public string ItemAlias { get; set; }

		[Category("Data"), Description("物品名称")]
		public string ItemName
		{
			get => this.RealName;
			set => this.ItemShow.ItemName = this.RealName = value;
		}

		[Category("Data"), Description("物品品质")]
		public byte ItemGrade
		{
			get => this.ItemShow.ItemGrade;
			set => this.ItemShow.ItemGrade = value;
		}


		/// <summary>
		/// 物品实例化
		/// </summary>
		public void ItemInstace()
		{
			var ItemData = FileCache.Data.Item[ItemAlias];
			if (ItemData != null)
			{
				this.ItemShow.ItemData = ItemData;
				this.ItemShow.ItemIcon = ItemData.FrontIcon;
				this.ItemGrade = ItemData.ItemGrade;
				this.ItemName = ItemData.Name2;
			}


			this.Refresh();
		}
		#endregion

		#region 界面处理
		/// <summary>
		/// 要求界面重绘
		/// </summary>
		public override void Refresh()
		{
			if (!this.Visible) return;

			#region 是否显示额外信息
			if (this.ShowExtra && this.ItemExtra != null) this.ItemShow.ItemName = $"[{this.ItemExtra}] {this.RealName}";
			else this.ItemShow.ItemName = this.RealName;
			#endregion

			this.lbl_Count.Location = new Point(this.ItemShow.Width, this.lbl_Count.Location.Y);
			if (this.m_Type.Image != null)
			{
				this.m_Type.Location = new Point
					   (this.lbl_Count.Location.X + (this.lbl_Count.Visible ? this.lbl_Count.Width : 0) + 5,
					   (this.ItemShow.Height - this.m_Type.Height + 10) / 2);
			}


			base.Refresh();
		}

		/// <summary>
		/// 渲染数量显示
		/// </summary>
		public void RenderCount()
		{
			if (Count_Min == 0 && Count_Max == 0)
			{
				this.lbl_Count.Visible = false;
				return;
			}



			this.lbl_Count.Visible = true;

			//显示最大数量
			if ((Count_Min == 0 && Count_Max != 0) || (Count_Min == Count_Max && Count_Min != 0))
			{
				if (Group == CellGroup.Fixed && Count_Max == 1) this.lbl_Count.Visible = false;
				else this.lbl_Count.Text = Count_Max + "个";
			}
			//显示最小数量
			else if (Count_Min != 0 && Count_Max == 0)
			{
				this.lbl_Count.Text = Count_Min + "个";
			}
			//显示波动数量
			else if (Count_Min != 0 && Count_Max != 0)
			{
				this.lbl_Count.Text = Count_Min + "~" + Count_Max + "个";
			}
		}

		private void RewardCell_Load(object sender, EventArgs e)
		{
			//Load会比Constructor函数慢
			this.Refresh();
		}
		#endregion
	}
}