using System.Windows.Media;

using CUE4Parse.BNS.Exports;

using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.UI.Common.Documents;
public class Font : Element
{
	#region Constructor
	public Font()
	{

	}

	public Font(string Name , params Element[] elements)
	{
		this.Name = Name;
		this.Children = elements.ToList();
	}
	#endregion


	/// <summary>
	/// fontset path
	/// </summary>
	public string Name;


	protected override Size MeasureCore(Size availableSize)
	{
		GetFont(Name);
		return base.MeasureCore(availableSize);
	}

	private void GetFont(string name)
	{
		#region data
		var FontSet = FileCache.Provider.LoadObject<UFontSet>(name);
		if (FontSet is null) return;

		var FontFace = FontSet.FontFace?.Load<UBNSFontFace>();
		var FontAttribute = FontSet.FontAttribute?.Load<UFontAttribute>();
		var FontColor = FontSet.FontColors?.Load<UFontColor>();
		#endregion


		//param.Font = new Font(param.Font.FontFamily, size, style);

		if (FontFace != null)
		{
			FontSize = FontFace.Height;
		}

		if (FontAttribute != null)
		{
			var style = System.Drawing.FontStyle.Regular;

			if (FontAttribute.Italic) style |= System.Drawing.FontStyle.Italic;
			//if (FontAttribute.Shadow) style |= System.Drawing.FontStyle.Shadow;
			if (FontAttribute.Strokeout) style |= System.Drawing.FontStyle.Strikeout;
			if (FontAttribute.Underline) style |= System.Drawing.FontStyle.Underline;
		}

		if (FontColor != null)
		{
			var f = FontColor.FontColor.ToFColor(true);
			var c = Color.FromArgb(f.A, f.R, f.G, f.B);
			Foreground = new SolidColorBrush(c);
		}
	}
}