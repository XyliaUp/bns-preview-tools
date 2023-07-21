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
	public byte IndexedDetect;

	public override void LoadData(XmlElement data)
	{
		base.LoadData(data);

		Action = new();
		foreach (var record in data.SelectNodes("./action").OfType<XmlElement>())
		{
			throw new NotImplementedException();
		}
	}



	#region Sub
	public sealed class Act : ActSequence
	{

	}

	public sealed class Peace : ActSequence
	{

	}
	#endregion
}