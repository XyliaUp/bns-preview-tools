using System.Xml;

namespace Xylia.Preview.Data.Record;
public sealed class Script : BaseRecord
{
	public List<Decision> Decision = new();


	public string Parent;

	public HangerSeq Hanger;
	public enum HangerSeq
	{
		None,

		Object,
	}



	public override void LoadData(XmlElement data)
	{
		base.LoadData(data);

		foreach (var record in data.SelectNodes("./decision|./quest-decision").OfType<XmlElement>())
		{
			Decision DecisionEntity = null;
			if (record.Name == "decision") DecisionEntity = new Decision.Default();
			else if (record.Name == "quest-decision") DecisionEntity = new Decision.QuestDecision();

			DecisionEntity.LoadData(record);
			Decision.Add(DecisionEntity);
		}
	}
}