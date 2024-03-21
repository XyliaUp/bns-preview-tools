using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.DatData;

namespace Xylia.Preview.Data.Common;
public class BnsTimeZoneInfo(EPublisher publisher, Msec offset)
{
	public EPublisher Publisher { get; } = publisher;

	public Msec Offset { get; } = offset;


	#region Methods
	public static BnsTimeZoneInfo FromPublisher(EPublisher? publisher = null)
	{
		publisher ??= Locale.Current;
		var offset = publisher switch
		{
			EPublisher.NcSoft or EPublisher.ZNcs => new Msec(9, 0, 0),   // Korea Standard Time
			EPublisher.Tencent or EPublisher.ZTx => new Msec(8, 0, 0),   // China Standard Time
			EPublisher.Innova => new Msec(0, 0, 0),    //
			EPublisher.NcJapan => new Msec(9, 0, 0),   // Tokyo Standard Time
			EPublisher.NcTaiwan => new Msec(8, 0, 0),  // Taipei Standard Time
			EPublisher.NcWest => new Msec(-5, 0, 0),   // Eastern Standard Time
			EPublisher.Garena => new Msec(7, 0, 0),    // SE Asia Standard Time
			_ => default,
		};

		return new BnsTimeZoneInfo(publisher.Value, offset);
	}
	#endregion
}