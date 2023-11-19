using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Helpers;

using static Xylia.Preview.Data.Models.ItemGraph;

namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_ItemMap;
[DesignTimeVisible(false)]
public partial class BnsCustomGraphMapWidget : Panel
{
	#region Constructor
	public BnsCustomGraphMapWidget()
	{
		InitializeComponent();
	}
	#endregion

	#region EquipType
	public EquipType EquipType
	{
		set => OnEquipTypeChanged(value);
	}

	private void OnEquipTypeChanged(EquipType value)
	{
		if (value == EquipType.None) return;

		#region item
		this.Children.Clear();

		var table = FileCache.Data.ItemGraph;
		var seeds = table.Where(record => record is Seed seed && seed.ItemEquipType == value).Cast<Seed>();
		if (!seeds.Any()) return;

		var items = new Dictionary<string, ContentControl>();
		foreach (var seed in seeds)
		{
			var item = seed.SeedItem.First();

			var widget = new ContentControl { Tag = item.Instance };
			Grid.SetRow(widget, seed.Row);
			Grid.SetColumn(widget, seed.Column);

			items[item.Alias] = widget;
			this.Children.Add(widget);
		}
		#endregion

		#region path
		var definition = table.Definition.ElRecord.SubtableByType(1)?["start-item"];
		foreach (var item in items)
		{
			var startItem = item.Value;
			foreach (var edges in table.Search(definition, item.Key).GroupBy(x => x.Attributes["end-item"]))
			{
				if (!items.TryGetValue(edges.Key, out var endItem)) continue;

				startItem.Loaded += new((o, args) =>
				{
					var rect1 = new Rect(startItem.TranslatePoint(new Point(), this), startItem.DesiredSize);
					var rect2 = new Rect(endItem.TranslatePoint(new Point(), this), endItem.DesiredSize);

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


					// get recipe total count between start with end
					foreach (var edge in edges.Select(x => x.Model.Value).OfType<Edge>())
					{
						sour.X += 5;
						dest.X += 5;
						this.Children.Add(new Connector()
						{
							ToolTip = edge.FeedRecipe,
							Source = sour,
							Destination = dest,
							Foreground = edge.SuccessProbability == Edge.SuccessProbabilitySeq.Definite ? Brushes.Green : Brushes.Blue,
						});
					}
				});
			}
		}
		#endregion
	}
	#endregion

	#region Events
	ContentControl Starting;
	ContentControl Destination;

	private void SetStartingPoint(object sender, RoutedEventArgs e)
	{
		if (Starting != null) Starting.Uid = null;

		var menu = (sender as MenuItem).Parent as ContextMenu;
		Starting = menu.PlacementTarget as ContentControl;
		Starting.Uid = "Starting";
	}

	private void SetDestination(object sender, RoutedEventArgs e)
	{
		if (Destination != null) Destination.Uid = null;

		var menu = (sender as MenuItem).Parent as ContextMenu;
		Destination = menu.PlacementTarget as ContentControl;
		Destination.Uid = "Destination";
	}
	#endregion

	#region Protected Methods
	const int width = 130;
	const int height = 150;

	/// <summary>
	/// Content measurement.
	/// </summary>
	/// <param name="constraint">Constraint</param>
	/// <returns>Desired size</returns>
	protected override Size MeasureOverride(Size constraint)
	{
		int MaxColumn = 0, MaxRow = 0;
		Size childConstraint = new Size(double.PositiveInfinity, double.PositiveInfinity);

		foreach (UIElement child in this.InternalChildren)
		{
			if (child == null) continue;
			child.Measure(childConstraint);

			if (Grid.GetColumn(child) == int.MaxValue) continue;
			MaxRow = Math.Max(MaxRow, Grid.GetRow(child) + 1);
			MaxColumn = Math.Max(MaxColumn, Grid.GetColumn(child) + 1);
		}

		return new Size(MaxColumn * width, MaxRow * height);
	}

	/// <summary>
	/// Content arrangement.
	/// </summary>
	/// <param name="arrangeSize">Arrange size</param>
	protected override Size ArrangeOverride(Size arrangeSize)
	{
		Size gridDesiredSize = new Size(width, height);

		foreach (UIElement child in this.InternalChildren)
		{
			if (child == null) continue;
			if (child is Connector)
			{
				child.Arrange(new Rect(new Point(0, 0), child.DesiredSize));
				continue;
			}

			//Compute offset of the child:
			var column = Grid.GetColumn(child);
			var row = Grid.GetRow(child);

			double x = column * width + ((width - child.DesiredSize.Width) / 2);
			double y = row * height + ((height - child.DesiredSize.Height) / 2);

			child.Arrange(new Rect(new Point(x, y), child.DesiredSize));
		}

		return arrangeSize;
	}


	protected override void OnRender(DrawingContext dc)
	{
		var DefaultBorder = Color.FromArgb(52, 0, 255, 110);

		//int MaxColumn = 0, MaxRow = 0;
		//foreach (UIElement child in InternalChildren)
		//{
		//	if (child == null) continue;

		//	MaxRow = Math.Max(MaxRow, Grid.GetRow(child) + 1);
		//	MaxColumn = Math.Max(MaxColumn, Grid.GetColumn(child) + 1);

		//	var rect = new Rect(Canvas.GetLeft(child), Canvas.GetTop(child), width, height);
		//	dc.DrawRectangle(new SolidColorBrush(), new Pen(new SolidColorBrush(DefaultBorder), 1), rect);
		//}

		//for (int column = 0; column < MaxColumn; column++)
		//{
		//	for (int row = 0; row < MaxRow; row++)
		//	{
		//		double x = column * width;
		//		double y = row * height;
		//		dc.DrawRectangle(new SolidColorBrush(), new Pen(new SolidColorBrush(DefaultBorder), 1),
		//				 new Rect(x, y, width, height));

		//		var format = new FormattedText($"{row}-{column}", CultureInfo.CurrentCulture,
		//			FlowDirection.LeftToRight, new Typeface("Arial"), 10, Brushes.Black,
		//			VisualTreeHelper.GetDpi(this).PixelsPerDip);
		//		dc.DrawText(format, new Point(x, y));
		//	}
		//}
	}
	#endregion
}

internal sealed class Connector : UserControl
{
	#region Properties
	public static readonly DependencyProperty SourceProperty =
		DependencyProperty.Register("Source", typeof(Point), typeof(Connector), new FrameworkPropertyMetadata(default(Point)));

	public static readonly DependencyProperty DestinationProperty =
		DependencyProperty.Register("Destination", typeof(Point), typeof(Connector), new FrameworkPropertyMetadata(default(Point)));

	public Point Source { get => (Point)this.GetValue(SourceProperty); set => this.SetValue(SourceProperty, value); }

	public Point Destination { get => (Point)this.GetValue(DestinationProperty); set => this.SetValue(DestinationProperty, value); }
	#endregion


	public Connector()
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
}