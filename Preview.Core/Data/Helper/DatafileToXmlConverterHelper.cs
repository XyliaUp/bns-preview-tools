using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using BnsBinTool.Core;
using BnsBinTool.Core.DataStructs;
using BnsBinTool.Core.Definitions;
using BnsBinTool.Core.Helpers;

using Xylia.Extension;

using RecordModel = BnsBinTool.Core.Models.Record;
using TableModel = BnsBinTool.Core.Models.Table;

namespace Xylia.Preview.Data.Helper;

public class DatafileToXmlConverterHelper
{
	private readonly DatafileDefinition _datafileDef;

	private readonly ResolvedAliases _tablesAliases;

	public DatafileToXmlConverterHelper(DatafileDefinition datafileDef, IEnumerable<TableModel> Tables = null)
	{
		_datafileDef = datafileDef;
		_tablesAliases = new();


		if (Tables is null)
			return;

		DatafileAliasResolverHelper.Resolve(_tablesAliases, datafileDef, Tables);
		DatafileAliasResolverHelper.ResolveXmlDatAlias(_tablesAliases, datafileDef);
	}



	public void ProcessTable(TableModel table, TableDefinition tableDef, string _outputPath)
	{
		if (tableDef is null && !_datafileDef.TryGetValue(table.Type, out tableDef))
			return;

		if (tableDef.IsEmpty)
			return;

		// Split table by subtypes
		var typeDictionary = new Dictionary<short, List<RecordModel>>();
		if (true && !table.IsCompressed) typeDictionary[-1] = table.Records;
		else
		{
			foreach (var record in table.Records)
			{
				if (!typeDictionary.TryGetValue(record.SubclassType, out var typeRecords))
				{
					typeRecords = new List<RecordModel>();
					typeDictionary[record.SubclassType] = typeRecords;
				}

				typeRecords.Add(record);
			}
		}

		var hasMany = typeDictionary.Count > 1;


		try
		{
			foreach (var group in typeDictionary)
			{
				var name = tableDef.Name.TitleCase() + "Data";

				var memory = new MemoryStream();
				using var writer = new XmlTextWriter(memory, Encoding.UTF8)
				{
					Formatting = Formatting.Indented,
					Indentation = 4
				};


				writer.WriteStartDocument();
				writer.WriteStartElement("table");
				writer.WriteAttributeString("release-module", "LocalizationData");
				writer.WriteAttributeString("release-side", "client");
				writer.WriteAttributeString("type", tableDef.Name);
				writer.WriteAttributeString("version", table.MajorVersion + "." + table.MinorVersion);
				writer.WriteComment($" {name}.xml ");

				foreach (var record in group.Value)
				{
					writer.WriteStartElement("record");

					ITableDefinition subtableDef;
					var type = record.SubclassType;
					if (type == -1) subtableDef = tableDef;
					else if (type >= tableDef.Subtables.Count)
					{
						//continue;
						subtableDef = tableDef;
						writer.WriteAttributeString("type", type.ToString());
					}
					else
					{
						subtableDef = tableDef.Subtables[type];
						writer.WriteAttributeString("type", subtableDef.Name);
					}

					ConvertRecord(writer, record, subtableDef.ExpandedAttributes);
					writer.WriteEndElement();
				}

				writer.WriteEndElement();
				writer.WriteEndDocument();
				writer.Flush();



				Span<byte> data = memory.GetBuffer()[..(int)memory.Length];

				if (!hasMany) Directory.CreateDirectory(_outputPath);
				else
				{
					Directory.CreateDirectory($"{_outputPath}\\{name}");

					string sub = group.Key >= 0 ?
						group.Key >= tableDef.Subtables.Count ? group.Key.ToString() : tableDef.Subtables[group.Key].Name :
						$"base_{name}";

					name = $"{name}\\{sub}";
				}

				using var fstream = File.Open(_outputPath + $"\\{name}.xml", FileMode.Create, FileAccess.Write, FileShare.Read);
				fstream.Write(data);
				fstream.Close();
			}
		}
		catch
		{
			System.Diagnostics.Debug.WriteLine("[ProcessTable] " + tableDef.Name);
		}
	}

	private void ConvertRecord(XmlTextWriter writer, RecordModel record, List<AttributeDefinition> attributes) => attributes.ForEach(attr =>
	{
		var value = ConvertRecord(record, attr);
		if (value is null) return;

		writer.WriteAttributeString(attr.Name, value);
	});

	public string ConvertRecord(RecordModel record, AttributeDefinition attribute, bool _noValidate = true)
	{
		if (_noValidate && attribute.Offset >= record.DataSize)
			return null;

		string value;

		switch (attribute.Type)
		{
			case AttributeType.TRef:
				var r = record.Get<Ref>(attribute.Offset);
				if (r == attribute.AttributeDefaultValues.DRef)
					return null;

				if (!this._tablesAliases.ByRef.TryGetValue(attribute.ReferedTable, out var tableAliases)
					|| !tableAliases.TryGetValue(r, out value)
					|| string.IsNullOrWhiteSpace(value))
					value = $"{r.Id}:{r.Variant}";

				break;

			case AttributeType.TIcon:
				var ir = record.Get<IconRef>(attribute.Offset);

				if (ir == attribute.AttributeDefaultValues.DIconRef)
					return null;

				if (!_tablesAliases.ByRef.TryGetValue(_datafileDef.IconTextureTableId, out tableAliases)
					|| !tableAliases.TryGetValue(ir, out value))
					value = $"{ir.IconTextureRecordId}:{ir.IconTextureVariantId},{ir._unk_i32_0}";
				else
					value += $",{ir._unk_i32_0}";

				break;

			case AttributeType.TTRef:
				var tr = record.Get<TRef>(attribute.Offset);

				if (tr == attribute.AttributeDefaultValues.DTRef || tr.Id == 0)
					return null;

				var tableName = tr.Table.ToString();
				if (_datafileDef.TryGetValue(tr.Table, out var table)) tableName = table.Name;


				if (!_tablesAliases.ByRef.TryGetValue(tr.Table, out tableAliases)
					|| !tableAliases.TryGetValue(tr, out value))
					value = $"{tableName}:{tr.Id}:{tr.Variant}";
				else
					value = $"{tableName}:" + value;

				break;

			case AttributeType.TNative:
				var n = record.Get<Native>(attribute.Offset);

				if (n == attribute.AttributeDefaultValues.DNative)
					return null;

				value = record.StringLookup.GetString(n.Offset);
				break;

			case AttributeType.TVector32:
				var v = record.Get<Vector32>(attribute.Offset);

				if (v == attribute.AttributeDefaultValues.DVector32)
					return null;

				value = $"{v.X},{v.Y},{v.Z}";
				break;


			case AttributeType.TInt16:
			case AttributeType.TSub:
				var s = record.Get<short>(attribute.Offset);

				if (s == attribute.AttributeDefaultValues.DShort)
					return null;

				value = s.ToString();

				break;

			case AttributeType.TVelocity:
				var velocity = record.Get<ushort>(attribute.Offset) / 4;

				if (velocity == attribute.AttributeDefaultValues.DVelocity)
					return null;

				value = velocity.ToString();
				break;

			case AttributeType.TDistance:

				var distance = record.Get<short>(attribute.Offset) / 4;

				if (distance == attribute.AttributeDefaultValues.DShort)
					return null;

				value = distance.ToString();

				break;


			case AttributeType.TInt64:
				var l = record.Get<long>(attribute.Offset);

				if (l == attribute.AttributeDefaultValues.DLong)
					return null;

				value = l.ToString();
				break;


			case AttributeType.TTime64:
				var t = record.Get<long>(attribute.Offset);

				if (t == attribute.AttributeDefaultValues.DLong)
					return null;

				value = t.GetBNSTime().ToString();
				break;


			case AttributeType.TInt32:
			case AttributeType.TMsec:
				var integer = record.Get<int>(attribute.Offset);

				if (integer == attribute.AttributeDefaultValues.DInt)
					return null;

				value = integer.ToString();
				break;

			case AttributeType.TInt8:
				var b = (sbyte)record.Data[attribute.Offset];

				if (b == attribute.AttributeDefaultValues.DByte)
					return null;

				value = b.ToString();
				break;

			case AttributeType.TFloat32:
				var f = record.Get<float>(attribute.Offset);

				if (Math.Abs(f - attribute.AttributeDefaultValues.DFloat) < 0.001)
					return null;

				value = f.ToString(CultureInfo.InvariantCulture);
				break;

			case AttributeType.TString:
				var str = record.StringLookup.GetString(record.Get<int>(attribute.Offset));

				if (str == attribute.AttributeDefaultValues.DString)
					return null;

				value = str;
				break;

			case AttributeType.TBool:
				var bol = record.Data[attribute.Offset] == 1;

				if (bol == attribute.AttributeDefaultValues.DBool)
					return null;

				value = bol ? "y" : "n";
				break;

			case AttributeType.TSeq:
			case AttributeType.TProp_seq:
			{
				var idx = record.Get<sbyte>(attribute.Offset);
				if (idx >= 0 && idx < attribute.Sequence.Count)
				{
					var seq = attribute.Sequence[idx];

					if (seq == attribute.AttributeDefaultValues.DSeq)
						return null;

					value = seq;
				}
				else
				{
					value = idx.ToString();
					if (!_noValidate) ThrowHelper.ThrowException("Invalid sequence index");
				}

				break;
			}

			case AttributeType.TSeq16:
			case AttributeType.TProp_field:
			{
				var idx = record.Get<short>(attribute.Offset);
				if (idx >= 0 && idx < attribute.Sequence.Count)
				{
					var seq = attribute.Sequence[idx];

					if (seq == attribute.AttributeDefaultValues.DSeq)
						return null;

					value = seq;
				}
				else
				{
					value = idx.ToString();
					if (!_noValidate) ThrowHelper.ThrowException("Invalid sequence index");
				}

				break;
			}

			case AttributeType.TScript_obj:
				var scriptObjBytes = record.Data[
					attribute.Offset..(attribute.Offset + attribute.Size)
				];

				if (scriptObjBytes.All(x => x == 0))
					return null;

				value = Convert.ToBase64String(scriptObjBytes);
				break;

			case AttributeType.TIColor:
				var c = record.Get<IColor>(attribute.Offset);

				if (c == attribute.AttributeDefaultValues.DIColor)
					return null;

				value = $"{c.R},{c.G},{c.B}";
				break;

			case AttributeType.TBox:
				var box = record.Get<Box>(attribute.Offset);

				value = $"{box.X1},{box.Y1},{box.Z1},{box.X2},{box.Y2},{box.Z2}";
				break;

			case AttributeType.TXUnknown1:
			case AttributeType.TXUnknown2:
				l = record.Get<long>(attribute.Offset);

				if (l == attribute.AttributeDefaultValues.DLong)
					return null;

				value = l.ToString();
				break;

			case AttributeType.TNone:
				value = "";
				break;

			default:
				ThrowHelper.ThrowException($"Unhandled type name: '{attribute.Type}'");
				value = null;
				break;
		}

		return value;
	}
}