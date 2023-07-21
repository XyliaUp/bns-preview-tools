using HZH_Controls.Controls;

using Xylia.Configure;

namespace Xylia.Match.Util;
public static class TestEx
{
	public static void SaveConfig(object sender, EventArgs e)
	{
		object value;
		if (sender is not Control c) return;
		else if (sender is CheckBox CheckBox) value = CheckBox.Checked;
		else if (sender is UCCheckBox UCCheckBox) value = UCCheckBox.Checked;
		else value = c.Text;


		Control frm = c;
		while (frm is not Form and not UserControl)
			frm = frm.Parent;

		Ini.WriteValue(frm.GetType(), c.Name, value);
	}
}