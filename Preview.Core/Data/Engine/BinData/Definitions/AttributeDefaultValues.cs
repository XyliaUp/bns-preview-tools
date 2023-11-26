using Xylia.Extension;
using Xylia.Preview.Data.Common;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Engine.BinData.Definitions;
public class AttributeDefaultValues
{
    public sbyte DByte { get; set; }
    public short DShort { get; set; }
    public int DInt { get; set; }
    public long DLong { get; set; }
    public string DString { get; set; }
    public bool DBool { get; set; }
    public Ref DRef { get; set; }
    public IconRef DIconRef { get; set; }
    public TRef DTRef { get; set; }
    public Vector16 DVector16 { get; set; }
    public Vector32 DVector32 { get; set; }
    public float DFloat { get; set; }
    public string DSeq { get; set; }
    public ushort DVelocity { get; set; }
    public IColor DIColor { get; set; }
    public Native DNative { get; set; }
    public Box DBox { get; set; }

    public static AttributeDefaultValues FromAttribute(AttributeDefinition attrDef)
    {
        var attrDefDefaults = new AttributeDefaultValues();

        if (attrDef.Name.Contains("forwarding-types"))
        {
            attrDefDefaults.DSeq = attrDef.Sequence[0];
            return attrDefDefaults;
        }

        if (attrDef.DefaultValue == null)
            return attrDefDefaults;

        switch (attrDef.Type)
        {
            case AttributeType.TRef:
            case AttributeType.TIcon:
            case AttributeType.TTRef:
            case AttributeType.TNative:
                break;

            case AttributeType.TVector32:
                attrDefDefaults.DVector32 = Vector32.Parse(attrDef.DefaultValue);
                break;

            case AttributeType.TVelocity:
                attrDefDefaults.DVelocity = ushort.Parse(attrDef.DefaultValue);
                break;

            case AttributeType.TDistance:
            case AttributeType.TInt16:
            case AttributeType.TAngle:
            case AttributeType.TSub:
                if (attrDef.DefaultValue.Length > 0)
                    attrDefDefaults.DShort = short.Parse(attrDef.DefaultValue);
                break;

            case AttributeType.TInt64:
            case AttributeType.TTime64:
                if (attrDef.DefaultValue.Length > 0)
                    attrDefDefaults.DLong = long.Parse(attrDef.DefaultValue);
                break;

            case AttributeType.TInt32:
            case AttributeType.TMsec:
                attrDefDefaults.DInt = int.Parse(attrDef.DefaultValue);
                break;

            case AttributeType.TInt8:
                attrDefDefaults.DByte = sbyte.Parse(attrDef.DefaultValue);
                break;

            case AttributeType.TFloat32:
                attrDefDefaults.DFloat = float.Parse(attrDef.DefaultValue);
                break;

            case AttributeType.TString:
                attrDefDefaults.DString = attrDef.DefaultValue;
                break;

            case AttributeType.TBool:
                attrDefDefaults.DBool = attrDef.DefaultValue.ToBool();
                break;

            case AttributeType.TSeq:
            case AttributeType.TSeq16:
            case AttributeType.TProp_seq:
            case AttributeType.TProp_field:
                attrDefDefaults.DSeq = attrDef.DefaultValue;
                break;

            case AttributeType.TScript_obj:
                throw new Exception("Should never execute");
                return null;

            case AttributeType.TIColor:
                attrDefDefaults.DIColor = IColor.Parse(attrDef.DefaultValue);
                break;

            case AttributeType.TNone:
            case AttributeType.TXUnknown1:
            case AttributeType.TXUnknown2:
                break;

            case AttributeType.TVector16:
                attrDefDefaults.DVector16 = Vector16.Parse(attrDef.DefaultValue);
                break;

            default:
                throw new Exception($"Unhandled type: {attrDef.Type}");
                return null;
        }

        return attrDefDefaults;
    }

    public static void SetRecordDefaults(Record record, ITableDefinition tableDefinition)
    {
        foreach (var attrDef in tableDefinition.ExpandedAttributes)
        {
            switch (attrDef.Type)
            {
                case AttributeType.TRef:
                    SetDefaultRef(record, attrDef);
                    break;

                case AttributeType.TIcon:
                    SetDefaultIcon(record, attrDef);
                    break;

                case AttributeType.TTRef:
                    SetDefaultTRef(record, attrDef);
                    break;

                case AttributeType.TNative:
                    // Skipped
                    break;

                case AttributeType.TVector32:
                    SetDefaultVector32(record, attrDef);
                    break;

                case AttributeType.TVelocity:
                    SetDefaultVelocity(record, attrDef);
                    break;

                case AttributeType.TDistance:
                case AttributeType.TInt16:
                case AttributeType.TSub:
                case AttributeType.TAngle:
                    SetDefaultInt16(record, attrDef);
                    break;

                case AttributeType.TInt64:
                case AttributeType.TTime64:
                    SetDefaultInt64(record, attrDef);
                    break;

                case AttributeType.TInt32:
                case AttributeType.TMsec:
                    SetDefaultInt32(record, attrDef);
                    break;

                case AttributeType.TInt8:
                    SetDefaultInt8(record, attrDef);
                    break;

                case AttributeType.TFloat32:
                    SetDefaultFloat32(record, attrDef);
                    break;

                case AttributeType.TString:
                    // Skipped
                    break;

                case AttributeType.TBool:
                    SetDefaultBool(record, attrDef);
                    break;

                case AttributeType.TSeq:
                case AttributeType.TProp_seq:
                    {
                        var index = (sbyte)attrDef.Sequence.IndexOf(attrDef.AttributeDefaultValues.DSeq);

                        if (index < 0)
                            index = 0;

                        record.Set(attrDef.Offset, index);
                        break;
                    }

                case AttributeType.TSeq16:
                case AttributeType.TProp_field:
                    {
                        var index = (short)attrDef.Sequence.IndexOf(attrDef.AttributeDefaultValues.DSeq);

                        if (index < 0)
                            index = 0;

                        record.Set(attrDef.Offset, index);
                        break;
                    }

                case AttributeType.TScript_obj:
                    // Skipped
                    break;

                case AttributeType.TIColor:
                    SetDefaultIColor(record, attrDef);
                    break;

                case AttributeType.TBox:
                    SetDefaultBox(record, attrDef);
                    break;

                case AttributeType.TNone:
                case AttributeType.TXUnknown1:
                case AttributeType.TXUnknown2:
                    // Skipped
                    break;

                case AttributeType.TVector16:
                    SetDefaultVector16(record, attrDef);
                    break;

                default: throw new Exception($"Unhandled type: {attrDef.Type}");
            }
        }
    }

    private static void SetDefaultVector16(Record record, AttributeDefinition attrDef)
    {
        record.Set(attrDef.Offset, attrDef.AttributeDefaultValues.DVector16);
    }

    private static void SetDefaultBox(Record record, AttributeDefinition attrDef)
    {
        record.Set(attrDef.Offset, attrDef.AttributeDefaultValues.DBox);
    }

    private static void SetDefaultIColor(Record record, AttributeDefinition attrDef)
    {
        record.Set(attrDef.Offset, attrDef.AttributeDefaultValues.DIColor);
    }

    private static void SetDefaultBool(Record record, AttributeDefinition attrDef)
    {
        record.Data[attrDef.Offset] = attrDef.AttributeDefaultValues.DBool ? (byte)1 : (byte)0;
    }

    private static void SetDefaultFloat32(Record record, AttributeDefinition attrDef)
    {
        record.Set(attrDef.Offset, attrDef.AttributeDefaultValues.DFloat);
    }

    private static void SetDefaultInt64(Record record, AttributeDefinition attrDef)
    {
        record.Set(attrDef.Offset, attrDef.AttributeDefaultValues.DLong);
    }

    private static void SetDefaultInt32(Record record, AttributeDefinition attrDef)
    {
        record.Set(attrDef.Offset, attrDef.AttributeDefaultValues.DInt);
    }

    private static void SetDefaultInt16(Record record, AttributeDefinition attrDef)
    {
        record.Set(attrDef.Offset, attrDef.AttributeDefaultValues.DShort);
    }

    private static void SetDefaultInt8(Record record, AttributeDefinition attrDef)
    {
        record.Set(attrDef.Offset, attrDef.AttributeDefaultValues.DByte);
    }

    private static void SetDefaultRef(Record record, AttributeDefinition attrDef)
    {
        record.Set(attrDef.Offset, attrDef.AttributeDefaultValues.DRef);
    }

    private static void SetDefaultIcon(Record record, AttributeDefinition attrDef)
    {
        record.Set(attrDef.Offset, attrDef.AttributeDefaultValues.DIconRef);
    }

    private static void SetDefaultTRef(Record record, AttributeDefinition attrDef)
    {
        record.Set(attrDef.Offset, attrDef.AttributeDefaultValues.DTRef);
    }

    private static void SetDefaultVector32(Record record, AttributeDefinition attrDef)
    {
        record.Set(attrDef.Offset, attrDef.AttributeDefaultValues.DVector32);
    }

    private static void SetDefaultVelocity(Record record, AttributeDefinition attrDef)
    {
        record.Set(attrDef.Offset, attrDef.AttributeDefaultValues.DVelocity);
    }
}