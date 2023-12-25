using Xylia.Preview.Data.Common.Abstractions;

namespace Xylia.Preview.Data.Models;
public sealed class Cave2 : ModelElement, IAttraction
{
	public sbyte UiTextGrade;

	#region IAttraction
	public string Text => this.Attributes["cave2-name2"].GetText();

	public string Describe => this.Attributes["cave2-desc"].GetText();
	#endregion
}