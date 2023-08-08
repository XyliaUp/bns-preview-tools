using System.Diagnostics;

using BnsBinTool.Core.DataStructs;

using Xylia.Extension;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.Tests.DatTool;
static class Program
{
	[STAThread]
	static void Main()
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		Application.Run(new Windows.MainForm());
	}
}