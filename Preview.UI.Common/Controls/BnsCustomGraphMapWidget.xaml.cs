using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.UI.Controls;
public partial class BnsCustomGraphMapWidget
{
	#region Fields
	double scale = 1;

	ContentControl Starting;
	ContentControl Destination;
	#endregion

	#region Constructor
	public BnsCustomGraphMapWidget()
	{
		InitializeComponent();
	}
	#endregion

	#region Public Properties
	public static readonly DependencyProperty CellSizeProperty = DependencyProperty.Register("CellSize",
		typeof(Size), typeof(BnsCustomGraphMapWidget), new FrameworkPropertyMetadata(new Size(150, 150),
			FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

	public static readonly DependencyProperty ShowLinesProperty = DependencyProperty.Register("ShowLines",
		typeof(bool), typeof(BnsCustomGraphMapWidget), new FrameworkPropertyMetadata(false,
			FrameworkPropertyMetadataOptions.AffectsRender));

	public Size CellSize { get => (Size)this.GetValue(CellSizeProperty); set => this.SetValue(CellSizeProperty, value); }

	public bool ShowLines { get => (bool)this.GetValue(ShowLinesProperty); set => this.SetValue(ShowLinesProperty, value); }
	#endregion



	#region Protected Methods
	protected override Size MeasureOverride(Size constraint)
	{
		int MaxColumn = 0, MaxRow = 0;
		Size childConstraint = new Size(double.PositiveInfinity, double.PositiveInfinity);

		foreach (UIElement child in this.InternalChildren)
		{
			if (child == null) continue;
			child.Measure(childConstraint);

			MaxRow = Math.Max(MaxRow, Grid.GetRow(child) + 1);
			MaxColumn = Math.Max(MaxColumn, Grid.GetColumn(child) + 1);
		}

		return new Size(MaxColumn * CellSize.Width, MaxRow * CellSize.Height);
	}

	protected override Size ArrangeOverride(Size arrangeSize)
	{
		var size = CellSize;

		foreach (UIElement child in this.InternalChildren)
		{
			if (child == null) continue;

			if (child is GraphMapEdge)
			{
				child.Arrange(new Rect(new Point(), child.DesiredSize));
			}
			else
			{
				var column = Grid.GetColumn(child);
				var row = Grid.GetRow(child);

				//Compute offset of the child
				double x = column * size.Width + ((size.Width - child.DesiredSize.Width) / 2);
				double y = row * size.Height + ((size.Height - child.DesiredSize.Height) / 2);

				child.Arrange(new Rect(new Point(x, y), child.DesiredSize));
			}
		}

		return arrangeSize;
	}

	protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
	{
		base.OnPreviewMouseWheel(e);

		if (!Keyboard.IsKeyDown(Key.LeftCtrl)) return;

		if (e.Delta < 0)
		{
			if (scale <= 0.75) return;
			scale -= 0.1;
		}
		else
		{
			if (scale >= 1.25) return;
			scale += 0.1;
		}

		this.LayoutTransform = new ScaleTransform() { ScaleX = scale, ScaleY = scale };
		e.Handled = true;
	}

	protected override void OnRender(DrawingContext dc)
	{
		base.OnRender(dc);

		if (this.ShowLines)
		{
			var size = CellSize;
			var DefaultBorder = Color.FromArgb(52, 0, 255, 110);

			int MaxColumn = 0, MaxRow = 0;
			foreach (UIElement child in InternalChildren)
			{
				if (child == null) continue;

				MaxRow = Math.Max(MaxRow, Grid.GetRow(child) + 1);
				MaxColumn = Math.Max(MaxColumn, Grid.GetColumn(child) + 1);
			}

			for (int column = 0; column < MaxColumn; column++)
			{
				for (int row = 0; row < MaxRow; row++)
				{
					double x = column * size.Width;
					double y = row * size.Height;

					var rect = new Rect(x, y, size.Width, size.Height);
					dc.DrawRectangle(new SolidColorBrush(), new Pen(new SolidColorBrush(DefaultBorder), 1), rect);

					dc.DrawText(new FormattedText($"{row}-{column}",
						CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 10, Brushes.Black, 96),
						new Point(x, y));
				}
			}
		}
	}
	#endregion

	#region Private Helper
	public void Update(object type)
	{
		var seq = type.ToString().ToEnum<EquipType>();

		var table = FileCache.Data.Get<ItemGraph>();
		var seeds = table.Where(record => record is ItemGraph.Seed seed && seed.ItemEquipType == seq).Cast<ItemGraph.Seed>();
		if (!seeds.Any()) return;


		#region node
		this.Children.Clear();

		var items = new Dictionary<Item, ContentControl>();
		foreach (var seed in seeds)
		{
			var item = seed.SeedItem.First();

			var widget = new ContentControl { Tag = item.Instance };
			Grid.SetRow(widget, seed.Row);
			Grid.SetColumn(widget, seed.Column);

			items[item.Instance] = widget;
			this.Children.Add(widget);
		}
		#endregion

		#region line
		var edges = table.OfType<ItemGraph.Edge>().ToLookup(seed => seed.StartItem.Instance);

		foreach (var item in items)
		{
			if (!edges.Contains(item.Key)) continue;

			foreach (var lines in edges[item.Key].GroupBy(x => x.EndItem.Instance))
			{
				if (!items.TryGetValue(lines.Key, out var endItem)) continue;

				// after node load completely
				item.Value.Loaded += new((o, args) =>
				{
					// TODO: Add Pin at node
					Vector vector;
					foreach (var edge in lines)
					{
						//edge.EndOrientation;

						vector.X += 5;
						this.Children.Add(new GraphMapEdge(edge, item.Value, endItem, vector));
					}
				});
			}
		}
		#endregion


		//Scroller.ScrollToRightEnd();
	}

	private void SetStartingPoint(object sender, RoutedEventArgs e)
	{
		if (Starting != null) Starting.Uid = null;

		var menu = (sender as MenuItem).Parent as ContextMenu;
		Starting = menu.PlacementTarget as ContentControl;
		Starting.Uid = "Starting";

		Cacl();
	}

	private void SetDestination(object sender, RoutedEventArgs e)
	{
		if (Destination != null) Destination.Uid = null;

		var menu = (sender as MenuItem).Parent as ContextMenu;
		Destination = menu.PlacementTarget as ContentControl;
		Destination.Uid = "Destination";

		Cacl();
	}


	private void Cacl()
	{
		if (Starting?.Tag is not Item start ||
			Destination?.Tag is not Item dest) return;

		var table = FileCache.Data.Get<ItemGraph>();
		var edges = table.OfType<ItemGraph.Edge>().ToLookup(seed => seed.EndItem.Instance);

		ConcurrentBag<ItemGraph.Edge[]> routes = [];
		Test(start, dest, 0, []);

		void Test(Item start, Item dest, byte mode, ItemGraph.Edge[] route)
		{
			// only definite path									 
			var paths = edges[dest].DistinctBy(x => x.StartItem);

			foreach (var edge in paths)
			{
				// check current
				if (!edges.Contains(dest)) continue;
				dest = edge.StartItem.Instance;

				// create route copy
				var _route = new ItemGraph.Edge[route.Length + 1];
				Array.Copy(route, 0, _route, 0, route.Length);
				_route[^1] = edge;

				// next step
				if (start.Equals(dest)) routes.Add(_route);
				else Test(start, dest, mode, _route);
			}
		}


		MessageBox.Show($"{routes.Count}");
		if (!routes.IsEmpty)
		{
			var route = routes.FirstOrDefault();
			MessageBox.Show(route.Aggregate("", (a, n) => a + n.StartItem.Instance.ItemNameOnly + " -> "));
		}
	}
	#endregion
}

internal sealed class GraphMapEdge : UserControl
{
	private static readonly Brush DefiniteBrush = Brushes.Green;
	private static readonly Brush StochasticBrush = Brushes.Blue;


	#region Properties
	public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source",
		typeof(Point), typeof(GraphMapEdge), new FrameworkPropertyMetadata(default(Point)));

	public static readonly DependencyProperty DestinationProperty = DependencyProperty.Register("Destination",
		typeof(Point), typeof(GraphMapEdge), new FrameworkPropertyMetadata(default(Point)));

	public Point Source { get => (Point)this.GetValue(SourceProperty); set => this.SetValue(SourceProperty, value); }

	public Point Destination { get => (Point)this.GetValue(DestinationProperty); set => this.SetValue(DestinationProperty, value); }
	#endregion


	public GraphMapEdge()
	{
		LineSegment segment = new LineSegment(default, true);
		BindingOperations.SetBinding(segment, LineSegment.PointProperty, new Binding { Source = this, Path = new PropertyPath(DestinationProperty) });

		PathFigure figure = new PathFigure(default, new[] { segment }, false);
		BindingOperations.SetBinding(figure, PathFigure.StartPointProperty, new Binding { Source = this, Path = new PropertyPath(SourceProperty) });

		Geometry geometry = new PathGeometry(new[] { figure });
		Path path = new Path { Data = geometry, StrokeThickness = 3 };
		BindingOperations.SetBinding(path, Shape.StrokeProperty, new Binding { Source = this, Path = new PropertyPath(ForegroundProperty) });

		Content = path;
	}

	public GraphMapEdge(ItemGraph.Edge Edge, UIElement StartItem, UIElement EndItem, Vector Offset) : this()
	{
		ToolTip = Edge.Source.ToString();  // Edge.FeedRecipe.Instance;
		Foreground = Edge.SuccessProbability == ItemGraph.Edge.SuccessProbabilitySeq.Definite ? DefiniteBrush : StochasticBrush;

		// TODO: Add Pin at node
		var p = VisualTreeHelper.GetParent(StartItem) as UIElement;

		var rect1 = new Rect(StartItem.TranslatePoint(new Point(), p), StartItem.DesiredSize);
		var rect2 = new Rect(EndItem.TranslatePoint(new Point(), p), EndItem.DesiredSize);

		Point sour, dest;
		if (rect1.Y == rect2.Y)
		{
			var y = rect1.Y + rect1.Height / 2;

			sour = new Point(rect1.X + rect1.Width - (rect1.Width - 64) / 2, y);
			dest = new Point(rect2.X + (rect2.Width - 64) / 2, y);
		}
		else
		{
			sour = new Point(rect1.X + rect1.Width / 2, rect1.Y);
			dest = new Point(rect2.X + rect2.Width / 2, rect2.Y + rect2.Height);
		}

		Source = sour + Offset;
		Destination = dest + Offset;
	}
}