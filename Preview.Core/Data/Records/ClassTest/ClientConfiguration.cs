using System.Xml;
using System.Xml.Serialization;

using Xylia.Extension;
using Xylia.Xml;

namespace Xylia.Preview.Data.Record.Test;

[XmlRoot("config")]
public class ClientConfiguration
{
	[XmlElement]
	public List<Option> option = new();


	public static ClientConfiguration LoadFrom()
	{
		var path = string.Concat(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "\\BnS\\TENCENT\\ClientConfiguration.xml");
		var config = File.ReadAllText(path).GetObject<ClientConfiguration>();

		// create option hash
		var hash = new OptionCollection(config.option);


		var UiScale = (hash["ui-scale"]?.value).ToInt32();
		var controls = Linq.For(150, id => new HUDControl($"hc-{id}-", hash));




		Rectangle tScreenRect = new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
		Bitmap tSrcBmp = new Bitmap(tScreenRect.Width, tScreenRect.Height);

		Graphics gp = Graphics.FromImage(tSrcBmp);
		gp.CopyFromScreen(0, 0, 0, 0, tScreenRect.Size);
		gp.DrawImage(tSrcBmp, 0, 0, tScreenRect, GraphicsUnit.Pixel);
																	  
		var font = new Font("Microsoft YaHei UI", 7F);

		foreach (var control in controls)
		{
			//Trace.WriteLine(control.name);

			gp.DrawString(control.name, font, new SolidBrush(Color.Red), 
				new PointF(tScreenRect.Width * control.x, tScreenRect.Height * control.y));
		}


		tSrcBmp.Save(@"C:\Users\10565\Desktop\test.png");




		return config;
	}

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

		public HUDControl(string group, OptionCollection options)
		{
			x = (options[group + "x"]?.value).ToFloat32();
			y = (options[group + "y"]?.value).ToFloat32();
			a_h = (options[group + "a-h"]?.value).ToInt32();
			a_v = (options[group + "a-v"]?.value).ToInt32();
			name = (options[group + "n"]?.value);
			o = (options[group + "o"]?.value).ToInt32();
			l_h_t = (options[group + "l-h-t"]?.value).ToInt32();
			l_v_t = (options[group + "l-v-t"]?.value).ToInt32();
			l_h_o_1 = (options[group + "l-h-o-1"]?.value).ToFloat32();
			l_v_o_1 = (options[group + "l-v-o-1"]?.value).ToFloat32();
			l_h_o_2 = (options[group + "l-h-o-2"]?.value).ToFloat32();
			l_v_o_2 = (options[group + "l-v-o-2"]?.value).ToFloat32();
			l_h_tn_1 = (options[group + "l-h-tn-1"]?.value);
			l_v_tn_1 = (options[group + "l-v-tn-1"]?.value);
			l_h_tn_2 = (options[group + "l-h-tn-2"]?.value);
			l_v_tn_2 = (options[group + "l-v-tn-2"]?.value);


			this.FixSize();
		}


		public SizeF size;
		private void FixSize()
		{
			//if (name == "Cmd_Left_Icon") size = ;


			//79,76
		}
	}
}




	 


public class Option
{
	[XmlAttribute]
	public string name;

	[XmlAttribute]
	public string value;
}

public class OptionCollection
{
	readonly Dictionary<string, Option> value;

	public OptionCollection(IEnumerable<Option> options)
	{
		value = options.ToLookup(o => o.name, o => o).ToDictionary(o => o.Key, o => o.First());
	}

	public Option this[string name] => value.GetValueOrDefault(name);
}