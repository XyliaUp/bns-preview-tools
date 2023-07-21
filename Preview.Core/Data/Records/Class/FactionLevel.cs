
using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;
public sealed class FactionLevel : BaseRecord
{
	public short Level;


	public int Reputation;

	[Signal("grade-name-1")]
	public Text GradeName1;

	[Signal("grade-name-2")]
	public Text GradeName2;

	[Signal("max-faction-score")]
	public int MaxFactionScore;
}