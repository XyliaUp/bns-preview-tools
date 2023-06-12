using System;
using System.Net;

namespace Xylia.Match.Util.ItemMatch.Util
{
	public class ItemDataInfo
	{
		public int id;

		public string Alias;

		public string Name2;

		public string Desc;

		public string Info;


		public string Job;




		public string Key => Convert.ToBase64String(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(this.id)));
	}
}