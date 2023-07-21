using System.ComponentModel;

using Xylia.Extension;
using Xylia.Preview.Common.Cast;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.BinData.Table.Record;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.GameUI.Scene.Game_ItemGrowth2
{
    [DesignTimeVisible(false)]
	public partial class SubIngredientPreview : Panel
	{
		#region Events & Delegates
		public delegate void RecipeChangedHandle(RecipeChangedEventArgs e);

		public event RecipeChangedHandle RecipeChanged;


		public event EmptyHandler DataLoaded;
		#endregion

		#region Functions
		private FeedItemIconCell CreateNew(BaseRecord ObjectRef, Bitmap Image, int StackCount, ref int LocX)
		{
			var ItemIcon = new FeedItemIconCell
			{
				ObjectRef = ObjectRef,
				Image = Image,
				Location = new Point(LocX, 0),
				StackCount = StackCount,
				Size = new Size(82, 90),
				ShowStackCount = true,
				ShowStackCountOnlyOne = false
			};

			ItemIcon.Click += new EventHandler((s, e) =>
			{
				var Cells = this.Controls.OfType<FeedItemIconCell>();
				if (Cells.Count() == 1) return;

				Cells.ForEach(c =>
				{
					c.ShowFrameImage = false;
					c.Refresh();
				});

				ItemIcon.ShowFrameImage = true;
				ItemIcon.Refresh();
			});

			this.Controls.Add(ItemIcon);
			LocX = ItemIcon.Right + 5;

			return ItemIcon;
		}

		private void HandleSize(int LocX)
		{
			this.Width = LocX;
			this.Height = 90;

			this.DataLoaded?.Invoke();

			//触发第一个选项 
			this.Controls.OfType<FeedItemIconCell>().FirstOrDefault()?.CallEvent("OnClick");
		}
		#endregion


		#region SetData
		public void SetData(IEnumerable<ItemTransformRecipe> ResultRecipes)
		{
			//清理资源
			this.Controls.Remove<FeedItemIconCell>();

			#region Load 控件
			int LocX = 0;
			foreach (var Recipe in ResultRecipes)
			{
				#region Get data
				var SubIngredient1 = Recipe.Attributes["sub-ingredient-1"].CastObject();
				var SubIngredientStackCount1 = Recipe.Attributes["sub-ingredient-stack-count-1"].ToShort();
				var SubIngredientConditionType1 = Recipe.Attributes["sub-ingredient-condition-type-1"].ToEnum<ConditionType>();

				string ItemAlias = null;
				Bitmap Image = null;

				if (SubIngredient1 is Item ItemInfo)
				{
					ItemAlias = ItemInfo.alias;
					Image = ItemInfo.Icon();
				}
				else if (SubIngredient1 is ItemBrand ItemBrand)
				{
					var ItemTooltip = FileCache.Data.ItemBrandTooltip[ItemBrand.Id, (byte)SubIngredientConditionType1];

					ItemAlias = ItemBrand.alias + "_" + ItemTooltip?.ItemConditionType + $" ({ ItemTooltip?.Name2.GetText() })";
					Image = ItemTooltip?.Icon();
				}
				#endregion

				this.CreateNew(SubIngredient1, Image, SubIngredientStackCount1, ref LocX).Click +=
					new((s, e) => this.RecipeChanged?.Invoke(new RecipeChangedEventArgs(Recipe)));
			}
			#endregion

			this.HandleSize(LocX);
		}

		public void SetData(ItemImprove ItemImprove)
		{
			//清理资源
			this.Controls.Remove<FeedItemIconCell>();

			#region Load
			int LocX = 0;
			for (byte idx = 1; idx <= 5; idx++)
			{
				var Item = FileCache.Data.Item[ItemImprove.Attributes["cost-main-item-" + idx]];
				if (Item is null) break;
				var Count = ItemImprove.Attributes["cost-main-item-count-" + idx].ToShort();
				
				
				byte CurIdx = idx;
				this.CreateNew(Item, Item.Icon(), Count, ref LocX).Click +=
					new((s, e) => this.RecipeChanged?.Invoke(new RecipeChangedEventArgs(ItemImprove, CurIdx)));
			}
			#endregion

			this.HandleSize(LocX);
		}

		public void SetData(ItemImproveOptionList ItemImproveOptionList)
		{
			//清理资源
			this.Controls.Remove<FeedItemIconCell>();

			#region Load
			int LocX = 0;
			for (byte idx = 1; idx <= 4; idx++)
			{
				var DrawCostMainItem = FileCache.Data.Item[ItemImproveOptionList.Attributes["draw-cost-main-item-" + idx]];
				if (DrawCostMainItem is null) break;

				var DrawCostMainItemCount = ItemImproveOptionList.Attributes["draw-cost-main-item-count-" + idx].ToShort();

				//绑定Event
				byte CurIdx = idx;
				this.CreateNew(DrawCostMainItem, DrawCostMainItem.Icon(), DrawCostMainItemCount, ref LocX).Click +=
					new((s, e) => this.RecipeChanged?.Invoke(new RecipeChangedEventArgs(ItemImproveOptionList, CurIdx)));
			}
			#endregion

			this.HandleSize(LocX);
		}

		public void SetData(Item Item)
		{
			this.Controls.Remove<FeedItemIconCell>();

			int LocX = 0;
			this.CreateNew(Item, Item?.Icon(), 1, ref LocX);
			this.HandleSize(LocX);
		}
		#endregion
	}

	/// <summary>
	/// 成长路径变更Event
	/// </summary>
	public sealed class RecipeChangedEventArgs : EventArgs
	{
		public byte Index { get; }

		public ItemTransformRecipe ItemTransformRecipe { get; }
		public RecipeChangedEventArgs(ItemTransformRecipe ItemTransformRecipe) => this.ItemTransformRecipe = ItemTransformRecipe;

		public ItemImprove ItemImprove { get; }
		public RecipeChangedEventArgs(ItemImprove ItemImprove, byte Index)
		{
			this.ItemImprove = ItemImprove;
			this.Index = Index;
		}

		public ItemImproveOptionList ItemImproveOptionList { get; }
		public RecipeChangedEventArgs(ItemImproveOptionList ItemImproveOptionList, byte Index)
		{
			this.ItemImproveOptionList = ItemImproveOptionList;
			this.Index = Index;
		}
	}
}