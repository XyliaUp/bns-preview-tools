using Xylia.Preview.Data.Common.Abstractions;
using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public sealed class PublicRaid : Record, IAttraction
{
	public Ref<Text> PublicraidName2;
	public Ref<Text> PublicraidDesc;

	public string Text => this.PublicraidName2.GetText();

	public string Describe => this.PublicraidDesc.GetText();
}