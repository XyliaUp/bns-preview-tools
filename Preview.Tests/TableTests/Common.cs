using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Xylia.Preview.Tests;

[TestClass]
public partial class TableTests
{
	[TestMethod]
	public void SerializeTest()
	{
		using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
		var PrivateKey = rsa.ToXmlString(true);
		var PublicKey = rsa.ToXmlString(false);
		var Parameter = rsa.ExportParameters(true);

		Console.WriteLine(PrivateKey); 
		Console.WriteLine(PublicKey);
	}

	[TestMethod]
	public void QueryTest()
	{
		//using var db = new BnsDatabase();

		//var table = db.Get<Text>();
		//Debug.WriteLine(table[500].Attributes);

		//	DateTime dt = DateTime.Now;
		//	Debug.WriteLine(records.FirstOrDefault(o => o.Alias == "Effect.Name2.Weapon_Effect_CriPer_0582_1"));
		//	Debug.WriteLine(DateTime.Now - dt);
	}
}