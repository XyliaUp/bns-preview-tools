using System.Text;

namespace Xylia.Preview.Tests.DatTool.Windows.DevTools;
public static class Create
{
    public static string Class(string Text)
    {
        StringBuilder result = new();

   //     ushort? SubClass = null;
   //     void EndClass()
   //     {
   //         if (SubClass.HasValue)
   //         {
   //             result.Remove(result.Length - 2, 1);
   //             result.AppendLine("}\n");
   //         }
   //     }



   //     XmlDocument tmp = new();
   //     tmp.LoadXml($"<?xml version=\"1.0\"?>\n<table>{Text?.Trim()}</table>");

   //     var table = DataTableDefinition.LoadFrom(tmp.SelectNodes("table/record").OfType<XmlElement>(), null);
   //     foreach (AttributeDef attr in table.Attributes)
   //     {
			//var name = attr.Name;
			//if (attr.Repeat > 1) name += $"-{0}";

			////if (SubClass != attr.Filter)
			////{
			////    EndClass();

			////    result.AppendLine($"public sealed class {table.TypeInfo.GetName((short)attr.Filter.Value).TitleCase()} : BaseClass");
			////    result.AppendLine("{");

			////    SubClass = attr.Filter;
			////}

			//#region Type
			//string TypeInfo = attr.Type switch
   //         {
   //             AttributeType.TInt8 => "byte",
   //             AttributeType.TInt16 => "short",
   //             AttributeType.TInt32 => "int",
   //             AttributeType.TInt64 => "long",
   //             AttributeType.TFloat32 => "float",
   //             AttributeType.TBool => "bool",
   //             AttributeType.TString or AttributeType.TNative => "string",
   //             AttributeType.TRef => attr.ReferedTableName?.TitleCase(),
   //             AttributeType.TXUnknown1 or AttributeType.TTime64 => "DateTime",
   //             AttributeType.TXUnknown2 => "FPath",
   //             AttributeType.TSeq or AttributeType.TSeq16 or AttributeType.TProp_seq or AttributeType.TProp_field
   //                 => attr.SequenceDef?.Name.TitleCase() ?? attr.Name.TitleCase() + "Seq",

   //             _ => attr.Type.ToString(),
   //         };
   //         #endregion


   //         #region sys_attr
   //         List<string> sys_attr = new();
   //         if (name.Contains('-'))
   //         {
   //             if (SubClass.HasValue) result.Append('\t');
   //             sys_attr.Add($"Signal(\"{name}\")");
   //         }

   //         if (attr.Repeat > 1) sys_attr.Add($"Repeat({attr.Repeat})");

   //         if (sys_attr.Any()) result.AppendLine($"[{sys_attr.Aggregate(", ")}]");
   //         #endregion


   //         if (SubClass.HasValue) result.Append('\t');
   //         result.Append($"public {TypeInfo} {name.TitleCase()};\n\n");
   //     }
   //     EndClass();

        return result.ToString();
    }

    public static string Seq(string Text)
    {
        return null;
    }
}