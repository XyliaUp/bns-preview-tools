using Xylia.Preview.Data.Common.Abstractions;

namespace Xylia.Preview.Data.Models;
public sealed class RaidDungeon : ModelElement, IAttraction
{
	#region IAttraction
	public string Text => this.Attributes["name2"].GetText();

	public string Describe => this.Attributes["raid-dungeon-desc"].GetText();
	#endregion
}