using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

using Newtonsoft.Json;
using Xylia.Preview.Data.Client;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Views.Editor;
public class TaskData
{
	public const int LIMITNUM = 1000;


	public List<AttributeValue> Result { get; set; } = null;

	public bool LimitExceeded { get; set; }

	public void ReadResult(IDataReader reader)
	{
		this.Result = [];

		while (reader.Read())
		{
			this.Result.Add(reader.Current);
		}
	}


	public void BindData(DataGrid grd)
	{
		using var dt = new System.Data.DataTable();

		foreach (var value in this.Result)
		{
			var row = dt.NewRow();

			var doc = value.IsDocument ?
				value.AsDocument :
				new AttributeDocument { ["[value]"] = value };

			if (!doc.Any()) doc["[root]"] = "{}";

			foreach (var key in doc)
			{
				var col = dt.Columns[key.Key];
				if (col is null)
				{
					dt.Columns.Add(key.Key);
				}
			}

			foreach (var key in doc)
			{
				row[key.Key] = value.IsDocument ? value[key.Key] : value;
			}

			dt.Rows.Add(row);
		}

		if (dt.Rows.Count == 0)
		{
			dt.Columns.Add("no-data");
			dt.Rows.Add("[no result]");
		}

		grd.ItemsSource = dt.DefaultView;
	}

	public void BindData(ICSharpCode.AvalonEdit.TextEditor txt, bool indent)
	{
		var index = 0;
		var builder = new StringBuilder();
		var settings = new JsonSerializerSettings()
		{
			Formatting = indent ? Formatting.Indented : Formatting.None,
		};

		if (this.Result.Count > 0)
		{
			foreach (var value in this.Result)
			{
				builder.AppendLine($"/* {index++ + 1} */");
				builder.AppendLine(JsonConvert.SerializeObject(value, settings));
				builder.AppendLine();
			}

			if (this.LimitExceeded)
			{
				builder.AppendLine();
				builder.AppendLine("/* Limit exceeded */");
			}
		}
		else
		{
			builder.AppendLine("no result");
		}

		txt.Text = builder.ToString();
	}
}

public static class ImageHelper
{
	public static BitmapImage Table { get; } = new BitmapImage(new Uri("/Resources/Images/table2.png", UriKind.Relative));

	public static BitmapImage TableSys { get; } = new BitmapImage(new Uri("/Resources/Images/table_set.png", UriKind.Relative));

	public static BitmapImage Database { get; } = new BitmapImage(new Uri("/Resources/Images/database.png", UriKind.Relative));

	public static BitmapImage Folder { get; } = new BitmapImage(new Uri("/Resources/Images/folder.png", UriKind.Relative));
}