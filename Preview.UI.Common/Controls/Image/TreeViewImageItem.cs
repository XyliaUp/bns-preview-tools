using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Xylia.Preview.UI.Controls;
public class TreeViewImageItem : TreeViewItem
{
	readonly Image icon;
	readonly TextBlock textBlock;


	public TreeViewImageItem()
	{
		StackPanel stack = new StackPanel();
		stack.Orientation = Orientation.Horizontal;
		Header = stack;

		//Uncomment this code If you want to add an Image after the Node-HeaderText
		//textBlock = new TextBlock();
		//textBlock.VerticalAlignment = VerticalAlignment.Center;
		//stack.Children.Add(textBlock);
		icon = new Image();
		icon.VerticalAlignment = VerticalAlignment.Center;
		icon.Height = 18;
		icon.Width = 18;
		icon.Margin = new Thickness(0, 0, 4, 0);
		stack.Children.Add(icon);

		//Add the HeaderText After Adding the icon
		textBlock = new TextBlock();
		textBlock.VerticalAlignment = VerticalAlignment.Center;
		stack.Children.Add(textBlock);
	}


	public ImageSource Image { get => icon.Source; set => icon.Source = value; }

	public string HeaderText { get => textBlock.Text; set => textBlock.Text = value; }
}