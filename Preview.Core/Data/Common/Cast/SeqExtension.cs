using Xylia.Extension;
using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Models;

using static Xylia.Preview.Data.Models.Item;

namespace Xylia.Preview.Data.Common.Cast;
public static partial class SeqExtension
{
	public static object CastSeq(this string value, string name)
	{
		if (!name.TryParseToEnum<SeqType>(out var SeqType)) return null;
		else if (SeqType == SeqType.KeyCap) return KeyCap.Cast(KeyCap.GetKeyCode(value));
		else if (SeqType == SeqType.KeyCommand) return KeyCommand.Cast(value.ToEnum<KeyCommandSeq>());

		throw new InvalidCastException($"Cast Failed: {name} > {value}");
	}


	public static string GetName(this EquipType Seq)
	{
		if (Seq == EquipType.None) return null;
		else if (Seq == EquipType.AppearanceNormalState) return "Name.EquipSlot.NormalStateAppearance".GetText();
		else if (Seq == EquipType.AppearanceIdleState) return "Name.EquipSlot.IdleStateAppearance".GetText();
		else if (Seq == EquipType.AppearanceChatting) return "Name.EquipSlot.ChattingSymbol".GetText();
		else if (Seq == EquipType.AppearancePortrait) return "Name.EquipSlot.PortraitAppearance".GetText();
		else if (Seq == EquipType.AppearanceHypermove) return "Name.EquipSlot.HypermoveAppearance".GetText();
		else if (Seq == EquipType.AppearanceNamePlate) return "Name.EquipSlot.NameplateAppearance".GetText();
		else if (Seq == EquipType.AppearanceSpeechBubble) return "Name.EquipSlot.SpeechBubble".GetText();

		var Text = $"Name.item.equip-type.{Seq.GetName()}".GetText();
		return Text ?? Seq.ToString();
	}

	public static string GetName(this ConditionType Seq) => $"Name.item.equip-type.{Seq.GetName()}".GetText() ?? Seq.ToString();

	public static string GetName(this SexSeq2 Seq) => $"Name.sex.{Seq.GetName()}".GetText() ?? Seq.ToString();
}