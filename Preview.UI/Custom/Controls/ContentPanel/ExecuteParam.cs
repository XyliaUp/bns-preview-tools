using CUE4Parse.BNS.Exports;

using Xylia.Extension;
using Xylia.Preview.Data.Helper;

namespace Xylia.Preview.UI.Custom.Controls;
public class ExecuteParam
{
	#region Fields
	public Font Font;

	public Color ForeColor;

	public HorizontalAlignment HorizontalAlignment;
	#endregion

	#region Constructor
	public ExecuteParam()
	{
		this.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point);
	}

	public ExecuteParam(Control c)
	{
		this.Font = c.Font;
		this.ForeColor = c.ForeColor;
	}
	#endregion


	#region Functions
	public ExecuteParam GetFont(string name)
	{
		if (name is null) return this;
		var param = (ExecuteParam)this.MemberwiseClone();

		#region	Font
		var FontSet = FileCache.Provider.LoadObject<UFontSet>(name);
		if (FontSet is not null)
		{
			float size = param.Font.Size;
			var style = FontStyle.Regular;

			if (FontSet.FontFace?.Load() is UBNSFontFace fontFace)
			{
				size = fontFace.GetOrDefault<float>("Height") / 1.15F;
			}

			if (FontSet.FontAttribute?.Load() is UFontAttribute fontAttribute)
			{
				if (fontAttribute.Italic) style |= FontStyle.Italic;
				//if (fontAttribute.Shadow) style |= FontStyle.Shadow;
				if (fontAttribute.Strokeout) style |= FontStyle.Strikeout;
				if (fontAttribute.Underline) style |= FontStyle.Underline;
			}

			if (FontSet.FontColors?.Load() is UFontColor fontColor)
			{
				var color = fontColor.FontColor.ToFColor(true);

				param.ForeColor = Color.FromArgb(color.A, color.R, color.G, color.B);
			}


			param.Font = new Font(param.Font.FontFamily, size, style);
		}
		#endregion

		return param;
	}
	#endregion
}

public class ExecuteUnit
{
	public ExecuteParam param;

	public PointF point;

	public int Width;


	public string text;

	public Bitmap bitmap;



	#region Constructor
	public ExecuteUnit(ExecuteParam param, PointF point, string text)
	{
		this.param = param;
		this.point = point;
		this.text = text;
		this.Width = (int)text.MeasureString(param.Font).Width;
	}

	public ExecuteUnit(ExecuteParam param, PointF point, Bitmap bitmap)
	{
		this.param = param;
		this.point = point;
		this.bitmap = bitmap;
		this.Width = bitmap.Width;
	}
	#endregion
}