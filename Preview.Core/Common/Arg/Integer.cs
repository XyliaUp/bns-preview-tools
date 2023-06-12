namespace Xylia.Preview.Common.Arg;

public sealed class Integer : ArgParam<float>
{
	public Integer(float Value) => this.Value = Value;



	public string FloatDot0 => (Value / 10).ToString("#0");

	public string FloatDot1 => (Value / 10).ToString("#0.0");

	public string FloatDot2 => (Value / 10).ToString("#0.00");

	public string Dategmtime24 => $"#{Value} dategmtime24";

	public string Money => new MoneyConvert((int)Value).ToString();

	public string MoneyDefault => $"#{Value} MoneyDefault";

	public string MoneyNonTooltip => $"#{Value} MoneyNonTooltip";

	public string Time => $"#{Value}Time";

	public string Timedate => $"#{Value}Timedate";

	public string Timedhm => $"#{Value}Timedhm";

	public string Timehm => $"#{Value}Timehm";

	public string Timeymd => $"#{Value}Timeymd";

	public string TimeRoundDown => $"#{Value}TimeRoundDown";
}

public struct MoneyConvert
{
	#region Constructor
	public MoneyConvert(int? Total) => this.Total = Total ?? 0;

	public MoneyConvert(string Total) => this.Total = int.TryParse(Total, out int temp) ? temp : 0;
	#endregion

	#region Properties
	public int Total { get; set; }

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