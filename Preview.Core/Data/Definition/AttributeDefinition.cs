using System;
using System.Linq;

using BnsBinTool.Core.Definitions;

using Xylia.Extension;
using Xylia.Preview.Data.Models.BinData.Analyse.Struct.Record;

namespace Xylia.Preview.Data.Definition;

public class MyAttributeDefinition : AttributeDefinition
{
	public string ReferedTableName;

	public static MyAttributeDefinition LoadFrom(RecordDef AttrDef)
	{
		if (AttrDef.Deprecated)
			return null;

		var attribute = new MyAttributeDefinition
		{
			Name = AttrDef.Alias,
			OriginalName = AttrDef.Alias,
			Type = AttrDef.Type,
			DefaultValue = AttrDef.DefaultInfo?.Value,
			Repeat = AttrDef.Repeat,
			ReferedTable = AttrDef.RefInfo?.RefTable.ToShort() ?? 0,
			ReferedTableName = AttrDef.RefInfo?.RefTable,
			Offset = AttrDef.Offset,
			Size = AttrDef.Size,
			IsKey = AttrDef.IsKey,
			IsRequired = false, //AttrDef["required"];
			IsHidden = false,   //AttrDef["hidden"];
		};

		if (AttrDef.Seq != null)
			attribute.Sequence.AddRange(AttrDef.Seq?.Select(seq => seq.Alias));

		switch (attribute.Type)
		{
			case AttributeType.TSeq:
			case AttributeType.TProp_seq:
				if (attribute.Size != 1)
					Console.WriteLine("Fixing attribute size");
				attribute.Size = 1;
				break;

			case AttributeType.TSeq16:
			case AttributeType.TProp_field:
				if (attribute.Size != 2)
					Console.WriteLine("Fixing attribute size");
				attribute.Size = 2;
				break;
		}

		try
		{
			attribute.AttributeDefaultValues = AttributeDefaultValues.FromAttribute(attribute);
		}
		catch (Exception)
		{
			attribute.AttributeDefaultValues = new();
		}

		return attribute;
	}

	public new MyAttributeDefinition DuplicateOffseted(int offset)
        {
            var attr =  new MyAttributeDefinition
		{
                Name = Name,
                OriginalName = OriginalName,
                TypeName = TypeName,
                Type = Type,
                DefaultValue = DefaultValue,
                Repeat = Repeat,
			ReferedTable = ReferedTable,
                Offset = (ushort) (Offset + offset),
                Size = Size,
                IsKey = IsKey,
                IsRequired = IsRequired,
                IsHidden = IsHidden,
                AttributeDefaultValues = AttributeDefaultValues,
			ReferedTableName = ReferedTableName,
		};

		attr.Sequence.AddRange(Sequence);

		return attr;
        }
}