//using NPOI.HSSF.UserModel;
//using NPOI.SS.UserModel;
//using NPOI.XSSF.UserModel;

//namespace Xylia.Preview.Common.Extension;
//public static class SheetEx
//{
//    public static ICellStyle CreateStyle(this IWorkbook Workbook)
//    {
//        ICellStyle CellStyle = Workbook.CreateCellStyle();
//        CellStyle.Alignment = HorizontalAlignment.Center;
//        CellStyle.VerticalAlignment = VerticalAlignment.Center;
//        CellStyle.WrapText = true;

//        return CellStyle;
//    }

//    public static IFont CreateFont(this IWorkbook workbook, string FontName = "微软雅黑", double FontHeight = 11.2, bool Bold = false)
//    {
//        IFont font = workbook.CreateFont();
//        font.FontHeightInPoints = FontHeight;
//        font.FontName = FontName;
//        font.IsBold = Bold;

//        return font;
//    }


//    public static string[] GetSheetName(this string filePath, out IWorkbook Workbook, bool IncludingEmpty = false)
//    {
//        Workbook = null;

//        var ext = Path.GetExtension(filePath).ToLower();
//        if (ext == ".xlsx") Workbook = new XSSFWorkbook(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
//        else if (ext == ".xls") Workbook = new HSSFWorkbook(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
//        else return null;


//        #region get names
//        var sheetNames = new List<string>();
//        for (int i = 0; i < Workbook.NumberOfSheets; i++)
//        {
//            var sheet = Workbook.GetSheetAt(i);

//            bool hasContent = false;
//            if (!IncludingEmpty)
//            {
//                var rowIndex = sheet.LastRowNum;
//                for (int x = 0; x < rowIndex; i++)
//                {
//                    var row = sheet.GetRow(x);

//                    //all cells are empty, so is a 'blank row' 
//                    if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;

//                    hasContent = true;
//                    break;
//                }
//            }

//            if (IncludingEmpty || hasContent) sheetNames.Add(sheet.SheetName);
//        }

//        return sheetNames.ToArray();
//        #endregion
//    }
//}