using Microsoft.VisualStudio.TestTools.UnitTesting;

using Xylia.Preview.Data.Helper.Output;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.Tests
{
	[TestClass]
	public class OutTest
	{
		[TestMethod]
		public void LoadData()
		{
			new OutSet<WorldAccountMuseum>();
		}
	}
}