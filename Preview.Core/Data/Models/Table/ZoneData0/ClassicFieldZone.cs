using Xylia.Preview.Data.Common.Abstractions;

namespace Xylia.Preview.Data.Models;
public sealed class ClassicFieldZone : ModelElement, IAttraction
{
	#region IAttraction
	public string Text => this.Attributes["classic-field-zone-name2"].GetText();

	public string Describe => this.Attributes["classic-field-zone-desc"].GetText();
	#endregion
}