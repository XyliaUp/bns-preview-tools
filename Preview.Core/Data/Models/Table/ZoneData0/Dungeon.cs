using Xylia.Preview.Data.Common.Abstractions;

namespace Xylia.Preview.Data.Models;
public sealed class Dungeon : ModelElement, IAttraction
{
	#region IAttraction
	public string Text => this.Attributes["dungeon-name2"].GetText();

	public string Describe => this.Attributes["dungeon-desc"].GetText();
	#endregion
}