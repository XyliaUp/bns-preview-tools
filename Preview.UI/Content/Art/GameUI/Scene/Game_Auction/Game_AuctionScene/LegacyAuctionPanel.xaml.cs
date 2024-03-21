using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Helpers;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;
using Xylia.Preview.Data.Models.Sequence;
using Xylia.Preview.UI.Controls;

namespace Xylia.Preview.UI.GameUI.Scene.Game_Auction;
public partial class LegacyAuctionPanel  : INotifyPropertyChanged
{
	#region Constructor
	public LegacyAuctionPanel()
	{
		InitializeComponent();
		DataContext = this;

		#region Category
		TreeView.Items.Add(new TreeViewImageItem() { Header = "UI.Market.Category.All".GetText(), Tag = "all" });
		TreeView.Items.Add(new TreeViewImageItem() { Header = "UI.Market.Category.Favorites".GetText(), Tag = "favorites" });

		foreach (var category2 in SequenceExtensions.MarketCategory2Group())
		{
			if (category2.Key == MarketCategory2Seq.None) continue;

			var node = new TreeViewItem() { Tag = category2, Header = category2.Key.GetText() };
			TreeView.Items.Add(node);

			foreach (var category3 in category2.Value)
			{
				node.Items.Add(new TreeViewItem()
				{
					Tag = category3,
					Header = category3.GetText(),
				});
			}
		}
		#endregion

		#region Source
		source = CollectionViewSource.GetDefaultView(FileCache.Data.Provider.GetTable<Item>());
		source.Filter = Filter;

		ItemList.ItemsSource = source;
		#endregion
	}
	#endregion

	#region Methods
	private void Comapre_Checked(object sender, RoutedEventArgs e)
	{
		var dialog = new Microsoft.Win32.OpenFileDialog() { Filter = @"|*.chv|All files|*.*" };
		Lst = dialog.ShowDialog() == true ? new HashList(dialog.FileName) : null;
	}

	private void Comapre_Unchecked(object sender, RoutedEventArgs e) => Lst = null;

	private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
	{
		if (e.OldValue == e.NewValue) return;
		if (e.NewValue is not FrameworkElement item) return;

		if (item.Tag is MarketCategory2Seq seq2)
		{
			marketCategory2 = seq2;
		}
		else if (item.Tag is MarketCategory3Seq seq3)
		{
			marketCategory3 = seq3;
		}
		else
		{
			marketCategory2 = default;
			marketCategory3 = default;
		}

		RefreshList();
	}
	#endregion

	#region Fields
	private readonly ICollectionView source;

	private string? _nameFilter;
	public string? NameFilter
	{
		get => _nameFilter;
		set
		{
			SetProperty(ref _nameFilter, value);
			RefreshList();
		}
	}


	private bool _auctionable;
	public bool Auctionable
	{
		get => _auctionable;
		set
		{
			SetProperty(ref _auctionable, value);
			RefreshList();
		}
	}


	private HashList? _lst;
	public HashList? Lst
	{
		get => _lst;
		set
		{
			SetProperty(ref _lst, value);
			RefreshList();
		}
	}


	private MarketCategory2Seq marketCategory2;
	private MarketCategory3Seq marketCategory3;

	private bool Filter(object obj)
	{
		if (obj is Record record) { }
		else if (obj is ModelElement model) record = model.Source;
		else return false;

		if (_lst != null && _lst.CheckFailed(record.PrimaryKey)) return false;

		var IsAll = marketCategory2 == default && marketCategory3 == default;
		var IsEmpty = string.IsNullOrEmpty(_nameFilter);

		// must set rule
		if (IsAll)
		{
			if (_lst is null && IsEmpty) return false;
		}
		else
		{
			var MarketCategory2 = record.Attributes["market-category-2"].ToEnum<MarketCategory2Seq>();
			var MarketCategory3 = record.Attributes["market-category-3"].ToEnum<MarketCategory3Seq>();

			if (marketCategory3 != default && marketCategory3 != MarketCategory3) return false;
			else if (marketCategory2 != default && marketCategory2 != MarketCategory2) return false;
		}


		// filter 
		if (_auctionable &&
			!record.Attributes.Get<BnsBoolean>("auctionable") &&
			!record.Attributes.Get<BnsBoolean>("seal-renewal-auctionable")) return false;

		// filter rule
		if (IsEmpty) return true;
		else
		{
			if (int.TryParse(_nameFilter, out int id)) return record.PrimaryKey.Id == id;

			var alias = record.Attributes.Get<string>("alias");
			if (alias != null && alias.Contains(_nameFilter, StringComparison.OrdinalIgnoreCase)) return true;

			var name = record.Attributes.Get<Record>("name2").GetText();
			if (name != null && name.Contains(_nameFilter, StringComparison.OrdinalIgnoreCase)) return true;

			return false;
		}
	}

	private void RefreshList()
	{
		Dispatcher.BeginInvoke(() =>
		{
			source.Refresh();
			source.MoveCurrentToFirst();

			ItemList.ScrollIntoView(source.CurrentItem);
		});
	}
	#endregion


	#region	PropertyChange

	public event PropertyChangedEventHandler PropertyChanged;

	protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
	{
		if (EqualityComparer<T>.Default.Equals(storage, value))
			return false;

		storage = value;
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		return true;
	}
	#endregion
}