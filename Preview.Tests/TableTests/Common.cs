using System.Data;
using System.Diagnostics;

using CUE4Parse.Utils;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Xylia.Preview.Data;
using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Tests.TableTests;

[TestClass]
public class Common
{
	[DataRow("Bard_G1_Var_1")]
	public void ContextScriptTest(string alias)
	{
		var record = FileCache.Data.ContextScript[alias];
		if (record is null) return;

		foreach (var stance in record.Stance)
		{
			foreach (var decision in stance.Layer.SelectMany(layer => layer.Decision))
			{
				var condition = decision.Condition.Find(condition => condition.Skill == 66301);
				if (condition is null) continue;

				var result = decision.Result.Find(result => result.ControlMode == ContextScript.Result.ControlModeSeq.Bns);
				Debug.WriteLine(result.Attributes);
			}
		}
	}


	[TestMethod]
	public void SerializeTest()
	{
		FileCache.Data = new BnsDatabase(new FolderProvider(@"D:\资源\客户端相关\Auto\data"));

		//var o = Equipments.Get(new Creature() { WorldId = 1911, Name = "三千问乀" });
		//Debug.WriteLine(o.finger_left.equip.item.name);


		//FileCache.Data.LoadConverter();

		//var table = FileCache.Data.AccountLevel;
		//foreach (var attr in table.Definition.Attributes)
		//{
		//	Trace.WriteLine(attr.Name);
		//}

		//table[1].Serialize();
		//Trace.WriteLine(table[1].Data.ToHex(true));



		//var record = table[1];
		//record.Exp = 100;
		//record.Serialize(new RecordBuilder(null, null));
		//Trace.WriteLine(record.Data.ToHex(true));

		//table.WriteXml(@"C:\Users\10565\Desktop\新建 文本文档.xml");

		//FileCache.Data.AccountLevel.ReadXml(
		//	XmlReader.Create(new FileStream(@"C:\Users\10565\Desktop\新建 文本文档.xml", FileMode.Open)));
	}

	[TestMethod]
	public void Test2()
	{
		//using var db = new BnsDatabase();

		//var table = db.Get<Text>();
		//Debug.WriteLine(table[500].Attributes);

		//	DateTime dt = DateTime.Now;
		//	Debug.WriteLine(records.FirstOrDefault(o => o.Alias == "Effect.Name2.Weapon_Effect_CriPer_0582_1"));
		//	Debug.WriteLine(DateTime.Now - dt);
	}
}