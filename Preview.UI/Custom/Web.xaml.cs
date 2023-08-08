using System.Windows;

using Microsoft.Web.WebView2.Core;

namespace Xylia.Preview.UI.Custom;
public partial class Web : Window
{
	public Web()
	{
		InitializeComponent();
	}

	private void WebView_InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
	{
		if (!e.IsSuccess) return;

		//WebView.CoreWebView2.Settings.AreDevToolsEnabled = false;
		//WebView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
		//WebView.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = false;
		//WebView.CoreWebView2.Settings.IsStatusBarEnabled = false;
	}
}