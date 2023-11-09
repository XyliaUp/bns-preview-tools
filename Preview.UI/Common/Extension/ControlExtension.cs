using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using Xylia.Extension;
using Xylia.Preview.Common.Extension;

namespace Xylia.Preview.UI.Extension;
public static class ControlExtension
{
	public static System.Drawing.SizeF MeasureString(this string Txt, Font Font)
	{
		using Graphics g = Graphics.FromHwnd(IntPtr.Zero);

		var sf = StringFormat.GenericTypographic;
		sf.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;

		var Result = g.MeasureString(Txt, Font ?? System.Drawing.SystemFonts.DefaultFont, System.Drawing.PointF.Empty, sf);

		return Result;
	}


	public static void CallEvent(this object obj, string EventName, EventArgs e = null)
	{
		MethodInfo m = obj.GetType().GetMethod(EventName, ClassExtension.Flags);
		if (m is null) return;

		m.Invoke(obj, new object[1]
		{
			e is not null ? e : Type.GetType(m.GetParameters()[0].ParameterType.BaseType.FullName).GetProperty("Empty")
		});
	}

	public static void Remove<T>(this Control.ControlCollection collection) where T : Control =>
		collection.OfType<T>().ForEach(collection.Remove);



	public static Bitmap DrawMeToBitmap(this Control Obj, bool CopyRight = true)
	{
		DateTime dt = DateTime.Now;

		#region 初始化
		var Font = new Font("微软雅黑", 9.75F);

		List<Control> Controls = new List<Control>();
		foreach (Control item in Obj.Controls)
		{
			Controls.Add(item);
		}


		int w = 0, h = 0;
		foreach (var c in Controls.Where(c => c.Visible))
		{
			w = Math.Max(w, c.Right);
			h = Math.Max(h, c.Bottom);
		}

		if (CopyRight)
		{
			h += Font.Height + 2;
		}

		Bitmap bitmap = new Bitmap(w, h);
		Graphics g = Graphics.FromImage(bitmap);
		#endregion

		#region 获取背景色
		var CurObj = Obj;
		var BackColor = System.Drawing.Color.Transparent;
		while (CurObj != null && BackColor == System.Drawing.Color.Transparent)
		{
			BackColor = CurObj.BackColor;

			if (Obj.Parent is null) break;
			else CurObj = Obj.Parent;
		}
		#endregion


		using Brush brush = new SolidBrush(BackColor);
		g.FillRectangle(brush, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height));

		//绘制当前控件背景层
		if (Obj.BackgroundImage != null)
		{
			var TempImage = CurObj.BackgroundImage;
			g.DrawImage(TempImage, new System.Drawing.Rectangle(0, 0, TempImage.Width, TempImage.Height));
		}


		Controls.Sort(new ControlSort());
		foreach (var c in Controls.Where(c => c.Visible))
		{
			if (c.Width != 0 && c.Height != 0)
			{
				using Bitmap bitmap2 = new Bitmap(c.Width, c.Height);
				c.DrawToBitmap(bitmap2, c.ClientRectangle);
				g.DrawImage(bitmap2, new System.Drawing.PointF(c.Location.X, c.Location.Y));
			}
		}

		if (CopyRight)
		{
			StringFormat sf = new StringFormat();
			sf.Alignment = StringAlignment.Center;

			g.DrawString("由剑灵预览工具生成截图  Powered by Xylia, 2022.", Font, new SolidBrush(System.Drawing.Color.FromArgb(51, 204, 255)), bitmap.Width / 2, bitmap.Height - Font.Height - 1, sf);
		}

		return bitmap;
	}

	public class ControlSort : IComparer<Control>
	{
		public int Compare(Control x, Control y)
		{
			if (x is PictureBox) return -1;
			else if (y is PictureBox) return 1;
			else return 0;
		}
	}
}