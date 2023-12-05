using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

using OfficeOpenXml;

using Ookii.Dialogs.Wpf;

using Xylia.Preview.Data;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Helpers.Output;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Controls;
using Xylia.Preview.UI.ViewModels;

namespace Xylia.Preview.UI.Views.Editor;
public partial class DatabaseStudio : Window
{
	#region Constructor
	private readonly SqlParser parser = new();

	public DatabaseStudio()
	{
		TextEditor.Register();
		InitializeComponent();

		if (!FileCache.IsEmpty)
		{
			parser.Database = FileCache.Data;
			LoadTreeView();
		}
	}
	#endregion


	#region Methods
	private void LoadTreeView()
	{
		tvwDatabase.Items.Clear();
		if (parser.Database is null) return;
		var provider = parser.Database.Provider;

		var root = new TreeViewImageItem
		{
			Header = provider.Name,
			IsExpanded = true,
			Image = new BitmapImage(new Uri("/Resources/Images/database.png", UriKind.Relative))
		};
		tvwDatabase.Items.Add(root);

		var system = new TreeViewImageItem
		{
			Header = "System",
			Image = new BitmapImage(new Uri("/Resources/Images/folder.png", UriKind.Relative))
		};
		root.Items.Add(system);

		system.Items.Add(new TreeViewImageItem { Header = "CreatedAt: " + provider.CreatedAt });
		system.Items.Add(new TreeViewImageItem { Header = "Version: " + provider.ClientVersion });


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
				Tag = $"SELECT * FROM \"{table.Name ?? table.Type.ToString()}\"\nLIMIT 1000",
				Image = new BitmapImage(new Uri("/Resources/Images/table2.png", UriKind.Relative)),
				ContextMenu = this.TryFindResource("TableMenu") as ContextMenu,

				Margin = new Thickness(0, 0, 0, 2),
			});
		}
	}

	private async Task ExecuteSql(string sql)
	{
		QueryResult.Columns.Clear();
		QueryResult.ItemsSource = null;

		await Task.Run(() => parser.Execute(sql));

		var source = parser.Source;
		if (source is null) return;

		Array.ForEach(parser.Fields, x => QueryResult.Columns.Add(new DataGridTextColumn { Header = x, Binding = new Binding($"Attributes[{x}]") }));
		QueryResult.ItemsSource = source;

		// update tip
		QueryResult.Visibility = source.Length == 0 ? Visibility.Hidden : Visibility.Visible;
		QueryEmpty.Visibility = source.Length != 0 ? Visibility.Hidden : Visibility.Visible;
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
	#endregion

	#region Methods (UI)
	private async void Connect_Click(object sender, RoutedEventArgs e)
	{
		if (parser.Database == null)
		{
			Connect.IsEnabled = false;
			var root = new TreeViewItem() { Header = "loading..." };
			tvwDatabase.Items.Add(root);

			await Task.Run(() =>
			{
				parser.Database = new BnsDatabase();
				if (FileCache.IsEmpty) FileCache.Data = parser.Database;
			});

			Connect.Tag = "Disconnect";

			LoadTreeView();
			Connect.IsEnabled = true;
		}
		else
		{
			parser.Database.Dispose();
			parser.Database = null;

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
			HandyControl.Controls.MessageBox.Show(ex.Message);
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
		if (text is null)
		{
			return;
		}

		// save
		var dialog = new VistaSaveFileDialog()
		{
			Filter = "|*.sql",
			FileName = "Query.sql",
		};
		if (dialog.ShowDialog() == true) File.WriteAllText(dialog.FileName, text);
	}

	private void tvwDatabase_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
			FileName = $"test.xlsx",
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
		int Column = 1;
		for (int i = 0; i < QueryResult.Columns.Count; i++)
		{
			sheet.SetColumn(Column++, QueryResult.Columns[i].Header.ToString());
		}
		#endregion

		#region Row
		int Row = 1;
		for (int i = 0; i < QueryResult.Items.Count; i++)
		{
			Row++;
			int column = 1;

			var item = QueryResult.Items[i] as Record;
			for (int j = 0; j < QueryResult.Columns.Count; j++)
			{
				var col = QueryResult.Columns[j];
				sheet.Cells[Row, column++].SetValue(item.Attributes[col.Header.ToString()]);
			}
		}
		#endregion

		package.SaveAs(save.FileName);
	}


	private void TableToXml_Click(object sender, RoutedEventArgs e)
	{
		// TODO: as sql result ?
		if (tvwDatabase.SelectedItem is TreeViewItem item && item.DataContext is Table table)
			table.WriteXml(UserSettings.Default.OutputFolder + "\\data");
	}

	private void TableView_Click(object sender, RoutedEventArgs e)
	{
		if (tvwDatabase.SelectedItem is TreeViewItem item && item.DataContext is Table table)
		{
			var window = new TableView { Table = table };
			window.Show();
		}
	}
	#endregion
}