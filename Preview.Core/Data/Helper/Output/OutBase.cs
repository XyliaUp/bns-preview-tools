using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using Xylia.Configure;
using Xylia.Preview.GameUI.Controls;
using Xylia.Workbook;

namespace Xylia.Preview.Data.Helper.Output
{
	public abstract class OutBase
    {
        #region Fields
        protected virtual string Name => GetType().Name;


        protected ExcelInfo ExcelInfo = null;
        #endregion

        #region Functions
        public void Output()
        {
			#region Initialize
			var Save = new SaveFileDialog
			{
				Filter = "表格文件|*.xlsx",
				FileName = $"{Name} ({DateTime.Now:yy年MM月}).xlsx",

				InitialDirectory = Ini.ReadValue("Folder", "OutputExcel")
			};

			if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
			{
				if (Save.ShowDialog() != DialogResult.OK) return;
				Ini.WriteValue("Folder", "OutputExcel", Path.GetDirectoryName(Save.FileName));

				//FrmTips.ShowTipsSuccess("开始执行, 请等待结束提示");
			}
			else
			{
				Save.FileName = PathDefine.Desktop + "\\test.xlsx";
			}
			#endregion

			#region Functions
			DateTime dt = DateTime.Now;

			ExcelInfo = new ExcelInfo(Name);
			CreateData();
			ExcelInfo.Save(Save.FileName);

			//FrmTips.ShowTipsWarning($"执行已完成, 耗时{(int)(DateTime.Now - dt).TotalSeconds}s");

			ExcelInfo.Dispose();
			ExcelInfo = null;

			GC.Collect();
			#endregion
		}

		protected virtual void CreateData()
        {

        }
        #endregion



        #region Static Functions
        public static string GetCount(int count) => " " + new ContentParams(count).Handle("<arg p=\"1:integer\"/>个");
        #endregion
    }
}