using System;
using System.Windows;
using System.Windows.Controls;

namespace Xylia.Preview.UI.Controls;
public class VerticalBox : UserWidget
{
	protected override Size MeasureOverride(Size constraint)
	{
		Size desiredSize = new Size();
		Size layoutSlotSize = constraint with { Height = double.PositiveInfinity };

		foreach (UIElement child in Children)
		{
			if (child == null) continue;

			// Measure the child.
			child.Measure(layoutSlotSize);

			desiredSize.Width = Math.Max(desiredSize.Width, child.DesiredSize.Width);
			desiredSize.Height += child.DesiredSize.Height;
		}

		return desiredSize;
	}

	protected override Size ArrangeOverride(Size arrangeSize)
	{
		Rect rcChild = new Rect(arrangeSize);
		double previousChildSize = 0.0;

		foreach (UIElement child in Children)
		{
			if (child == null) continue;

			rcChild.Y += previousChildSize;
			rcChild.Height = previousChildSize = child.DesiredSize.Height;
			rcChild.Width = Math.Max(arrangeSize.Width, child.DesiredSize.Width);

			child.Arrange(rcChild);
		}

		return arrangeSize;
	}
}

public class HorizontalBox : StackPanel
{
	protected override Size MeasureOverride(Size constraint)
	{
		Size desiredSize = new Size();
		Size layoutSlotSize = constraint with { Width = double.PositiveInfinity };

		foreach (UIElement child in Children)
		{
			if (child == null) continue;

			// Measure the child.
			child.Measure(layoutSlotSize);

			desiredSize.Width += child.DesiredSize.Width;
			desiredSize.Height = Math.Max(desiredSize.Height, child.DesiredSize.Height);
		}

		return desiredSize;
	}

	protected override Size ArrangeOverride(Size arrangeSize)
	{
		Rect rcChild = new Rect(arrangeSize);
		double previousChildSize = 0.0;

		foreach (UIElement child in Children)
		{
			if (child == null) continue;

			rcChild.X += previousChildSize;
			rcChild.Width = previousChildSize = child.DesiredSize.Width;
			rcChild.Height = Math.Max(arrangeSize.Height, child.DesiredSize.Height);

			child.Arrange(rcChild);
		}

		return arrangeSize;
	}
}