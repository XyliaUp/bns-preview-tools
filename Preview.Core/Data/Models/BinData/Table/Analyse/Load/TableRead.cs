using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

using BnsBinTool.Core.Definitions;

using Xylia.Extension;
using Xylia.Preview.Data.Models.BinData.Analyse.Enums;
using Xylia.Preview.Data.Models.BinData.Analyse.Extension;
using Xylia.Preview.Data.Models.BinData.Analyse.Struct;
using Xylia.Preview.Data.Models.BinData.Analyse.Struct.Condition.Base;
using Xylia.Preview.Data.Models.BinData.Analyse.Struct.Record;
using Xylia.Preview.Data.Models.BinData.Analyse.Struct.Seq;
using Xylia.Preview.Data.Models.Util.Sort;
using Xylia.Xml;

namespace Xylia.Preview.Data.Models.BinData.Analyse.Load;

public static class TableRead
{
	public static void TableConfigRead(this List<TableInfo> TableInfos, XmlNode XNode, ConfigParam ConfigInfo)
	{
		#region Initialize
		var xp = XNode.Property();
		if (xp.Attributes["retired"].ToBool()) return;  // 判断是否需要加载

		string ListType = xp.Attributes["type"];               //类型
		short.TryParse(xp.Attributes["id"], out var Id);

		string ListSignal = null;
		if (Id != 0) ListSignal = Id.ToString();
		else if (!string.IsNullOrWhiteSpace(ListType)) ListSignal = ListType;
		else throw new ArgumentNullException("未定义需要解析的数据源");

		//如果已经存在相同表信息
		if (TableInfos.Find(r => r.TypeName.MyEquals(ListSignal)) != null)
			throw new DuplicateNameException("已存在数据: " + ListSignal);
		#endregion

		#region 读取部分list信息 
		var Rules = xp.Attributes["rule"].GetListRules(out var msg);
		if (msg != null) Console.WriteLine(ListType + " " + msg);

		// 要求长度检查
		bool? CheckSize = xp.Attributes["check-size"].ToBoolOrNull();
		if (CheckSize is null)
		{
			CheckSize = Rules.Contains(ListRule.Complete);
			//if (CheckSize == true && !ConfigInfo.HideRuleTips)
			//	System.Diagnostics.Debug.WriteLine($"{ListSignal} 要求长度检查。");		

			//throw new Exception($"[{ ListSignal }] 配置文件Initialize失败", ex);
		}
		#endregion

		#region 读取记录器信息
		TableInfo RecordInfo = XNode.ChildNodes.OfType<XmlElement>().GetRecordInfo(ListSignal, ConfigInfo);
		RecordInfo.RelativePath = xp.Attributes["relative-path"];
		RecordInfo.Module = xp.Attributes["module"];
		RecordInfo.Type = Id;
		RecordInfo.TypeName = ListType;

		xp.Attributes["version"].GetVersion(out RecordInfo.ConfigMajorVersion, out RecordInfo.ConfigMinorVersion);
		RecordInfo.Rules = Rules;
		RecordInfo.CheckSize = CheckSize.Value;
		RecordInfo.CheckVersion = xp.Attributes["check-version"].ToBool();
		if (uint.TryParse(xp.Attributes["auto-start"], out uint AutoStart)) RecordInfo.AutoStart = AutoStart;

		TableInfos.Add(RecordInfo);
		#endregion
	}

	public static TableInfo GetRecordInfo(this IEnumerable<XmlElement> XmlElements, string ListSignal, ConfigParam ConfigInfo = null)
	{
		#region Initialize
		var TableInfo = new TableInfo();

		//Initialize类型信息
		TableInfo.TypeInfo = new();
		TableInfo.TypeInfo.Add(new(-1, "#default"));
		#endregion

		#region	预先处理
		//用于字典组数据存储
		var PublicSeqs = ConfigInfo?.PublicSeq is null ? new Dictionary<string, SeqInfo>() : new(ConfigInfo.PublicSeq);
		var Structs = new Dictionary<string, StructInfo>();
		foreach (var RecordNode in XmlElements)
		{
			string Alias = RecordNode.Attributes["alias"]?.Value?.Trim();
			string Type = RecordNode.Attributes["type"]?.Value?.Trim();

			//读取特殊类型
			if (Type.MyEquals("struct")) RecordNode.ReadStructInfo(Structs, PublicSeqs);
			else if (Type.MyEquals("dictionary"))
			{
				var seq = RecordNode.GetSeqInfo(Alias);
				PublicSeqs[seq.Name] = seq;
			}
			else if (Type.MyEquals("type"))
			{
				var SeqInfo = RecordNode.GetSeqInfo(Alias);

				if (TableInfo.TypeInfo.Count > 1) throw new Exception("当前配置文件存在多个 type");
				TableInfo.TypeInfo.Name = Alias;
				SeqInfo?.ForEach(r => TableInfo.TypeInfo.Add(new(r)));
			}
		}
		#endregion


		#region 处理数据
		var Records = new List<RecordDef>();
		foreach (XmlElement Child in XmlElements)
		{
			XmlProperty RecordNode = Child.Property();

			string Alias = RecordNode.Attributes["alias"]?.Trim();
			var Type = RecordNode.Attributes["type"].GetRecordType();

			#region 获取Ref信息
			string RefTable = RecordNode.Attributes["ref"];
			if (RefTable == "this") RefTable = ListSignal;

			//自动变更类型
			RefInfo RefInfo = null;
			if (Type == AttributeType.TIcon) RefInfo = new RefInfo("IconTexture");
			else if (RefTable != null)
			{
				Type = AttributeType.TRef;
				RefInfo = RefInfo.Load(RefTable, RecordNode.Attributes["link"]);
			}
			#endregion


			#region Initialize
			string Name = RecordNode.Attributes["name"]?.Trim();     //读取 Name
			var Filters = RecordNode.Attributes["filter"].GetFilters(TableInfo.TypeInfo);  //Initialize过滤信息

			if (!RecordNode.Attributes["client"].ToBool(out bool IsClient)) IsClient = true;   //判断是否是客户端需要属性
			if (!RecordNode.Attributes["server"].ToBool(out bool IsServer)) IsServer = true;   //判断是否是服务端需要属性

			//获取错误处理类型
			Enum.TryParse<ErrorType>(RecordNode.Attributes["error-type"], true, out var ErrorType);

			var Offset = (ushort)RecordNode.Attributes["offset"].ToShort();

			var OutCond = RecordNode.Attributes[new List<string> { "out-cond", "out-condition" }].GetCondition();
			var ValidCond = RecordNode.Attributes[new List<string> { "Valid-cond", "Valid-condition" }].GetCondition();
			#endregion

			#region 校验类型
			if (Type == AttributeType.TNone)
			{
				if (RecordNode.ContainAttribute("struct", out string StructInfo))
				{
					if (Structs is null || !Structs.TryGetValue(StructInfo, out var FStruct))
					{
						Console.WriteLine($"{Alias} 绑定的结构体 {StructInfo} 未定义");
						continue;
					}

					foreach (var r in FStruct)
					{
						//克隆对象
						var rc = (RecordDef)r.Clone();

						//生成对应的记录器别名
						rc.Alias = rc.Alias.CreateStructMetaName(Alias);

						//条件校验别名只能进行通配符替换
						if (rc.OutCond != null)
						{
							rc.OutCond = (Condition)r.OutCond.Clone();
							rc.OutCond.TargetAlias = rc.OutCond.TargetAlias.CreateStructMetaName(Alias, true);
						}

						rc.Offset = 0;
						rc.TableIndex = Records.Count;
						rc.Filter = Filters;  //组内成员全追加过滤信息

						Records.Add(rc);
					}

					continue;
				}
				else
				{
					//由于以下类型为已预先处理的虚拟类型，不需要再进行处理
					var typeName = RecordNode.Attributes["type"];
					if (typeName.MyEquals("struct")) continue;
					else if (typeName.MyEquals("dictionary")) continue;
					else if (typeName.MyEquals("type")) continue;

					throw new Exception($"Failed to determine attribute type: {Alias} => {typeName}");
				}
			}
			#endregion




			#region 重复alias处理
			ArgumentNullException.ThrowIfNull(Alias);

			if (!Alias.MyEquals("unk-"))
			{
				string RepeatTmp = Regex.Replace(Alias, @"-(\d|\d\d|\d\d\d)$", "");
				string alias_bak = Alias;

				int AliasRepeatIdx = 2;

				//check repeat alias
				while (Records.Where(g => g.Client == IsClient && g.Server == IsServer && g.Alias == Alias && (Filters.Count == 0 || g.Filter.Contains(Filters.First()))).Any())
					Alias = RepeatTmp + "-" + AliasRepeatIdx++;

				if (alias_bak != Alias) Console.WriteLine($"[LoadConfig] alias出现冲突，已变更为{Alias} （{ListSignal}）");
			}
			#endregion


			#region 枚举
			SeqInfo SeqInfo = Child.GetSeqInfo(Alias, PublicSeqs);
			if (Type == AttributeType.TSeq || Type == AttributeType.TProp_seq)
			{
				if (SeqInfo != null && SeqInfo.Count > sbyte.MaxValue)
					throw new ArgumentOutOfRangeException($"{Alias} -> 枚举成员已经超过当前支持的最大值，请使用Seq16");
			}
			else if (Type == AttributeType.TSeq16 || Type == AttributeType.TProp_field)
			{
				if (SeqInfo != null && SeqInfo.Count > short.MaxValue)
					throw new ArgumentOutOfRangeException($"{Alias} -> 枚举成员已经超过当前支持的最大值，请使用Seq32");
			}
			else if (SeqInfo != null) throw new TypeLoadException($"{Alias} -> 不能使用枚举值，请使用Seq类型");
			#endregion

			#region 默认值
			//判断枚举是否存在默认值，如果存在那么说明他就是Fields的默认值
			string DefaultValue = RecordNode.Attributes["default"]?.Trim();
			if (string.IsNullOrWhiteSpace(DefaultValue))
			{
				if (Type == AttributeType.TString) DefaultValue = "";
				else if (SeqInfo?.DefaultCell != null)
					DefaultValue = SeqInfo.DefaultCell.Alias;
			}

			DefaultInfo defaultInfo = new(DefaultValue, null);
			#endregion

			#region 创建记录器实例并分类处理
			RecordDef Rec = null;
			//if (RecordNode.ContainAttribute("repeat", out string Repeat) && int.TryParse(Repeat, out int RepeatTime))
			//{
			//	Rec = new RepeatRecord() { FinishNumber = RepeatTime };

			//	string Children1 = RecordNode.Attributes["children-1"];
			//	string Children2 = RecordNode.Attributes["children-2"];
			//	string Children3 = RecordNode.Attributes["children-3"];
			//	string Children4 = RecordNode.Attributes["children-4"];
			//	string Children5 = RecordNode.Attributes["children-5"];

			//	if (Children1 != null)
			//	{
			//		((RepeatRecord)Rec).Children = new List<string>();
			//		((RepeatRecord)Rec).Children.AddItem(Children1);
			//		((RepeatRecord)Rec).Children.AddItem(Children2);
			//		((RepeatRecord)Rec).Children.AddItem(Children3);
			//		((RepeatRecord)Rec).Children.AddItem(Children4);
			//		((RepeatRecord)Rec).Children.AddItem(Children5);
			//	}
			//}

			////对于定义了 repeat 的元素
			////start不表示起始位置，而是表示计数的开始编号
			//if (Rec is RepeatRecord repeatRecord)
			//{
			//	repeatRecord.StartNumber = RecordNode.ContainAttribute("start", out string tmpStart) && int.TryParse(tmpStart, out var tmp) ? tmp : 1;
			//}
			#endregion



			#region 存储记录器数据
			Rec = new RecordDef();
			Rec.TableIndex = Records.Count;
			Rec.Alias = Alias;
			Rec.Deprecated = RecordNode.Attributes["deprecated"].ToBool();
			Rec.IsKey = RecordNode.Attributes["key"].ToBool();
			Rec.Type = Type;
			Rec.Offset = Offset;
			Rec.Repeat = ushort.TryParse(RecordNode.Attributes["repeat"], out var tmp) ? tmp : (ushort)1;
			#endregion

			#region 存储记录器数据
			Rec.DefaultInfo = defaultInfo;
			Rec.Server = IsServer;
			Rec.Client = IsClient;
			Rec.ErrorType = ErrorType;
			Rec.RefInfo = RefInfo;
			Rec.Filter = Filters;       //数据筛选条件
			Rec.Seq = SeqInfo;          //枚举信息
			Rec.OutCond = OutCond;      //反序列输出条件
			Rec.ValidCond = ValidCond;  //输入条件
			Rec.CanInput = RecordNode.Attributes["input"].ToBoolOrNull() ?? true;
			Rec.CanOutput = RecordNode.Attributes["output"].ToBoolOrNull() ?? true;

			//注意：两种清除设置虽然名称一致，但是筛选Functions完全不同
			Rec.RuleDispel = RecordNode.Attributes["rule-dispel"].ToBoolOrNull() ?? false;
			Rec.NotRuleDispel = RecordNode.Attributes["not-rule-dispel"].ToBoolOrNull() ?? false;
			Rec.ApplyServer = RecordNode.Attributes["apply"].GetApplyMode();
			Rec.MinValue = RecordNode.Attributes["min"].ToLong();
			Rec.MaxValue = RecordNode.Attributes["max"].ToLong();

			Records.Add(Rec);
			#endregion
		}
		#endregion


		TableInfo.Records = Records;
		TableInfo.Records.Sort(new RecordSort());
		if (ListSignal != "#struct")
		{
			TableInfo.RecordTables = TableInfo.GetRecordTable(true, true);
#if (false)
				TableInfo.RecordTables.ShowInfo();
#endif
		}

		return TableInfo;
	}


	public static void GetVersion(this string Info, out ushort MajorVersion, out ushort MinorVersion)
	{
		MajorVersion = MinorVersion = 0;
		if (Info is null) return;

		var VersionText = Info.Split('.');
		MajorVersion = (ushort)VersionText[0].ToShort();
		MinorVersion = (ushort)VersionText[1].ToShort();
	}





	private static void ShowInfo(this Dictionary<short, TypeRecordTable> TableInfo)
	{
		foreach (var Table in TableInfo)
		{
			var subclass = Table.Key;
			var records = Table.Value.Records;
			foreach (var recordDef in records)
			{
				#region 获取类型信息
				string FilterInfo = null;
				if (subclass != -1 && !recordDef.Filter.Any()) continue;
				else if (subclass != -1) FilterInfo = $"[{subclass} - {null /*TableInfo.TypeInfo.GetCell(type.Key).Alias*/ }] ";
				#endregion

				for (int x = 1; x <= recordDef.Repeat; x++)
				{
					int CurOffset = recordDef.Offset + recordDef.Size * (x - 1);
					Console.WriteLine($"#notime#{FilterInfo}{CurOffset}  -  {recordDef.GetAlias(x)}");
				}
			}
		}
	}
}