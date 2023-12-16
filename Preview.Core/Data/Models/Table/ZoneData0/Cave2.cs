using Xylia.Preview.Data.Common.Abstractions;
using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public sealed class Cave2 : ModelElement, IAttraction
{
	public sbyte UiTextGrade;

	public Ref<Text> Cave2Name2;

	public Ref<Text> Cave2Desc;


	#region Interface
	public string Text => this.Cave2Name2.GetText();

	public string Describe => this.Cave2Desc.GetText();
	#endregion
}