using Xylia.Configure;
using Xylia.Extension;

namespace Xylia.Preview.UI.Extension;
public static class ControlExtension
{
	private static string GetKey(this Control c) => $"{c.FindForm().Name}_{c.Name}";

	public static void ReadConfig(this Control container)
	{
		foreach (Control c in container.Controls)
		{
			c.ReadConfig();

			var val = Ini.ReadValue("Config", c.GetKey());
			if (string.IsNullOrWhiteSpace(val)) continue;

			if (c is CheckBox checkBox) checkBox.Checked = val.ToBool();
			else c.Text = val;
		}
	}

	public static void SaveConfig(this Control c)
	{
		Ini.WriteValue("Config", c.GetKey(), c.Text);
	}





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
		Color BackColor = Color.Transparent;
		while (CurObj != null && BackColor == Color.Transparent)
		{
			BackColor = CurObj.BackColor;

			if (Obj.Parent is null) break;
			else CurObj = Obj.Parent;
		}
		#endregion


		using Brush brush = new SolidBrush(BackColor);
		g.FillRectangle(brush, new Rectangle(0, 0, bitmap.Width, bitmap.Height));

		//绘制当前控件背景层
		if (Obj.BackgroundImage != null)
		{
			var TempImage = CurObj.BackgroundImage;
			g.DrawImage(TempImage, new Rectangle(0, 0, TempImage.Width, TempImage.Height));
		}


		Controls.Sort( new ControlSort());
		foreach (var c in Controls.Where(c => c.Visible))
		{
			if (c.Width != 0 && c.Height != 0)
			{
				using Bitmap bitmap2 = new Bitmap(c.Width, c.Height);
				c.DrawToBitmap(bitmap2, c.ClientRectangle);
				g.DrawImage(bitmap2, new PointF(c.Location.X, c.Location.Y));
			}
		}

		if (CopyRight)
		{
			StringFormat sf = new StringFormat();
			sf.Alignment = StringAlignment.Center;

			g.DrawString("由剑灵预览工具生成截图  Powered by Xylia, 2022.", Font, new SolidBrush(Color.FromArgb(51, 204, 255)), bitmap.Width / 2, bitmap.Height - Font.Height - 1, sf);
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
