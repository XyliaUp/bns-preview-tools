using System.Data;
using System.Diagnostics;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Xylia.Preview.Data.Helper;
using Xylia.Preview.Data.Models.BinData.AliasTable;
using Xylia.Preview.Data.Models.DatData;
using Xylia.Preview.Data.Models.DatData.DataProvider;
using Xylia.Preview.Data.Models.Definition;
using Xylia.Preview.Data.Record;
using Xylia.Preview.Data.Record.Test;
using Xylia.Preview.Properties;


namespace Xylia.Preview.Tests;

[TestClass]
public class TableTests
{
	//[TestMethod]
	[DataRow("Bard_G1_Var_1")]
	public void ContextScriptTest(string alias)
	{
		var table = FileCache.Data.ContextScript;
		var record = table[alias];
		if (record is null) return;

		foreach (var stance in record.stance)
		{
			foreach (var decision in stance.Layer.SelectMany(layer => layer.Decision))
			{
				var condition = decision.Condition.Find(condition => condition.Skill == 66301);
				if (condition is null) continue;

				var result = decision.Result.Find(result => result.ControlMode == ContextScript.Result.ControlModeSeq.bns);
				Debug.WriteLine(result.Special1);
			}
		}
	}

	//[TestMethod]
	[DataRow(1931)]
	public void QuestTest(int id)
	{
		var table = FileCache.Data.Quest;
		var record = table[id];

		Console.WriteLine(record);
		//Console.WriteLine(record.XmlInfo());
	}



	//[TestMethod]
	public void Test2()
	{
		ClientConfiguration.LoadFrom();

		//foreach (var record in Data.FileCache.Data.TextData.Where(r => 
		//	r.alias.StartsWith("UI.ItemTooltip.") ||
		//	r.alias.StartsWith("Name.Item.")))
		//{
		//	Trace.WriteLine(record.Attributes);
		//}


		//var tmp = "ItemSkill" + "Data";
		//foreach (var file in new DirectoryInfo(@"F:\Bns")
		//	.GetFiles(tmp + "*.xml"))
		//{
		//	var name = file.Name
		//		.RemoveSuffixString(file.Extension)
		//		.RemovePrefixString(tmp)
		//		.RemovePrefixString("_");


		//	XmlDocument doc = new();
		//	doc.Load(file.FullName);

		//	var record = doc.DocumentElement.ChildNodes.OfType<XmlElement>().First();
		//	Trace.WriteLine($"\t<file name=\"{name}\" alias=\"{record.Attributes["alias"].Value}\" />");
		//}
	}
}


public sealed class TestSet : TableSet
{
	public TestSet() => this.LoadData(true, null);

	public override void LoadData(bool UseDB, string Folder)
	{
		if (Tables is not null) return;

		var _provider = new DefaultProvider(Folder ?? CommonPath.GameFolder);
		this.Provider = _provider;

		var data = _provider.XmlData.ExtractBin();
		var local = _provider.LocalData.ExtractBin();

		Tables = data.Tables.Concat(local.Tables).ToArray();

		// auto detect
		if (true)
		{
			var AliasTable = data.NameTable.CreateTable();
			this.detect.Detect(this.Tables, AliasTable);
		}
	}



	/// <summary>
	/// from external files
	/// </summary>
	/// <param name="files"></param>
	public void Output(params string[] files)
	{
		tableDefinitions = DefinitionHelper.LoadTableDefinition(files);
		this.LoadConverter();

		Parallel.ForEach(tableDefinitions, def =>
		{
			var table = Tables.FirstOrDefault(o => o.Type == def.Type);
			if (table is null) return;

			converter.ProcessTable(table, def, CommonPath.DataFiles);
		});
	}
}