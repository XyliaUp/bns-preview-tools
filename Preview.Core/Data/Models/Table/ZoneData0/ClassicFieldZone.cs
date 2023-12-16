using Xylia.Preview.Data.Common.Abstractions;

namespace Xylia.Preview.Data.Models;
public sealed class ClassicFieldZone : ModelElement, IAttraction
{
	public Ref<Text> ClassicFieldZoneName2 { get; set; }
	public Ref<Text> ClassicFieldZoneDesc { get; set; }


	#region Interface
	public string Text => this.ClassicFieldZoneName2.GetText();

	public string Describe => this.ClassicFieldZoneDesc.GetText();
	#endregion
}