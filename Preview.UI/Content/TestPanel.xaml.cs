using System.Windows.Controls;
using CUE4Parse.UE4.Objects.Core.Math;
using SkiaSharp;
using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.UI.Controls;

namespace Xylia.Preview.UI.Content;
public partial class TestPanel
{
	public TestPanel()
	{
		FileCache.Data = new(new FolderProvider(@"G:\新建文件夹"));
		InitializeComponent();

		//BindingOperations.SetBinding(ItemMapPanel_MapField_ScrollBar_SliderBar, BnsCustomRangeBaseWidget.MaximumProperty, new Binding("MaxZoomRatio") { Source = ItemMapPanel_MapField });
		//BindingOperations.SetBinding(ItemMapPanel_MapField_ScrollBar_SliderBar, BnsCustomRangeBaseWidget.MinimumProperty, new Binding("MinZoomRatio") { Source = ItemMapPanel_MapField });
		//BindingOperations.SetBinding(ItemMapPanel_MapField_ScrollBar_SliderBar, BnsCustomRangeBaseWidget.ValueProperty, new Binding("Ratio") { Source = ItemMapPanel_MapField });

		//ItemMapPanel_MapField_NodeTemplate.ExpansionComponentList["Node_ItemName"]?.SetValue("<font name=\"00008130.UI.Normal_Out_14\">123</font>");
		//ItemMapPanel_MapField_HorizontalRulerItemTemplate.ExpansionComponentList["TreeTitle"]?.SetValue("<font name=\"00008130.UI.Normal_Out_14\">挑战武器</font>");

		//ItemMapPanel_JobComboBox_List.ItemsSource = new List<string> { "job1", "job2" };
		//ItemMapPanel_Search.TextChanged += ItemMapPanel_Search_TextChanged;
	}

	private void LoadPictures()
	{
		var source = SKBitmap.Decode(@"C:\Users\10565\Desktop\scene\Output\Exports\BNSR\Content\Art\UI\V2\Resource\Neo\Loading\Neo_Flipbook.png");
		var test = BnsCustomImageWidget.Flipbook(source, new FVector2D(256, 128), 5);
		this.ApplyAnimationClock(BackgroundProperty, test.CreateClock());
	}


	private void ItemMapPanel_Search_TextChanged(object sender, TextChangedEventArgs e)
	{
		//ItemMapPanel_ClearSearchTextBtn.Visibility = string.IsNullOrEmpty(ItemMapPanel_Search.Text) ? Visibility.Hidden : Visibility.Visible;
	}
}