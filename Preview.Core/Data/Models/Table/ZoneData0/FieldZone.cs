using Xylia.Preview.Data.Common.Abstractions;

namespace Xylia.Preview.Data.Models;
public class FieldZone : ModelElement, IAttraction
{
	#region IAttraction
	public string Text => this.Attributes["name2"].GetText();

	public string Describe => this.Attributes["desc"].GetText();
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