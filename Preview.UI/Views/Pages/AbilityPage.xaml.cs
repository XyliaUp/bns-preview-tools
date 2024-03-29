﻿using System.Windows;
using System.Windows.Controls;

using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

using Xylia.Preview.Data.Models.Creature;
using Xylia.Preview.UI.ViewModels;

namespace Xylia.Preview.UI.Views.Pages;
public partial class AbilityPage : Page
{
	private AbilityViewModel _viewModel;
	const sbyte DEFAULT_LEVEL = 60;

	public AbilityPage()
	{
		DataContext = _viewModel = new AbilityViewModel();
		InitializeComponent();
	}

	private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
	{
		if (e.NewValue is not AbilityFunction ability) return;

		_viewModel.Selected = ability;
		LevelText.IsEnabled = ability.Φ != 0;
		LevelText.Value = DEFAULT_LEVEL;

		#region Chart	
		int CHART_MAX_VALUE = 20000;
		int CHART_INTERVAL = 500;

		var values = new ChartValues<ObservablePoint>();
		for (int i = 0; i <= CHART_MAX_VALUE; i += CHART_INTERVAL)
			values.Add(new(i, ability.GetPercent(i, DEFAULT_LEVEL)));

		Chart.Series = new SeriesCollection
		{
			new LineSeries
			{
				Title = $"{ability.Type} converted percent in Lv{DEFAULT_LEVEL}",
				Values = values,
				LineSmoothness = 1,
			}
		};
		#endregion
	}
}