using CUE4Parse.UE4.Assets.Exports.BuildData;

using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.DatData.DataProvider;
using Xylia.Preview.GameUI.Scene.Game_CharacterInfo;

using Application = System.Windows.Forms.Application;


namespace Xylia.Preview.UI;
public partial class DebugFrm : Form
{
	#region Constructor	   	
	public DebugFrm() => InitializeComponent();

	[STAThread]
	static void Main()
	{
		Application.EnableVisualStyles();
		Application.SetHighDpiMode(HighDpiMode.SystemAware);
		Application.SetCompatibleTextRenderingDefault(false);


		// register
		Helper.Register.Main();
		//TextExtension.data_replace = new LocalDataTableSet(@"C:\腾讯游戏\Blade_and_Soul\新建文件夹\local64 - copy.dat");
		FileCache.Data.Provider = new FolderProvider(@"D:\资源\客户端相关\Auto\data");


		Application.Run(new DebugFrm());
		//new DebugFrm2().ShowDialog();
	}
	#endregion



	private void DebugFrm_Load(object sender, EventArgs e)
	{
		//TestTooltip2.SetTooltip(this.contentPanel2, "<p justification=\"true\" justificationtype=\"linefeedbywidgetarea\"><link id=\"none\"/> </p><p horizontalalignment=\"center\"><br/><image enablescale=\"false\" imagesetpath=\"00027918.InterD_ChungGakjiBu\"/><br/><image enablescale=\"true\" imagesetpath=\"00009499.Field_Boss\" scalerate=\"1.4\"/>铁傀王<br/><br/>中原的海盗组织——冲角团的平南舰队支部。<br/>支部长是啸四海。</p>");

		new Game_CharacterInfo_Scene().ShowDialog();
	}

	private void TestMap(string name)
	{
		var MapRegistry = FileCache.Provider.LoadObject<UMapBuildDataRegistry>($"/Game/bns/Package/World/Area/{name}_BuiltData");
		throw new Exception(MapRegistry.ToString());
	}
}