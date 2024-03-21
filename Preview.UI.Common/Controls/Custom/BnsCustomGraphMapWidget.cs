using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;
using Xylia.Preview.Data.Models.Sequence;
using Xylia.Preview.UI.Controls.Helpers;
using Xylia.Preview.UI.Controls.Primitives;
using Xylia.Preview.UI.Extensions;
using static Xylia.Preview.Data.Models.Item.Gem;
using static Xylia.Preview.Data.Models.ItemGraph;

namespace Xylia.Preview.UI.Controls;
public class BnsCustomGraphMapWidget : BnsCustomBaseWidget
{
	#region Constructors
	static BnsCustomGraphMapWidget()
	{
		// Initialize Commands
		InitializeCommands();
	}

	public BnsCustomGraphMapWidget()
	{
		this.Background = Brushes.Transparent;
		this.ClipToBounds = true;
	}
	#endregion

	#region Commands
	private static readonly Type Owner = typeof(BnsCustomGraphMapWidget);
	public static RoutedCommand ChangeSubGroup { get; private set; }
	public static RoutedCommand SetStartingPoint { get; private set; }
	public static RoutedCommand SetDestination { get; private set; }

	static void InitializeCommands()
	{
		ChangeSubGroup = new RoutedCommand("SetStartingPoint", Owner);
		SetStartingPoint = new RoutedCommand("SetStartingPoint", Owner);
		SetDestination = new RoutedCommand("SetDestination", Owner);

		Owner.RegisterCommandHandler(ChangeSubGroup, OnChangeSubGroup, CanChangeSubGroup);
		Owner.RegisterCommandHandler(SetStartingPoint, OnSetStartingPoint);
		Owner.RegisterCommandHandler(SetDestination, OnSetDestination);
	}
	#endregion

	#region Event
	public event EventHandler<IEnumerable<ItemGraphRouteHelper>>? RoutesChanged;

	public event EventHandler<Edge>? SetRouteThrough;
	#endregion


	#region Public Properties
	public static readonly DependencyProperty RowProperty = DependencyProperty.RegisterAttached("Row",
		typeof(int), Owner, new FrameworkPropertyMetadata(-1,
			FrameworkPropertyMetadataOptions.AffectsParentArrange | FrameworkPropertyMetadataOptions.AffectsParentArrange));

	public static readonly DependencyProperty ColumnProperty = DependencyProperty.RegisterAttached("Column",
		typeof(int), Owner, new FrameworkPropertyMetadata(-1,
			FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

	public static readonly DependencyProperty CellSizeProperty = Owner.Register(nameof(CellSize), new Size(150, 150),
		FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender);

	public static readonly DependencyProperty ShowLinesProperty = Owner.Register(nameof(ShowLines), BooleanBoxes.FalseBox);
	internal static readonly DependencyProperty RatioProperty = Owner.Register(nameof(Ratio), 1d, callback: SetRatio);


	public Size CellSize { get => (Size)this.GetValue(CellSizeProperty); set => this.SetValue(CellSizeProperty, value); }

	public bool ShowLines { get => (bool)this.GetValue(ShowLinesProperty); set => this.SetValue(ShowLinesProperty, value); }



	//public int ColumnGap { get; set; } = 45;
	//public int RowGap { get; set; } = 35;

	public float MaxZoomRatio { get; set; } = 1.3F;
	public float MinZoomRatio { get; set; } = 0.7F;

	public double Ratio
	{
		get => (double)this.GetValue(RatioProperty);
		set => this.SetValue(RatioProperty, value);
	}
	#endregion

	#region Public Methods
	public static int GetRow(UIElement element)
	{
		ArgumentNullException.ThrowIfNull(element);
		return (int)element.GetValue(RowProperty);
	}

	public static void SetRow(UIElement element, int value)
	{
		ArgumentNullException.ThrowIfNull(element);
		element.SetValue(RowProperty, value);
	}

	public static int GetColumn(UIElement element)
	{
		ArgumentNullException.ThrowIfNull(element);
		return (int)element.GetValue(ColumnProperty);
	}

	public static void SetColumn(UIElement element, int value)
	{
		ArgumentNullException.ThrowIfNull(element);
		element.SetValue(ColumnProperty, value);
	}


	public void Update(string? type, JobSeq job = JobSeq.JobNone)
	{
		Starting = Destination = null;
		this.Children.Clear();

		#region Data
		var table = FileCache.Data.Provider.GetTable<ItemGraph>();
		var seeds = table.Where(record => record is ItemGraph.Seed seed).Cast<ItemGraph.Seed>();

		var seq = type.ToEnum<EquipType>();
		if (seq != EquipType.None) seeds = seeds.Where(x => x.ItemEquipType == seq);
		else if (type == "EnchantGem1") seeds = seeds.Where(x => x.SeedItem.FirstOrDefault().Instance is Item.Gem gem && gem.WeaponEnchantGemSlotType == WeaponEnchantGemSlotTypeSeq.First);
		else if (type == "EnchantGem2") seeds = seeds.Where(x => x.SeedItem.FirstOrDefault().Instance is Item.Gem gem && gem.WeaponEnchantGemSlotType == WeaponEnchantGemSlotTypeSeq.Second);
		else if (type == "AccessoryGem") seeds = seeds.Where(x => x.SeedItem.FirstOrDefault().Instance is Item.Gem gem && gem.AccessoryEnchantGemEquipAccessoryType != AccessoryEnchantGemEquipAccessoryTypeSeq.None);
		else return;

		if (!seeds.Any()) return;
		#endregion

		#region Node	
		var groups = seeds.Select(record => record.SeedItemGroup.Instance).Where(x => x != null).Reverse().Distinct();
		var items = new Dictionary<Item, BnsCustomImageWidget>();

		foreach (var seed in seeds)
		{
			// valid data
			var SeedItems = seed.SeedItem.Select(x => x.Instance).Where(x => x != null).ToArray();
			var item = SeedItems.FirstOrDefault(x => x.EquipJobCheck.CheckSeq(job)) ?? SeedItems.FirstOrDefault();
			if (item is null) continue;

			#region Widget
			var widget = this.NodeTemplate.Clone();
			widget.DataContext = item;
			widget.ContextMenu = NodeMenu;

			SetRow(widget, seed.Row);
			SetColumn(widget, seed.Column);
			#endregion

			#region Expansion
			//widget.ExpansionComponentList["Node_Icon"]?.SetValue(item.Icon);
			widget.ExpansionComponentList["Node_ItemName"]?.SetValue(item.ItemName);

			if (SeedItems.Length > 1) widget.Expansion.Add("Node_SubGroupImage");
			#endregion

			this.Children.Add(items[item] = widget);
		}
		#endregion

		#region line
		foreach (var edge in table.OfType<ItemGraph.Edge>())
		{
			if (!items.TryGetValue(edge.StartItem, out var StartItem)) continue;
			if (!items.TryGetValue(edge.EndItem, out var EndItem)) continue;

			// valid main ingredient
			var MainIngredient = edge.FeedRecipe.Instance?.MainIngredient.Instance;
			if (MainIngredient is Item item && !item.EquipJobCheck.CheckSeq(job)) continue;

			this.Children.Add(new Edge(edge, StartItem, EndItem));
		}
		#endregion
	}

	/// <summary>
	/// Search for the specified item and move to it if exist
	/// </summary>
	public void Search(Item item)
	{

	}
	#endregion


	#region Protected Methods
	protected override void OnInitialized(EventArgs e)
	{
		base.OnInitialized(e);

		// Template
		NodeTemplate = GetChild<BnsCustomImageWidget>("NodeTemplate");
		NodeTemplate.Expansion = ["Node_Icon", "Node_ItemName"];
		HorizontalRulerItemTemplate = GetChild<BnsCustomImageWidget>("HorizontalRulerItemTemplate");
		VerticalRulerItemTemplate = GetChild<BnsCustomImageWidget>("VerticalRulerItemTemplate");

		// ContextMenu
		NodeMenu = new ContextMenu();
		NodeMenu.Items.Add(new MenuItem() { Header = "切换", Command = ChangeSubGroup });
		NodeMenu.Items.Add(new MenuItem() { Header = "设为起始物品", Command = SetStartingPoint });
		NodeMenu.Items.Add(new MenuItem() { Header = "设为结束物品", Command = SetDestination });
		NodeMenu.Items.ForEach(item => ((DependencyObject)item).SetBinding(MenuItem.CommandTargetProperty, new Binding(NodeMenu, "PlacementTarget")));



		//NodeTemplate.ExpansionComponentList["Node_Icon"].ImageProperty =
		//	FileCache.Data.Provider.GetTable<Item>()[new Ref(2080002, 1)]?.Icon;
	}

	protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
	{
		base.OnPreviewMouseWheel(e);

		if (!Keyboard.IsKeyDown(Key.LeftCtrl)) return;

		if (e.Delta < 0) Ratio -= 0.05;
		else Ratio += 0.05;

		SetRatio(this, default);
		e.Handled = true;
	}

	protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
	{
		if (e.LeftButton == MouseButtonState.Pressed)
		{
			_original = new Point(ScrollOffset.X, ScrollOffset.Y);
			_mouseOffset = e.GetPosition(this);
		}
	}

	protected override void OnPreviewMouseMove(MouseEventArgs e)
	{
		if (e.LeftButton == MouseButtonState.Pressed)
		{
			_isDragging = true;
			Cursor = Cursors.SizeAll;

			var offset = _mouseOffset - e.GetPosition(this);
			this.ScrollOffset = new Vector(
				_original.X + offset.X , 
				_original.Y + offset.Y);
			this.InvalidateVisual();
		}
	}

	protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
	{
		if (_isDragging)
		{
			_isDragging = false;
			e.Handled = true;

			Cursor = Cursors.Arrow;
		}
	}

	protected override Rect ArrangeChildren(UIElement child, Size constraint)
	{
		var size = CellSize;

		var column = GetColumn(child);
		var row = GetRow(child);

		if (column >= 0)
		{
			double x = column * size.Width + ((size.Width - child.RenderSize.Width) / 2) - ScrollOffset.X;
			double y = row * size.Height + ((size.Height - child.RenderSize.Height) / 2) - ScrollOffset.Y;
			return new Rect(new Point(x, y), child.RenderSize);
		}
		else if (child is Edge)
		{
			Panel.SetZIndex(child, -1);
			return new Rect();
		}

		return base.ArrangeChildren(child, constraint);
	}

	protected override void OnRender(DrawingContext dc)
	{
		base.OnRender(dc);

		if (this.ShowLines)
		{
			var size = CellSize;
			var DefaultBorder = Color.FromArgb(52, 0, 255, 110);

			int MaxColumn = 0, MaxRow = 0;
			foreach (UIElement child in Children)
			{
				if (child == null) continue;

				MaxRow = Math.Max(MaxRow, GetRow(child) + 1);
				MaxColumn = Math.Max(MaxColumn, GetColumn(child) + 1);
			}

			for (int column = 0; column < MaxColumn; column++)
			{
				for (int row = 0; row < MaxRow; row++)
				{
					double x = column * size.Width - ScrollOffset.X;
					double y = row * size.Height - ScrollOffset.Y;

					var rect = new Rect(x, y, size.Width, size.Height);
					dc.DrawRectangle(new SolidColorBrush(), pen: new Pen(new SolidColorBrush(DefaultBorder), 1), rect);

					dc.DrawText(new FormattedText($"{row}-{column}",
						CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 10, Brushes.Black, 96),
						new Point(x, y));
				}
			}
		}
	}
	#endregion

	#region Private Helpers

	#region	Widget
	private static void SetRatio(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		// get final scale 
		var widget = (BnsCustomGraphMapWidget)d;
		widget.Ratio = Math.Max(widget.MinZoomRatio, Math.Min(widget.MaxZoomRatio, widget.Ratio));
		widget.LayoutTransform = new ScaleTransform() { ScaleX = widget.Ratio, ScaleY = widget.Ratio };
	}
	#endregion

	#region NodeTemplate
	private static void CanChangeSubGroup(object sender, CanExecuteRoutedEventArgs e)
	{
		if (e.Source is not BnsCustomImageWidget node) return;

		// check if exist sub-group
		//e.CanExecute = !inloading && source != null && source.CanSave;
	}

	private static void OnChangeSubGroup(object sender, ExecutedRoutedEventArgs e)
	{
		if (sender is not BnsCustomGraphMapWidget widget) return;
	}

	private static void OnSetStartingPoint(object sender, ExecutedRoutedEventArgs e)
	{
		if (sender is not BnsCustomGraphMapWidget widget) return;

		widget.Starting?.Expansion.Remove("Node_StartImage");
		widget.Starting = e.Source as BnsCustomImageWidget;
		widget.Starting!.Expansion.Add("Node_StartImage");

		//FileCache.Data.Provider.GetTable<GameMessage>()["Msg.ItemGraph.SetStartingPoint"]?.Instant();
		widget.FindPath();
	}

	private static void OnSetDestination(object sender, ExecutedRoutedEventArgs e)
	{
		if (sender is not BnsCustomGraphMapWidget widget) return;

		widget.Destination?.Expansion.Remove("Node_PurposeImage");
		widget.Destination = e.Source as BnsCustomImageWidget;
		widget.Destination!.Expansion.Add("Node_PurposeImage");

		//FileCache.Data.Provider.GetTable<GameMessage>()["Msg.ItemGraph.SetDestination"]?.Instant();
		widget.FindPath();
	}

	private void FindPath()
	{
		if (Starting?.DataContext is not Item start ||
		 Destination?.DataContext is not Item dest) return;

		// Create lookup
		var edges = Children.OfType<Edge>().ToLookup(seed => seed.Data.EndItem.Instance);

		ConcurrentBag<Edge[]> routes = [];
		Test(start, dest, 0, []);

		void Test(Item start, Item dest, PriorityMode mode, Edge[] route)
		{
			// For same item, means that is different recipe 
			// select one of according the user configuration
			var paths = edges[dest].GroupBy(x => x.Data.StartItem).Select(paths =>
			{
				Edge? edge = null;

				if (mode == PriorityMode.Definite)
				{
					edge = paths.FirstOrDefault(o => o.Data.SuccessProbability == SuccessProbabilitySeq.Definite);
				}

				return edge ?? paths.First();
			});

			foreach (var edge in paths)
			{
				// check current step
				if (!edges.Contains(dest)) continue;

				// create route copy
				var _route = new Edge[route.Length + 1];
				Array.Copy(route, 0, _route, 0, route.Length);
				_route[^1] = edge;

				// find next step
				var _dest = edge.Data.StartItem.Instance;
				if (start.Equals(_dest)) routes.Add(_route);
				else Test(start, _dest, mode, _route);
			}
		}

		RoutesChanged?.Invoke(this, routes.Select(x => new ItemGraphRouteHelper(x)));
	}
	#endregion

	#region Edge
	public sealed class Edge : Shape
	{
		#region Fields
		private static readonly Brush DefiniteBrush = Brushes.Green;
		private static readonly Brush StochasticBrush = Brushes.Blue;
		public const double PinOffset = 8;
		#endregion

		#region Properties
		public ItemGraph.Edge Data { get; private set; }

		protected override Geometry DefiningGeometry => Path;

		public static readonly DependencyProperty PathProperty = DependencyProperty.Register("Path",
			typeof(Geometry), typeof(Edge), new FrameworkPropertyMetadata(Geometry.Empty,
				FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

		public Geometry Path
		{
			get { return (Geometry)GetValue(PathProperty); }
			set { SetValue(PathProperty, value); }
		}

		public static readonly DependencyProperty HighlightProperty = DependencyProperty.Register("Highlight",
			typeof(bool), typeof(Edge), new FrameworkPropertyMetadata(BooleanBoxes.FalseBox,
				FrameworkPropertyMetadataOptions.AffectsRender));

		public bool Highlight
		{
			get { return (bool)GetValue(HighlightProperty); }
			set { SetValue(HighlightProperty, value); }
		}
		#endregion

		#region Methods
		public Edge(ItemGraph.Edge Edge, BnsCustomImageWidget StartItem, BnsCustomImageWidget EndItem)
		{
			DataContext = Data = Edge;
			StrokeThickness = 2.5;
			Stroke = Edge.SuccessProbability == SuccessProbabilitySeq.Definite ? DefiniteBrush : StochasticBrush;

			// Set direction 
			var dock1 = Edge.StartOrientation == OrientationSeq.Horizontal ? Dock.Right : Dock.Top;
			if (Edge.EdgeType == EdgeTypeSeq.JumpTransform) dock1 = (Dock)4;

			var dock2 = Edge.EndOrientation == OrientationSeq.Horizontal ? Dock.Left : Dock.Bottom;

			//// Compute total size, ensure keep alignment
			//StartItem.Pins.TryAdd(dock1, -PinOffset);
			//EndItem.Pins.TryAdd(dock2, -PinOffset);
			//StartItem.Pins[dock1] += PinOffset;
			//EndItem.Pins[dock2] += PinOffset;

			// exec after loaded
			StartItem.Loaded += (_, _) => OnLoaded(StartItem, EndItem, dock1, dock2);
		}

		private void OnLoaded(BnsCustomImageWidget node1, BnsCustomImageWidget node2, Dock dock1, Dock dock2)
		{
			//// Compute pos
			//var sour = node1.PinPoint(dock1);
			//var dest = node2.PinPoint(dock2);

			//// Create path point
			//var points = new List<Point>();
			//var current = sour;

			//if (dock1 == (Dock)4)
			//{
			//	points.Add(new Point(sour.X - 50, sour.Y));
			//	points.Add(new Point(sour.X - 50, dest.Y));
			//}
			//else if (dock1 == Dock.Left || dock1 == Dock.Right) points.Add(current = current with { X = (sour.X + dest.X) / 2 });
			//else points.Add(current = current with { Y = (sour.Y + dest.Y) / 2 });

			//if (dock2 == Dock.Left || dock2 == Dock.Right) points.Add(current = current with { Y = dest.Y });
			//else points.Add(current = current with { X = dest.X });

			//points.Add(dest);

			//// Update data
			//var figure = new PathFigure(sour, points.Select(x => new LineSegment(x, true)), false);
			//Path = new PathGeometry([figure]);
		}
		#endregion
	}

	private void SetRoute(object sender, RoutedEventArgs e)
	{
		var menu = ((MenuItem)sender).Parent as ContextMenu;
		var item = menu.PlacementTarget as Edge;

		SetRouteThrough?.Invoke(this, item);
	}
	#endregion

	#endregion

	#region Private Fields
	bool _isDragging;
	Point _mouseOffset;
	Point _original;

	private BnsCustomImageWidget NodeTemplate;
	private BnsCustomImageWidget HorizontalRulerItemTemplate;
	private BnsCustomImageWidget VerticalRulerItemTemplate;

	private ContextMenu NodeMenu;

	private BnsCustomImageWidget? Starting;
	private BnsCustomImageWidget? Destination;
	#endregion
}


#region Template
//public sealed class NodeTemplate : Control
//{
//	public NodeTemplate()
//	{
//		//this.Template = XamlReader.Parse();
//	}

//	private Size ImageSize = new Size(64, 64);


//	public readonly Dictionary<Dock, double> Pins = [];

//	private readonly Dictionary<Dock, int> PinCount = [];


//	/// <summary>
//	/// Get next point
//	/// </summary>
//	public Point PinPoint(Dock dock)
//	{
//		// Register pin
//		PinCount.TryAdd(dock, 0);
//		var count = PinCount[dock]++;

//		// Get node pos
//		var parent = VisualTreeHelper.GetParent(this) as UIElement;
//		var pos = this.TranslatePoint(new Point(), parent);

//		// Compute offset, avoid overlap
//		// Move the central axis to align content
//		var offset = count * EdgeTemplate.PinOffset;
//		if (dock == Dock.Left || dock == Dock.Right) pos.Y += offset - Pins[dock] / 2;
//		if (dock == Dock.Top || dock == Dock.Bottom) pos.X += offset - Pins[dock] / 2;
//		if (dock == (Dock)4) pos.X -= offset;

//		return dock switch
//		{
//			// Center
//			(Dock)4 => new Point(pos.X + RenderSize.Width / 2, pos.Y + RenderSize.Height / 2),
//			Dock.Top => new Point(pos.X + RenderSize.Width / 2, pos.Y),
//			Dock.Bottom => new Point(pos.X + RenderSize.Width / 2, pos.Y + RenderSize.Height),
//			Dock.Left => new Point(pos.X + (RenderSize.Width - ImageSize.Width) / 2, pos.Y + RenderSize.Height / 2),
//			Dock.Right => new Point(pos.X + (RenderSize.Width + ImageSize.Width) / 2, pos.Y + RenderSize.Height / 2),

//			_ => throw new Exception(),
//		};
//	}
//}
#endregion

#region Helper
public class ItemGraphRouteHelper
{
	public BnsCustomGraphMapWidget.Edge[] Edges;

	public ItemGraphRouteHelper(BnsCustomGraphMapWidget.Edge[] route)
	{
		Edges = route;
	}


	public void SwitchHighlight(bool status)
	{
		Edges.ForEach(x =>
		{
			x.Highlight = status;
		});
	}


	public override string ToString() => Edges.Aggregate("", (a, n) => n.Data.StartItem.Instance.ItemNameOnly + " -> ") +
		Edges.LastOrDefault()?.Data.EndItem.Instance.ItemNameOnly;

	public IReadOnlyDictionary<Item, int> Ingredients
	{
		get
		{
			var items = new Dictionary<Item, int>();
			void AddItem(Item item, int count)
			{
				if (item is null) return;

				items.TryAdd(item, 0);
				items[item] += count;
			}

			foreach (var edge in Edges)
			{
				var recipe = edge.Data.Recipe;
				if (recipe is null) continue;

				AddItem(recipe.MainItem, recipe.MainItemCount);
				recipe.SubItemList?.ForEach(sub => AddItem(sub.Item1, sub.Item2));
			}

			return items;
		}
	}
}

public enum PriorityMode
{
	Definite,
	Inexpensive,
}
#endregion