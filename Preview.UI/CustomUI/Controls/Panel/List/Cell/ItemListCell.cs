using System.Drawing;

using Xylia.Preview.Data.Record;

namespace Xylia.Preview.GameUI.Controls.List
{
	public partial class ItemListCell : ListCell
	{
		public ItemListCell(Item DisplayItem, Bitmap Icon = null)
		{
			InitializeComponent();

			if (DisplayItem is null) return;

			if (Icon != null) this.ItemShow.LoadData(DisplayItem, Icon);
			else this.ItemShow.LoadData(DisplayItem, true);

			//显示职业信息
			var Jobs = DisplayItem.JobInfo;
			if (Jobs != null) this.RightText = Jobs;
		}

		/// <summary>
		/// 叠加数量
		/// </summary>
		public int StoreBundleCount
		{
			get => this.ItemShow.IconCell.StackCount;
			set
			{
				this.ItemShow.IconCell.StackCount = value;

				this.ItemShow.IconCell.ShowStackCountOnlyOne = false;
				this.ItemShow.IconCell.ShowStackCount = true;
			}
		}
	}


	public class ItemData : ListData
	{
		private readonly Item DisplayItem;

		public ItemData(Item DisplayItem)
		{
		   this.DisplayItem = DisplayItem;
		}


		public ListCell GetCell() => new ItemListCell(DisplayItem);
	}
}