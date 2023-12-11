using Xylia.Preview.Data.Common.Abstractions;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public sealed class ClassicFieldZone : ModelElement, IAttraction
{
	public Ref<Text> ClassicFieldZoneName2;
	public Ref<Text> ClassicFieldZoneDesc;


	#region Interface
	public string Text => this.ClassicFieldZoneName2.GetText();

	public string Describe => this.ClassicFieldZoneDesc.GetText();
	#endregion
}