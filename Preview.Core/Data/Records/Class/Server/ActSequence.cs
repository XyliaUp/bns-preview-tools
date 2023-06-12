using System.Collections.Generic;
using System.Linq;
using System.Xml;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record.AIData.ActSequence.Action;
using Xylia.Preview.Data.Table.XmlRecord;

namespace Xylia.Preview.Data.Record.ActSequence
{
	[Signal("act-sequence")]
	public class ActSequence : BaseRecord
	{
		public List<IAction> Action;


		public string Alias;

		public ActType Type;
		public enum ActType
		{
			Act,
			Peace,
		}


		public Detect Detect;

		[Signal("indexed-detect")]
		public byte IndexedDetect;



		public override void LoadData(XmlElement data)
		{
			base.LoadData(data);

			Action = new();
			foreach (var record in data.SelectNodes("./action").OfType<XmlElement>())
			{
				Action.Add(record.TypeFactory<ActionType, IAction>(s => s switch
				{
					ActionType.Despawn => new Despawn(),
					ActionType.Hide => new Hide(),
					ActionType.IndexedMovearound => new IndexedMovearound(),
					ActionType.IndexedPathway => new IndexedPathway(),
					ActionType.IndexedSocial => new IndexedSocial(),
					ActionType.Loop => new Loop(),
					ActionType.Movearound => new Movearound(),
					ActionType.MovearoundForFieldBossSpawn => new MovearoundForFieldBossSpawn(),
					ActionType.MovearoundForRandomSpawn => new MovearoundForRandomSpawn(),
					ActionType.Pause => new Pause(),
					ActionType.Pathway => new Pathway(),
					ActionType.Select => new Select(),
					ActionType.Social => new AIData.ActSequence.Action.Social(),
					ActionType.Stay => new Stay(),

					_ => null
				}));
			}
		}
	}
}