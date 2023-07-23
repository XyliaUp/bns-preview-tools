using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Helper;

using static Xylia.Preview.Data.Record.Item;

namespace Xylia.Preview.Data.Record;

[Side(ReleaseSide.Client)]
public sealed class Race : BaseRecord
{
	public RaceSeq race;

	public Text Name2;



	public static Race Get(RaceSeq seq) => FileCache.Data.Race.FirstOrDefault(record => record.race == seq);

	public static Race Get(RaceSeq2 seq) => seq switch
	{
		RaceSeq2.Kun => Get(RaceSeq.건),
		RaceSeq2.Gon => Get(RaceSeq.곤),
		RaceSeq2.Lyn => Get(RaceSeq.린),
		RaceSeq2.Jin => Get(RaceSeq.진),
		RaceSeq2.SummonedCat => Get(RaceSeq.고양이),

		_ => null,
	};
}