using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Controls;

using AduSkin.Controls.Metro;

using CUE4Parse.BNS;
using CUE4Parse.BNS.Conversion;

using Ookii.Dialogs.Wpf;

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
		Combobox_Grade.SelectedIndex = Combobox_BottomLeft.SelectedIndex = Combobox_TopRight.SelectedIndex = 0;
	}
	#endregion


	#region Asset
	private void Brower_OutputFolder_Click(object sender, RoutedEventArgs e)
	{
		new SettingsView().ShowDialog();
	}

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
	private void Icon_BrowerOutputFolder_Click(object sender, RoutedEventArgs e)
	{
		var dialog = new VistaFolderBrowserDialog() { };
		if (dialog.ShowDialog() == true) _viewModel.Icon_OutputFolder = dialog.SelectedPath;
	}

	private void Icon_BrowerItemList_Click(object sender, RoutedEventArgs e)
	{
		var dialog = new VistaOpenFileDialog() { Filter = null };
		if (dialog.ShowDialog() == true) ItemListPath.Text = dialog.FileName;
	}

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
			if (AduMessageBox.Show("是否确认取消? ", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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
			if (AduMessageBox.Show("是否确认取消? ", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
			{
				source1?.Cancel();
				source1 = null;
			}

			return;
		}


		// filter
		if (!string.IsNullOrWhiteSpace(ItemListPath.Text) && !File.Exists(ItemListPath.Text))
		{
			AduMessageBox.Show("配置文件路径错误或不存在, 请重新确认！");
			return;
		}
		else if (this.FilterMode.IsChecked == true && !File.Exists(ItemListPath.Text))
		{
			AduMessageBox.Show("选择白名单模式时, 必须选择配置文件!");
			return;
		}

		// format
		var format = this.NameFormat.Text;
		if (string.IsNullOrWhiteSpace(format) || !format.Contains('['))
		{
			AduMessageBox.Show("输出格式必须至少包含一个特殊规则");
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
			ChvPath = ItemListPath.Text,
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
			AduMessageBox.Show($"任务已经全部结束！ 共计 {Ts.Hours}小时 {Ts.Minutes}分 {Ts.Seconds}秒。");
		}
		catch (Exception ee)
		{
			AduMessageBox.Show("由于发生了错误, 进程已提前结束。");
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

	private void MergeIcon_Reset_Click(object sender, RoutedEventArgs e)
	{
		//	if (ComboBox1.Source.Count > 7) ComboBox1.TextValue = ComboBox1.Source[7];
		//	if (ComboBox2.Source.Count != 0) ComboBox2.TextValue = ComboBox2.Source[0];
		//	if (ComboBox3.Source.Count != 0) ComboBox3.TextValue = ComboBox3.Source[0];
		//	IsInitialization = false;


		//	this.ImageCompose = new();
		//	this.ImageCompose.RefreshHandle += new((s, e) => pictureBox1.Image = ImageCompose.DrawICON(Radio_64px.Checked ? null : 2));

		//	this.ComboBox1_TextChanged(sender, e);
	}

	private void MergeIcon_Save_Click(object sender, RoutedEventArgs e)
	{
		//	string ItemName = string.IsNullOrEmpty(this.IconPath) ? "道具名称" : this.IconPath + "_" + ImageCompose_GetGrade();


		//	SaveFileDialog.FileName = ItemName;
		//	SaveFileDialog.Filter = "PNG格式|*.png|GIF格式|*.gif|JPEG格式|*.jpg|位图格式|*.bmp|ICO格式|*.ico";

		//	if (SaveFileDialog.ShowDialog() == DialogResult.OK)
		//	{
		//		ImageFormat Format = ImageFormat.Png;

		//		switch (SaveFileDialog.DefaultExt)
		//		{
		//			case ".png": Format = ImageFormat.Png; break;
		//			case ".gif": Format = ImageFormat.Gif; break;
		//			case ".jpg": Format = ImageFormat.Jpeg; break;
		//			case ".bmp": Format = ImageFormat.Bmp; break;
		//			case ".ico": Format = ImageFormat.Icon; break;
		//		}

		//		pictureBox1.Image.Save(SaveFileDialog.FileName, Format);
		//	}
	}
	#endregion
}