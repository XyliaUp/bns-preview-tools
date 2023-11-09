using System.Diagnostics;
using System.Drawing;

using Vanara.PInvoke;

using Xylia.Extension;
using Xylia.Preview.Data.Models.Config;

namespace Xylia.Preview.Data.Models;
public class ClientConfiguration : ConfigTable
{
	#region Load Methods
	public string version;

	public static ClientConfiguration LoadFrom()
	{
		var path = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "\\BnS\\TENCENT\\ClientConfiguration.xml");
		return LoadFrom<ClientConfiguration>(new FileInfo(path));
	}
	#endregion

	#region Methods
	public struct HUDControl
	{
		public float x;
		public float y;
		public int a_h;
		public int a_v;
		public string name;
		public int o;
		public int l_h_t;
		public int l_v_t;
		public float l_h_o_1;
		public float l_v_o_1;
		public float l_h_o_2;
		public float l_v_o_2;
		public string l_h_tn_1;
		public string l_v_tn_1;
		public string l_h_tn_2;
		public string l_v_tn_2;

		public HUDControl(string group, IReadOnlyDictionary<string, Option> options)
		{
			// classic mode? 
			if (!options.ContainsKey(group + "x")) group += "1-";

			// struct
			x = (options.GetValueOrDefault(group + "x")?.value).ToFloat32();
			y = (options.GetValueOrDefault(group + "y")?.value).ToFloat32();
			a_h = (options.GetValueOrDefault(group + "a-h")?.value).ToInt32();
			a_v = (options.GetValueOrDefault(group + "a-v")?.value).ToInt32();
			name = (options.GetValueOrDefault(group + "n")?.value);
			o = (options.GetValueOrDefault(group + "o")?.value).ToInt32();
			l_h_t = (options.GetValueOrDefault(group + "l-h-t")?.value).ToInt32();
			l_v_t = (options.GetValueOrDefault(group + "l-v-t")?.value).ToInt32();
			l_h_o_1 = (options.GetValueOrDefault(group + "l-h-o-1")?.value).ToFloat32();
			l_v_o_1 = (options.GetValueOrDefault(group + "l-v-o-1")?.value).ToFloat32();
			l_h_o_2 = (options.GetValueOrDefault(group + "l-h-o-2")?.value).ToFloat32();
			l_v_o_2 = (options.GetValueOrDefault(group + "l-v-o-2")?.value).ToFloat32();
			l_h_tn_1 = (options.GetValueOrDefault(group + "l-h-tn-1")?.value);
			l_v_tn_1 = (options.GetValueOrDefault(group + "l-v-tn-1")?.value);
			l_h_tn_2 = (options.GetValueOrDefault(group + "l-h-tn-2")?.value);
			l_v_tn_2 = (options.GetValueOrDefault(group + "l-v-tn-2")?.value);


			this.size = FixSize();
		}


		public SizeF size;
		private SizeF FixSize()
		{
			switch (name)
			{
				case "SkillBar_Line_1":
				case "SkillBar_Line_2":
					return new SizeF(200, 60);

				// Q E SS TAB
				case "Cmd_Left_Icon":
				case "Cmd_Right_Icon":
				case "Cmd_Down_Icon":
				case "SkillBarStanceHolder":

				// LB RB F G B
				case "Context_Icon_1":
				case "Context_Icon_2":
				case "Context_Icon_3":
				case "Context_Icon_4":
				case "Context_Icon_5":
				case "Context_Classic_Icon_1":
				case "Context_Classic_Icon_2":
				case "Context_Classic_Icon_3":
				case "Context_Classic_Icon_4":
				case "Context_Classic_Icon_5":
					return new SizeF(60, 60);

				// nova
				case "novaSkillBar": return new SizeF(80, 80);


				default: return default;
			}
		}
	}

	public void Test()
	{
		// process
		var process = Process.GetProcessesByName("BNSR").FirstOrDefault();
		if (process is null) Console.WriteLine("process not found");

		// option
		//var UiScale = (hash.GetValueOrDefault("ui-scale")?.value).ToInt32();
		//var controls = Linq.For(150, id => new HUDControl($"hc-{id}-", hash));


		#region draw at process
		var hwnd = process.MainWindowHandle;
		User32.GetWindowRect(hwnd, out var lprect);

		//foreach (var control in controls)
		//{
		//	var w = control.x * (lprect.Width - control.size.Width);
		//	var h = control.y * (lprect.Height - control.size.Height);

		//	// TODO
		//}
		#endregion

		#region Test
		//Rectangle rect = new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
		//Bitmap bmp = new Bitmap(rect.Width, rect.Height);

		//Graphics gp = Graphics.FromImage(bmp);
		//gp.CopyFromScreen(0, 0, 0, 0, rect.Size);
		//gp.DrawImage(bmp, 0, 0, rect, GraphicsUnit.Pixel);

		//foreach (var control in controls)
		//{
		//	var w = control.x * (lprect.Width - control.size.Width);
		//	var h = control.y * (lprect.Height - control.size.Height);

		//	gp.DrawString(control.name, new Font("Microsoft YaHei UI", 7F), new SolidBrush(Color.Red),
		//		new PointF(w, h));
		//}

		//bmp.Save(@"C:\Users\10565\Desktop\test.png");
		#endregion
	}
	#endregion
}