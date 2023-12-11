using Xylia.Preview.Data.Common.Abstractions;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public sealed class Cave2 : ModelElement, IAttraction
{
	[Name("ui-text-grade")]
	public sbyte UiTextGrade;

	[Name("cave2-name2")]
	public Ref<Text> Cave2Name2;

	[Name("cave2-desc")]
	public Ref<Text> Cave2Desc;


	#region Interface
	public string Text => this.Cave2Name2.GetText();

	public string Describe => this.Cave2Desc.GetText();
	#endregion
}