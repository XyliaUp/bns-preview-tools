using System.ComponentModel;

using Xylia.Configure;
using Xylia.Preview.Data.Models.BinData.Table;
using Xylia.Workbook;

namespace Xylia.Preview.Data.Helper.Output;
public abstract partial class OutBase
{
	#region Fields
	public ITable data;


	protected virtual string Name => GetType().Name;


	protected ExcelInfo ExcelInfo = null;
	#endregion

	#region Functions
	public void Output(ITable data = null, string path = null , Action<string> msg = null)
	{
		#region Initialize
		var res = new ComponentResourceManager(typeof(OutBase));
		if (path is null)
		{
			if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
			{
				path = PathDefine.Desktop + "\\test.xlsx";
			}
			else
			{
				var save = new SaveFileDialog
				{
					Filter = "Excel Files|*.xlsx",
					FileName = $"{Name} ({DateTime.Now.ToString(res.GetString("Format_yyMM"))}).xlsx",

					InitialDirectory = Ini.ReadValue("Folder", "OutputExcel")
				};
				if (save.ShowDialog() != DialogResult.OK) return;

				path = save.FileName;
				Ini.WriteValue("Folder", "OutputExcel", Path.GetDirectoryName(path));
				msg?.Invoke(res.GetString("output_start"));
			} 
		}
		#endregion

		#region Functions
		DateTime dt = DateTime.Now;
		ExcelInfo = new ExcelInfo(Name);

		this.data = data;
		CreateData();

		ExcelInfo.Save(path);

		msg?.Invoke(res.GetString("output_finish") + $"{(DateTime.Now - dt).TotalSeconds:#0}s");
		ExcelInfo.Dispose();
		ExcelInfo = null;

		GC.Collect();
		#endregion
	}

	protected abstract void CreateData();
	#endregion


	public static void StartOutput<T>() where T : OutBase, new()
	{
		var thread = new Thread(act => new T().Output(msg: msg => PreviewRegister.ShowTip(msg)));
		thread.SetApartmentState(ApartmentState.STA);
		thread.Start();
	}
}