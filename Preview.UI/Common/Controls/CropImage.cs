using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Xylia.Preview.UI.Controls;
public class CropImage : Canvas
{
	#region Fields
	private Border _border;
	private AdornerLayer adornerLayer;
	private CropAdorner adorner;
	private bool isDragging;
	private double offsetX, offsetY;
	#endregion

	#region Ctor
	public CropImage()
	{
		_border = new Border()
		{
			Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5563C6F4")),
			BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0C76B8")),
			BorderThickness = new Thickness(2),
		};
		_border.SizeChanged += Border_SizeChanged;
		_border.MouseDown += Border_MouseDown;
		_border.MouseMove += Border_MouseMove;
		_border.MouseUp += Border_MouseUp;

		this.Children.Add(_border);

		adorner = new CropAdorner(_border);
	}
	#endregion



	#region Public Properties 
	public ImageSource Source
	{
		get { return (ImageSource)GetValue(SourceProperty); }
		set { SetValue(SourceProperty, value); }
	}

	public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(CropImage),
		new PropertyMetadata(null, OnSourceChanged));

	/// <summary>
	/// If empty, editing function will be enabled
	/// </summary>
	public Size CellSize
	{
		get { return (Size)GetValue(CellSizeProperty); }
		set { SetValue(CellSizeProperty, value); }
	}

	public static readonly DependencyProperty CellSizeProperty = DependencyProperty.Register("CellSize", typeof(Size), typeof(CropImage),
		new PropertyMetadata(Size.Empty, OnSizeChanged));

	/// <summary>
	/// Cell index, available when <see cref="CellSize"/> not empty
	/// </summary>
	public int SelectedIndex
	{
		get { return (int)GetValue(SelectedIndexProperty); }
		set { SetValue(SelectedIndexProperty, value); }
	}

	public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.Register("SelectedIndex", typeof(int), typeof(CropImage),
		new PropertyMetadata(1, OnSelectedChanged));

	public Int32Rect SelectedRect
	{
		get { return (Int32Rect)GetValue(SelectedRectProperty); }
		set { SetValue(SelectedRectProperty, value); }
	}

	public static readonly DependencyProperty SelectedRectProperty = DependencyProperty.Register("SelectedRect", typeof(Int32Rect), typeof(CropImage),
		new PropertyMetadata(Int32Rect.Empty, OnSelectedChanged));

	/// <summary>
	/// Selected sub image
	/// </summary>
	public ImageSource Selected
	{
		get { return (ImageSource)GetValue(SelectedProperty); }
		private set { SetValue(SelectedProperty, value); }
	}

	public static readonly DependencyProperty SelectedProperty = DependencyProperty.Register("Selected", typeof(ImageSource), typeof(CropImage));
	#endregion

	#region Dependency Helpers
	private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var crop = (CropImage)d;
		crop?.DrawImage();
	}

	private static void OnSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var crop = (CropImage)d;
		crop?.DrawAdorner();
	}

	private static void OnSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var crop = (CropImage)d;
		crop?.DrawAdorner();
	}
	#endregion


	#region Protected Methods
	protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
	{
		if (CellSize.IsEmpty) return;
		var pos = e.GetPosition(this);

		var amountRow = this.Width / this.CellSize.Width;
		var row = pos.X / CellSize.Width;
		var col = pos.Y / CellSize.Height;

		this.SelectedIndex = (int)(Math.Floor(col) * amountRow + (Math.Floor(row) + 1));
	}

	protected override void OnPreviewMouseMove(MouseEventArgs e)
	{

	}
	#endregion

	#region Private Methods
	private void Border_MouseUp(object sender, MouseButtonEventArgs e)
	{
		isDragging = false;
		var draggableControl = sender as UIElement;
		draggableControl.ReleaseMouseCapture();
	}

	private void Border_MouseDown(object sender, MouseButtonEventArgs e)
	{
		if (!isDragging && CellSize.IsEmpty)
		{
			isDragging = true;
			var draggableControl = sender as UIElement;
			var position = e.GetPosition(this);
			offsetX = position.X - Canvas.GetLeft(draggableControl);
			offsetY = position.Y - Canvas.GetTop(draggableControl);
			draggableControl.CaptureMouse();
		}
	}

	private void Border_MouseMove(object sender, MouseEventArgs e)
	{
		if (isDragging && e.LeftButton == MouseButtonState.Pressed)
		{
			var draggableControl = sender as UIElement;
			var position = e.GetPosition(this);
			var x = position.X - offsetX;
			x = x < 0 ? 0 : x;
			x = x + _border.Width > this.Width ? this.Width - _border.Width : x;
			var y = position.Y - offsetY;
			y = y < 0 ? 0 : y;
			y = y + _border.Height > this.Height ? this.Height - _border.Height : y;
			Canvas.SetLeft(draggableControl, x);
			Canvas.SetTop(draggableControl, y);

			Refresh();
		}
	}

	private void Border_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		Refresh();
	}


	private void DrawImage()
	{
		if (Source == null) return;

		var bitmap = (BitmapSource)Source;
		this.Width = bitmap.Width;
		this.Height = bitmap.Height;
		this.Background = new ImageBrush(bitmap);

		_border.Width = bitmap.Width * 0.2;
		_border.Height = bitmap.Height * 0.2;
		var cx = this.Width / 2 - _border.Width / 2;
		var cy = this.Height / 2 - _border.Height / 2;
		Canvas.SetLeft(_border, cx);
		Canvas.SetTop(_border, cy);

		DrawAdorner();
	}

	private void DrawAdorner()
	{
		this.adornerLayer = AdornerLayer.GetAdornerLayer(_border);
		this.adornerLayer?.Remove(this.adorner);

		if (CellSize.IsEmpty)
		{
			this.adornerLayer?.Add(this.adorner);
			this._border.Cursor = Cursors.SizeAll;

			// rect
			if (!this.SelectedRect.IsEmpty)
			{
				this._border.Width = SelectedRect.Width;
				this._border.Height = SelectedRect.Height;
				Canvas.SetLeft(_border, SelectedRect.X);
				Canvas.SetTop(_border, SelectedRect.Y);
			}
		}
		else
		{
			this._border.Cursor = this.Cursor;
			this._border.Width = CellSize.Width;
			this._border.Height = CellSize.Height;
			this._border.Visibility = CellSize == RenderSize ? Visibility.Hidden : Visibility.Visible;

			// index
			var amountRow = (int)(this.Width / this.CellSize.Width);
			if (amountRow == 0) return;
			int row = SelectedIndex % amountRow;
			int col = SelectedIndex / amountRow;

			if (row == 0)
			{
				row = amountRow;
				col--;
			}
			row--;

			Canvas.SetLeft(_border, row * CellSize.Width);
			Canvas.SetTop(_border, col * CellSize.Height);

			Refresh();
		}
	}

	private void Refresh()
	{
		if (this.Source is null) return;

		var width = _border.Width;
		var height = _border.Height;
		var left = Canvas.GetLeft(_border);
		var top = Canvas.GetTop(_border);

		if (double.IsNaN(width) || double.IsNaN(height))
		{
			Selected = null;
			return;
		}

		SelectedRect = new Int32Rect((int)left, (int)top, (int)width, (int)height);
		Selected = new CroppedBitmap((BitmapSource)this.Source, SelectedRect);
	}
	#endregion
}

public class CropAdorner : Adorner
{
	#region Fields
	private const double THUMB_SIZE = 15;
	private const double MINIMAL_SIZE = 20;
	private readonly Thumb lc;
	private readonly Thumb tl;
	private readonly Thumb tc;
	private readonly Thumb tr;
	private readonly Thumb rc;
	private readonly Thumb br;
	private readonly Thumb bc;
	private readonly Thumb bl;
	private readonly VisualCollection visCollec;
	private readonly FrameworkElement parent;
	#endregion

	#region Ctor
	public CropAdorner(UIElement adorned) : base(adorned)
	{
		parent = FindParent(adorned) as FrameworkElement;

		visCollec = new VisualCollection(this)
		{
			(lc = GetResizeThumb(Cursors.SizeWE, HorizontalAlignment.Left, VerticalAlignment.Center)),
			(tl = GetResizeThumb(Cursors.SizeNWSE, HorizontalAlignment.Left, VerticalAlignment.Top)),
			(tc = GetResizeThumb(Cursors.SizeNS, HorizontalAlignment.Center, VerticalAlignment.Top)),
			(tr = GetResizeThumb(Cursors.SizeNESW, HorizontalAlignment.Right, VerticalAlignment.Top)),
			(rc = GetResizeThumb(Cursors.SizeWE, HorizontalAlignment.Right, VerticalAlignment.Center)),
			(br = GetResizeThumb(Cursors.SizeNWSE, HorizontalAlignment.Right, VerticalAlignment.Bottom)),
			(bc = GetResizeThumb(Cursors.SizeNS, HorizontalAlignment.Center, VerticalAlignment.Bottom)),
			(bl = GetResizeThumb(Cursors.SizeNESW, HorizontalAlignment.Left, VerticalAlignment.Bottom))
		};
	}

	private Thumb GetResizeThumb(Cursor cur, HorizontalAlignment hor, VerticalAlignment ver)
	{
		var thumb = new Thumb
		{
			Width = THUMB_SIZE,
			Height = THUMB_SIZE,
			HorizontalAlignment = hor,
			VerticalAlignment = ver,
			Cursor = cur,
			Template = new ControlTemplate(typeof(Thumb))
			{
				VisualTree = GetFactory(new SolidColorBrush(Colors.White))
			}
		};
		var maxWidth = double.IsNaN(parent.Width) ? parent.ActualWidth : parent.Width;
		var maxHeight = double.IsNaN(parent.Height) ? parent.ActualHeight : parent.Height;
		thumb.DragDelta += (s, e) =>
		{
			if (AdornedElement is not FrameworkElement element)
				return;

			Resize(element);

			switch (thumb.VerticalAlignment)
			{
				case VerticalAlignment.Bottom:
					if (element.Height + e.VerticalChange > MINIMAL_SIZE)
					{
						var newHeight = element.Height + e.VerticalChange;
						var top = Canvas.GetTop(element) + newHeight;
						if (newHeight > 0 && top <= parent.ActualHeight)
							element.Height = newHeight;
					}
					break;

				case VerticalAlignment.Top:
					if (element.Height - e.VerticalChange > MINIMAL_SIZE)
					{

						var newHeight = element.Height - e.VerticalChange;
						var top = Canvas.GetTop(element);
						if (newHeight > 0 && top + e.VerticalChange >= 0)
						{
							element.Height = newHeight;
							Canvas.SetTop(element, top + e.VerticalChange);
						}
					}

					break;
			}

			switch (thumb.HorizontalAlignment)
			{
				case HorizontalAlignment.Left:
					if (element.Width - e.HorizontalChange > MINIMAL_SIZE)
					{
						var newWidth = element.Width - e.HorizontalChange;
						var left = Canvas.GetLeft(element);
						if (newWidth > 0 && left + e.HorizontalChange >= 0)
						{
							element.Width = newWidth;
							Canvas.SetLeft(element, left + e.HorizontalChange);
						}
					}

					break;
				case HorizontalAlignment.Right:
					if (element.Width + e.HorizontalChange > MINIMAL_SIZE)
					{
						var newWidth = element.Width + e.HorizontalChange;
						var left = Canvas.GetLeft(element) + newWidth;
						if (newWidth > 0 && left <= parent.ActualWidth)
							element.Width = newWidth;
					}

					break;
			}

			e.Handled = true;
		};
		return thumb;
	}
	#endregion

	#region Static Methods
	private static UIElement FindParent(UIElement element)
	{
		DependencyObject obj = element;
		obj = VisualTreeHelper.GetParent(obj);
		return obj as UIElement;
	}

	private static void Resize(FrameworkElement frameworkElement)
	{
		if (double.IsNaN(frameworkElement.Width))
			frameworkElement.Width = frameworkElement.RenderSize.Width;
		if (double.IsNaN(frameworkElement.Height))
			frameworkElement.Height = frameworkElement.RenderSize.Height;
	}

	private static FrameworkElementFactory GetFactory(Brush back)
	{
		var fef = new FrameworkElementFactory(typeof(Ellipse));
		fef.SetValue(Shape.FillProperty, back);
		fef.SetValue(Shape.StrokeThicknessProperty, (double)2);
		return fef;
	}
	#endregion

	#region Protected Methods
	protected override int VisualChildrenCount => visCollec.Count;

	protected override Visual GetVisualChild(int index)
	{
		return visCollec[index];
	}

	protected override Size ArrangeOverride(Size finalSize)
	{
		var offset = THUMB_SIZE / 2;
		var sz = new Size(THUMB_SIZE, THUMB_SIZE);
		lc.Arrange(new Rect(new Point(-offset, AdornedElement.RenderSize.Height / 2 - offset), sz));
		tl.Arrange(new Rect(new Point(-offset, -offset), sz));
		tc.Arrange(new Rect(new Point(AdornedElement.RenderSize.Width / 2 - offset, -offset), sz));
		tr.Arrange(new Rect(new Point(AdornedElement.RenderSize.Width - offset, -offset), sz));
		rc.Arrange(new Rect(
			new Point(AdornedElement.RenderSize.Width - offset, AdornedElement.RenderSize.Height / 2 - offset),
			sz));
		br.Arrange(new Rect(
			new Point(AdornedElement.RenderSize.Width - offset, AdornedElement.RenderSize.Height - offset), sz));
		bc.Arrange(new Rect(
			new Point(AdornedElement.RenderSize.Width / 2 - offset, AdornedElement.RenderSize.Height - offset),
			sz));
		bl.Arrange(new Rect(new Point(-offset, AdornedElement.RenderSize.Height - offset), sz));
		return finalSize;
	}
	#endregion
}