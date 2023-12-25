using System;
using System.Windows;
using System.Windows.Controls;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Controls;
interface IBnsCustomBaseWidget
{
	ChildCollection Items { get; }
}

public class BnsCustomBaseWidget : ItemsControl
{
	#region Constructor
	public BnsCustomBaseWidget()
	{
		this.ItemsPanel = new ItemsPanelTemplate(new FrameworkElementFactory(typeof(AnchorPanel)));
		this.ItemsPanel.Seal();
	}
	#endregion


	#region DependencyProperty 
	public static readonly DependencyProperty MetaDataProperty = DependencyProperty.Register(nameof(MetaData), typeof(string), typeof(BnsCustomBaseWidget),
		new PropertyMetadata(null, OnMetaDataChanged));

	public string MetaData
	{
		get { return (string)GetValue(MetaDataProperty); }
		set { SetValue(MetaDataProperty, value); }
	}


	public static readonly DependencyProperty StringProperty = DependencyProperty.Register(nameof(String), typeof(string), typeof(BnsCustomBaseWidget));

	public string String
	{
		get { return (string)GetValue(StringProperty); }
		set { SetValue(StringProperty, value); }
	}
	#endregion

	#region Dependency Helpers
	private static async void OnMetaDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var widget = (BnsCustomBaseWidget)d;

		if (e.NewValue is string meta)
		{
			var ls = meta.Split('=', 2);
			if (ls.Length < 2) return;

			await FileCache.Data.Text.LoadAsync();

			if (ls[0] == "textref") widget.SetText(ls[1].GetText());
			if (ls[0] == "tooltip") widget.SetTooltip(ls[1].GetText());
		}
	}
	#endregion


	#region Protected Methods
	protected override void OnInitialized(EventArgs e)
	{
		base.OnInitialized(e);

		//SetText(this.String);
	}

	protected virtual void SetText(string text)
	{

	}

	protected virtual void SetTooltip(string tooltip)
	{
		this.ToolTip = tooltip;
	}
	#endregion
}