using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using CUE4Parse.BNS.Assets.Exports;
using Xylia.Preview.UI.Controls.Primitives;

namespace Xylia.Preview.UI.Controls;

[ContentProperty("Children")]
public class BnsCustomEditBoxWidget : TextBox, IUserWidget
{
	#region Constructors
	public BnsCustomEditBoxWidget()
	{
		Children = new UIElementCollection(this, this);

		// default style
		this.Background = Brushes.Transparent;
		this.Foreground = Brushes.White;
		this.BorderThickness = new Thickness(0);
	}
	#endregion

	#region UserWidget
	public UIElementCollection Children { get; }

	//protected override int VisualChildrenCount => Children.Count;

	//protected override Visual GetVisualChild(int index) => Children[index] as Visual;
	#endregion


	#region Properties
	public StringProperty String { get; set; }
	#endregion
}

public class BnsCustomEditBoxWidget2 : BnsCustomTextBoxBaseWidget
{
	#region Properties
	public StringProperty String { get; set; }
	#endregion
}