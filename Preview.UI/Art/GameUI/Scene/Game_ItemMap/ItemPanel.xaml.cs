using System.ComponentModel;
using System.Windows.Controls;

using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Common.Controls;
[ToolboxItem(false)]
public partial class ItemPanel : UserControl
{
	public ItemPanel()
	{
		InitializeComponent();
	}

	public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }

	public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(ItemPanel));


	public Item Data { get => (Item)GetValue(DataProperty); set => SetValue(DataProperty, value); }

	public static readonly DependencyProperty DataProperty = DependencyProperty.Register(nameof(Data), typeof(Item), typeof(ItemPanel));
}