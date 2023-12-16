using System.Xml;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Exceptions;

namespace Xylia.Preview.Data.Engine.Definitions;
public partial class AttributeDefinition
{
	public string Name { get; set; }
	public AttributeType Type { get; set; }
	public string DefaultValue { get; set; }
	public ushort Repeat { get; set; }
	public short ReferedTable { get; set; }
	public ushort Offset { get; set; }
	public ushort Size { get; set; }
	public bool IsDeprecated { get; set; }
	public bool IsKey { get; set; }
	public bool IsRequired { get; set; }
	public bool IsHidden { get; set; }
	public SequenceDefinition Sequence { get; set; }


	public string ReferedTableName { get; set; }
	public double Max { get; set; }
	public double Min { get; set; }
	public bool CanOutput { get; set; } = true;
	public bool CanInput { get; set; } = true;
	public bool Server { get; set; } = true;
	public bool Client { get; set; } = true;


	#region Load Methods
	public AttributeDefinition Clone() => (AttributeDefinition)MemberwiseClone();

	public static AttributeDefinition LoadFrom(XmlElement node, ITableDefinition table, Func<SequenceDefinition> seqfun)
	{
		if ((node.Attributes["deprecated"]?.Value).ToBool())
			return null;

		var Name = node.Attributes["name"]?.Value?.Trim();
		ArgumentNullException.ThrowIfNull(Name);

		#region Type & Ref
		var TypeName = node.Attributes["type"]?.Value;
		var Type = Enum.TryParse("T" + TypeName?.Trim(), true, out AttributeType ParseVType) ? ParseVType : default;

		var RefTable = node.Attributes["ref"]?.Value;
		if (RefTable != null) Type = AttributeType.TRef;

		if (Type == AttributeType.TIcon) RefTable = "IconTexture";
		if (Type == AttributeType.TNone)
		{
			if (TypeName.Equals("struct", StringComparison.OrdinalIgnoreCase)) return null;
			else if (TypeName.Equals("dictionary", StringComparison.OrdinalIgnoreCase)) return null;

			throw BnsDataException.InvalidDefinition($"Failed to determine attribute type: {table.Name}: {Name} ({TypeName})");
		}
		#endregion

		#region Seq
		var seq = seqfun();
		seq?.Check(Type);
		#endregion

		#region Default
		double MaxValue = (node.Attributes["max"]?.Value).ToDouble();
		double MinValue = (node.Attributes["min"]?.Value).ToDouble();
		string DefaultValue = node.Attributes["default"]?.Value?.Trim();
		if (string.IsNullOrEmpty(DefaultValue)) DefaultValue = null;

		switch (Type)
		{
			case AttributeType.TInt8:
				DefaultValue ??= "0";
				if (MinValue == 0) MinValue = sbyte.MinValue;
				if (MaxValue == 0) MaxValue = sbyte.MaxValue;
				break;

			case AttributeType.TInt16:
			case AttributeType.TDistance:
			case AttributeType.TAngle:
				DefaultValue ??= "0";
				if (MinValue == 0) MinValue = short.MinValue;
				if (MaxValue == 0) MaxValue = short.MaxValue;
				break;

			case AttributeType.TVelocity:
				DefaultValue ??= "0";
				break;

			case AttributeType.TInt32:
			case AttributeType.TMsec:
				DefaultValue ??= "0";
				if (MinValue == 0) MinValue = int.MinValue;
				if (MaxValue == 0) MaxValue = int.MaxValue;
				break;


			case AttributeType.TInt64:
				DefaultValue ??= "0";
				if (MinValue == 0) MinValue = long.MinValue;
				if (MaxValue == 0) MaxValue = long.MaxValue;
				break;

			case AttributeType.TFloat32:
				DefaultValue ??= "0";
				if (MinValue == 0) MinValue = float.MinValue;
				if (MaxValue == 0) MaxValue = float.MaxValue;
				break;

			case AttributeType.TBool:
				DefaultValue ??= "n";
				break;


			case AttributeType.TRef:
			case AttributeType.TIcon:
			case AttributeType.TTRef:
				break;


			case AttributeType.TString:
			case AttributeType.TNative:
			case AttributeType.TXUnknown2:
				DefaultValue ??= "";
				break;

			case AttributeType.TSeq:
			case AttributeType.TSeq16:
			case AttributeType.TProp_seq:
			case AttributeType.TProp_field:
			{
				if (Name.Contains("forwarding-types"))
					seq.Default = seq.FirstOrDefault();

				DefaultValue ??= seq?.Default;
				break;
			}

			case AttributeType.TSub:
				DefaultValue ??= "0";
				break;

			case AttributeType.TVector16:
				DefaultValue ??= "0,0,0";
				break;

			case AttributeType.TVector32:
				DefaultValue ??= "0,0,0";
				break;

			case AttributeType.TIColor:
				DefaultValue ??= new IColor().ToString();
				break;

			case AttributeType.TScript_obj:
				break;

			case AttributeType.TTime64:
			case AttributeType.TXUnknown1:
				break;
		}
		#endregion


		return new AttributeDefinition
		{
			Name = Name,

			//IsDeprecated = (node.Attributes["deprecated"]?.Value).ToBool(),
			IsKey = (node.Attributes["key"]?.Value).ToBool(),
			IsRequired = (node.Attributes["required"]?.Value).ToBool(),
			IsHidden = (node.Attributes["hidden"]?.Value).ToBool(),

			Type = Type,
			Offset = (ushort)(node.Attributes["offset"]?.Value).ToInt16(),
			Repeat = ushort.TryParse(node.Attributes["repeat"]?.Value, out var tmp) ? tmp : (ushort)1,
			ReferedTableName = RefTable,
			Sequence = seq,
			DefaultValue = DefaultValue,
			Max = MaxValue,
			Min = MinValue,

			Server = node.Attributes["server"]?.Value.ToBool() ?? true,
			Client = node.Attributes["client"]?.Value.ToBool() ?? true,
			CanInput = node.Attributes["input"]?.Value.ToBool() ?? true,
			CanOutput = node.Attributes["output"]?.Value.ToBool() ?? true,
		};
	}

	public static ushort GetSize(AttributeType attributeType, bool is64 = false)
	{
		return attributeType switch
		{
			AttributeType.TInt8 or
			AttributeType.TBool or
			AttributeType.TSeq or
			AttributeType.TProp_seq => 1,

			AttributeType.TInt16 or
			AttributeType.TSub or
			AttributeType.TDistance or
			AttributeType.TVelocity or
			AttributeType.TSeq16 or
			AttributeType.TProp_field => 2,


			AttributeType.TIColor => 3,


			AttributeType.TInt64 or
			AttributeType.TTime64 or
			AttributeType.TRef or
			AttributeType.TXUnknown1 or
			AttributeType.TXUnknown2 => 8,

			AttributeType.TTRef or
			AttributeType.TIcon or
			AttributeType.TVector32 or
			AttributeType.TBox => 12,

			AttributeType.TScript_obj => 20,
			AttributeType.TString => is64 ? (ushort)8 : (ushort)4,
			AttributeType.TNative => is64 ? (ushort)12 : (ushort)8,

			_ => 4,
		};
	}
	#endregion



	#region Static Methods
	public static string ToString(AttributeDefinition attribute, object value)
	{
		var text = value?.ToString();
		if (value is float f) text = f.ToString("0.0");
		if (value is Time64 { Ticks: 0 }) return null;

		// check default
		if (text == attribute.DefaultValue) return null;
		return text;
	}
	#endregion
}