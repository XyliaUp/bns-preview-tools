using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Interface;

namespace Xylia.Preview.Data.Models;
public sealed class RaidDungeon : Record, IAttraction
{
	public string Alias;

	public Ref<Text> Name2;


	[Name("arena-minimap")]
	public string ArenaMinimap;

	[Name("raid-dungeon-desc")]
	public Ref<Text> RaidDungeonDesc;

	[Name("ui-text-grade")]
	public sbyte UiTextGrade;



	#region Interface Methdos
	public override string GetText => this.Name2.GetText();

	public string GetDescribe() => this.RaidDungeonDesc.GetText();
	#endregion
}