using Xylia.Preview.Data.Common.Abstractions;

namespace Xylia.Preview.Data.Models;
public class FieldZone : ModelElement, IAttraction
{
	#region Fields
	public Ref<Text> Name2 { get; set; }

	public Ref<Text> Desc { get; set; }

	public string Text => this.Name2.GetText();

	public string Describe => this.Desc.GetText();
	#endregion


	public sealed class Normal : FieldZone
	{
	}

	public sealed class GuildBattleFieldEntrance : FieldZone
	{
		public Ref<GuildBattleFieldZone> GuildBattleFieldZone { get; set; }

		public sbyte MinFixedChannel { get; set; }
	}
}