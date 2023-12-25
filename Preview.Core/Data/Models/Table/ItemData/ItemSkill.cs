namespace Xylia.Preview.Data.Models;
public sealed class ItemSkill : ModelElement
{
	public string Description2 => this.Attributes["description2"]?.GetText();
}