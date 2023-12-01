using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public sealed class ItemEvent : Record
{
	public string Alias;


	public DateTime EventExpirationTime;

	public Ref<Text> Name2;


	#region Functions
	public bool IsExpiration => this.EventExpirationTime < DateTime.Now;
	#endregion
}