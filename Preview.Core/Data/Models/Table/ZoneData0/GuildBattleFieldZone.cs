using Xylia.Preview.Data.Common.Abstractions;

namespace Xylia.Preview.Data.Models;
public sealed class GuildBattleFieldZone : ModelElement, IAttraction
{
	#region IAttraction
	public string Text => this.Attributes["guild-battle-field-zone-name2"].GetText();

	public string Describe => this.Attributes["guild-battle-field-zone-desc"].GetText();
	#endregion
}