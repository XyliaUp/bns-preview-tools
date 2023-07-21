namespace Xylia.Preview.Common.Arg;
public sealed class Integer
{
	#region Constructor
	float Value { get; set; }

	public Integer(float Value) => this.Value = Value;
	#endregion


	public override string ToString() => Value.ToString();

	public string FloatDot0 => (Value / 10).ToString("#0");

	public string FloatDot1 => (Value / 10).ToString("#0.0");

	public string FloatDot2 => (Value / 10).ToString("#0.00");

	public string Dategmtime24 => $"#{Value} dategmtime24";

	public string Time => $"#{Value}Time";

	public string Timedate => $"#{Value}Timedate";

	public string Timedhm => $"#{Value}Timedhm";

	public string Timehm => $"#{Value}Timehm";

	public string Timeymd => $"#{Value}Timeymd";

	public string TimeRoundDown => $"#{Value}TimeRoundDown";



	public Money Money => new Money((int)Value);
	public Money MoneyDefault => new Money((int)Value);
	public string MoneyNonTooltip => $"#{Value} MoneyNonTooltip";
}

public struct Money
{
	#region Constructor
	public Money(int Total) => this.Total = Total;
	#endregion

	#region Properties
	readonly int Total;

	public int Gold => this.Total / 10000;

	public int Silver => (this.Total % 10000) / 100;

	public int Copper => this.Total % 100;
	#endregion


	public override string ToString() => this.ToString(true);

	public string ToString(bool ShowImg = true)
	{
		string Info = null;

		if (this.Gold != 0) Info += this.Gold + (!ShowImg ? "金" : "<image enablescale=\"true\" path=\"00009076.GameUI_Coin_Gold\" scalerate=\"1.2\"/>");
		if (this.Silver != 0) Info += this.Silver + (!ShowImg ? "银" : "<image enablescale=\"true\" path=\"00009076.GameUI_Coin_Silver\" scalerate=\"1.2\"/>");
		if (this.Copper != 0) Info += this.Copper + (!ShowImg ? "铜" : "<image enablescale=\"true\" path=\"00009076.GameUI_Coin_Bronze\" scalerate=\"1.2\"/>");

		return Info;
	}
}