using Xylia.Preview.Data.Common.Abstractions;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public class FieldZone : ModelElement, IAttraction
{
	#region Fields
	public Ref<Text> Name2;

	public Ref<Text> Desc;

	public string Text => this.Name2.GetText();

	public string Describe => this.Desc.GetText();
	#endregion


	public sealed class Normal : FieldZone
	{
	}

	public sealed class GuildBattleFieldEntrance : FieldZone
	{
		public Ref<GuildBattleFieldZone> GuildBattleFieldZone;

		public sbyte MinFixedChannel;
	}
}