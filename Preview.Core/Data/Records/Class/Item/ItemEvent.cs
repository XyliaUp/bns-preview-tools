using System;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class ItemEvent : BaseRecord	
	{
		[Signal("event-expiration-time")]
		public DateTime EventExpirationTime;

		public Text Name2;

		
		#region Functions
		public bool IsExpiration => this.EventExpirationTime < DateTime.Now;
		#endregion

	}
}