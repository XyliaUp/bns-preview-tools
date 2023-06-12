using System.Collections.Generic;
using System.Linq;
using System.Xml;

using Xylia.Preview.Data.Record.ScriptData.Decision;

namespace Xylia.Preview.Data.Record
{
	public sealed class Script : BaseRecord
	{
		public List<IDecision> Decision = new();

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
				IDecision DecisionEntity = null;
				if (record.Name == "decision") DecisionEntity = new ScriptData.Decision.Decision();
				else if (record.Name == "quest-decision") DecisionEntity = new QuestDecision();

				DecisionEntity.LoadData(record);
				Decision.Add(DecisionEntity);
			}
		}
	}
}