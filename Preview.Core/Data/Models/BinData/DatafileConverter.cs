using System.Globalization;
using System.Text;
using System.Xml;

using BnsBinTool.Core;
using BnsBinTool.Core.DataStructs;
using BnsBinTool.Core.Definitions;
using BnsBinTool.Core.Helpers;

using Xylia.Extension;
using Xylia.Preview.Data.Models.BinData.Table;

using RecordModel = BnsBinTool.Core.Models.Record;
using TableModel = BnsBinTool.Core.Models.Table;

namespace Xylia.Preview.Data.Models.BinData;
public class DatafileConverter
{
	private readonly DatafileDefinition _datafileDef;

	private readonly ResolvedAliases _tablesAliases;

	public readonly RecordBuilder Builder;

	public DatafileConverter(DatafileDefinition datafileDef, IEnumerable<TableModel> Tables)
	{
		_datafileDef = datafileDef;
		_tablesAliases = new();

		DatafileAliasResolverHelper.Resolve(_tablesAliases, datafileDef, Tables);
		DatafileAliasResolverHelper.ResolveXmlDatAlias(_tablesAliases, datafileDef);


		Builder = new RecordBuilder(_datafileDef , _tablesAliases);
	}




	public void ProcessTable(TableModel table, TableDefinition tableDef, string _outputPath)
	{
		if (tableDef is null && !_datafileDef.TryGetValue(table.Type, out tableDef))
			return;

		TableDefinitionEx.CheckSize(tableDef, table);


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

					// attributes
					subtableDef.ExpandedAttributes.ForEach(attr =>
					{
						var value = ConvertRecord(record, attr);
						if (value is null || value == attr.DefaultValue) return;

						writer.WriteAttributeString(attr.Name, value);
					});

					writer.WriteEndElement();
				}

				writer.WriteEndElement();
				writer.WriteEndDocument();
				writer.Flush();



				if (hasMany)
				{
					string sub = group.Key >= 0 ?
						group.Key >= tableDef.Subtables.Count ? group.Key.ToString() : tableDef.Subtables[group.Key].Name :
						$"base_{name}";

					name = $"{name}\\{name}_{sub}";
				}

				var path = _outputPath + $"\\{name}.xml";
				Directory.CreateDirectory(Path.GetDirectoryName(path));


				using var fstream = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.Read);
				fstream.Write(memory.GetBuffer()[..(int)memory.Length]);
				fstream.Close();
			}
		}
		catch (Exception ex)
		{
			Debug.WriteLine($"table `{tableDef.Name}` process failed: {ex.Message}");
		}
	}

	/// <summary>
	/// get attribute value
	/// </summary>
	/// <param name="record"></param>
	/// <param name="attribute"></param>
	/// <param name="_noValidate"></param>
	/// <returns></returns>
	public string ConvertRecord(RecordModel record, AttributeDefinition attribute, bool _noValidate = true)
	{
		if (_noValidate && attribute.Offset >= record.DataSize)
			return null;

		string value;

		switch (attribute.Type)
		{
			case AttributeType.TNone:
				return null;

			case AttributeType.TRef:
			{
				var r = record.Get<Ref>(attribute.Offset);
				if (r.Id == 0 && r.Variant == 0) return "";

				value = ToRef(attribute.ReferedTable , r);
			}
			break;

			case AttributeType.TTRef:
			{
				var r = record.Get<TRef>(attribute.Offset);
				if (r.Id == 0 && r.Variant == 0) return "";

				var tableName = r.Table.ToString();
				if (_datafileDef.TryGetValue(r.Table, out var table)) tableName = table.Name;

				value = $"{tableName}:" + ToRef(r.Table, r);
			}
			break;

			case AttributeType.TIcon:
			{
				var r = record.Get<IconRef>(attribute.Offset);
				if (r.IconTextureRecordId == 0 && r.IconTextureVariantId == 0) return "";

				value = ToRef(_datafileDef.IconTextureTableId, r) + $",{r._unk_i32_0}";
				
			}
			break;

			case AttributeType.TNative:
			{
				var n = record.Get<Native>(attribute.Offset);
				//if (n == attribute.AttributeDefaultValues.DNative)
				//	return null;

				value = record.StringLookup.GetString(n.Offset);
				break;
			}

			case AttributeType.TVector32:
			{
				var v = record.Get<Vector32>(attribute.Offset);
				//if (v == attribute.AttributeDefaultValues.DVector32)
				//	return null;

				value = $"{v.X},{v.Y},{v.Z}";
				break;
			}


			case AttributeType.TInt16:
			case AttributeType.TSub:
			{
				var s = record.Get<short>(attribute.Offset);

				value = s.ToString();
				break;
			}

			case AttributeType.TVelocity:
			{
				var velocity = record.Get<ushort>(attribute.Offset) / 4;

				value = velocity.ToString();
				break;
			}

			case AttributeType.TDistance:
			{
				var distance = record.Get<short>(attribute.Offset) / 4;

				value = distance.ToString();
				break;
			}

			case AttributeType.TInt64:
			{
				var l = record.Get<long>(attribute.Offset);

				value = l.ToString();
				break;
			}

			case AttributeType.TInt32:
			case AttributeType.TMsec:
			{
				var integer = record.Get<int>(attribute.Offset);

				value = integer.ToString();
				break;
			}

			case AttributeType.TInt8:
			{
				var b = (sbyte)record.Data[attribute.Offset];

				value = b.ToString();
				break;
			}

			case AttributeType.TFloat32:
			{
				var f = record.Get<float>(attribute.Offset);
				if (Math.Abs(f - attribute.AttributeDefaultValues.DFloat) < 0.001)
					return null;

				value = f.ToString(CultureInfo.InvariantCulture);
				break;
			}

			case AttributeType.TString:
				return record.StringLookup.GetString(record.Get<int>(attribute.Offset));

			case AttributeType.TBool:
			{
				var bol = record.Data[attribute.Offset] == 1;
				//if (bol == attribute.AttributeDefaultValues.DBool)
				//	return null;

				value = bol ? "y" : "n";
				break;
			}

			case AttributeType.TSeq:
			case AttributeType.TProp_seq:
			{
				var idx = record.Get<sbyte>(attribute.Offset);
				if (idx >= 0 && idx < attribute.Sequence.Count)
				{
					value = attribute.Sequence[idx];
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
					value = attribute.Sequence[idx];
				}
				else
				{
					value = idx.ToString();
					if (!_noValidate) ThrowHelper.ThrowException("Invalid sequence index");
				}

				break;
			}

			case AttributeType.TScript_obj:
			{
				var scriptObjBytes = record.Data[attribute.Offset..(attribute.Offset + attribute.Size)];
				if (scriptObjBytes.All(x => x == 0))
					return null;

				value = Convert.ToBase64String(scriptObjBytes);
				break;
			}

			case AttributeType.TIColor:
			{
				var c = record.Get<IColor>(attribute.Offset);
				//if (c == attribute.AttributeDefaultValues.DIColor)
				//	return null;

				value = $"{c.R},{c.G},{c.B}";
				break;
			}

			case AttributeType.TBox:
			{
				var box = record.Get<Box>(attribute.Offset);

				value = $"{box.X1},{box.Y1},{box.Z1},{box.X2},{box.Y2},{box.Z2}";
				break;
			}

			case AttributeType.TTime64:
			case AttributeType.TXUnknown1:
			{
				var l = record.Get<long>(attribute.Offset);
				if (l == 0) return null;

				// get time zone
				//if (l > 0) l += 3600;
				value = l.GetDateTime().ToString();

				//new DateTime(tick, DateTimeKind.Local, isAmbiguousLocalDst);

				break;
			}

			case AttributeType.TXUnknown2:
				return record.StringLookup.GetString(record.Get<int>(attribute.Offset));


			default: throw new Exception($"Unhandled type name: '{attribute.Type}'");
		}

		return value;
	}




	private string ToRef(int table, Ref Ref)
	{
		if (!_tablesAliases.ByRef.TryGetValue(table, out var tableAliases) ||
			!tableAliases.TryGetValue(Ref, out string value) ||
			string.IsNullOrWhiteSpace(value))
			return Ref.Variant == 0 ? $"{Ref.Id}" : $"{Ref.Id}.{Ref.Variant}";

		return value;
	}

	//public DateTime ToLocalTime(DateTime time)
	//{

	//}
}