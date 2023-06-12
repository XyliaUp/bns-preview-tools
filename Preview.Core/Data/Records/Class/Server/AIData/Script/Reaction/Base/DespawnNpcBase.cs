using Xylia.Preview.Common.Attribute;

using  Xylia.Preview.Data.Record;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction.Base
{
	public abstract class DespawnNpcBase : IReaction
	{
		/// <summary>
		/// 消除延迟
		/// </summary>
		[Signal("despawn-delay")]
		public long DespawnDelay;

		/// <summary>
		/// 强制消除
		/// </summary>
		[Signal("despawn-force")]
		public bool DespawnForce;

		/// <summary>
		/// 消除后允许再刷新
		/// </summary>
		[Signal("respawn-after-despawn")]
		public bool RespawnAfterDespawn;




		[Signal("despawn-social")]
		public Social DespawnSocial;

		[Signal("despawn-social-1")]
		public Social DespawnSocial1;

		[Signal("despawn-social-2")]
		public Social DespawnSocial2;

		[Signal("despawn-social-3")]
		public Social DespawnSocial3;

		[Signal("despawn-social-4")]
		public Social DespawnSocial4;

		[Signal("despawn-social-5")]
		public Social DespawnSocial5;

		[Signal("despawn-social-6")]
		public Social DespawnSocial6;

		[Signal("despawn-social-7")]
		public Social DespawnSocial7;

		[Signal("despawn-social-8")]
		public Social DespawnSocial8;

		[Signal("despawn-social-9")]
		public Social DespawnSocial9;

		[Signal("despawn-social-9")]
		public Social DespawnSocial10;
	}
}