using System;
using System.Data;
using System.Diagnostics;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Record;

using static Xylia.Preview.Data.Record.Item;

namespace Xylia.Preview.Tests
{
	[TestClass]
	public class TableTests
	{
		//[TestMethod]
		[DataRow("Bard_G1_Var_1")]
		public void ContextScript(string alias)
		{
			var table = FileCache.Data.ContextScript;
			var record = table[alias];
			if (record is null) return;

			foreach (var stance in record.Stance)
			{
				foreach (var decision in stance.Layer.SelectMany(layer => layer.Decision))
				{
					var condition = decision.Condition.Find(condition => condition.Skill == 66301);
					if (condition is null) continue;

					var result = decision.Result.Find(result => result.ControlMode == Result.ControlModeSeq.bns);
					Debug.WriteLine(result.Special1);
				}
			}
		}

		//[TestMethod]
		[DataRow(1931)]
		public void Quest(int id)
		{
			var table = FileCache.Data.Quest;
			var record = table[id];

			Console.WriteLine(record);
			Console.WriteLine(record.XmlInfo());
		}
		
		//[TestMethod]
		public void Category()
		{
			foreach (var seq in Enum.GetValues<MarketCategory2Seq>())
			{
				if (seq == MarketCategory2Seq.None) continue;

				Console.WriteLine($"Name.item.game-category-2.{seq.GetSignal()}".GetText());
			}

			Console.WriteLine();
			foreach (var seq in Enum.GetValues<MarketCategory3Seq>())
			{
				if (seq == MarketCategory3Seq.None) continue;

				Console.WriteLine($"Name.item.game-category-3.{seq.GetSignal()}".GetText());
			}
		}
	}
}