using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

using AduSkin.Controls.Metro;

using Ookii.Dialogs.Wpf;

using Xylia.Preview.Data;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Common.Controls;
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
			HeaderText = provider.Name,
			IsExpanded = true,
			Image = new BitmapImage(new Uri("/Resources/Images/database.png", UriKind.Relative))
		};
		tvwDatabase.Items.Add(root);

		var system = new TreeViewImageItem
		{
			HeaderText = "System",
			Image = new BitmapImage(new Uri("/Resources/Images/folder.png", UriKind.Relative))
		};
		root.Items.Add(system);

		system.Items.Add(new TreeViewImageItem { HeaderText = "CreatedAt: " + provider.CreatedAt });
		system.Items.Add(new TreeViewImageItem { HeaderText = "Version: "   + provider.ClientVersion });


		foreach (var table in provider.Tables.OrderBy(x => x.Type))
		{
			// text
			var text = table.Type.ToString();
			if (table.Name != null) text = $"{table.Name} ({table.Type})";

			// node
			root.Items.Add(new TreeViewImageItem
			{
				DataContext = table,
				HeaderText = text,
				Tag = $"SELECT * FROM \"{table.Name ?? table.Type.ToString()}\"\nLIMIT 1000",
				Image = new BitmapImage(new Uri("/Resources/Images/table2.png", UriKind.Relative)),
				ContextMenu = this.TryFindResource("TableMenu") as ContextMenu,

				Margin = new Thickness(0, 0, 0, 2),
			});
		}
	}

	private async Task ExecuteSql(string sql)
	{
		grdResult.Columns.Clear();
		grdResult.ItemsSource = null;

		await Task.Run(() => parser.Execute(sql));

		var source = parser.Source;
		if (source is null) return;

		Array.ForEach(parser.Fields, x => grdResult.Columns.Add(new DataGridTextColumn { Header = x, Binding = new Binding($"Attributes[{x}]") }));
		grdResult.ItemsSource = source;
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

			await Task.Run(() => parser.Database = new BnsDatabase());
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
			await ExecuteSql(editor.Text);
		}
		catch(Exception ex)
		{
			AduMessageBox.Show(ex.Message);
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

		editor.Text = File.ReadAllText(dialog.FileName);
	}

	private void SaveSql_Click(object sender, RoutedEventArgs e)
	{
		var dialog = new VistaSaveFileDialog()
		{
			Filter = "|*.sql",
			FileName = "Query.sql",
		};
		if (dialog.ShowDialog() == true)
			File.WriteAllText(dialog.FileName, editor.Text);
	}

	private void tvwDatabase_MouseDoubleClick(object sender, MouseButtonEventArgs e)
	{
		if (tvwDatabase.SelectedItem is TreeViewItem item)
		{
			if (item.Tag != null) editor.Text = item.Tag as string;
		}	
	}

	private void TableToXml_Click(object sender, RoutedEventArgs e)
	{
		// TODO: as sql result ?
		if (tvwDatabase.SelectedItem is TreeViewItem item && item.DataContext is Table table)
			table.WriteXml(UserSettings.Default.OutputFolder + "\\data");
	}


	private void OutputExcel_Click(object sender, RoutedEventArgs e)
	{
		//if (grdResult.Items.Count == 0)
		//{
		//	AduMessageBox.Show("no data");
		//	return;
		//}

		//var save = new VistaSaveFileDialog
		//{
		//	Filter = "Excel Files|*.xlsx",
		//	FileName = $"test.xlsx",
		//};
		//if (save.ShowDialog() != true) return;

		//using var workbook = new XSSFWorkbook();
		//var sheet = workbook.CreateSheet("Sheet");

		//#region Title
		//var title = sheet.CreateRow(0);
		//for (int i = 0; i < grdResult.Columns.Count; i++)
		//{
		//	title.CreateCell(i).SetCellValue(grdResult.Columns[i].Header.ToString());
		//}
		//#endregion

		//#region Row
		//for (int i = 0; i < grdResult.Items.Count; i++)
		//{
		//	var item = grdResult.Items[i] as Record;
		//	var row = sheet.CreateRow(i + 1);

		//	for (int j = 0; j < grdResult.Columns.Count; j++)
		//	{
		//		var column = grdResult.Columns[j];
		//		row.CreateCell(j).SetCellValue(item.Attributes[column.Header.ToString()]);
		//	}
		//}
		//#endregion


		//#region Write
		//MemoryStream stream = new MemoryStream();
		//workbook.Write(stream, false);
		//workbook.Dispose();

		//using var fs = new FileStream(save.FileName, FileMode.Create, FileAccess.Write);
		//fs.Write(stream.ToArray());
		//fs.Flush();
		//fs.Close();
		//#endregion
	}
	#endregion
}