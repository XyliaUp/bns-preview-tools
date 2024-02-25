using System.Text;

namespace Xylia.Preview.Data.Common.DataStruct;
public struct Integer(double Value)
{
	#region Float
	public override readonly string ToString() => Value.ToString();
	public readonly string FloatDot0 => $"{Value / 10: 0}";
	public readonly string FloatDot1 => $"{Value / 10: 0.0}";
	public readonly string FloatDot2 => $"{Value / 10: 0.00}";
	#endregion

	#region Time
	public readonly string Dategmtime24 => $"#{Value} dategmtime24";
	public readonly string Time => $"#{Value}Time";
	public readonly string Timedate => $"#{Value}Timedate";
	public readonly string Timedhm => $"#{Value}Timedhm";
	public readonly string Timehm => $"#{Value}Timehm";
	public readonly string Timeymd => $"#{Value}Timeymd";
	public readonly string TimeRoundDown => $"#{Value}TimeRoundDown";
	#endregion

	#region Money
	public readonly string Money => ToMoney(false);
	public readonly string MoneyNonTooltip => ToMoney(false, false);
	public readonly string MoneyDefault => ToMoney(true);

	private readonly string ToMoney(bool IsDefault, bool Tooltip = true)
	{
		var gold = (int)(Value / 10000);
		var silver = (int)(Value % 10000 / 100);
		var copper = (int)(Value % 100);

		var builder = new StringBuilder();
		if (IsDefault || gold > 0)
		{
			builder.Append(gold + """<image enablescale="true" imagesetpath="00009076.GuildBank_Coin_Gold" scalerate="1.2"/>""");
		}

		if (IsDefault || silver > 0)
		{
			builder.Append(silver + """<image enablescale="true" imagesetpath="00009076.GuildBank_Coin_Silver" scalerate="1.2"/>""");
		}

		if (IsDefault || copper > 0)
		{
			builder.Append(copper + """<image enablescale="true" imagesetpath="00009076.GuildBank_Coin_Bronze" scalerate="1.2"/>""");
		}

		return builder.ToString();
	}
	#endregion
}