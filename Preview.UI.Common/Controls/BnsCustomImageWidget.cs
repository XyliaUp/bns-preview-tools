using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using AduSkin.Controls.Metro;

using SkiaSharp;
using SkiaSharp.Views.WPF;

namespace Xylia.Preview.UI.Controls;
public class BnsCustomImageWidget : FrameworkElement
{
    #region Public Properties
    /// <summary>
    /// Gets/Sets the Source on this Image.
    ///
    /// The Source property is the ImageSource that holds the actual image drawn.
    /// </summary>
    public ImageSource Source
    {
        get { return (ImageSource)GetValue(SourceProperty); }
        set { SetValue(SourceProperty, value); }
    }

    /// <summary>
    /// DependencyProperty for Image Source property.
    /// </summary>
    /// <seealso cref="Image.Source" />
    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register("Source", typeof(ImageSource), typeof(BnsCustomImageWidget),
            new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public SKBitmap Image
    {
        get { return (SKBitmap)GetValue(ImageProperty); }
        set { SetValue(ImageProperty, value); }
    }

    public static DependencyProperty ImageProperty =
        DependencyProperty.Register(nameof(Image), typeof(SKBitmap), typeof(BnsCustomImageWidget),
        new FrameworkPropertyMetadata(null,
            FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
            OnImageChanged));

    public ushort? Count
    {
        get { return (ushort?)GetValue(CountProperty); }
        set { SetValue(CountProperty, value); }
    }

    public static DependencyProperty CountProperty =
        DependencyProperty.Register(nameof(Count), typeof(ushort?), typeof(BnsCustomImageWidget),
        new FrameworkPropertyMetadata(default,
            FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

    public static void OnImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var widget = (BnsCustomImageWidget)d;
        var value = (SKBitmap)e.NewValue;

        widget.Source = value.ToWriteableBitmap();
    }
    #endregion


    #region Private Methods
    /// <summary>
    /// Contains the code common for MeasureOverride and ArrangeOverride.
    /// </summary>
    /// <param name="inputSize">input size is the parent-provided space that Image should use to "fit in", according to other properties.</param>
    /// <returns>Image's desired size.</returns>
    private Size MeasureArrangeHelper(Size inputSize)
    {
        ImageSource imageSource = Source;
        Size naturalSize = new Size();

        if (imageSource == null) return naturalSize;
        naturalSize = new Size(imageSource.Width, imageSource.Height);

        //get computed scale factor
        Size scaleFactor = ComputeScaleFactor(inputSize, naturalSize);

        // Returns our minimum size & sets DesiredSize.
        return new Size(naturalSize.Width * scaleFactor.Width, naturalSize.Height * scaleFactor.Height);
    }

    /// <summary>
    /// This is a helper function that computes scale factors depending on a target size and a content size
    /// </summary>
    /// <param name="availableSize">Size into which the content is being fitted.</param>
    /// <param name="contentSize">Size of the content, measured natively (unconstrained).</param>
    private static Size ComputeScaleFactor(Size availableSize, Size contentSize)
    {
        // Compute scaling factors to use for axes
        double scaleX = 1.0;
        double scaleY = 1.0;

        bool isConstrainedWidth = !double.IsPositiveInfinity(availableSize.Width);
        bool isConstrainedHeight = !double.IsPositiveInfinity(availableSize.Height);

        if (isConstrainedWidth || isConstrainedHeight)
        {
            // Compute scaling factors for both axes
            scaleX = DoubleUtil.IsZero(contentSize.Width) ? 0.0 : availableSize.Width / contentSize.Width;
            scaleY = DoubleUtil.IsZero(contentSize.Height) ? 0.0 : availableSize.Height / contentSize.Height;

            if (!isConstrainedWidth) scaleX = scaleY;
            else if (!isConstrainedHeight) scaleY = scaleX;
        }

        //Return this as a size now
        return new Size(scaleX, scaleY);
    }
    #endregion

    #region	Protected Methods
    /// <summary>
    /// Updates DesiredSize of the Image.  Called by parent UIElement.  This is the first pass of layout.
    /// </summary>
    /// <remarks>
    /// Image will always return its natural size, if it fits within the constraint.  If not, it will return
    /// as large a size as it can.  Remember that image can later arrange at any size and stretch/align.
    /// </remarks>
    /// <param name="constraint">Constraint size is an "upper limit" that Image should not exceed.</param>
    /// <returns>Image's desired size.</returns>
    protected override Size MeasureOverride(Size constraint)
    {
        return MeasureArrangeHelper(constraint);
    }

    /// <summary>
    /// Override for <seealso cref="FrameworkElement.ArrangeOverride" />.
    /// </summary>
    protected override Size ArrangeOverride(Size arrangeSize)
    {
        return MeasureArrangeHelper(arrangeSize);
    }

    /// <summary>
    /// Render control
    /// </summary>
    /// <param name="dc"></param>
    protected override void OnRender(DrawingContext dc)
    {
        var imageSource = Source;
        if (imageSource is null) return;

        // computed from the ArrangeOverride return size
        dc.DrawImage(imageSource, new Rect(new Point(), RenderSize));

        // draw count string
        if (Count is null) return;
        var format = new FormattedText(Count.ToString(), CultureInfo.CurrentCulture,
            FlowDirection.LeftToRight, new Typeface("Arial"), 10, Brushes.White,
            VisualTreeHelper.GetDpi(this).PixelsPerDip);
        dc.DrawText(format, new Point(
            RenderSize.Width - format.Width - 3,
            RenderSize.Height - format.Height - 2));
    }
    #endregion
}