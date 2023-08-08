using System.Xml;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record.ActSequence;

[Signal("act-sequence")]
[AliasRecord]
public class ActSequence : BaseRecord
{
	public List<Action> Action;

	public Detect Detect;

	[Signal("indexed-detect")]
	public sbyte IndexedDetect;


	#region Sub
	public sealed class Act : ActSequence
	{

	}

	public sealed class Peace : ActSequence
	{

	}
	#endregion
}