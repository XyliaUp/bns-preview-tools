using System.Windows;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Models.Creature;

namespace Xylia.Preview.UI.Views.Pages;
public partial class AbilitySetting
{
	public AbilitySetting()
	{
		InitializeComponent();
	}

	private void GetFactor_Click(object sender, RoutedEventArgs e)
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
		double k = Page1_k_Lock.IsChecked == true ? Page1_k.Text.ToDouble() : 0;
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
	}

	private void GetParams_Click(object sender, RoutedEventArgs e)
	{
		if (!double.TryParse(Page2_Value1.Text, out double Value1)) return;
		if (!double.TryParse(Page2_Value2.Text, out double Value2)) return;
		if (!double.TryParse(Page2_Percent1.Text, out double Percent1)) return;
		if (!double.TryParse(Page2_Percent2.Text, out double Percent2)) return;

		var level1 = (sbyte)Level1.Value;
		var level2 = (sbyte)Level2.Value;


		var ability = new AbilityFunction()
		{
			C = int.Parse(Txt_Inital.Text),
			K = int.Parse(Params_k.Text),
		};
		var Factor1 = new AbilityFunction.LevelFactor(level1, ability.GetFactor(Value1, Percent1));
		var Factor2 = new AbilityFunction.LevelFactor(level2, ability.GetFactor(Value2, Percent2));

		ability.GetFactorParam(Factor1, Factor2);
		Params_k.Text = ability.Φ.ToString();
		Params_Φ.Text = ability.μ.ToString();
	}
}
