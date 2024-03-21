﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Interactivity;
using HandyControl.Tools.Extension;
using Xylia.Preview.Data.Engine.Definitions;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Controls;

[TemplatePart(Name = "PART_ItemsControl", Type = typeof(PropertyItemsControl))]
[TemplatePart(Name = "PART_SearchBar", Type = typeof(SearchBar))]
public class AttributeGrid : Control
{
	private PropertyItemsControl? _itemsControl;
	private ICollectionView? _dataView;
	private SearchBar? _searchBar;
	private string? _searchKey;

	public AttributeGrid()
	{
		PropertyResolver = new();

		// register sort mode
		CommandBindings.Add(new CommandBinding(ControlCommands.SortByCategory, SortByOffset));
		CommandBindings.Add(new CommandBinding(ControlCommands.SortByName, SortByName));
	}

	private AttributeResolver PropertyResolver { get; }

	public static readonly DependencyProperty SelectedObjectProperty = DependencyProperty.Register(
		nameof(SelectedObject), typeof(Record), typeof(AttributeGrid), new PropertyMetadata(default, OnSelectedObjectChanged));

	private static void OnSelectedObjectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var ctl = (AttributeGrid)d;
		ctl.OnSelectedObjectChanged(e.OldValue, e.NewValue);
	}

	public Record SelectedObject
	{
		get => (Record)GetValue(SelectedObjectProperty);
		set => SetValue(SelectedObjectProperty, value);
	}

	protected virtual void OnSelectedObjectChanged(object oldValue, object newValue)
	{
		UpdateItems(newValue);
	}


	public static readonly DependencyProperty MaxTitleWidthProperty = DependencyProperty.Register(
	 nameof(MaxTitleWidth), typeof(double), typeof(PropertyGrid), new PropertyMetadata(double.NegativeZero));

	public double MaxTitleWidth
	{
		get => (double)GetValue(MaxTitleWidthProperty);
		set => SetValue(MaxTitleWidthProperty, value);
	}

	public static readonly DependencyProperty MinTitleWidthProperty = DependencyProperty.Register(
		nameof(MinTitleWidth), typeof(double), typeof(PropertyGrid), new PropertyMetadata(double.NegativeZero));

	public double MinTitleWidth
	{
		get => (double)GetValue(MinTitleWidthProperty);
		set => SetValue(MinTitleWidthProperty, value);
	}

	public override void OnApplyTemplate()
	{
		if (_searchBar != null)
		{
			_searchBar.SearchStarted -= SearchBar_SearchStarted;
		}

		base.OnApplyTemplate();

		_itemsControl = GetTemplateChild("PART_ItemsControl") as PropertyItemsControl;
		_searchBar = GetTemplateChild("PART_SearchBar") as SearchBar;

		if (_searchBar != null)
		{
			_searchBar.SearchStarted += SearchBar_SearchStarted;
		}

		UpdateItems(SelectedObject);
	}

	private void UpdateItems(object obj)
	{
		if (obj == null || _itemsControl == null) return;
		if (obj is not Record record) return;

		_dataView = CollectionViewSource.GetDefaultView(record.Definition.ExpandedAttributes
			.Where(PropertyResolver.ResolveIsBrowsable)
			.Select(CreatePropertyItem)
			.Do(item => item.InitElement()));

		SortByOffset(null, null);
		_itemsControl.ItemsSource = _dataView;
	}

	private void SortByOffset(object sender, ExecutedRoutedEventArgs e)
	{
		if (_dataView == null) return;

		using (_dataView.DeferRefresh())
		{
			_dataView.GroupDescriptions.Clear();
			_dataView.SortDescriptions.Clear();
			_dataView.SortDescriptions.Add(new SortDescription(PropertyItem.TagProperty.Name, ListSortDirection.Ascending));
			_dataView.SortDescriptions.Add(new SortDescription(PropertyItem.PropertyNameProperty.Name, ListSortDirection.Ascending));
		}
	}

	private void SortByName(object sender, ExecutedRoutedEventArgs e)
	{
		if (_dataView == null) return;

		using (_dataView.DeferRefresh())
		{
			_dataView.GroupDescriptions.Clear();
			_dataView.SortDescriptions.Clear();
			_dataView.SortDescriptions.Add(new SortDescription(PropertyItem.CategoryProperty.Name, ListSortDirection.Ascending));
			_dataView.SortDescriptions.Add(new SortDescription(PropertyItem.DisplayNameProperty.Name, ListSortDirection.Ascending));
			_dataView.GroupDescriptions.Add(new PropertyGroupDescription(PropertyItem.CategoryProperty.Name));
		}
	}

	private void SearchBar_SearchStarted(object? sender, FunctionEventArgs<string> e)
	{
		if (_dataView == null) return;

		_searchKey = e.Info;
		if (string.IsNullOrEmpty(_searchKey))
		{
			foreach (UIElement item in _dataView)
			{
				item.Show();
			}
		}
		else
		{
			foreach (PropertyItem item in _dataView)
			{
				item.Show(item.PropertyName.ToLower().Contains(_searchKey) || item.DisplayName.ToLower().Contains(_searchKey));
			}
		}
	}

	protected virtual PropertyItem CreatePropertyItem(AttributeDefinition attribute) => new()
	{
		Category = PropertyResolver.ResolveCategory(attribute),
		PropertyName = $"[{attribute.Name}]",
		Value = SelectedObject.Attributes,
		DisplayName = PropertyResolver.ResolveDisplayName(attribute),
		Description = PropertyResolver.ResolveDescription(attribute),
		DefaultValue = PropertyResolver.ResolveDefaultValue(attribute),
		Editor = PropertyResolver.ResolveEditor(attribute),
		IsReadOnly = PropertyResolver.ResolveIsReadOnly(attribute),
		Tag = attribute.Offset,
	};

	protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
	{
		base.OnRenderSizeChanged(sizeInfo);
		TitleElement.SetTitleWidth(this, new GridLength(Math.Max(MinTitleWidth, Math.Min(MaxTitleWidth, ActualWidth / 3))));
	}
}