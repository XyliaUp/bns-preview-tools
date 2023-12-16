using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
[Side(ReleaseSide.Client)]
public sealed class NpcResponse : ModelElement
{
	public enum FactionCheckTypeSeq : byte
	{
		Is,
		IsNot,
		IsNone,
	}

	public enum FactionLevelCheckTypeSeq : byte
	{
		None,
		CheckForSuccess,
		CheckForFail,
	}
}