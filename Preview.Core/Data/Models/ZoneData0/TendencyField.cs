using Xylia.Preview.Data.Common.Abstractions;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public sealed class TendencyField : Record, IAttraction
{
	#region Fields
	public Ref<Text> TendencyFieldName2;
	public Ref<Text> TendencyFieldDesc;
	#endregion

	#region Interface Methdos
	public string Text => this.TendencyFieldName2.GetText();

	public string Describe => this.TendencyFieldDesc.GetText();
	#endregion
}