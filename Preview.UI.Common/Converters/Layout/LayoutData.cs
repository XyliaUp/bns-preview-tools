using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using CUE4Parse.BNS.Assets.Exports;
using CUE4Parse.UE4.Objects.Core.Math;
using static Xylia.Preview.UI.Converters.LayoutData;

namespace Xylia.Preview.UI.Converters;
public class LayoutData
{
	#region Struct
	[TypeConverter(typeof(AnchorsConverter))]
	public struct Anchors
	{
		public Vector Minimum;
		public Vector Maximum;


		public static Anchors Full = new Anchors()
		{
			Minimum = new Vector(),
			Maximum = new Vector(1, 1)
		};
	}

	[TypeConverter(typeof(OffsetsConverter))]
	public struct Offsets
	{
		public double Left;
		public double Top;
		public double Right;
		public double Bottom;

		public Offsets(double left, double top, double right, double bottom)
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}

		public override string ToString() => $"{Left},{Top},{Right},{Bottom}";
	}
	#endregion


	#region Public Properties
	/// <summary>
	/// This is the dependency property registered for the AnchorPanel' Offset attached property.
	/// 
	/// The Offset property is read by a AnchorPanel on its children to determine where to position them.
	/// The child's offset from this property does not have an effect on the AnchorPanel' own size.
	/// </summary>
	public static readonly DependencyProperty OffsetsProperty = DependencyProperty.RegisterAttached("Offsets",
		typeof(Offsets), typeof(LayoutData), new FrameworkPropertyMetadata((Offsets)default,
			FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

	public static readonly DependencyProperty AnchorsProperty = DependencyProperty.RegisterAttached("Anchors",
		typeof(Anchors), typeof(LayoutData), new FrameworkPropertyMetadata((Anchors)default,
			FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

	public static readonly DependencyProperty AlignmentsProperty = DependencyProperty.RegisterAttached("Alignments",
		typeof(Vector), typeof(LayoutData), new FrameworkPropertyMetadata((Vector)default,
			FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));
	#endregion

	#region Public Methods
	/// <summary>
	/// Reads the attached property Anchor from the given element.
	/// </summary>
	/// <param name="element">The element from which to read the Anchor attached property.</param>
	/// <returns>The property's value.</returns>
	/// <seealso cref="Anchor.AnchorProperty" />
	public static Anchors GetAnchors(UIElement element)
	{
		ArgumentNullException.ThrowIfNull(element);
		return (Anchors)element.GetValue(AnchorsProperty);
	}

	/// <summary>
	/// Writes the attached property Anchor to the given element.
	/// </summary>
	/// <param name="element">The element to which to write the Anchor attached property.</param>
	/// <param name="value">The Anchor to set</param>
	/// <seealso cref="Anchor.AnchorProperty" />
	public static void SetAnchors(UIElement element, Anchors value)
	{
		ArgumentNullException.ThrowIfNull(element);
		element.SetValue(AnchorsProperty, value);
	}

	/// <summary>
	/// Reads the attached property Offset from the given element.
	/// </summary>
	/// <param name="element">The element from which to read the Offset attached property.</param>
	/// <returns>The property's value.</returns>
	/// <seealso cref="Anchor.OffsetProperty" />
	public static Offsets GetOffsets(UIElement element)
	{
		ArgumentNullException.ThrowIfNull(element);
		return (Offsets)element.GetValue(OffsetsProperty);
	}

	/// <summary>
	/// Writes the attached property Offset to the given element.
	/// </summary>
	/// <param name="element">The element to which to write the Offset attached property.</param>
	/// <param name="value">The offset to set</param>
	/// <seealso cref="Anchor.OffsetProperty" />
	public static void SetOffsets(UIElement element, Offsets value)
	{
		ArgumentNullException.ThrowIfNull(element);
		element.SetValue(OffsetsProperty, value);
	}

	/// <summary>
	/// Reads the attached property Alignment from the given element.
	/// </summary>
	/// <param name="element">The element from which to read the Alignment attached property.</param>
	/// <returns>The property's value.</returns>
	/// <seealso cref="AlignmentsProperty" />
	public static Vector GetAlignments(UIElement element)
	{
		ArgumentNullException.ThrowIfNull(element);
		return (Vector)element.GetValue(AlignmentsProperty);
	}

	/// <summary>
	/// Writes the attached property Alignment to the given element.
	/// </summary>
	/// <param name="element">The element to which to write the Alignment attached property.</param>
	/// <param name="value">The Alignment to set</param>
	/// <seealso cref="AlignmentsProperty" />
	public static void SetAlignments(UIElement element, Vector value)
	{
		ArgumentNullException.ThrowIfNull(element);
		element.SetValue(AlignmentsProperty, value);
	}


	public static Point ComputeOffset(Size clientSize, FVector2D inkSize, HAlignment ha = default, VAlignment va = default, FVector2D Offset = default)
	{
		var offset = new Point();

		if (ha == HAlignment.HAlign_Center)
		{
			offset.X = (clientSize.Width - inkSize.X) * 0.5;
		}
		else if (ha == HAlignment.HAlign_Right)
		{
			offset.X = clientSize.Width - inkSize.X;
		}
		else
		{
			offset.X = 0;
		}

		if (va == VAlignment.VAlign_Center)
		{
			offset.Y = (clientSize.Height - inkSize.Y) * 0.5;
		}
		else if (va == VAlignment.VAlign_Bottom)
		{
			offset.Y = clientSize.Height - inkSize.Y;
		}
		else
		{
			offset.Y = 0;
		}

		offset.X += Offset.X;
		offset.Y += Offset.Y;

		return offset;
	}
	#endregion
}

#region Converter
public class AnchorsConverter : TypeConverter
{
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		if (sourceType == typeof(string)) return true;

		return base.CanConvertFrom(context, sourceType);
	}

	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (value is string s)
		{
			var anchor = new Anchors();

			try
			{
				var collection = VectorCollection.Parse(s);
				if (collection.Count == 1)
				{
					var vect = collection[0];
					anchor.Minimum = new Vector(vect.X, vect.X);
					anchor.Maximum = new Vector(vect.Y, vect.Y);
				}
				else
				{
					anchor.Minimum = collection[0];
					anchor.Maximum = collection[1];
				}
			}
			catch
			{

			}

			return anchor;
		}

		return base.ConvertFrom(context, culture, value);
	}
}

public class OffsetsConverter : TypeConverter
{
	public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
	{
		if (sourceType == typeof(string)) return true;

		return base.CanConvertFrom(context, sourceType);
	}

	public override object ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
	{
		if (value is string s)
		{
			var anchor = new Offsets();

			var array = s.Split(' ', ',');
			if (array.Length >= 2)
			{
				anchor.Left = Convert.ToDouble(array[0]);
				anchor.Top = Convert.ToDouble(array[1]);
			}

			if (array.Length == 4)
			{
				anchor.Right = Convert.ToDouble(array[2]);
				anchor.Bottom = Convert.ToDouble(array[3]);
			}

			return anchor;
		}

		return base.ConvertFrom(context, culture, value);
	}
}
#endregion