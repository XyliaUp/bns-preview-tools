using System.Collections.Generic;

namespace Xylia.Preview.Data.Record.QuestData
{
	public class Transit : BaseRecord
	{
		public byte id;

		public string Zone;


		public List<Destination> Destination;

		public List<Complete> Complete;

		//[FStruct(StructType.Ref)]
		//public Quest OwnerQuest => this.Owner as Quest;
	}
}