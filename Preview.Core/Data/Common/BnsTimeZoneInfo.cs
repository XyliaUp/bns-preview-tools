using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.DatData;

namespace Xylia.Preview.Data.Common;
public class BnsTimeZoneInfo
{
	public Publisher Publisher { get; }
	public Msec Offset { get; }

	public BnsTimeZoneInfo(Publisher publisher, Msec offset)
	{
		Offset = offset;
		Publisher = publisher;
	}


	/// <summary>
	/// Unable to exclude members in Time64, therefore as a global attribute
	/// </summary>
	public static Publisher Current { get; set; }

	public static BnsTimeZoneInfo FromPublisher(Publisher? publisher = null)
	{
		publisher ??= Current;
		var offset = publisher switch
		{
			Publisher.NcKorean => new Msec(9, 0, 0),   // Korea Standard Time
			Publisher.Tencent => new Msec(8, 0, 0),   // China Standard Time
			Publisher.Innova => new Msec(0, 0, 0),    //
			Publisher.NcJapan => new Msec(9, 0, 0),   // Tokyo Standard Time
			Publisher.Sea => new Msec(0, 0, 0),        //
			Publisher.NcTaiwan => new Msec(8, 0, 0),  // Taipei Standard Time
			Publisher.NcWest => new Msec(-5, 0, 0),   // Eastern Standard Time
			Publisher.Garena => new Msec(7, 0, 0),    // SE Asia Standard Time
			_ => default,
		};

		return new BnsTimeZoneInfo(publisher.Value, offset);
	}
}