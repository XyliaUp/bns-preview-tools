using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Xylia.Preview.UI.Controls;
public class TreeViewImageItem : TreeViewItem
{
	#region Constructors	  
	static TreeViewImageItem()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(TreeViewImageItem), new FrameworkPropertyMetadata(typeof(TreeViewImageItem)));
	}
	#endregion

	#region Public Properties
	/// <summary>
	/// DependencyProperty for Image property.
	/// </summary>
	/// <seealso cref="Image.Source" />
	public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(TreeViewImageItem));

	public ImageSource Image
	{
		get { return (ImageSource)GetValue(ImageProperty); }
		set { SetValue(ImageProperty, value); }
	}
	#endregion
}

public class TreeViewItemMarginConverter : MarkupExtension, IValueConverter
{
	public override object ProvideValue(IServiceProvider serviceProvider) => this;

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		var left = 0.0;
		UIElement element = value as TreeViewItem;
		while (element != null && element.GetType() != typeof(TreeView))
		{
			element = (UIElement)VisualTreeHelper.GetParent(element);
			if (element is TreeViewItem)
				left += 19.0;
		}
		return new Thickness(left, 0, 0, 0);
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotSupportedException();
	}
}