using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

using CUE4Parse.BNS;
using CUE4Parse.BNS.Conversion;

using SkiaSharp;

using Xylia.Preview.UI.Helpers.Output.Textures;
using Xylia.Preview.UI.ViewModels;

namespace Xylia.Preview.UI.Views.Pages;
public partial class GameResourcePage : Page
{
	#region Constructor
	GameResourcePageViewModel _viewModel;

	public GameResourcePage()
	{
		DataContext = _viewModel = new GameResourcePageViewModel();

		InitializeComponent();
		this.Loaded += Page_Loaded;
	}

	private void Page_Loaded(object sender, RoutedEventArgs e)
	{
		Reset_Click(sender, e);
	}
	#endregion


	#region Asset
	private async void Extract_Click(object sender, RoutedEventArgs e)
	{
		if (string.IsNullOrWhiteSpace(Selector.Text))
			return;

		this.Extract.IsEnabled = false;
		await UeExporter(Selector.Text, OutputClassName.IsChecked ?? true);

		this.Extract.IsEnabled = true;
	}

	private async Task UeExporter(string FilterText, bool ContainType) => await Task.Run(() =>
	{
		using var provider = new GameFileProvider(UserSettings.Default.GameFolder);
		var filter = provider.FixPath(FilterText, false) ?? FilterText;

		Parallel.ForEach(provider.Files.Values, gamefile =>
		{
			if (gamefile.Extension != "uasset" || !gamefile.Path.Contains(filter, StringComparison.OrdinalIgnoreCase))
				return;

			try
			{
				new Exporter(UserSettings.Default.OutputFolderResource)
					.Run(provider.LoadPackage(gamefile), ContainType);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		});
	});
	#endregion

	#region Icon
	private void OutputGoodIcon(object sender, RoutedEventArgs e)
	{
		_viewModel.Run(new GoodIcon(UserSettings.Default.GameFolder, _viewModel.Icon_OutputFolder + @"\Goods"), null, 1);
	}

	private void OutputItemIcon(object sender, RoutedEventArgs e)
	{
		// filter
		var ItemListPath = _viewModel.Icon_ItemListPath;
		if (!string.IsNullOrWhiteSpace(ItemListPath) && !File.Exists(ItemListPath)) throw new WarningException(StringHelper.Get("IconOut_Error1"));
		else if (this.FilterMode.IsChecked == true && !File.Exists(ItemListPath)) throw new WarningException(StringHelper.Get("IconOut_Error2"));

		// format
		var format = this.NameFormat.Text;
		if (string.IsNullOrWhiteSpace(format) || !format.Contains('[')) throw new WarningException(StringHelper.Get("IconOut_Error3"));
		else
		{
			format = format.ToLower();
			format = new Regex(@"\[\s+", RegexOptions.Singleline).Replace(format, "[");
			format = new Regex(@"\s+\]", RegexOptions.Singleline).Replace(format, "]");
		}


		_viewModel.Run(new ItemIcon(UserSettings.Default.GameFolder, _viewModel.Icon_OutputFolder + @"\Items")
		{
			ChvPath = ItemListPath,
			UseBackground = this.UseBackground.IsChecked == true,
			isWhiteList = this.FilterMode.IsChecked == true,

		}, format, 0);
	}
	#endregion

	#region Merge
	private void MergeIcon_DragEnter(object sender, DragEventArgs e)
	{
		if (e.Data.GetDataPresent(DataFormats.FileDrop))
			e.Effects = DragDropEffects.Copy;
	}

	private void MergeIcon_DragDrop(object sender, DragEventArgs e)
	{
		if (e.Data.GetDataPresent(DataFormats.FileDrop))
		{
			var files = (string[])e.Data.GetData(DataFormats.FileDrop);
			if (files.Length == 0) return;

			try
			{
				_viewModel.MergeIcon_Source = SKBitmap.Decode(File.ReadAllBytes(files[0]));
			}
			catch
			{

			}
		}
	}

	private void Reset_Click(object sender, RoutedEventArgs e)
	{
		_viewModel.MergeIcon_Source = null;
		Combobox_Grade.SelectedIndex = 6;
		Combobox_BottomLeft.SelectedIndex = 0;
		Combobox_TopRight.SelectedIndex = 0;
	}
	#endregion
}