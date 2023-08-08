using System.ComponentModel;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Record;
using Xylia.Preview.UI.Custom.Controls;
using Xylia.Preview.UI.Custom.Controls.Currency;
using Xylia.Preview.UI.Extension;
using Xylia.Preview.UI.Resources;

namespace Xylia.Preview.GameUI.Scene.Game_ItemStore;

[DesignTimeVisible(false)]
public partial class BuyPriceCell : Panel
{
	#region Constructor
	public BuyPriceCell() => InitializeComponent();
	#endregion

	#region Fields
	/// <summary>
	/// 图片规格
	/// </summary>
	private new int Scale { get; set; } = 28;
	#endregion


	#region Functions
	public void LoadData(ItemBuyPrice ItemBuyPrice)
	{
		#region items
		var PriceCtls = new List<Control>();
		PriceCtls.AddItem(CretePriceCell(CurrencyType.FactionScore, ItemBuyPrice.RequiredFactionScore));
		PriceCtls.AddItem(CretePriceCell(CurrencyType.DuelPoint, ItemBuyPrice.RequiredDuelPoint));
		PriceCtls.AddItem(CretePriceCell(CurrencyType.PartyBattlePoint, ItemBuyPrice.RequiredPartyBattlePoint));
		PriceCtls.AddItem(CretePriceCell(CurrencyType.Pearl, ItemBuyPrice.RequiredLifeContentsPoint));
		PriceCtls.AddItem(CretePriceCell(CurrencyType.FieldPlayPoint, ItemBuyPrice.RequiredFieldPlayPoint));
		PriceCtls.AddItem(CretePriceCell(CurrencyType.Money, ItemBuyPrice.Money));


		var ItemCtls = new List<ItemIconCell>();
		if (ItemBuyPrice.RequiredItembrand != null)
			ItemCtls.Add(this.CreteItemBrandCell(ItemBuyPrice.RequiredItembrand, ItemBuyPrice.RequiredItembrandConditionType));

		Linq.For(ItemBuyPrice.RequiredItem.Length, (i) =>
			ItemCtls.AddItem(this.CreteItemCell(ItemBuyPrice.RequiredItem[i], ItemBuyPrice.RequiredItemCount[i])));
		#endregion



		#region UI
		var width1 = PriceCtls.Any() ? PriceCtls.Max(o => o.Width) : 0;
		var width2 = ItemCtls.Any() ? ItemCtls.Sum(o => o.Scale + 4) : 0;
		this.Width = Math.Max(width1, width2);

		int x = 0, y = 0;
		foreach (var o in PriceCtls)
		{
			this.Controls.Add(o);

			o.Location = new Point(this.Width - o.Width, y);
			y += o.Height;
		}

		// items
		if (width2 != 0)
		{
			y += 5;
			x = this.Width - width2;
			foreach (ItemIconCell o in ItemCtls)
			{
				this.Controls.Add(o);

				o.Location = new Point(x, y - ((o.Scale - this.Scale) / 2));
				x += o.Scale + 4;
			}

			y += this.Scale + 5;
		}


		this.Height = y;
		#endregion
	}



	private static Control CretePriceCell(CurrencyType Type, int CurrencyCount)
	{
		if (CurrencyCount == 0) return null;

		var PriceCell = new PriceCell()
		{
			CurrencyCount = CurrencyCount,
			CurrencyType = Type,
			AutoSize = false,
		};

		PriceCell.Refresh();

		return PriceCell;
	}

	private ItemIconCell CreteItemBrandCell(ItemBrand ItemBrand, ConditionType ConditionType)
	{
		var ItemBrandTooltip = FileCache.Data.ItemBrandTooltip[ItemBrand.Id, (byte)ConditionType];

		int ExtraScale = 10;
		var FrameIconCell = new ItemIconCell()
		{
			Scale = this.Scale + ExtraScale,
			FrameImage = Resource_Common.FrameImg1,
			Image = ItemBrandTooltip?.Icon(),
			ShowStackCount = false,
		};

		FrameIconCell.SetToolTip(ItemBrandTooltip?.Name2.GetText());
		return FrameIconCell;
	}

	private ItemIconCell CreteItemCell(Item Item, short Count)
	{
		if (Item is null) return null;
		var ItemIconCell = new ItemIconCell()
		{
			Scale = this.Scale,
			Image = Item.IconExtra(),

			StackCount = Count,
			ShowStackCount = true,
			ObjectRef = Item,
		};

		ItemIconCell.SetToolTip(Item.Name2 + (Count == 1 ? null : $" -{Count}"));
		return ItemIconCell;
	}
	#endregion
}