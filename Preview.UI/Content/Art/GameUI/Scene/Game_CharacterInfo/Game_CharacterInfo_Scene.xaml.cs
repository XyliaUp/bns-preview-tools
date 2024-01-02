using System.ComponentModel;
using System.Diagnostics;

using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models.Config;
using Xylia.Preview.Data.Models.Creature;

namespace Xylia.Preview.UI.GameUI.Scene.Game_CharacterInfo;
public partial class Game_CharacterInfo_Scene
{
	#region Constructor
	private string CharacterInfoUrl;
	private string CharacterInfoUrl2;

	private string CharacterInfoHomeUrn;
	private string CharacterInfoOtherHomeUrn;
	private string CharacterInfoDiffHomeUrn;

	public Game_CharacterInfo_Scene()
	{
		InitializeComponent();

		var config = (FileCache.Data.Provider as DefaultProvider)?.ConfigData.EnumerateFiles("release.config2.xml").FirstOrDefault();
		var release = ConfigTable.LoadFrom<Release>(config?.Data);
		if (release is null) throw new Exception("invalid define");

		var group = release.group.First(x => x.name == "in-game-web");
		CharacterInfoUrl = group["character-info-url"]?.value;
		CharacterInfoUrl2 = group["character-info-url-2"]?.value;
		CharacterInfoHomeUrn = group["character-info-home-urn"]?.value;
		CharacterInfoOtherHomeUrn = group["character-info-other-home-urn"]?.value;
		CharacterInfoDiffHomeUrn = group["character-info-diff-home-urn"]?.value;

		InitUrl(new Creature() { WorldId = 1911, Name = "三千问乀" });
	}
	#endregion

	#region Methods
	private async void WebView_WebBrowserInitialized(object sender, Microsoft.Web.WebView2.Core.CoreWebView2 e)
	{
		await e.AddScriptToExecuteOnDocumentCreatedAsync($$"""
		onmouseover = (e) => {
		    var obj = e.target; 
			if (obj.tagName != 'IMG') return;

		    obj.removeAttribute('title');

			var parent = $(obj).parent('.item-img');
			if (parent != null) chrome.webview.hostObjects.WebObject.Message(parent.data('tooltip')); 
		};
		""");
	}

	private async void WebView_PostMessage(object sender, string meaasge)
	{
		await Task.Run(() =>
		{
			var data = meaasge.Split('.').Select(int.Parse).ToArray();
			Trace.WriteLine(data.Aggregate("", (sum, now) => sum + now + ";"));


			//var uri = new Uri(meaasge);
			//if (uri.Scheme == "nc")
			//{
			//	var query = HttpUtility.ParseQueryString(uri.Query);
			//	if (uri.Host == "bns.charinfo" && uri.AbsolutePath == "/ItemTooltip")
			//	{
			//		var data = query["item"].Split('.').Select(int.Parse).ToArray();
			//		Trace.WriteLine(data.Aggregate("", (sum, now) => sum + now + ";"));

			//		//Task.Run(() => FileCache.Data.Item[data[0], data[1]].PreviewShow());
			//	}
			//}
		});
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		//CharacterInfoPanelWeb.Dispose();
		CharacterInfoPanelWeb = null;
	}


	public void InitUrl(Creature creature)
	{
		CharacterInfoPanelWeb.Source = new UriBuilder(CharacterInfoUrl.Replace("%s", creature.WorldId.ToString()[..2]) + CharacterInfoHomeUrn) { Query = $"c={creature.Name}&s={creature.WorldId}" }.Uri;
	}
	#endregion
}