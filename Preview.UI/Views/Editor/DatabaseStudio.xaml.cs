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
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.UI.Controls;
using Xylia.Preview.UI.Helpers.Output;
using Xylia.Preview.UI.ViewModels;

namespace Xylia.Preview.UI.Views.Editor;
public partial class DatabaseStudio
{
	#region Constructors
	static DatabaseStudio()
	{
		TextEditor.Register("Sql");
	}

	public DatabaseStudio()
	{
		DataContext = _viewModel = new DatabaseStudioViewModel();
		InitializeComponent();
		RegisterCommands(this.CommandBindings);

		// Remove design time information 
		PageHolder.SelectedItem = Page_SQL;
	}
	#endregion

	#region Command
	private void RegisterCommands(CommandBindingCollection commandBindings)
	{
		commandBindings.Add(new CommandBinding(ApplicationCommands.Close, (_, _) => SaveMessage.Visibility = Visibility.Collapsed));
		commandBindings.Add(new CommandBinding(ApplicationCommands.Print, RunCommand, CanExecuteRun));
	}


	private void CanExecuteRun(object sender, CanExecuteRoutedEventArgs e)
	{
		e.CanExecute = database != null && !string.IsNullOrEmpty(ActivateSql);
	}

	private async void RunCommand(object sender, RoutedEventArgs e)
	{
		try
		{
			this.Run.IsEnabled = false;
			await ExecuteSql(ActivateSql);
		}
		catch (Exception ex)
		{
			Growl.Error(ex.Message, TOKEN);
		}
		finally
		{
			this.Run.IsEnabled = true;
		}
	}
	#endregion



	#region Methods (UI)
	private async void Connect_Click(object sender, RoutedEventArgs e)
	{
		if (database == null)
		{
			// cancel dialog
			var dialog = new DatabaseManager() { Owner = this };
			if (dialog.ShowDialog() != true) return;

			// loading
			_viewModel.IsGlobalData = dialog.IsGlobalData;
			Connect.IsEnabled = false;
			tvwDatabase.Items.Add(new TreeViewItem() { Header = "loading..." });

			database = dialog.Engine as BnsDatabase;
			await Task.Run(() => database!.Initialize());

			LoadTreeView();
			Connect.IsEnabled = _viewModel.ConnectStatus = true;
		}
		else
		{
			if (_viewModel.IsGlobalData)
			{
				var result = HandyControl.Controls.MessageBox.Show(
					StringHelper.Get("DatabaseStudio_ConnectMessage2"),
					StringHelper.Get("Message_Tip"), MessageBoxButton.YesNo);

				if (result != MessageBoxResult.Yes) return;
				else FileCache.Data = null;
			}

			// disconnect
			database.Dispose();
			database = null;

			tvwDatabase.Items.Clear();
			_viewModel.ConnectStatus = false;

			GC.Collect();
		}
	}

	private void Refresh_Click(object sender, RoutedEventArgs e)
	{
		LoadTreeView();
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
				AddTab(s, item.Header?.ToString());
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
				cell.SetValue(item![col.Header.ToString()!]);
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


	private void ViewTable_Click(object sender, RoutedEventArgs e)
	{
		if (tvwDatabase.SelectedItem is FrameworkElement item && item.DataContext is Table table)
		{
			var window = new TableView { Table = table };
			window.Show();
		}
	}

	private void ViewDefinition_Click(object sender, RoutedEventArgs e)
	{
		if (tvwDatabase.SelectedItem is FrameworkElement item && item.DataContext is Table table)
		{
			_viewModel.CurrentDefinition = table.Definition;
			PageHolder.SelectedItem = Page_Definition;
		}
	}

	private void ReturnBtn_Click(object sender, RoutedEventArgs e)
	{
		PageHolder.SelectedItem = Page_SQL;
	}


	private async void TableExport_Click(object sender, RoutedEventArgs e)
	{
		if (tvwDatabase.SelectedItem is TreeViewItem item && item.DataContext is Table table)
			await ExportAsync(table);
	}

	private async void TableExportAll_Click(object sender, RoutedEventArgs e)
	{
		await ExportAsync([.. database.Provider.Tables.Where(x => x.SearchPattern is null)]);
	}

	private async void Import_Click(object sender, RoutedEventArgs e)
	{
		try
		{
			Growl.Info("Start import", TOKEN);

			DateTime dt = DateTime.Now;

			serialize = new ProviderSerialize(database.Provider);
			await serialize.ImportAsync(_viewModel.SaveDataPath);

			Growl.Success(new GrowlInfo()
			{
				Token = TOKEN,
				Message = "Import finished, " + (DateTime.Now - dt).TotalSeconds,
				StaysOpen = false,
			});
		}
		catch (Exception ex)
		{
			Growl.Error(ex.Message, TOKEN);
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
				Token = TOKEN,
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
				Tag = $"SELECT * FROM \"{table.Name ?? table.Type.ToString()}\"\nLIMIT {TaskData.LIMITNUM}",
				ContextMenu = this.TryFindResource("TableMenu") as ContextMenu,

				Margin = new Thickness(0, 0, 0, 2),
			});
		}
	}

	/// <summary>
	/// update right-bottom message
	/// </summary>
	/// <param name="text"></param>
	private void UpdateMessage(string text)
	{
		Status.Text = text;
	}

	/// <summary>
	/// append new sql tab
	/// </summary>
	/// <param name="sql"></param>
	/// <param name="header"></param>
	private void AddTab(string sql, string? header = null)
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
			if (item is not ICSharpCode.AvalonEdit.TextEditor editor) return string.Empty;

			return editor.Text;
		}
	}

	private async Task ExecuteSql(string sql)
	{
		ArgumentNullException.ThrowIfNull(database);
		DateTime dt = DateTime.Now;

		var task = new TaskData();
		await Task.Run(() => task.ReadResult(database!.Execute(sql)));

		// update
		task.BindData(this.QueryGrid);
		task.BindData(this.QueryText, _viewModel.IndentText);

		UpdateMessage(string.Format("Execution Time: {0:g}", DateTime.Now - dt));
	}

	private async Task ExportAsync(params Table[] tables)
	{
		var progress = new Action<int, int>((current, total) =>
		{
			Dispatcher.Invoke(() =>
			{
				SaveMessage.Visibility = Visibility.Visible;
				SaveMessage.Text = current != tables.Length ?
					StringHelper.Get("DatabaseStudio_TaskMessage1", current, tables.Length, (double)current / tables.Length) :
					StringHelper.Get("DatabaseStudio_TaskMessage2", tables.Length);
			});
		});

		serialize = new ProviderSerialize(database.Provider);
		await serialize.ExportAsync(_viewModel.SaveDataPath, progress, tables);
	}
	#endregion


	#region Private Fields
	internal static string TOKEN = nameof(DatabaseStudio);
	private readonly DatabaseStudioViewModel _viewModel;

	private BnsDatabase? database;
	private ProviderSerialize? serialize;
	#endregion
}