using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Controls;
/// <summary>
/// temp implement
/// TODO: not use style
/// </summary>
public class BnsCustomCaptionWidget : Control //FrameworkElement
{
	#region Constructor
	static BnsCustomCaptionWidget()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(BnsCustomCaptionWidget), new FrameworkPropertyMetadata(typeof(BnsCustomCaptionWidget)));
	}
	#endregion

	#region Public Properties
	/// <summary>
	/// Gets/Sets the Source on this Image.
	///
	/// The Source property is the ImageSource that holds the actual image drawn.
	/// </summary>
	public ImageSource BaseImage
	{
		get { return (ImageSource)GetValue(BaseImageProperty); }
		set { SetValue(BaseImageProperty, value); }
	}

	/// <summary>
	/// DependencyProperty for Image Source property.
	/// </summary>
	/// <seealso cref="Image.Source" />
	public static readonly DependencyProperty BaseImageProperty = DependencyProperty.Register(nameof(BaseImage), typeof(ImageSource), typeof(BnsCustomCaptionWidget));

	public string String
	{
		get { return (string)GetValue(StringProperty); }
		set { SetValue(StringProperty, value); }
	}
	public static readonly DependencyProperty StringProperty = DependencyProperty.Register(nameof(String), typeof(string), typeof(BnsCustomCaptionWidget));

	public string MetaData
	{
		get { return (string)GetValue(MetaDataProperty); }
		set { SetValue(MetaDataProperty, value); }
	}
	public static readonly DependencyProperty MetaDataProperty = DependencyProperty.Register(nameof(MetaData), typeof(string), typeof(BnsCustomCaptionWidget));

	public string Text
	{
		get
		{
			var ls = MetaData?.Split('=', 2);
			if (ls != null && ls[0] == "textref") return ls[1].GetText();

			return String;
		}
	}
	#endregion
}