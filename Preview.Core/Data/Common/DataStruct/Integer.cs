namespace Xylia.Preview.Data.Common.DataStruct;
public struct Integer
{
    #region Constructor
    public double Value { get; set; }

    public Integer(double Value) => this.Value = Value;
	#endregion


	#region Properties
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

    public Money Money => (int)Value;
    public Money MoneyDefault => (int)Value;
    public string MoneyNonTooltip => $"#{Value} MoneyNonTooltip";
	#endregion
}

public struct Money
{
	#region Constructor
	public int Value { get; private set; }

	public Money(int Value) => this.Value = Value;
	#endregion

	#region Properties
	public readonly int Gold => Value / 10000;
    public readonly int Silver => Value % 10000 / 100;
    public readonly int Copper => Value % 100;
	#endregion


	public static implicit operator Money(int value) => new() { Value = value };
}