using Xylia.Preview.Data.Common.Abstractions;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public sealed class RaidDungeon : Record, IAttraction
{
	public Ref<Text> Name2;

	[Name("raid-dungeon-desc")]
	public Ref<Text> RaidDungeonDesc;

	public string Text => this.Name2.GetText();

	public string Describe => this.RaidDungeonDesc.GetText();
}