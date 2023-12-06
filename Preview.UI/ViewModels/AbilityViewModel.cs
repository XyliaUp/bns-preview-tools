using System.Reflection;

using CommunityToolkit.Mvvm.ComponentModel;

using Xylia.Preview.Data.Models.Creature;

namespace Xylia.Preview.UI.ViewModels;
public class AbilityViewModel : ObservableObject
{
	List<AbilityFunction> source;

	public List<AbilityFunction> Source
	{
		get => source;
		private set => SetProperty(ref source, value);
	}


	AbilityFunction selected;

	public AbilityFunction Selected
	{
		get => selected;
		set => SetProperty(ref selected, value);
	}


	sbyte _level;
	public sbyte Level
	{
		get => _level;
		set { SetProperty(ref _level, value); GetPercent(); }
	}

	int _value;
	public int Value
	{
		get => _value;
		set { SetProperty(ref _value, value); GetPercent(); }
	}

	double percent;

	public double Percent
	{
		get => percent;
		private set => SetProperty(ref percent, value);
	}



	#region Methods
	public AbilityViewModel()
	{
		var source = typeof(AbilityFunction)
			.GetProperties(BindingFlags.Static | BindingFlags.Public)
			.Select(x => x.GetValue(null))
			.Where(x => x is AbilityFunction ability && ability.K != 0)
			.OfType<AbilityFunction>();

		this.Source = source.ToList();
	}

	public void GetPercent()
	{
		if (Selected == null) return;

		double extra = 0;   // double)numericUpDown1.Value * 0.01;
		double percent = Selected.GetPercent(Value, Level) + extra;
		this.Percent = percent;
	}
	#endregion
}