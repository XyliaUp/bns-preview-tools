using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class QuestSealedDungeonReward : Record
{
	public int Id;
	public sbyte Level;


	public string Alias;


	[Repeat(4)]
	public Ref<Item>[] RewardItem;

	[Repeat(4)]
	public short[] RewardItemCount;
}