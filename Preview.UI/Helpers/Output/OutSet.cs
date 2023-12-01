using System.IO;

using OfficeOpenXml;

namespace Xylia.Preview.Data.Helpers.Output;
public abstract class OutSet
{
	#region Fields
	protected ExcelPackage package;

	public virtual string Name => GetType().Name;

	public int Column { get; protected set; } = 1;
	public int Row { get; protected set; } = 1;
	#endregion

	#region Methods
	public Task Output(FileInfo path = null) => Task.Run(() =>
	{
		ArgumentNullException.ThrowIfNull(path);

		ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
		package = new ExcelPackage();
		var sheet = package.Workbook.Worksheets.Add(Name);
		sheet.Cells.Style.Font.Name = "宋体";
		sheet.Cells.Style.Font.Size = 11F;
		sheet.Cells.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
		sheet.Cells.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

		CreateData(sheet);

		package.SaveAs(path.FullName);

		GC.Collect();
	});

	/// <summary>
	/// Create xlsx file
	/// </summary>
	/// <param name="sheet"></param>
	protected abstract void CreateData(ExcelWorksheet sheet);

	/// <summary>
	/// Create text file
	/// </summary>
	/// <exception cref="NotImplementedException"></exception>
	protected virtual void CreateText() => throw new NotImplementedException();
	#endregion
}


public static class FileExtension
{
	public static void SetColumn(this ExcelWorksheet sheet, int index, string header, int width = 10)
	{
		sheet.Column(index).Width = width;
		sheet.Cells[1, index].Value = header;
	}

	public static void SetValue(this ExcelRange cell, object value)
	{
		if (value is null) return;

		cell.Value = value;
	}
}