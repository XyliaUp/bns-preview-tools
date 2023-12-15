using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HandyControl.Controls;
using HandyControl.Data;
using Microsoft.Win32;
using OfficeOpenXml;
using Ookii.Dialogs.Wpf;

using Xylia.Preview.Data.Client;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Engine.BinData.Serialization;
using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Helpers.Output;
using Xylia.Preview.UI.Controls;
using Xylia.Preview.UI.ViewModels;

namespace Xylia.Preview.UI.Views.Editor;
public partial class DatabaseStudio
{
	#region Constructor
	private BnsDatabase database;
	private ProviderSerialize serialize;

	static DatabaseStudio()
	{
		TextEditor.Register("Sql");
	}

	public DatabaseStudio()
	{
		DataContext = this;

		InitializeComponent();
		RegisterCommands(this.CommandBindings);

		// load cache data if exists
		if (!FileCache.IsEmpty)
		{
			database = FileCache.Data;
			LoadTreeView();
		}

		// dev
		database = new BnsDatabase(new FolderProvider(@"G:\新建文件夹"));
		LoadTreeView();

		ExecuteSql("SELECT COUNT(*) FROM store2  ");
	}
	#endregion


	#region Command
	private void RegisterCommands(CommandBindingCollection commandBindings)
	{
		commandBindings.Add(new CommandBinding(ApplicationCommands.Close, (_, _) => SaveMessage.Visibility = Visibility.Collapsed));
	}
	#endregion

	#region Methods (UI)
	private async void Connect_Click(object sender, RoutedEventArgs e)
	{
		if (database == null)
		{
			Connect.IsEnabled = false;
			var root = new TreeViewItem() { Header = "loading..." };
			tvwDatabase.Items.Add(root);

			await Task.Run(() =>
			{
				database = new BnsDatabase();
				if (FileCache.IsEmpty) FileCache.Data = database;
			});

			Connect.Tag = "Disconnect";

			LoadTreeView();
			Connect.IsEnabled = true;
		}
		else
		{
			database.Dispose();
			database = null;

			tvwDatabase.Items.Clear();
			Connect.Tag = "Connect";
		}
	}

	private void Refresh_Click(object sender, RoutedEventArgs e)
	{
		LoadTreeView();
	}

	private async void Run_Click(object sender, RoutedEventArgs e)
	{
		try
		{
			this.Run.IsEnabled = false;
			await ExecuteSql(ActivateSql);
		}
		catch (Exception ex)
		{
			Growl.Error(ex.Message, nameof(DatabaseStudio));
		}
		finally
		{
			this.Run.IsEnabled = true;
		}
	}

	private void LoadSql_Click(object sender, RoutedEventArgs e)
	{
		var dialog = new VistaOpenFileDialog();
		if (dialog.ShowDialog() != true) return;

		// valid
		var text = File.ReadAllText(dialog.FileName);
		var header = Path.GetFileName(dialog.FileName);
		AddTab(text, header);
	}

	private void SaveSql_Click(object sender, RoutedEventArgs e)
	{
		// valid
		var text = ActivateSql;
		if (text is null) return;

		// save
		var dialog = new VistaSaveFileDialog()
		{
			Filter = "|*.sql",
			FileName = "Query.sql",
		};
		if (dialog.ShowDialog() == true) File.WriteAllText(dialog.FileName, text);
	}

	private void TvwDatabase_MouseDoubleClick(object sender, MouseButtonEventArgs e)
	{
		if (tvwDatabase.SelectedItem is TreeViewImageItem item)
		{
			if (item.Tag is string s)
				AddTab(s, item.Header);
		}
	}

	private void OutputExcel_Click(object sender, RoutedEventArgs e)
	{
		var save = new VistaSaveFileDialog
		{
			Filter = "Excel Files|*.xlsx",
			FileName = $"query.xlsx",
		};
		if (save.ShowDialog() != true) return;


		#region Sheet
		ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
		var package = new ExcelPackage();
		var sheet = package.Workbook.Worksheets.Add("Sheet");
		sheet.Cells.Style.Font.Name = "宋体";
		sheet.Cells.Style.Font.Size = 11F;
		sheet.Cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
		sheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
		#endregion


		#region Title
		for (int j = 0; j < QueryGrid.Columns.Count; j++)
		{
			sheet.SetColumn(j + 1, QueryGrid.Columns[j].Header.ToString());
		}
		#endregion

		#region Row
		for (int i = 0; i < QueryGrid.Items.Count; i++)
		{
			var item = QueryGrid.Items[i] as DataRowView;

			var row = i + 2;
			for (int j = 0; j < QueryGrid.Columns.Count; j++)
			{
				var col = QueryGrid.Columns[j];

				var cell = sheet.Cells[row, j + 1];
				cell.SetValue(item[col.Header.ToString()]);
			}
		}
		#endregion

		package.SaveAs(save.FileName);
	}

	private void OutputText_Click(object sender, RoutedEventArgs e)
	{
		var save = new VistaSaveFileDialog
		{
			Filter = "Text Files|*.txt",
			FileName = $"query.txt",
		};
		if (save.ShowDialog() != true) return;

		File.WriteAllText(save.FileName, QueryText.Text);
	}


	private void TableView_Click(object sender, RoutedEventArgs e)
	{
		if (tvwDatabase.SelectedItem is TreeViewItem item && item.DataContext is Table table)
		{
			var window = new TableView { Table = table };
			window.Show();
		}
	}

	private async void TableExport_Click(object sender, RoutedEventArgs e)
	{
		if (tvwDatabase.SelectedItem is TreeViewItem item && item.DataContext is Table table)
			await ExportAsync(table);
	}

	private async void TableExportAll_Click(object sender, RoutedEventArgs e)
	{
		await ExportAsync([.. database.Provider.Tables]);
	}

	private async void Import_Click(object sender, RoutedEventArgs e)
	{
		try
		{
			Growl.Info("Start import", nameof(DatabaseStudio));

			DateTime dt = DateTime.Now;

			serialize = new ProviderSerialize(database.Provider);
			await serialize.ImportAsync(SaveDataPath);

			Growl.Success(new GrowlInfo()
			{
				Token = nameof(DatabaseStudio),
				Message = "Import finished, " + (DateTime.Now - dt).TotalSeconds,
				StaysOpen = false,
			});
		}
		catch (Exception ex)
		{
			Growl.Error(ex.Message, nameof(DatabaseStudio));
		}
	}

	private async void Save_Click(object sender, RoutedEventArgs e)
	{
		var dialog = new OpenFolderDialog();
		if (dialog.ShowDialog() == true)
		{
			serialize ??= new ProviderSerialize(database.Provider);
			await serialize.SaveAsync(dialog.FolderName);

			Growl.Success(new GrowlInfo()
			{
				Token = nameof(DatabaseStudio),
				Message = "Save finished",
				StaysOpen = true,
			});
		}
	}
	#endregion


	#region Methods
	private void LoadTreeView()
	{
		tvwDatabase.Items.Clear();
		if (database is null) return;

		// system nodes  		
		var provider = database.Provider;
		var root = new TreeViewImageItem { Image = ImageHelper.Database, Header = provider.Name, IsExpanded = true };
		tvwDatabase.Items.Add(root);

		var system = new TreeViewImageItem { Image = ImageHelper.Folder, Header = "System", IsExpanded = false };
		root.Items.Add(system);

		system.Items.Add(new TreeViewImageItem { Image = ImageHelper.TableSys, Header = "CreatedAt: " + provider.CreatedAt });
		system.Items.Add(new TreeViewImageItem { Image = ImageHelper.TableSys, Header = "Version: " + provider.ClientVersion });

		// table nodes
		foreach (var table in provider.Tables.OrderBy(x => x.Type))
		{
			// text
			var text = table.Type.ToString();
			if (table.Name != null) text = $"{table.Name} ({table.Type})";

			// node
			root.Items.Add(new TreeViewImageItem
			{
				DataContext = table,
				Header = text,
				Image = ImageHelper.Table,
				Tag = $"SELECT $ FROM \"{table.Name ?? table.Type.ToString()}\"\nLIMIT {TaskData.LIMITNUM}",
				ContextMenu = this.TryFindResource("TableMenu") as ContextMenu,

				Margin = new Thickness(0, 0, 0, 2),
			});
		}
	}

	private void AddTab(string sql, string header = null)
	{
		header ??= ("TabItem" + (editors.Items.Count + 1));
		var item = new HandyControl.Controls.TabItem()
		{
			Content = new ICSharpCode.AvalonEdit.TextEditor() { Text = sql },
			Header = header,
			ToolTip = header,
		};

		editors.Items.Insert(0, item);
		editors.SelectedItem = item;
	}

	private string ActivateSql
	{
		get
		{
			var item = (editors.SelectedItem as ContentControl)?.Content;
			if (item is null) return null;

			return (item as ICSharpCode.AvalonEdit.TextEditor).Text;
		}
	}

	public bool IndentText { get; set; } = true;

	private string SaveDataPath => UserSettings.Default.OutputFolder + "\\data";


	private async Task ExecuteSql(string sql)
	{
		DateTime dt = DateTime.Now;

		var task = new TaskData();
		await Task.Run(() => task.ReadResult(database.Execute(sql)));

		// update
		task.BindData(this.QueryGrid);
		task.BindData(this.QueryText, IndentText);

		Status.Text = string.Format("Execution Time: {0:g}", DateTime.Now - dt);
	}

	private async Task ExportAsync(params Table[] tables)
	{
		var progress = new Action<int, int>((current, total) =>
		{
			Dispatcher.Invoke(() =>
			{
				SaveMessage.Visibility = Visibility.Visible;

				if (current != tables.Length)
				{
					SaveMessage.Text = StringHelper.Get("DatabaseStudio_TaskMessage1", current, tables.Length, (double)current / tables.Length);
				}
				else
				{
					SaveMessage.Text = StringHelper.Get("DatabaseStudio_TaskMessage2", tables.Length);
				}
			});
		});

		serialize = new ProviderSerialize(database.Provider);
		await serialize.ExportAsync(this.SaveDataPath, progress, tables);
	}
	#endregion
}