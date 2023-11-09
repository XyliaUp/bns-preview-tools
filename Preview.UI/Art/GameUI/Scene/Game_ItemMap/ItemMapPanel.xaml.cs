using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Common.Controls;

using static Xylia.Preview.Data.Models.ItemGraph;

using Point = System.Windows.Point;

namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_ItemMap;
[ToolboxItem(false)]
public partial class ItemMapPanel : UserControl
{
	public ItemMapPanel()
	{
		InitializeComponent();
	}

	#region Events
	double scale = 1;

	private void PanelMouseWheel(object sender, MouseWheelEventArgs e)
	{
		if (!Keyboard.IsKeyDown(Key.LeftCtrl)) return;
		//if (scale <= 0.5 || scale >= 1.5) return;

		if (e.Delta < 0) scale -= 0.1;
		else scale += 0.1;

		root.LayoutTransform = new ScaleTransform() { ScaleX = scale, ScaleY = scale, };
		e.Handled = true;
	}
	#endregion

	const int width = 130;
	const int height = 150;



	#region EquipType
	public EquipType EquipType
	{
		get => (EquipType)GetValue(EquipTypeProperty);
		set => SetValue(EquipTypeProperty, value);
	}

	public static readonly DependencyProperty EquipTypeProperty =
		DependencyProperty.Register(nameof(EquipType), typeof(object), typeof(ItemMapPanel),
			  new PropertyMetadata((o, a) => (o as ItemMapPanel)?.OnEquipTypeChanged((EquipType)a.NewValue)));

	private void OnEquipTypeChanged(EquipType value)
	{
		if (value == EquipType.None)
			return;

		root.Children.Clear();

		var table = FileCache.Data.ItemGraph;
		var seeds = table.Where(record => record is Seed seed && seed.ItemEquipType == value).Cast<Seed>();
		if (!seeds.Any()) return;


		var items = new Dictionary<string, ItemPanel>();
		foreach (var seed in seeds)
		{
			var item = seed.SeedItem.First();

			var widget = new ItemPanel { Text = item.Instance?.Name2.GetText() ?? item.Alias, Data = item.Instance };
			Canvas.SetLeft(widget, seed.Column * width);
			Canvas.SetTop(widget, seed.Row * height);

			items[item.Alias] = widget;
			root.Children.Add(widget);
		}

		root.Width = seeds.Max(o => o.Column + 2) * width;
		root.Height = seeds.Max(o => o.Row + 1) * height;



		var definition = table.Definition.ElRecord.SubtableByType(1)?["start-item"];
		foreach (var item in items)
		{
			var startItem = item.Value;

			//var edges = table.Search(definition , item.Key).Cast<Edge>();
			//foreach (var edge in edges)
			//{
			//	if (!items.TryGetValue(edge.EndItem.Alias, out var endItem)) continue;

			//	// get recipe total count between start with end
			//	var recipes = edges.Select(o => edge.EndItem == o.EndItem).Count();
			//	startItem.Loaded += new((o, args) =>
			//	{
			//		var rect1 = new Rect(Canvas.GetLeft(startItem), Canvas.GetTop(startItem), startItem.ActualWidth, startItem.ActualHeight);
			//		var rect2 = new Rect(Canvas.GetLeft(endItem), Canvas.GetTop(endItem), endItem.ActualWidth, endItem.ActualHeight);

			//		Point source, dest;
			//		if (rect1.Y == rect2.Y)
			//		{
			//			var y = rect1.Y + rect1.Height / 2;

			//			source = new Point(rect1.X + rect1.Width - (rect1.Width - 64) / 2, y);
			//			dest = new Point(rect2.X + (rect2.Width - 64) / 2, y);
			//		}
			//		else
			//		{
			//			source = new Point(rect1.X + rect1.Width / 2, rect1.Y);
			//			dest = new Point(rect2.X + rect2.Width / 2, rect2.Y + rect2.Height);
			//		}


			//		Connector conn = new Connector();
			//		conn.ToolTip = edge.FeedRecipe;
			//		conn.Source = source;
			//		conn.Destination = dest;
			//		conn.Brushes = Brushes.Blue;

			//		root.Children.Add(conn);
			//	});
			//}
		}


		UIElement target = null;
		if (target != null)
		{
			var targetPosition = target.TransformToVisual(scroller).Transform(new Point(scroller.HorizontalOffset, scroller.VerticalOffset));

			scroller.ScrollToHorizontalOffset(targetPosition.X);
			scroller.ScrollToVerticalOffset(targetPosition.Y);
		}
		else
		{
			scroller.ScrollToRightEnd();
		}
	}
	#endregion




	private void SetStartingPoint(object sender, RoutedEventArgs e)
	{
		var menu = (sender as MenuItem).Parent as ContextMenu;
		var panel = menu.PlacementTarget as ItemPanel;

		Debug.WriteLine(panel.Data);
	}

	private void SetDestination(object sender, RoutedEventArgs e)
	{
		var menu = (sender as MenuItem).Parent as ContextMenu;
		var panel = menu.PlacementTarget as ItemPanel;

		Debug.WriteLine(panel.Data);
	}
}


public sealed class Connector : UserControl
{
	public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(Point), typeof(Connector),
		   new FrameworkPropertyMetadata(default(Point)));

	public static readonly DependencyProperty DestinationProperty = DependencyProperty.Register("Destination", typeof(Point), typeof(Connector),
		 new FrameworkPropertyMetadata(default(Point)));


	public Point Source { get => (Point)this.GetValue(SourceProperty); set => this.SetValue(SourceProperty, value); }

	public Point Destination { get => (Point)this.GetValue(DestinationProperty); set => this.SetValue(DestinationProperty, value); }


	public Brush Brushes = System.Windows.Media.Brushes.Blue;


	public Connector()
	{
		LineSegment segment = new LineSegment(default, true);
		PathFigure figure = new PathFigure(default, new[] { segment }, false);

		PathGeometry geometry = new PathGeometry(new[] { figure });
		BindingBase sourceBinding = new Binding { Source = this, Path = new PropertyPath(SourceProperty) };
		BindingBase destinationBinding = new Binding { Source = this, Path = new PropertyPath(DestinationProperty) };
		BindingOperations.SetBinding(figure, PathFigure.StartPointProperty, sourceBinding);
		BindingOperations.SetBinding(segment, LineSegment.PointProperty, destinationBinding);

		Content = new Path
		{
			Data = geometry,
			StrokeThickness = 5,
			Stroke = Brushes,
			MinWidth = 1,
			MinHeight = 1
		};
	}
}