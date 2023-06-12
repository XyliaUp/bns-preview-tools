using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

using Xylia.Extension;
using Xylia.Preview.Data.Record;
using Xylia.Xml;

namespace Xylia.Preview.Data.Table.XmlRecord
{
	public static class Extension
	{
		/// <summary>
		/// 处理存在子类的节点
		/// </summary>
		/// <typeparam name="SubType"></typeparam>
		/// <typeparam name="TNode"></typeparam>
		/// <param name="CaseNode"></param>
		/// <param name="Index"></param>
		/// <param name="Owner"></param>
		/// <param name="Func"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="InvalidCastException"></exception>
		public static TNode TypeFactory<SubType, TNode>(this XmlElement CaseNode, Func<SubType, TNode> Func)
			where SubType : Enum
			where TNode : TypeBaseRecord<SubType>
		{
			var typeVal = CaseNode.Attributes["type"]?.Value?.Trim();
			if (!typeVal.TryParseToEnum(out SubType Type)) 
				throw new InvalidCastException($"转换失败 (type: { typeVal })");

			var record = Func(Type);
			if (record is null) throw new InvalidCastException($"{Type} 尚未适配转换类");

			record.Type = Type;
			record.LoadData(CaseNode);

			return record;
		}



		#region ReadFile
		/// <summary>
		/// 将指定文件的所有 <see langword="节点"/> 读取为 <see cref="TableNode"/> 实例
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="FilePath"></param>
		/// <param name="TableIndex"></param>
		/// <param name="NodeName"></param>
		/// <returns></returns>
		public static List<T> ReadFile<T>(this string FilePath, ref uint TableIndex, string NodeName = null) where T : BaseRecord, new()
			 => FilePath.GetXmlDocument().ReadFile<T>(ref TableIndex, NodeName);

		public static List<T> ReadFile<T>(this XmlDocument XmlDoc, ref uint TableIndex, string NodeName = null) where T : BaseRecord, new()
	        => XmlDoc.DocumentElement.ReadFile<T>(ref TableIndex, NodeName);

		public static List<T> ReadFile<T>(this XmlElement DocElement, ref uint TableIndex, string NodeName = null) where T : BaseRecord, new()
		{
			List<T> tables = new();
			NodeName ??= typeof(T).Name.ToLower();
			foreach (XmlElement table in DocElement.SelectNodes("./" + NodeName))
			{
				var o = new T();
				o.TableIndex = TableIndex++;
				o.LoadData(table);

				tables.Add(o);
			}

			return tables;
		}

		





		/// <summary>
		/// 将指定文件夹下特定的所有文件的 <see langword="T"/> 节点Load 为 <see cref="TableNode"/> 实例
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="FolderPath"></param>
		/// <param name="TableIndex"></param>
		/// <param name="NodeName"></param>
		/// <param name="Pattern"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public static List<T> ReadFiles<T>(this string FolderPath, ref uint TableIndex, string NodeName = null, string Pattern = null) where T : BaseRecord, new()
		{
			//获取节点名称和文件名通配
			NodeName ??= typeof(T).Name.ToLower();
			Pattern ??= typeof(T).Name.ToLower() + "data*.xml";


			BlockingCollection<T> Result = new();
			BlockingCollection<int> KeyInfo = new();
			Parallel.ForEach(new DirectoryInfo(FolderPath).GetFiles(Pattern), fi =>
			{
				uint tableIndex = 0;
				foreach (var table in ReadFile<T>(fi.FullName, ref tableIndex, NodeName))
				{
					Result.Add(table);

					//判断主键是否重复
					var CurKey = table.Key();
					if (CurKey != 0 && KeyInfo.Contains(CurKey)) throw new Exception($"主键不能重复 ({CurKey})");
					else KeyInfo.Add(CurKey);
				}
			});

			return Result.ToList();
		}
		#endregion
	}
}