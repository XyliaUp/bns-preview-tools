using Xylia.Preview.Data.Common.Abstractions;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public sealed class Dungeon : ModelElement, IAttraction
{
	public Ref<Text> DungeonName2;
	public Ref<Text> DungeonDesc;


	#region Interface
	public string Text => this.DungeonName2.GetText();

	public string Describe => this.DungeonDesc.GetText();
	#endregion
}