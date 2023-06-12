using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;

namespace Xylia.Preview.Data.Record.ScriptData.Reaction
{

	[Signal("in-out-detect-start")]
	public sealed class InOutDetectStart : IReaction
	{
		public int Duration;

		/// <summary>
		/// 引用 Filter 对象
		/// </summary>
		[Signal("filter-1")]
		public Filter Filter1;

		[Signal("filter-2")]
		public Filter Filter2;

		[Signal("filter-3")]
		public Filter Filter3;

		[Signal("filter-4")]
		public Filter Filter4;




		/// <summary>
		/// 半径
		/// </summary>
		public short Radius;


		[Signal("ref-type")]
		public RefType RefType;

		[Signal("ref-object")]
		public Script_obj RefObject;

		[Signal("ref-area")]
		public ZoneArea RefArea;



		public Script_obj Subscriber;

		[Signal("gather-count")]
		public byte GatherCount;

		public byte Index;
	}


	public enum RefType
	{
		Area,

		Object,
	}
}