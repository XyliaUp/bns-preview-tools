//using NPOI.SS.UserModel;
//using NPOI.XSSF.UserModel;

//using Xylia.Preview.Common.Extension;

//namespace Xylia.Preview.Common.Files;
//public sealed class ExcelInfo : IDisposable
//{
//	#region Fields
//	public readonly IWorkbook Workbook;

//	public ISheet MainSheet;

//	public ICellStyle style;

//	public ExcelInfo(string SheetName = "数据")
//	{
//		this.Workbook = new XSSFWorkbook();

//		// sheet
//		CreateSheet(SheetName);

//		// style
//		var font = Workbook.CreateFont();
//		style = Workbook.CreateStyle();
//		style.SetFont(font);
//	}
//	#endregion

//	#region Methods
//	int ColumnIndex = 0;

//	public ISheet CreateSheet(string sheetname)
//	{
//		ColumnIndex = 0;
//		return this.MainSheet = Workbook.CreateSheet(sheetname);
//	}

//	public void SetColumn(string title, int width = 10, ISheet sheet = null)
//	{
//		sheet ??= MainSheet;
//		sheet.SetColumnWidth(ColumnIndex, width * 256);

//		var trow = sheet.GetRow(0);
//		if (trow is null)
//		{
//			trow = sheet.CreateRow(0);
//			trow.RowStyle = style;
//		}
//		tsheet.Cells[Row , column++].Value =title);


//		ColumnIndex++;
//	}

//	public IRow CreateRow(int rownum = -1, ISheet sheet = null) => (sheet ?? MainSheet).CreateRow(rownum, this.style);



//	public void Save(string FilePath, int RetryTime = 20)
//	{
//		MemoryStream stream = new MemoryStream();
//		Workbook.Write(stream, false);

//		for (int i = 0; i <= RetryTime; i++)
//		{
//			if (i != 0)
//			{
//				FilePath = Path.GetDirectoryName(FilePath) +
//					Path.GetFileNameWithoutExtension(FilePath) + $" ({i})" +
//					Path.GetExtension(FilePath);
//			}

//			try
//			{
//				using var fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
//				fs.Write(stream.ToArray());
//				fs.Flush();
//				fs.Close();

//				break;
//			}
//			catch (Exception ee)
//			{
//				if (i == RetryTime) throw;
//				else Console.WriteLine($"正在尝试保存文件，但是{ee.Message} (第{i}次)");
//			}
//			finally
//			{
//				stream.Close();
//			}
//		}
//	}
//	#endregion


//	public void Dispose()
//	{
//		this.Workbook.Dispose();

//		GC.SuppressFinalize(this);
//	}
//}