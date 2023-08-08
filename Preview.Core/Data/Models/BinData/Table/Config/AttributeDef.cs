using System.Configuration;
using System.Xml;

using BnsBinTool.Core.DataStructs;
using BnsBinTool.Core.Definitions;

using Xylia.Extension;

namespace Xylia.Preview.Data.Models.BinData.Table.Config;
public sealed class AttributeDef : AttributeDefinition
{
	#region Fields
	public string ReferedTableName;
	#endregion

	#region Properties
	public bool CanOutput { get; set; } = true;

	public bool CanInput { get; set; } = true;

	public bool Server { get; set; } = true;

	public bool Client { get; set; } = true;

	public ErrorType ErrorType;
	#endregion

	#region Interface
	public AttributeDef Clone() => (AttributeDef)MemberwiseClone();
	#endregion


	public static AttributeDef LoadFrom(XmlElement node, ITableDefinition table, Func<SeqInfo> seqfun)
	{
		if ((node.Attributes["deprecated"]?.Value).ToBool())
			return null;

		var Name = node.Attributes["name"]?.Value?.Trim();
		ArgumentNullException.ThrowIfNull(Name);

		Enum.TryParse<ErrorType>(node.Attributes["error-type"]?.Value, true, out var ErrorType);

		#region Type & Ref
		var TypeName = node.Attributes["type"]?.Value;
		var Type = GetType(TypeName);

		var RefTable = node.Attributes["ref"]?.Value;
		if (RefTable != null) Type = AttributeType.TRef;

		if (Type == AttributeType.TIcon) RefTable = "IconTexture";

		if (Type == AttributeType.TNone)
		{
			if (TypeName.Equals("struct", StringComparison.OrdinalIgnoreCase)) return null;
			else if (TypeName.Equals("dictionary", StringComparison.OrdinalIgnoreCase)) return null;

			throw new ConfigurationErrorsException($"Failed to determine attribute type: {table.Name}: {Name} ({TypeName})");
		}
		#endregion

		#region Seq
		var seq = seqfun();
		seq?.Check(Type);

		var seqdef = seq?.ConvertTo();
		#endregion

		#region Default
		string DefaultValue = node.Attributes["default"]?.Value?.Trim();
		if (string.IsNullOrEmpty(DefaultValue)) DefaultValue = null;


		var attrDefDefaults = new AttributeDefaultValues();

		switch (Type)
		{
			case AttributeType.TRef:
			case AttributeType.TIcon:
			case AttributeType.TTRef:
			case AttributeType.TNative:
				attrDefDefaults.DString = DefaultValue ?? "";
				break;

			case AttributeType.TDistance:
			case AttributeType.TInt16:
			case AttributeType.TAngle:
			case AttributeType.TSub:
				attrDefDefaults.DString = DefaultValue ?? "0";
				attrDefDefaults.DShort = short.Parse(attrDefDefaults.DString);
				break;

			case AttributeType.TInt64:
			case AttributeType.TTime64:
				attrDefDefaults.DString = DefaultValue ?? "0";
				attrDefDefaults.DLong = long.Parse(attrDefDefaults.DString);
				break;

			case AttributeType.TInt32:
			case AttributeType.TMsec:
				attrDefDefaults.DString = DefaultValue ?? "0";
				attrDefDefaults.DInt = int.Parse(attrDefDefaults.DString);
				break;

			case AttributeType.TInt8:
				attrDefDefaults.DString = DefaultValue ?? "0";
				attrDefDefaults.DByte = sbyte.Parse(attrDefDefaults.DString);
				break;

			case AttributeType.TFloat32:
				attrDefDefaults.DString = DefaultValue ?? "0";
				attrDefDefaults.DFloat = float.Parse(attrDefDefaults.DString);
				break;

			case AttributeType.TBool:
				attrDefDefaults.DString = DefaultValue ?? "n";
				attrDefDefaults.DBool = DefaultValue.ToBool();
				break;

			case AttributeType.TString:
			case AttributeType.TXUnknown2:
				attrDefDefaults.DString = DefaultValue ?? "";
				break;

			case AttributeType.TSeq:
			case AttributeType.TSeq16:
			case AttributeType.TProp_seq:
			case AttributeType.TProp_field:
			{
				if (Name.Contains("forwarding-types"))
					seq.DefaultCell = seq[0];

				attrDefDefaults.DString = seq?.DefaultCell?.Name;
				attrDefDefaults.DSeq = attrDefDefaults.DString;
				break;
			}




			case AttributeType.TVector16:
				attrDefDefaults.DString = DefaultValue ?? "0,0,0";
				attrDefDefaults.DVector16 = Vector16.Parse(attrDefDefaults.DString);
				break;

			case AttributeType.TVector32:
				attrDefDefaults.DString = DefaultValue ?? "0,0,0";
				attrDefDefaults.DVector32 = Vector32.Parse(attrDefDefaults.DString);
				break;

			case AttributeType.TIColor:
				attrDefDefaults.DString = DefaultValue ?? new IColor().ToString();
				attrDefDefaults.DIColor = IColor.Parse(DefaultValue);
				break;

			case AttributeType.TVelocity:
				attrDefDefaults.DString = DefaultValue ?? "0";
				attrDefDefaults.DVelocity = ushort.Parse(attrDefDefaults.DString);
				break;

			case AttributeType.TScript_obj:
				attrDefDefaults.DString = DefaultValue;
				break;

			case AttributeType.TXUnknown1:
				attrDefDefaults.DString = DefaultValue;
				break;
		}
		#endregion


		return new AttributeDef
		{
			Name = Name,
			OriginalName = Name,

			IsKey = (node.Attributes["key"]?.Value).ToBool(),
			IsRequired = (node.Attributes["required"]?.Value).ToBool(),
			IsHidden = (node.Attributes["hidden"]?.Value).ToBool(),

			Type = Type,
			TypeName = TypeName,
			Offset = (ushort)(node.Attributes["offset"]?.Value).ToInt16(),
			Repeat = ushort.TryParse(node.Attributes["repeat"]?.Value, out var tmp) ? tmp : (ushort)1,
			ReferedTableName = RefTable,
			Sequence = seqdef?.Sequence ?? new List<string>(),
			SequenceDef = seqdef,


			ErrorType = ErrorType,
			Server = node.Attributes["server"]?.Value.ToBool() ?? true,
			Client = node.Attributes["client"]?.Value.ToBool() ?? true,
			CanInput = node.Attributes["input"]?.Value.ToBool() ?? true,
			CanOutput = node.Attributes["output"]?.Value.ToBool() ?? true,


			DefaultValue = attrDefDefaults.DString,
			AttributeDefaultValues = attrDefDefaults,
		};
	}


	public static AttributeType GetType(string Info) => Enum.TryParse("T" + Info?.Trim(), true, out AttributeType ParseVType) ? ParseVType : default;

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
}