using System.ComponentModel;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Enums
{
	/// <summary>
	/// 特定对象类型
	/// </summary>
	[DefaultValue(SpecificID)]
	public enum SpecifyObjectType
	{
		[Signal("specific-id")]
		SpecificID,

		[Signal("pc")]
		Pc,

		[Signal("duelbot")]
		DuelBot,
	}
}