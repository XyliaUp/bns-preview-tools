using System.Text;
using System.Xml;

using BnsBinTool.Core.Definitions;

using Xylia.Extension;
using Xylia.Preview.Data.Models.BinData.Table;
using Xylia.Preview.Data.Models.BinData.Table.Config;

namespace Xylia.Preview.Tests.DatTool.Windows.DevTools;
public static class CreateClass
{
	public static string Instance(string text)
	{
		var xml = new XmlDocument();
		xml.LoadXml(text);

		return Instance(xml);
	}

	public static string Instance(XmlDocument xml)
	{
		StringBuilder result = new();

		var table = TableDefinitionEx.LoadFrom(null, xml.DocumentElement);
		foreach (var attribute in table.Attributes) InstanceAttribute(attribute, result);

		foreach (var sub in table.Subtables)
		{
			result.AppendLine($"public sealed class {sub.Name.TitleCase()} : BaseClass");
			result.AppendLine("{");

			foreach (var attribute in sub.Attributes) InstanceAttribute(attribute, result, true);

			result.Remove(result.Length - 2, 1);
			result.AppendLine("}\n");
		}

		return result.ToString();
	}


	private static void InstanceAttribute(AttributeDefinition attribute, StringBuilder result, bool SubClass = false)
	{
		var prefix = new string('\t', SubClass ? 1 : 0);

		#region type
		string TypeInfo = attribute.Type switch
		{
			AttributeType.TInt8 => "sbyte",
			AttributeType.TInt16 => "short",
			AttributeType.TInt32 => "int",
			AttributeType.TInt64 => "long",
			AttributeType.TFloat32 => "float",
			AttributeType.TBool => "bool",
			AttributeType.TString or AttributeType.TNative => "string",
			AttributeType.TXUnknown1 or AttributeType.TTime64 => "DateTime",
			AttributeType.TXUnknown2 => "FPath",
			AttributeType.TRef => attribute is AttributeDef attrDef ? attrDef.ReferedTableName?.TitleCase() : $"ref_{attribute.ReferedTable}",
			AttributeType.TSeq or AttributeType.TSeq16 or AttributeType.TProp_seq or AttributeType.TProp_field
				=> attribute.SequenceDef?.Name?.TitleCase() ?? attribute.Name.TitleCase() + "Seq",

			_ => attribute.Type.ToString(),
		};

		#endregion

		#region sys_attr
		List<string> sys_attr = new();
		if (attribute.Name.RegexMatch(@"^\d+$")) sys_attr.Add($"Signal(\"{attribute.Name}\")");
		if (attribute.Repeat > 1)
		{
			TypeInfo = $"{TypeInfo}[]";
			sys_attr.Add($"Repeat({attribute.Repeat})");
		}


		if (sys_attr.Any()) result.AppendLine($"{prefix}[{sys_attr.Aggregate(", ")}]");
		#endregion


		result.Append($"{prefix}public {TypeInfo} {attribute.Name.TitleCase()};\n\n");
	}
}