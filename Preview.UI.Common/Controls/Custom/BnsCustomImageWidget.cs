using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using CUE4Parse.BNS.Conversion;
using CUE4Parse.UE4.Objects.Core.Math;
using SkiaSharp;
using SkiaSharp.Views.WPF;
using Xylia.Preview.UI.Controls.Primitives;
using Xylia.Preview.UI.Converters;

namespace Xylia.Preview.UI.Controls;
public class BnsCustomImageWidget : BnsCustomBaseWidget
{
	#region Private Methods
	/// <summary>
	/// Contains the code common for MeasureOverride and ArrangeOverride.
	/// </summary>
	/// <param name="inputSize">input size is the parent-provided space that Image should use to "fit in", according to other properties.</param>
	/// <returns>Image's desired size.</returns>
	private Size MeasureArrangeHelper(Size inputSize)
	{
		if (!double.IsInfinity(inputSize.Width) && !double.IsInfinity(inputSize.Height))
			return inputSize;

		//get computed scale factor
		var naturalSize = BaseImageProperty?.ImageUVSize ?? default;
		var scaleFactor = ComputeScaleFactor(inputSize, naturalSize);

		// Returns our minimum size & sets DesiredSize.
		return new Size(naturalSize.X * scaleFactor.Width, naturalSize.Y * scaleFactor.Height);
	}

	/// <summary>
	/// This is a helper function that computes scale factors depending on a target size and a content size
	/// </summary>
	/// <param name="availableSize">Size into which the content is being fitted.</param>
	/// <param name="contentSize">Size of the content, measured natively (unconstrained).</param>
	private static Size ComputeScaleFactor(Size availableSize, FVector2D contentSize)
	{
		// Compute scaling factors to use for axes
		double scaleX = 1.0;
		double scaleY = 1.0;

		bool isConstrainedWidth = !double.IsPositiveInfinity(availableSize.Width);
		bool isConstrainedHeight = !double.IsPositiveInfinity(availableSize.Height);

		if (isConstrainedWidth || isConstrainedHeight)
		{
			// Compute scaling factors for both axes
			scaleX = contentSize.X > 0 ? availableSize.Width / contentSize.X : 0;
			scaleY = contentSize.Y > 0 ? availableSize.Height / contentSize.Y : 0;

			if (!isConstrainedWidth) scaleX = scaleY;
			else if (!isConstrainedHeight) scaleY = scaleX;
		}

		//Return this as a size now
		return new Size(scaleX, scaleY);
	}

	public static AnimationTimeline Flipbook(SKBitmap source, FVector2D size, int FramesPerSec = 25)
	{
		// init
		var count = (int)(source.Width / size.X * source.Height / size.Y);
		(float, float) pos = default;

		var span = 1000 / FramesPerSec;
		var frames = new ObjectAnimationUsingKeyFrames() { RepeatBehavior = RepeatBehavior.Forever };

		for (int index = 0; index < count; index++)
		{
			var frame = source.Clone(new FVector2D(pos.Item1, pos.Item2), size);
			frames.KeyFrames.Add(new DiscreteObjectKeyFrame
			{
				KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(index * span)),
				Value = new ImageBrush(frame.ToWriteableBitmap()),
			});

			// next pos
			if ((pos.Item1 += size.X) >= source.Width)
			{
				pos.Item1 = 0;
				pos.Item2 += size.Y;
			}
		}

		return frames;
	}
	#endregion


	#region	Protected Methods
	protected override Size MeasureOverride(Size constraint)
	{
		if (double.IsInfinity(constraint.Width) || double.IsInfinity(constraint.Height))
			constraint = MeasureArrangeHelper(constraint);

		return base.MeasureOverride(constraint);
	}

	/// <summary>
	/// Generate widget from a template widget
	/// </summary>
	/// <returns></returns>
	internal BnsCustomImageWidget Clone()
	{
		var widget = new BnsCustomImageWidget();

		// clone basic layout
		LayoutData.SetAnchors(widget, LayoutData.GetAnchors(this));
		LayoutData.SetOffsets(widget, LayoutData.GetOffsets(this));
		LayoutData.SetAlignments(widget, LayoutData.GetAlignments(this));

		// clone properties from template
		if (Expansion != null) widget.Expansion = [.. Expansion];
		widget.ExpansionComponentList = new(ExpansionComponentList);

		return widget;
	}
	#endregion
}