using System.ComponentModel;

using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Models.QuestData.Enums;
public enum SpecifyObjectType
{
	[Name("specific-id")]
	SpecificID,

	[Name("pc")]
	Pc,

	[Name("duelbot")]
	DuelBot,
}