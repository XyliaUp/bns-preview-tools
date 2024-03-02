using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Xylia.Preview.UI.Extensions;
public static class ControlHelpers
{
	#region Common
	public static byte[] Snapshot(this FrameworkElement element)
	{
		var bounds = new Rect(element.DesiredSize);
		element.Arrange(bounds);

		// If you need to create a bitmap with a specified resolution, you could directly
		// pass the specified dpiX and dpiY values to RenderTargetBitmap constructor.
		var bmp = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, 96d, 96d, PixelFormats.Pbgra32);
		bmp.Render(element);

		return bmp.AsData();
	}

	public static FrameworkElement GetPlacement(this MenuItem menuItem)
	{
		var menu = menuItem.Parent as ContextMenu;
		return menu.PlacementTarget as FrameworkElement;
	}

	public static void SetBinding(this DependencyObject target, DependencyProperty dp, BindingBase binding)
	{
		BindingOperations.SetBinding(target, dp, binding);
	}
	#endregion

	#region Property
	public static DependencyProperty Register<T>(this Type ownerType, string name, T? defaultValue = default,
		FrameworkPropertyMetadataOptions flags = FrameworkPropertyMetadataOptions.AffectsRender,
		PropertyChangedCallback? callback = null)
	{
		return DependencyProperty.Register(name, typeof(T), ownerType,
			new FrameworkPropertyMetadata(defaultValue, flags, callback));
	}
	#endregion

	#region Valid
	/// <summary>
	/// Validate input value
	/// </summary>
	/// <param name="value"></param>
	/// <returns>Returns False if value is NaN or NegativeInfinity or PositiveInfinity. Otherwise, returns True.</returns>
	public static bool IsValidDoubleValue(object value)
	{
		double d = (double)value;

		return !(double.IsNaN(d) || double.IsInfinity(d));
	}

	/// <summary>
	/// Validate input value
	/// </summary>
	/// <param name="value"></param>
	/// <returns>Returns False if value is NaN or NegativeInfinity or PositiveInfinity or negative. Otherwise, returns True.</returns>
	public static bool IsValidChange(object value)
	{
		double d = (double)value;

		return IsValidDoubleValue(value) && d >= 0.0;
	}
	#endregion
}