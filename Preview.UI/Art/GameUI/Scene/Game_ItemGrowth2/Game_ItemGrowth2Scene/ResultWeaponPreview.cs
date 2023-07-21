using System.ComponentModel;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Record;
using Xylia.Preview.UI.Custom.Controls;
using Xylia.Preview.UI.Custom.Controls.Designer;

namespace Xylia.Preview.GameUI.Scene.Game_ItemGrowth2;

[Designer(typeof(FixedHeightDesigner))]
[DesignTimeVisible(false)]
public partial class ResultWeaponPreview : UserControl
{
	#region Constructor
	public ResultWeaponPreview() => InitializeComponent();
	#endregion

	#region Events & Delegates
	public delegate void ResultItemChangedHandle(ResultItemChangedEventArgs e);
	public event ResultItemChangedHandle ResultItemChanged;
	#endregion


	#region Functions (UI)
	/// <summary>
	/// 单页最大目标数量
	/// </summary>
	const byte MaxCellNum = 5;

	private List<FeedItemIconCell> Items;

	private byte Index = 0;

	private byte RemainCount => (byte)(Items is null ? 0 : Items.Count - MaxCellNum - Index);

	private void Btn_Prev_Click(object sender, EventArgs e)
	{
		if (Index == 0) return;

		Index--;
		ShowItems();
	}

	private void Btn_Next_Click(object sender, EventArgs e)
	{
		if (RemainCount == 0) return;

		Index++;
		ShowItems();
	}

	private void ShowItems()
	{
		this.Controls.Remove<FeedItemIconCell>();
		this.Controls.Remove<ItemNamePanel>();

		ToolTip.SetToolTip(this.Btn_Prev, "+" + Index);
		ToolTip.SetToolTip(this.Btn_Next, "+" + RemainCount);


		int LocX = 35;
		foreach (var o in Items.Skip(Index).Take(MaxCellNum))
		{
			this.Controls.Add(o);
			this.Controls.Add(o.BindName);

			o.Location = new Point(LocX, 0);
			LocX = o.Right;

#pragma warning disable CS0612
			o.BindName.MaximumSize = new Size(o.Width, int.MaxValue);
			o.BindName.Location = new Point(o.Left + (o.Width - o.BindName.Width) / 2, o.Bottom + 5);
#pragma warning restore CS0612
		}
	}
	#endregion

	#region Functions
	private void SetData(Action<string> action, params string[] NextItem)
	{
		this.Items = new();
		foreach (var TitleItem in NextItem)
		{
			#region cell
			var ItemInfo = FileCache.Data.Item[TitleItem];
			if (ItemInfo is null) continue;

			var cell = new FeedItemIconCell()
			{
				Size = new Size(82, 90),

				ShowStackCount = false,
				ObjectRef = ItemInfo,
				Image = ItemInfo.IconExtra(),
				BindName = new ItemNamePanel()
				{
					Text = ItemInfo.Name2,
					ItemGrade = ItemInfo.ItemGrade,
				},
			};
			this.Items.Add(cell);
			#endregion

			#region Delegate & Event
			cell.Click += new EventHandler((s, e) =>
			{
				//将其他对象的选择状态取消
				this.Items.ForEach(c =>
				{
					c.ShowFrameImage = false;
					c.Refresh();
				});

				cell.ShowFrameImage = true;
				cell.Refresh();

				action(TitleItem);
			});
			#endregion
		}


		this.Btn_Prev.Visible = this.Btn_Next.Visible = this.Items.Count > MaxCellNum;
		this.ShowItems();
		this.Items.FirstOrDefault()?.CallEvent("OnClick");
	}


	public void SetData(string ResultItem) => this.SetData(item => { }, ResultItem);

	public void SetData(IEnumerable<ItemTransformRecipe> Recipes)
	{
		var PreviewResult = Recipes.Select(r => r.TitleItem).Distinct();
		this.SetData(item => this.ResultItemChanged?.Invoke(new ResultItemChangedEventArgs(Recipes.Where(o => item == o.TitleItem))), 
			PreviewResult.ToArray());
	}
	#endregion
}



/// <summary>
/// 目标物品变更
/// </summary>
public class ResultItemChangedEventArgs : EventArgs
{
	public ResultItemChangedEventArgs() { }


	public IEnumerable<ItemTransformRecipe> Recipes { get; set; }
	public ResultItemChangedEventArgs(IEnumerable<ItemTransformRecipe> Recipes) => this.Recipes = Recipes;


	public ItemImprove ItemImprove;
	public ResultItemChangedEventArgs(ItemImprove ItemImprove) => this.ItemImprove = ItemImprove;


	public ItemSpirit ItemSpirit;
	public ResultItemChangedEventArgs(ItemSpirit ItemSpirit) => this.ItemSpirit = ItemSpirit;
}