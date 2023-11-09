using System.Windows.Forms;

using Xylia.Extension;
using Xylia.Preview.Data.Models.Creature;

namespace Xylia.Preview.UI.Views.Pages;
public partial class SettingFrm : Form
{
	#region Constructor
	public SettingFrm()
	{
		InitializeComponent();
	}
	#endregion


	#region Page1
	private void Button1_Click(object sender, EventArgs e)
	{
		#region Initialize
		var Value1 = Page1_Value1.Text.ToDouble();
		var Value2 = Page1_Value2.Text.ToDouble();

		var extra = Txt_Inital.Text.ToDouble();
		var Percent1 = Page1_Percent1.Text.ToDouble();
		var Percent2 = Page1_Percent2.Text.ToDouble();

		Percent1 = (Percent1 - extra) / 100;
		Percent2 = (Percent2 - extra) / 100;

		//Debug.WriteLine($"比率1 = {Percent1}\n比率2 = {Percent2}");
		#endregion

		#region 计算
		double k = Page1_k_Lock.Checked ? Page1_k.Text.ToDouble() : 0;
		double A;

		if (k == 0)
		{
			k = (Value2 * Percent1 * Percent2 - Value1 * Percent1 * Percent2) / (Value2 * Percent1 - Value1 * Percent2);
			A = (-Value1 * Value2 * Percent2 + Value2 * Value1 * Percent1) / (Value1 * Percent2 - Value2 * Percent1);
		}
		else
		{
			A = ((Value1 * k / Percent1 - Value1) + (Value2 * k / Percent2 - Value2)) / 2;
		}


		this.Page1_k.Text = k.ToString();
		this.Page1_A.Text = A.ToString();
		#endregion


		DialogResult = DialogResult.OK;
	}
	#endregion

	#region Page2
	private void Button2_Click(object sender, EventArgs e)
	{
		if (!double.TryParse(Page2_Value1.Text, out double Value1)) return;
		if (!double.TryParse(Page2_Value2.Text, out double Value2)) return;
		if (!double.TryParse(Page2_Percent1.Text, out double Percent1)) return;
		if (!double.TryParse(Page2_Percent2.Text, out double Percent2)) return;

		var level1 = (sbyte)numericUpDown1.Value;
		var level2 = (sbyte)numericUpDown2.Value;


		var ability = new AbilityFunction()
		{
			C = int.Parse(Txt_Inital.Text),
			K = int.Parse(textBox7.Text),
		};
		var Factor1 = new AbilityFunction.LevelFactor(level1, ability.GetFactor(Value1, Percent1));
		var Factor2 = new AbilityFunction.LevelFactor(level2, ability.GetFactor(Value2, Percent2));

		ability.GetFactorParam(Factor1, Factor2);
		this.textBox8.Text = ability.Φ.ToString();
		this.textBox9.Text = ability.μ.ToString();
	}

	private void AttrInfoChanged1(object sender, EventArgs e)
	{
		try
		{
			var ability = new AbilityFunction()
			{
				C = int.Parse(Txt_Inital.Text),
				K = int.Parse(textBox7.Text),
			};

			var level = (sbyte)numericUpDown1.Value;
			var value = Page2_Value1.Text.ToDouble();
			var percent = Page2_Percent1.Text.ToDouble();
			var factor = new AbilityFunction.LevelFactor(level, ability.GetFactor(value, percent));

			this.Page2_Factor1.Text = factor.Value.ToString();
			this.Page2_Factor1.Visible = true;
		}
		catch
		{
			this.Page2_Factor1.Visible = false;
		}
	}

	private void AttrInfoChanged2(object sender, EventArgs e)
	{
		try
		{
			var ability = new AbilityFunction()
			{
				C = int.Parse(Txt_Inital.Text),
				K = int.Parse(textBox7.Text),
			};

			var level = (sbyte)numericUpDown2.Value;
			var value = Page2_Value1.Text.ToDouble();
			var percent = Page2_Percent1.Text.ToDouble();
			var factor = new AbilityFunction.LevelFactor(level, ability.GetFactor(value, percent));

			this.Page2_Factor2.Text = factor.Value.ToString();
			this.Page2_Factor2.Visible = true;
		}
		catch
		{
			this.Page2_Factor2.Visible = false;
		}
	}
	#endregion
}