using Xylia.Preview.Data.Common.Abstractions;

namespace Xylia.Preview.Data.Models;
public sealed class TendencyField : ModelElement, IAttraction
{
	#region Attraction
	public string Text => this.Attributes["tendency-field-name2"].GetText();

	public string Describe => this.Attributes["tendency-field-desc"].GetText();
	#endregion
}