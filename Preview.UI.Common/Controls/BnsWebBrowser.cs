using System;
using System.Windows.Data;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace Xylia.Preview.UI.Controls;
public class BnsWebBrowser : BnsCustomBaseWidget
{
	private WebView2 _webView;
	public event EventHandler<string> PostMessage;
	public new event EventHandler<CoreWebView2> Initialized;

	public BnsWebBrowser()
	{
		_webView = new WebView2();
		_webView.CoreWebView2InitializationCompleted += InitializationCompleted;

		BindingOperations.SetBinding(_webView, HeightProperty,new Binding("Height") { Source = this });
		BindingOperations.SetBinding(_webView, WidthProperty, new Binding("Width") { Source = this });

		this.Items.Add(_webView);
	}

	private void InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
	{
		if (!e.IsSuccess) return;

		_webView.CoreWebView2.Settings.AreDevToolsEnabled = false;
		_webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
		_webView.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = false;
		_webView.CoreWebView2.Settings.IsStatusBarEnabled = false;
		_webView.CoreWebView2.Settings.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.104 Safari/537.36 BnsIngameBrowser";
		_webView.CoreWebView2.AddHostObjectToScript("WebObject", this);

		Initialized?.Invoke(this, _webView.CoreWebView2);
	}

	public void Message(string meaasge)
	{
		if (meaasge is null) return;
		PostMessage?.Invoke(this, meaasge);
	}

	public Uri Source
	{
		get => _webView.Source;
		set => _webView.Source = value;
	}
}