using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace Xylia.Preview.UI.Custom.Controls;
public class BnsWebBrowser : WebView2
{
	public event EventHandler<string> PostMessage;
	public event EventHandler WebBrowserInitialized;


	public BnsWebBrowser()
	{
		this.CoreWebView2InitializationCompleted += InitializationCompleted;
	}

	private void InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
	{
		if (!e.IsSuccess) return;

		CoreWebView2.Settings.AreDevToolsEnabled = false;
		CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
		CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = false;
		CoreWebView2.Settings.IsStatusBarEnabled = false;
		CoreWebView2.Settings.UserAgent = "BnsIngameBrowser";
		CoreWebView2.AddHostObjectToScript("WebObject", this);

		WebBrowserInitialized?.Invoke(this, new());
	}
	   
	public void Message(string meaasge)
	{
		if (meaasge is null) return;
		PostMessage?.Invoke(this, meaasge);
	}
}