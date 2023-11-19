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
	GameResourcePageViewModel _viewModel = new GameResourcePageViewModel();

	public GameResourcePage()
	{
		InitializeComponent();
		DataContext = _viewModel;
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

	#region	Icon
	private void Output_ItemList(object sender, RoutedEventArgs e)
	{
		//SaveFileDialog.FileName = "配置文件";
		//SaveFileDialog.Filter = "Xylia Value 配置文件|*.chv";

		//if (SaveFileDialog.ShowDialog() == DialogResult.OK)
		//{
		//	Thread thread = new((ThreadStart)delegate
		//	{
		//		int Count = 1;

		//		using (StreamWriter sw = new(SaveFileDialog.FileName))
		//		{
		//			string FolderPath = this.Path_ResultPath.Text + @"\物品";
		//			if (!Directory.Exists(FolderPath))
		//			{
		//				FolderPath = Path_ResultPath.Text;
		//				this.Invoke(() => FrmTips.ShowTips("由于不存在目标子文件夹, 已变更为扫描所选的<输出目录>"));
		//			}

		//			var Files = new DirectoryInfo(FolderPath).GetFiles();
		//			foreach (FileInfo fileInfo in Files)
		//			{
		//				this.Invoke(() => Footer.Text = $"正在生成配置文件  {100 * Count++ / Files.Length}%");

		//				if (fileInfo.Name.Contains('_'))
		//				{
		//					string[] Temp = fileInfo.Name.Split('_');

		//					foreach (var T in Temp) if (int.TryParse(T.Replace(".png", null), out int Result)) sw.WriteLine(Result);
		//				}
		//				else if (int.TryParse(fileInfo.Name.Replace(".png", null), out int ItemID))
		//				{
		//					sw.WriteLine(ItemID);
		//				}
		//			}
		//		}

		//		this.Invoke(() =>
		//		{
		//			Footer.Text = $"输出配置文件已完成";
		//			FrmTips.ShowTips("输出配置文件已完成！");
		//		});
		//	});

		//	thread.SetApartmentState(ApartmentState.STA);
		//	thread.Start();
		//}
	}


	CancellationTokenSource source1;
	CancellationTokenSource source2;

	private void Extract_GoodIcon(object sender, RoutedEventArgs e)
	{
		if (source2 != null)
		{
			if (MessageBox.Show("是否确认取消? ", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
			{
				source2?.Cancel();
				source2 = null;
			}

			return;
		}

		Run(new GoodIcon(
			UserSettings.Default.GameFolder,
			_viewModel.Icon_OutputFolder + @"\Goods"),
			 null, source2 = new CancellationTokenSource());
	}

	private void Extract_ItemIcon(object sender, RoutedEventArgs e)
	{
		if (source1 != null)
		{
			if (MessageBox.Show("是否确认取消? ", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
			{
				source1?.Cancel();
				source1 = null;
			}

			return;
		}


		// filter
		var ItemListPath = _viewModel.Icon_ItemListPath;
		if (!string.IsNullOrWhiteSpace(ItemListPath) && !File.Exists(ItemListPath))
		{
			MessageBox.Show("配置文件路径错误或不存在, 请重新确认！");
			return;
		}
		else if (this.FilterMode.IsChecked == true && !File.Exists(ItemListPath))
		{
			MessageBox.Show("选择白名单模式时, 必须选择配置文件!");
			return;
		}

		// format
		var format = this.NameFormat.Text;
		if (string.IsNullOrWhiteSpace(format) || !format.Contains('['))
		{
			MessageBox.Show("输出格式必须至少包含一个特殊规则");
			return;
		}
		else
		{
			format = format.ToLower();
			format = new Regex(@"\[\s+", RegexOptions.Singleline).Replace(format, "[");
			format = new Regex(@"\s+\]", RegexOptions.Singleline).Replace(format, "]");
		}

		Run(new ItemIcon(
			UserSettings.Default.GameFolder,
			_viewModel.Icon_OutputFolder + @"\Items")
		{
			ChvPath = ItemListPath,
			UseBackground = this.UseBackground.IsChecked == true,
			isWhiteList = this.FilterMode.IsChecked == true,

		}, format, source1 = new CancellationTokenSource());
	}

	public void Run(IconOutBase Out, string format, CancellationTokenSource source) => Task.Run(async () =>
	{
		try
		{
			DateTime d1 = DateTime.Now;
			await Out.LoadData(source.Token);
			await Out.Output(format, source.Token);
			Out.Dispose();

			TimeSpan Ts = DateTime.Now - d1;
			MessageBox.Show($"任务已经全部结束！ 共计 {Ts.Hours}小时 {Ts.Minutes}分 {Ts.Seconds}秒。");
		}
		catch (Exception ee)
		{
			MessageBox.Show("由于发生了错误, 进程已提前结束。");
			Console.WriteLine(ee);
		}
		finally
		{
			ProcessEx.ClearMemory();
		}

		source.Dispose();
		source = null;
	});
	#endregion

	#region Merge
	private void MergeIcon_DragEnter(object sender, DragEventArgs e)
	{
		if (e.Data.GetDataPresent(DataFormats.FileDrop))
			e.Effects = DragDropEffects.Copy;
		else
			e.Effects = DragDropEffects.None;
	}

	private void MergeIcon_DragDrop(object sender, DragEventArgs e)
	{
		if (e.Data.GetDataPresent(DataFormats.FileDrop))
		{
			var files = (string[])e.Data.GetData(DataFormats.FileDrop);
			if (files.Length == 0) return;

			_viewModel.MergeIcon_Source = SKBitmap.Decode(File.ReadAllBytes(files[0]));
		}
	}

	private void Reset_Click(object sender, RoutedEventArgs e)
	{
		Combobox_Grade.SelectedIndex = 0;
		Combobox_BottomLeft.SelectedIndex = 0;
		Combobox_TopRight.SelectedIndex = 0;
	}
	#endregion
}