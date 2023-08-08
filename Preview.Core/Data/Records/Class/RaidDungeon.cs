using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Interface;

namespace Xylia.Preview.Data.Record;
[AliasRecord]
public sealed class RaidDungeon : BaseRecord, IAttraction
{
	public Text Name2;


	[Signal("arena-minimap")]
	public string ArenaMinimap;

	[Signal("raid-dungeon-desc")]
	public Text RaidDungeonDesc;

	[Signal("ui-text-grade")]
	public sbyte UiTextGrade;




	#region Interface Functions
	public string GetName() => this.Name2.GetText();

	public string GetDescribe() => this.RaidDungeonDesc.GetText();
	#endregion
}