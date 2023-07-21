using System.Data;

using BnsBinTool.Core.Models;

using Newtonsoft.Json;

using Xylia.Extension;
using Xylia.Preview.Data.Models.BinData;
using Xylia.Preview.Data.Models.BinData.Table;

namespace Xylia.Preview.Tests.DatTool.Utils.Extract;

public class ExportOption
{
	public bool OutputFieldAlias = false;
	public bool OutputListAlias = false;
}

public static partial class DumpEx
{
	public static void Dump(this List<Table> Tables, DatafileDetect set, string SaveFolder, ExportOption option)
	{
		#region Initialize
		if (!Directory.Exists(SaveFolder)) Directory.CreateDirectory(SaveFolder);
		else foreach (string f in Directory.GetFiles(SaveFolder, "*.json")) File.Delete(f);

		Console.WriteLine("读取Dat文件完成，开始解析数据...");
		#endregion

		#region 遍历数据区域
		int Count = 1;
		Parallel.ForEach(Tables, table =>
		{
			Console.WriteLine($"输出配置文件: {Count++,-3}/{Tables.Count}...{Count * 100 / Tables.Count,3}%  (ListId: {table.Type})");

			#region 创建数据结构
			DataTable RootData = new();
			RootData.Columns.Add("type", typeof(int));
			RootData.Columns.Add("version", typeof(string));
			RootData.Columns.Add("compressed", typeof(DataTable));
			RootData.Columns.Add("loose", typeof(DataTable));

			DataRow RootDataRow = RootData.NewRow();
			RootDataRow["type"] = table.Type;
			RootDataRow["version"] = table.MajorVersion + "." + table.MinorVersion;
			#endregion

			#region 生成表结构
			if (table.IsCompressed)
			{
				var TableSet = (DataTable)(RootDataRow["compressed"] = new DataTable());
				var TableRow = TableSet.NewRow();
				TableSet.Rows.Add(TableRow);

				DumpField(table.Type, TableSet, TableRow, table.Records, true);
			}
			else
			{
				var TableSet = (DataTable)(RootDataRow["loose"] = new DataTable());
				var TableRow = TableSet.NewRow();


				if (table.Records.Count > 0) DumpLookup(TableSet, TableRow, table.Records[0].StringLookup);
				DumpField(table.Type, TableSet, TableRow, table.Records);
				TableSet.Rows.Add(TableRow);
			}

			RootData.Rows.Add(RootDataRow);
			#endregion


			#region 创建最终文件
			string FilePath = $@"{SaveFolder}\{table.Type}";
			if (option.OutputListAlias && set.TryGetName(table.Type, out string TypeName) && TypeName != null)
				FilePath += $" ({TypeName})";

			using StreamWriter outfile = new(FilePath + ".json");
			outfile.WriteLine(JsonConvert.SerializeObject(RootData, new JsonSerializerSettings()
			{
				Formatting = Formatting.Indented,
				NullValueHandling = NullValueHandling.Ignore,
			}));


			RootData.Dispose();
			RootData = null;
			#endregion



			//输出类型
			//foreach (var type in Types) LogWriter.WriteLine("   List:" + table.Type + " => type:" + type.Value.Type + " => length:" + type.Value.Length);
		});
		#endregion


		GC.Collect();
		Console.WriteLine("输出已完成!!");
	}


	private static DataTable DumpField(int TableID, DataTable ParentTable, DataRow ParentRow, List<Record> records, bool IsCompressed = false)
	{
		//判断
		if (records is null) return null;

		ParentTable.Columns.Add("Count", typeof(int));
		ParentRow["Count"] = records.Count;


		ParentTable.Columns.Add(IsCompressed ? "Table" : "field", typeof(DataTable));
		var FieldTable = (DataTable)(ParentRow[IsCompressed ? "Table" : "field"] = new DataTable());
		FieldTable.Columns.Add("id", typeof(int));
		FieldTable.Columns.Add("XmlNodeType", typeof(int));
		FieldTable.Columns.Add("SubclassType", typeof(int));
		FieldTable.Columns.Add("size", typeof(int));
		FieldTable.Columns.Add("data", typeof(string));
		FieldTable.Columns.Add("alias", typeof(string));


		foreach (var record in records)
		{
			DataRow FieldRow = FieldTable.NewRow();  //Initialize
			FieldRow["id"] = record.RecordId;
			FieldRow["size"] = record.DataSize;
			FieldRow["data"] = record.Data.ToHex();

			if (record.XmlNodeType != 1) FieldRow["XmlNodeType"] = record.XmlNodeType;
			if (record.SubclassType != -1) FieldRow["SubclassType"] = record.SubclassType;


			#region 处理数据
			////处理别名
			//if (option.OutputFieldAlias && obj.Lookup != null && obj.Lookup.HasValue)
			//{
			//	//如果长度大于8，则进行处理
			//	if (obj.Field.Data.Length >= 8)
			//	{
			//		FieldRow[Const.Alias] = obj.Lookup.GetWordFromIndex(BitConverter.ToInt32(obj.Field.Data, 4), false);
			//	}
			//}
			#endregion

			if (IsCompressed) 
				DumpLookup(FieldTable, FieldRow, record.StringLookup);

			FieldTable.Rows.Add(FieldRow);
		}



		#region 统计类型长度
		//if (!Types.ContainsKey(bfield.Type ?? -1))
		//{
		//	Types.Add(bfield.Type ?? -1, new TypeInfo()
		//	{
		//		Type = bfield.Type,
		//		Length = bfield.Data.Length,

		//		TypeTarget = bloose.Lookup.GetWordFromIndex(0, false),
		//	});
		//}
		#endregion


		return FieldTable;
	}

	private static DataTable DumpLookup(DataTable ParentTable, DataRow ParentRow, StringLookup Lookups)
	{
		var words = new StringList(Lookups);

		#region 创建结构
		if (!ParentTable.Columns.Contains("LookupCount")) ParentTable.Columns.Add("LookupCount", typeof(int));
		ParentRow["LookupCount"] = words.Count;


		if (!ParentTable.Columns.Contains("Lookup")) ParentTable.Columns.Add("Lookup", typeof(DataTable));
		var LookupData = (DataTable)(ParentRow["Lookup"] = new DataTable());
		LookupData.Columns.Add("index", typeof(int));
		LookupData.Columns.Add("key", typeof(int));
		LookupData.Columns.Add("value", typeof(string));
		#endregion

		#region 实例化对象并赋值
		long tmpIndex = 0;
		for (int i = 0; i < words.Count; i++)
		{
			string w = words[i];
			if (w != null && w.Length > 0)
			{
				DataRow LookupDataRow = LookupData.NewRow();

				LookupDataRow["index"] = tmpIndex;
				LookupDataRow["key"] = i + 1;
				LookupDataRow["value"] = w;

				LookupData.Rows.Add(LookupDataRow);
			}

			tmpIndex += w.getLength();
		}
		#endregion

		return LookupData;
	}


	public static int getLength(this string Word) => ((Word?.Length ?? 0) + 1) * 2;
}