using System.ComponentModel;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.QuestData.Enums;
public enum SpecifyObjectType
{
	[Signal("specific-id")]
	SpecificID,

	[Signal("pc")]
	Pc,

	[Signal("duelbot")]
	DuelBot,
}