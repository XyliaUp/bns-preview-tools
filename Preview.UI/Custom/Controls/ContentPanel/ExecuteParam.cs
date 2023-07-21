using CUE4Parse.BNS.Conversion;

using Xylia.Extension;
using Xylia.Preview.Data.Helper;
using Xylia.Preview.Properties;
using Xylia.Xml;

namespace Xylia.Preview.UI.Custom.Controls;
public class ExecuteParam
{
	#region Fields
	public Font Font;

	public Color ForeColor;

	public HorizontalAlignment HorizontalAlignment;
	#endregion

	#region Constructor
	public ExecuteParam(Control c)
	{
		this.Font = c.Font;
		this.ForeColor = c.ForeColor;
	}
	#endregion

	#region Functions
	public ExecuteParam GetFont(string FontName, bool UseFontHeight = true)
	{
		if (FontName is null) return this;

		var param = (ExecuteParam)this.MemberwiseClone();
		if ((Program.IsDesignMode || Program.IsDebugMode) && FontName.StartsWith("00008130.Program.Fontset_ItemGrade_"))
		{
			param.ForeColor = GetGradeColor(byte.Parse(FontName.Replace("00008130.Program.Fontset_ItemGrade_", null)));
			return param;
		}


		#region	Font
		var UFontSet = FontName.GetUObject();
		if (UFontSet is not null)
		{
			var set = UFontSet.GetFont();

			// color
			if (set.Color != default) param.ForeColor = set.Color;

			// style
			var style = FontStyle.Regular;
			if (set.Italic) style |= FontStyle.Italic;
			if (set.Strokeout) style |= FontStyle.Strikeout;
			if (set.Underline) style |= FontStyle.Underline;

			// font
			float size = !UseFontHeight || set.Height == 0 ? param.Font.Size : set.Height;
			param.Font = new Font(param.Font.FontFamily, size, style);
		}
		#endregion

		return param;
	}

	public static Color GetGradeColor(byte grade) => grade switch
	{
		1 => Color.FromArgb(0x6c, 0x6c, 0x6c),
		2 => Color.FromArgb(0xff, 0xff, 0xff),
		3 => Color.FromArgb(0x58, 0xff, 0x77),
		4 => Color.FromArgb(0x46, 0xbe, 0xe1),
		5 => Color.FromArgb(0xd7, 0x39, 0xff),
		6 => Color.FromArgb(0xf1, 0xb2, 0x48),
		7 => Color.FromArgb(0xff, 0x77, 0x0a),
		8 => Color.FromArgb(0xff, 0x00, 0x84),
		_ => Color.White,
	};
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