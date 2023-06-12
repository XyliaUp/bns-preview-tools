using System;

using BnsBinTool.Core.Definitions;

namespace Xylia.Preview.Data.Models.BinData.Analyse.Extension;

public static class TypeExtension
{
    /// <summary>
    /// 加载类型
    /// </summary>
    /// <param name="Info"></param>
    /// <returns></returns>
    public static AttributeType GetRecordType(this string Info) => Enum.TryParse("T" + Info?.Trim(), true, out AttributeType ParseVType) ? ParseVType : default;

    /// <summary>
    /// 获取类型对应的长度
    /// </summary>
    /// <param name="attributeType"></param>
    /// <param name="is64"></param>
    /// <returns></returns>
    public static int GetLength(this AttributeType attributeType, bool is64 = false)
    {
        switch (attributeType)
        {
            case AttributeType.TInt8:
            case AttributeType.TBool:
            case AttributeType.TSeq:
            case AttributeType.TProp_seq:
                return 1;

            case AttributeType.TInt16:
            case AttributeType.TSub:
            case AttributeType.TDistance:
            case AttributeType.TVelocity:
            case AttributeType.TSeq16:
            case AttributeType.TProp_field:
                return 2;

            case AttributeType.TIColor:
                return 3;

            case AttributeType.TInt64:
            case AttributeType.TTime64:
            case AttributeType.TRef:
            case AttributeType.TXUnknown1:
            case AttributeType.TXUnknown2:
                return 8;

            case AttributeType.TTRef:
            case AttributeType.TIcon:
            case AttributeType.TVector32:
            case AttributeType.TBox:
                return 12;

            case AttributeType.TScript_obj:
                return 20;


            case AttributeType.TString:
            case AttributeType.TNative:
                return is64 ? 8 : 4;

            default: return 4;
        }
    }
}