using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;
using  Xylia.Preview.Data.Record;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{
	[Signal("warp")]
	public class Warp : IReaction
	{
		public Script_obj Target;

		/// <summary>
		/// 转移地图
		/// </summary>
		public Zone Zone;

		/// <summary>
		/// 转移区域 （Area、PcSpawn必须且仅能设置其中的一个）
		/// </summary>
		public short Area;

		/// <summary>
		/// 转移玩家刷新点 （Area、PcSpawn必须且仅能设置其中的一个）
		/// </summary>
		[Signal("pc-spawn")]
		public byte PcSpawn;



		#region 可缺省Fields
		[Signal("enter-cinematic")]
		public Cinematic EnterCinematic;

		[Signal("leave-cinematic")]
		public Cinematic LeaveCinematic;


		[Signal("phase-zone-pc-spawn-1")] public byte PhaseZonePcSpawn1;
		[Signal("phase-zone-pc-spawn-2")] public byte PhaseZonePcSpawn2;
		[Signal("phase-zone-pc-spawn-3")] public byte PhaseZonePcSpawn3;
		[Signal("phase-zone-pc-spawn-4")] public byte PhaseZonePcSpawn4;
		[Signal("phase-zone-pc-spawn-5")] public byte PhaseZonePcSpawn5;
		[Signal("phase-zone-pc-spawn-6")] public byte PhaseZonePcSpawn6;
		[Signal("phase-zone-pc-spawn-7")] public byte PhaseZonePcSpawn7;
		[Signal("phase-zone-pc-spawn-8")] public byte PhaseZonePcSpawn8;
		[Signal("phase-zone-pc-spawn-9")] public byte PhaseZonePcSpawn9;
		[Signal("phase-zone-pc-spawn-10")] public byte PhaseZonePcSpawn10;
		[Signal("phase-zone-pc-spawn-11")] public byte PhaseZonePcSpawn11;
		[Signal("phase-zone-pc-spawn-12")] public byte PhaseZonePcSpawn12;
		[Signal("phase-zone-pc-spawn-13")] public byte PhaseZonePcSpawn13;
		[Signal("phase-zone-pc-spawn-14")] public byte PhaseZonePcSpawn14;
		[Signal("phase-zone-pc-spawn-15")] public byte PhaseZonePcSpawn15;

		[Signal("phase-zone-enter-cinematic-1")] public Cinematic PhaseZoneEnterCinematic1;
		[Signal("phase-zone-enter-cinematic-2")] public Cinematic PhaseZoneEnterCinematic2;
		[Signal("phase-zone-enter-cinematic-3")] public Cinematic PhaseZoneEnterCinematic3;
		[Signal("phase-zone-enter-cinematic-4")] public Cinematic PhaseZoneEnterCinematic4;
		[Signal("phase-zone-enter-cinematic-5")] public Cinematic PhaseZoneEnterCinematic5;
		[Signal("phase-zone-enter-cinematic-6")] public Cinematic PhaseZoneEnterCinematic6;
		[Signal("phase-zone-enter-cinematic-7")] public Cinematic PhaseZoneEnterCinematic7;
		[Signal("phase-zone-enter-cinematic-8")] public Cinematic PhaseZoneEnterCinematic8;
		[Signal("phase-zone-enter-cinematic-9")] public Cinematic PhaseZoneEnterCinematic9;
		[Signal("phase-zone-enter-cinematic-10")] public Cinematic PhaseZoneEnterCinematic10;
		[Signal("phase-zone-enter-cinematic-11")] public Cinematic PhaseZoneEnterCinematic11;
		[Signal("phase-zone-enter-cinematic-12")] public Cinematic PhaseZoneEnterCinematic12;
		[Signal("phase-zone-enter-cinematic-13")] public Cinematic PhaseZoneEnterCinematic13;
		[Signal("phase-zone-enter-cinematic-14")] public Cinematic PhaseZoneEnterCinematic14;
		[Signal("phase-zone-enter-cinematic-15")] public Cinematic PhaseZoneEnterCinematic15;

		[Signal("phase-zone-leave-cinematic-1")] public Cinematic PhaseZoneLeaveCinematic1;
		[Signal("phase-zone-leave-cinematic-2")] public Cinematic PhaseZoneLeaveCinematic2;
		[Signal("phase-zone-leave-cinematic-3")] public Cinematic PhaseZoneLeaveCinematic3;
		[Signal("phase-zone-leave-cinematic-4")] public Cinematic PhaseZoneLeaveCinematic4;
		[Signal("phase-zone-leave-cinematic-5")] public Cinematic PhaseZoneLeaveCinematic5;
		[Signal("phase-zone-leave-cinematic-6")] public Cinematic PhaseZoneLeaveCinematic6;
		[Signal("phase-zone-leave-cinematic-7")] public Cinematic PhaseZoneLeaveCinematic7;
		[Signal("phase-zone-leave-cinematic-8")] public Cinematic PhaseZoneLeaveCinematic8;
		[Signal("phase-zone-leave-cinematic-9")] public Cinematic PhaseZoneLeaveCinematic9;
		[Signal("phase-zone-leave-cinematic-10")] public Cinematic PhaseZoneLeaveCinematic10;
		[Signal("phase-zone-leave-cinematic-11")] public Cinematic PhaseZoneLeaveCinematic11;
		[Signal("phase-zone-leave-cinematic-12")] public Cinematic PhaseZoneLeaveCinematic12;
		[Signal("phase-zone-leave-cinematic-13")] public Cinematic PhaseZoneLeaveCinematic13;
		[Signal("phase-zone-leave-cinematic-14")] public Cinematic PhaseZoneLeaveCinematic14;
		[Signal("phase-zone-leave-cinematic-15")] public Cinematic PhaseZoneLeaveCinematic15;
		#endregion
	}
}