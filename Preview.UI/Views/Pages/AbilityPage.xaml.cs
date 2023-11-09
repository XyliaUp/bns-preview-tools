using System.Reflection;
using System.Windows.Controls;

using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

using Xylia.Preview.Data.Models.Creature;

namespace Xylia.Preview.UI.Views.Pages;
public partial class AbilityPage : Page
{
	const sbyte DEFAULT_LEVEL = 60;

	public AbilityPage()
	{
		InitializeComponent();

		// ability list
		foreach (var prop in typeof(AbilityFunction).GetProperties(BindingFlags.Static | BindingFlags.Public))
		{
			if (prop.GetValue(null) is not AbilityFunction ability || ability.K == 0) continue;

			this.TreeView.Items.Add(new TreeViewItem()
			{
				Header = ability.Type,
				Tag = ability,
			});
		}
	}


	private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
	{
		if (TreeView.SelectedItem is not Control c || c.Tag is not AbilityFunction ability)
			return;

		LevelText.IsEnabled = ability.Φ != 0;
		LevelText.Text = DEFAULT_LEVEL.ToString();


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

	private void GetPercent_Click(object sender, RoutedEventArgs e)
	{
		if (TreeView.SelectedItem is not Control c || c.Tag is not AbilityFunction ability)
			return;


		if (!double.TryParse(AbilityText.Text, out double Value)) return;

		var level = sbyte.Parse(LevelText.Text);
		double extra = 0;   // double)numericUpDown1.Value * 0.01;
		double percent = ability.GetPercent(Value, level) + extra;


		ResultText.Text = $"在{level}级时所对应的 {ability.Type}率:\n{Value} ({percent:P3})";
		//if (UseCompare.Checked)
		//{
		//	double value2 = Value + AttritubeValue_Extra.Text.ToInt32();
		//	double percent2 = obj.GetPercent(value2, level) + extra;

		//	label1.Text += $"\n{value2} ({percent2:P3})\n\n差值为 {percent2 - percent:P3}";
		//}
	}
}