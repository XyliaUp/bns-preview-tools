using Xylia.Preview.Data.Common.Abstractions;

namespace Xylia.Preview.Data.Models;
public sealed class FactionBattleFieldZone : ModelElement, IAttraction
{
	#region IAttraction
	public string Text => this.Attributes["faction-battle-field-zone-name2"].GetText();

	public string Describe => this.Attributes["faction-battle-field-zone-desc"].GetText();
	#endregion
}