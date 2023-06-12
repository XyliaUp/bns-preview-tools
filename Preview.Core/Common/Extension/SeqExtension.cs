using System;

using Xylia.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record;

namespace Xylia.Preview.Common.Extension;

public static partial class SeqExtension
{
	public static string GetName(this Enum value)
	{
		if (value is EquipType EquipType) return EquipType.GetName();
		else if (value is ConditionType ConditionType) return ConditionType.GetName();
		else if (value is DifficultyType DifficultyType) return DifficultyType.GetName();
		else if (value is JobSeq JobSeq) return JobSeq.GetName();
		else if (value is RaceSeq RaceSeq) return RaceSeq.GetName();
		else if (value is RaceSeq2 RaceSeq2) return RaceSeq2.GetName();
		else if (value is SexSeq2 SexSeq2) return SexSeq2.GetName();
		else if (value is StanceSeq StanceSeq) return StanceSeq.GetName();
		else return null;
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

		var Text = $"Name.item.equip-type.{Seq.GetSignal()}".GetText(true);
		return Text ?? Seq.ToString();
	}

	public static string GetName(this ConditionType Seq)
	{
		var Text = $"Name.item.equip-type.{Seq.GetSignal()}".GetText(true);
		return Text ?? Seq.ToString();
	}


	public static string GetName(this DifficultyType Seq) => Seq.GetDescription();

	public static string GetName(this JobSeq Seq) => Seq.GetDescription();


	public static string GetName(this RaceSeq Seq) => Seq switch
	{
		RaceSeq.건 => "Name.race.Kun".GetText(),
		RaceSeq.곤 => "Name.race.Gon".GetText(),
		RaceSeq.린 => "Name.race.Lyn".GetText(),
		RaceSeq.진 => "Name.race.Jin".GetText(),

		_ => Seq.ToString(),
	};

	public static string GetName(this RaceSeq2 Seq) => $"Name.race.{Seq.GetSignal()}".GetText(true) ?? Seq.ToString();

	public static string GetName(this SexSeq2 Seq) => $"Name.sex.{Seq.GetSignal()}".GetText(true) ?? Seq.ToString();

	public static string GetName(this StanceSeq Seq) => Seq.GetDescription();
}