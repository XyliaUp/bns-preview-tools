using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xylia.Preview.Data.Client;
using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Tests.DatTests;

[TestClass]
public partial class Tables
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
	public void ClassicLevel()
	{
		var database = new BnsDatabase(new FolderProvider(@"D:\资源\客户端相关\Auto\data"));

		// ***
		int Day = 0, UsedExp = 0;

		var table = database.Provider.GetTable<Level>();
		foreach (var record in table)
		{
			// get exp for next level
			var vitality = record.TencentVitalityMax[0];
			int NextExp = (table[record.level + 1]?.Exp ?? 0) - record.Exp;

			// get result
			if (UsedExp + NextExp <= vitality)
			{
				UsedExp += NextExp;
			}
			else
			{
				Day++;

				var Exceed = vitality - UsedExp;
				UsedExp = NextExp - Exceed;

				Console.WriteLine($"{Day} | {record.level} | {(float)Exceed / NextExp:P0}");
			}
		}
	}
}