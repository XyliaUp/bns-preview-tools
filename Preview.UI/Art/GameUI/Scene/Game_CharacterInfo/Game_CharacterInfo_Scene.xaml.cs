using System.Web;
using System.Windows;

using Xylia.Extension;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.DatData.DataProvider;
using Xylia.Xml;

namespace Xylia.Preview.GameUI.Scene.Game_CharacterInfo;
public partial class Game_CharacterInfo_Scene : Window
{
	public Game_CharacterInfo_Scene()
	{
		InitializeComponent();
		InitUrl();
	}

	private async void WebView_WebBrowserInitialized(object sender, EventArgs e)
	{
		await WebView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync($$"""
onmouseover = (e) => {
    var obj = e.target; 
	if (obj.tagName=='IMG' && obj.classList=="")
		chrome.webview.hostObjects.WebObject.Message(obj.getAttribute('title')); 
};
""");
	}

	private async void WebView_PostMessage(object sender, string meaasge)
	{
		var uri = new Uri(meaasge);
		if (uri.Scheme == "nc")
		{
			var query = HttpUtility.ParseQueryString(uri.Query);
			if (uri.Host == "bns.charinfo" && uri.AbsolutePath == "/ItemTooltip")
			{
				var data = query["item"].Split('.').Select(int.Parse).ToArray();
				Trace.WriteLine(data.Aggregate("", (sum, now) => sum + now + ";"));

				//Task.Run(() => FileCache.Data.Item[data[0], data[1]].PreviewShow());
			}
		}
	}


	public void InitUrl()
	{
		FileCache.Data.LoadData(false);
		var doc = (FileCache.Data.Provider as DefaultProvider).ConfigData.EnumerateFiles("release.config2.xml").FirstOrDefault()?.Xml.Nodes;

		var group = doc.SelectSingleNode("config/group[@name='in-game-web']");
		var CharacterInfoUrl = group.SelectSingleNode("./option[@name='character-info-url']").GetValue();
		var CharacterInfoUrl2 = group.SelectSingleNode("./option[@name='character-info-url-2']").GetValue();

		var CharacterInfoHomeUrn = group.SelectSingleNode("./option[@name='character-info-home-urn']").GetValue();
		var CharacterInfoOtherHomeUrn = group.SelectSingleNode("./option[@name='character-info-other-home-urn']").GetValue();
		var CharacterInfoDiffHomeUrn = group.SelectSingleNode("./option[@name='character-info-diff-home-urn']").GetValue();


		var role = new Creature() { WorldId = 1911, Name = "天靑色等煙雨乀" };
		WebView.Source = new UriBuilder(CharacterInfoUrl.Replace("%s", role.WorldId.ToString()[..2]) + CharacterInfoHomeUrn) { Query = $"c={role.Name}&s={role.WorldId}" }.Uri;
	}
}