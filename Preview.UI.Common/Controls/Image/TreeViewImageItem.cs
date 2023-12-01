using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Xylia.Preview.UI.Controls;
public class TreeViewImageItem : TreeViewItem
{
	#region Ctor
	public TreeViewImageItem()
	{
		StackPanel stack = new StackPanel();
		stack.Orientation = Orientation.Horizontal;
		base.Header = stack;

		var icon = new Image
		{
			VerticalAlignment = VerticalAlignment.Center,
			Height = 18,
			Width = 18,
			Margin = new Thickness(0, 0, 4, 0),
		};
		var textBlock = new TextBlock
		{
			VerticalAlignment = VerticalAlignment.Center
		};

		BindingOperations.SetBinding(icon, System.Windows.Controls.Image.SourceProperty, new Binding { Source = this, Path = new PropertyPath(ImageProperty) });
		BindingOperations.SetBinding(textBlock, TextBlock.TextProperty, new Binding { Source = this, Path = new PropertyPath(HeaderProperty) });

		stack.Children.Add(icon);
		stack.Children.Add(textBlock);
	}
	#endregion


	#region Public Properties
	public ImageSource Image
	{
		get { return (ImageSource)GetValue(ImageProperty); }
		set { SetValue(ImageProperty, value); }
	}

	/// <summary>
	/// DependencyProperty for Image property.
	/// </summary>
	/// <seealso cref="Image.Source" />
	public static readonly DependencyProperty ImageProperty =
		DependencyProperty.Register("Image", typeof(ImageSource), typeof(TreeViewImageItem));


	public new string Header
	{
		get { return (string)GetValue(HeaderProperty); }
		set { SetValue(HeaderProperty, value); }
	}

	public new static readonly DependencyProperty HeaderProperty =
		DependencyProperty.Register("Header", typeof(string), typeof(TreeViewImageItem));
	#endregion
}