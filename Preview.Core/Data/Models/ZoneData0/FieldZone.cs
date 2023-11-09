using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.Interface;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public class FieldZone : Record, IAttraction
{
	public string Alias;



	[Repeat(30)]
	public Ref<Zone>[] Zone;

	public Ref<AttractionGroup> Group;

	[Repeat(5)]
	public Ref<Quest>[] AttractionQuest;

	public bool UiFilterAttractionQuestOnly;

	public Ref<Text> RespawnConfirmText;

	public Ref<Text> Name2;

	public Ref<Text> Desc;

	public sbyte UiTextGrade;

	public Ref<AttractionRewardSummary> RewardSummary;

	public sealed class Normal : FieldZone
	{
	}

	public sealed class GuildBattleFieldEntrance : FieldZone
	{
		public Ref<GuildBattleFieldZone> GuildBattleFieldZone;

		public sbyte MinFixedChannel;
	}


	#region Interface
	public string Text => this.Name2.GetText();

	public string GetDescribe() => this.Desc.GetText();
	#endregion
}