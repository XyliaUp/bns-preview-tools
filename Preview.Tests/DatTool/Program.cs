namespace Xylia.Preview.Tests.DatTool;
static class Program
{
	[STAThread]
	static void Main(string[] args = null)
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		Application.Run(new Windows.MainForm());
	}
}