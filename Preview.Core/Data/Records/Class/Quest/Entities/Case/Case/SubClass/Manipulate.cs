using System.Collections.Generic;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Case
{
	public sealed class Manipulate : CaseBase
	{
		public string Object2;

		[Signal("multi-object-1")] public string MultiObject1;
		[Signal("multi-object-2")] public string MultiObject2;
		[Signal("multi-object-3")] public string MultiObject3;
		[Signal("multi-object-4")] public string MultiObject4;
		[Signal("multi-object-5")] public string MultiObject5;
		[Signal("multi-object-6")] public string MultiObject6;
		[Signal("multi-object-7")] public string MultiObject7;
		[Signal("multi-object-8")] public string MultiObject8;
		[Signal("multi-object-9")] public string MultiObject9;
		[Signal("multi-object-10")] public string MultiObject10;
		[Signal("multi-object-11")] public string MultiObject11;
		[Signal("multi-object-12")] public string MultiObject12;
		[Signal("multi-object-13")] public string MultiObject13;
		[Signal("multi-object-14")] public string MultiObject14;
		[Signal("multi-object-15")] public string MultiObject15;
		[Signal("multi-object-16")] public string MultiObject16;


		[Signal("env-looting")]
		public string EnvLooting;

		/// <summary>
		/// 用于校验玩家第二势力
		/// </summary>
		[Signal("join-faction2")]
		public string JoinFaction2;

		/// <summary>
		/// 转换第二势力
		/// </summary>
		[Side(ReleaseSide.Server)]
		[Signal("transfer-faction2")]
		public string TransferFaction2;



		public override List<string> AttractionObject => new() 
		{
			Object2,

			MultiObject1,
			MultiObject2,
			MultiObject3,
			MultiObject4,
			MultiObject5,
			MultiObject6,
			MultiObject7,
			MultiObject8,
			MultiObject9,
			MultiObject10,
			MultiObject11,
			MultiObject12,
			MultiObject13,
			MultiObject14,
			MultiObject15,
			MultiObject16,
		};
	}
}