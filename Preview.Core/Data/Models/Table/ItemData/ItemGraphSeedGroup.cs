using Xylia.Preview.Data.Common.Abstractions;

namespace Xylia.Preview.Data.Models;
public sealed class ItemGraphSeedGroup : ModelElement ,IHaveName
{
	public string Text => this.Attributes["name2"]?.GetText() ?? ToString();
}