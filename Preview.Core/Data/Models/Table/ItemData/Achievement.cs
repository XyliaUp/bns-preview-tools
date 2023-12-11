using Xylia.Preview.Data.Common.Abstractions;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public sealed class Achievement : ModelElement ,IHaveName
{
	public short Id;
	public short Step;
	public JobSeq Job;

	public string Text => this.Attributes["name2"]?.GetText();
}