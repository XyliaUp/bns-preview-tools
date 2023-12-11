using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Controls;
using Xylia.Preview.UI.Helpers.Output;

namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_Auction;
public partial class Game_AuctionScene 
{
	#region Constructor
	public Game_AuctionScene()
	{
		InitializeComponent();

		#region Category
		TreeView.Items.Add(new TreeViewImageItem() { Header = "UI.Market.Category.All".GetText(), Tag = "all" });
		TreeView.Items.Add(new TreeViewImageItem() { Header = "UI.Market.Category.Favorites".GetText(), Tag = "favorites" });

		foreach (var category2 in Item.MarketCategory2Group())
		{
			if (category2.Key == Item.MarketCategory2Seq.None) continue;

			var node = new TreeViewItem() { Tag = category2, Header = $"Name.item.game-category-2.{category2.Key.GetName()}".GetText() };
			TreeView.Items.Add(node);

			foreach (var category3 in category2.Value)
			{
				node.Items.Add(new TreeViewItem()
				{
					Tag = category3,
					Header = $"Name.item.game-category-3.{category3.GetName()}".GetText() ?? category3.ToString(),
				});
			}
		}
		#endregion

		#region Source
		source = new ListCollectionView(FileCache.Data.Item.Records) { Filter = (x) => Filter(x as Record) };
		ItemList.ItemsSource = source;
		#endregion
	}
	#endregion


	#region Fields
	private readonly ListCollectionView source;

	private string _nameFilter;
	public string NameFilter
	{
		get => _nameFilter;
		set
		{
			SetProperty(ref _nameFilter, value);
			source.Refresh();
		}
	}


	private bool _auctionable;
	public bool Auctionable
	{
		get => _auctionable;
		set
		{
			SetProperty(ref _auctionable, value);
			source.Refresh();
		}
	}


	private HashSet<int> _lst;
	public HashSet<int> Lst
	{
		get => _lst;
		set
		{
			SetProperty(ref _lst, value);
			source.Refresh();
		}
	}


	private Item.MarketCategory2Seq marketCategory2;
	private Item.MarketCategory3Seq marketCategory3;


	private bool Filter(Record record)
	{
		if (_lst != null && _lst.Contains(record.RecordId)) return false;

		var IsAll = marketCategory2 == default && marketCategory3 == default;
		var IsEmpty = string.IsNullOrEmpty(_nameFilter);

		// must set rule
		if (IsAll)
		{
			if (_lst is null && IsEmpty) return false;
		}
		else
		{
			var MarketCategory2 = record.Attributes["market-category-2"].ToEnum<Item.MarketCategory2Seq>();
			var MarketCategory3 = record.Attributes["market-category-3"].ToEnum<Item.MarketCategory3Seq>();

			if (marketCategory3 != default && marketCategory3 != MarketCategory3) return false;
			else if (marketCategory2 != default && marketCategory2 != MarketCategory2) return false;
		}


		// filter 
		if (_auctionable &&
			!record.Attributes.Get<bool>("auctionable") &&
			!record.Attributes.Get<bool>("seal-renewal-auctionable")) return false;

		// filter rule
		if (IsEmpty) return true;
		else
		{
			if(int.TryParse(_nameFilter , out int id)) return record.RecordId == id;


			var alias = record.Attributes["alias"];
			if (alias != null && alias.IndexOf(_nameFilter, StringComparison.OrdinalIgnoreCase) > 0) return true;

			var name = record.Attributes.Get<Record>("name2")?.Attributes["text"];
			if (name != null && name.IndexOf(_nameFilter, StringComparison.OrdinalIgnoreCase) > 0) return true;

			return false;
		}
	}
	#endregion

	#region Methods
	private void Comapre_Checked(object sender, RoutedEventArgs e) => Lst = XList.LoadData();

	private void Comapre_Unchecked(object sender, RoutedEventArgs e) => Lst = null;

	private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
	{
		if (e.OldValue == e.NewValue) return;
		if (e.NewValue is not FrameworkElement item) return;

		if (item.Tag is Item.MarketCategory2Seq seq2)
		{
			marketCategory2 = seq2;
		}
		else if (item.Tag is Item.MarketCategory3Seq seq3)
		{
			marketCategory3 = seq3;
		}
		else
		{
			marketCategory2 = default;
			marketCategory3 = default;
		}

		source.Refresh();
	}
	#endregion
}