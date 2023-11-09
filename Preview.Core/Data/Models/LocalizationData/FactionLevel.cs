using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class FactionLevel : Record
{
	public short Level;

	public int Reputation;

	[Name("grade-name") , Repeat(2)]
	public Ref<Text>[] GradeName;

	[Name("max-faction-score")]
	public int MaxFactionScore;
}