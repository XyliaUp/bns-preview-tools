using System;

using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace Xylia.Preview.UI.Controls;
public class BnsWebBrowser : WebView2
{
	public event EventHandler<string> PostMessage;

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
		CoreWebView2.Settings.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.104 Safari/537.36 BnsIngameBrowser";
		CoreWebView2.AddHostObjectToScript("WebObject", this);
	}

	public void Message(string meaasge)
	{
		if (meaasge is null) return;
		PostMessage?.Invoke(this, meaasge);
	}
}