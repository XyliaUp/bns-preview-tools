using System.Collections.Concurrent;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;
using Xylia.Preview.Data.Models.Sequence;
using static Xylia.Preview.Data.Models.Item.Gem;
using static Xylia.Preview.Data.Models.ItemGraph;
using static Xylia.Preview.Data.Models.ItemGraph.Edge;

namespace Xylia.Preview.UI.Controls;
public partial class BnsCustomGraphMapWidget
{
	#region Fields
	double scale = 1;

	bool _isDragging;
	Point _mouseOffset;

	FrameworkElement Starting;
	FrameworkElement Destination;
	#endregion

	#region Event
	public event EventHandler<IEnumerable<ItemGraphRouteHelper>> RoutesChanged;
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

	protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
	{
		if (e.LeftButton == MouseButtonState.Pressed)
		{
			Cursor = Cursors.SizeAll;

			_isDragging = true;
			_mouseOffset = e.GetPosition(this);

			this.CaptureMouse();
		}
	}

	protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
	{
		if (_isDragging)
		{
			_isDragging = false;
			Cursor = Cursors.Arrow;

			this.ReleaseMouseCapture();

			var scroll = VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(this))) as ScrollViewer;
			var pos = e.GetPosition(this) - _mouseOffset;
			scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - pos.X);
			scroll.ScrollToVerticalOffset(scroll.VerticalOffset - pos.Y);
		}
	}


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
			if (child is GraphMapEdge)
			{
				SetZIndex(child, -1);
				child.Arrange(new Rect(new Point(), child.DesiredSize));
			}
			else
			{
				var column = Grid.GetColumn(child);
				var row = Grid.GetRow(child);

				// Compute offset of the child
				double x = column * size.Width + ((size.Width - child.DesiredSize.Width) / 2);
				double y = row * size.Height + ((size.Height - child.DesiredSize.Height) / 2);

				child.Arrange(new Rect(new Point(x, y), child.DesiredSize));
			}
		}

		return arrangeSize;
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
	public void Update(string type, JobSeq job = JobSeq.JobNone)
	{
		this.Children.Clear();

		#region Data
		var table = FileCache.Data.Get<ItemGraph>();
		var seeds = table.Where(record => record is ItemGraph.Seed seed).Cast<ItemGraph.Seed>();

		if (type is null) return;
		else if (type == "EnchantGem1") seeds = seeds.Where(x => x.SeedItem.FirstOrDefault().Instance is Item.Gem gem && gem.WeaponEnchantGemSlotType == WeaponEnchantGemSlotTypeSeq.First);
		else if (type == "EnchantGem2") seeds = seeds.Where(x => x.SeedItem.FirstOrDefault().Instance is Item.Gem gem && gem.WeaponEnchantGemSlotType == WeaponEnchantGemSlotTypeSeq.Second);
		else
		{
			var seq = type.ToEnum<EquipType>();
			if (seq == EquipType.None) return;

			seeds = seeds.Where(x => x.ItemEquipType == seq);
		}

		if (!seeds.Any()) return;
		#endregion

		#region node
		var MinRow = seeds.Min(record => record.Row);
		var MinCol = seeds.Min(record => record.Column);

		var items = new Dictionary<Item, GraphMapNode>();
		foreach (var seed in seeds)
		{
			var _items = seed.SeedItem.Select(x => x.Instance).Where(x => x != null);
			var item = _items.FirstOrDefault(x => x.EquipJobCheck.CheckSeq(job)) ?? _items.FirstOrDefault();
			if (item is null) continue;

			var widget = new GraphMapNode();
			widget.DataContext = item;

			Grid.SetRow(widget, seed.Row - MinRow);
			Grid.SetColumn(widget, seed.Column - MinCol);

			this.Children.Add(items[item] = widget);
		}
		#endregion

		#region line
		foreach (var edge in table.OfType<ItemGraph.Edge>())
		{
			if (!items.TryGetValue(edge.StartItem, out var StartItem)) continue;
			if (!items.TryGetValue(edge.EndItem, out var EndItem)) continue;

			// valid main-ingredient
			var MainIngredient = edge.FeedRecipe.Instance?.MainIngredient.Instance;
			if (MainIngredient is Item item && !item.EquipJobCheck.CheckSeq(job)) continue;

			this.Children.Add(new GraphMapEdge(edge, StartItem, EndItem));
		}
		#endregion
	}

	private void SetStartingPoint(object sender, RoutedEventArgs e)
	{
		if (Starting != null) Starting.Uid = null;

		var menu = (sender as MenuItem).Parent as ContextMenu;
		Starting = menu.PlacementTarget as FrameworkElement;
		Starting.Uid = "Starting";

		Cacl();
	}

	private void SetDestination(object sender, RoutedEventArgs e)
	{
		if (Destination != null) Destination.Uid = null;

		var menu = (sender as MenuItem).Parent as ContextMenu;
		Destination = menu.PlacementTarget as FrameworkElement;
		Destination.Uid = "Destination";

		Cacl();
	}


	private void Cacl()
	{
		if (Starting?.DataContext is not Item start ||
		 Destination?.DataContext is not Item dest) return;

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
				// check current step
				if (!edges.Contains(dest)) continue;

				// create route copy
				var _route = new ItemGraph.Edge[route.Length + 1];
				Array.Copy(route, 0, _route, 0, route.Length);
				_route[^1] = edge;

				// find next step
				var _dest = edge.StartItem.Instance;
				if (start.Equals(_dest)) routes.Add(_route);
				else Test(start, _dest, mode, _route);
			}
		}

		RoutesChanged?.Invoke(this, routes.Select(x => new ItemGraphRouteHelper(x)));
	}
	#endregion
}


public sealed class GraphMapNode : Control
{
	public GraphMapNode()
	{
		//this.Template = XamlReader.Parse();
	}

	private Size ImageSize = new Size(64, 64);


	public readonly Dictionary<Dock, double> Pins = [];

	private readonly Dictionary<Dock, int> PinCount = [];


	/// <summary>
	/// Get next point
	/// </summary>
	public Point PinPoint(Dock dock)
	{
		// Register pin
		PinCount.TryAdd(dock, 0);
		var count = PinCount[dock]++;

		// Get node pos
		var parent = VisualTreeHelper.GetParent(this) as UIElement;
		var pos = this.TranslatePoint(new Point(), parent);

		// Compute offset, avoid overlap
		// Move the central axis to align content
		var offset = count * GraphMapEdge.PinOffset;
		if (dock == Dock.Left || dock == Dock.Right) pos.Y += offset - Pins[dock] / 2;
		if (dock == Dock.Top || dock == Dock.Bottom) pos.X += offset - Pins[dock] / 2;
		if (dock == (Dock)4) pos.X -= offset;

		return dock switch
		{
			// Center
			(Dock)4 => new Point(pos.X + DesiredSize.Width / 2, pos.Y + DesiredSize.Height / 2),
			Dock.Top => new Point(pos.X + DesiredSize.Width / 2, pos.Y),
			Dock.Bottom => new Point(pos.X + DesiredSize.Width / 2, pos.Y + DesiredSize.Height),
			Dock.Left => new Point(pos.X + (DesiredSize.Width - ImageSize.Width) / 2, pos.Y + DesiredSize.Height / 2),
			Dock.Right => new Point(pos.X + (DesiredSize.Width + ImageSize.Width) / 2, pos.Y + DesiredSize.Height / 2),

			_ => throw new Exception(),
		};
	}
}

public sealed class GraphMapEdge : Shape
{
	private static readonly Brush DefiniteBrush = Brushes.Green;
	private static readonly Brush StochasticBrush = Brushes.Blue;
	public const double PinOffset = 6;

	#region Properties
	public static readonly DependencyProperty DataProperty = DependencyProperty.Register(
		"Data", typeof(Geometry), typeof(GraphMapEdge),
		new FrameworkPropertyMetadata(Geometry.Empty,
			FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

	public Geometry Data
	{
		get { return (Geometry)GetValue(DataProperty); }
		set { SetValue(DataProperty, value); }
	}

	protected override Geometry DefiningGeometry => Data;
	#endregion


	public GraphMapEdge(ItemGraph.Edge Edge, GraphMapNode StartItem, GraphMapNode EndItem)
	{
		StrokeThickness = 2.5;
		Stroke = Edge.SuccessProbability == SuccessProbabilitySeq.Definite ? DefiniteBrush : StochasticBrush;
		ToolTip = Edge.Attributes?.ToString();  // Edge.FeedRecipe.Instance;

		// Set direction 
		var dock1 = Edge.StartOrientation == OrientationSeq.Horizontal ? Dock.Right : Dock.Top;
		if (Edge.EdgeType == EdgeTypeSeq.JumpTransform) dock1 = (Dock)4;

		var dock2 = Edge.EndOrientation == OrientationSeq.Horizontal ? Dock.Left : Dock.Bottom;

		// Compute total size, ensure keep alignment
		StartItem.Pins.TryAdd(dock1, -PinOffset);
		EndItem.Pins.TryAdd(dock2, -PinOffset);
		StartItem.Pins[dock1] += PinOffset;
		EndItem.Pins[dock2] += PinOffset;

		// exec after loaded
		StartItem.Loaded += (_, _) => OnLoaded(StartItem, EndItem, dock1, dock2);
	}

	private void OnLoaded(GraphMapNode node1, GraphMapNode node2, Dock dock1, Dock dock2)
	{
		// Compute pos
		var sour = node1.PinPoint(dock1);
		var dest = node2.PinPoint(dock2);

		// Create path point
		var points = new List<Point>();
		var current = sour;

		if (dock1 == (Dock)4)
		{
			points.Add(new Point(sour.X - 50, sour.Y));
			points.Add(new Point(sour.X - 50, dest.Y));
		}
		else if (dock1 == Dock.Left || dock1 == Dock.Right) points.Add(current = current with { X = (sour.X + dest.X) / 2 });
		else points.Add(current = current with { Y = (sour.Y + dest.Y) / 2 });

		if (dock2 == Dock.Left || dock2 == Dock.Right) points.Add(current = current with { Y = dest.Y });
		else points.Add(current = current with { X = dest.X });

		points.Add(dest);

		// Update data
		var figure = new PathFigure(sour, points.Select(x => new LineSegment(x, true)), false);
		Data = new PathGeometry([figure]);
	}
}