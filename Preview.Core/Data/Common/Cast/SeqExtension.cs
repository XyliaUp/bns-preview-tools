﻿using Xylia.Extension;
using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Models;

using static Xylia.Preview.Data.Models.Item;

namespace Xylia.Preview.Data.Common.Cast;
public static partial class SeqExtension
{
	public static string GetText(this Enum value)
	{
		if (value is EquipType EquipType) return EquipType.GetName();
		else if (value is ConditionType ConditionType) return ConditionType.GetName();
		else if (value is DifficultyTypeSeq DifficultyType) return DifficultyType.GetDescription();
		else if (value is JobSeq JobSeq) return JobSeq.GetName();
		else if (value is RaceSeq RaceSeq) return Race.Get(RaceSeq).GetName();
		else if (value is SexSeq SexSeq) return ((SexSeq2)SexSeq).GetName();
		else if (value is SexSeq2 SexSeq2) return SexSeq2.GetName();
		else if (value is StanceSeq StanceSeq) return StanceSeq.GetDescription();

		return value.GetDescription();
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

	public static string GetName(this ConditionType Seq)
	{
		var Text = $"Name.item.equip-type.{Seq.GetName()}".GetText();
		return Text ?? Seq.ToString();
	}
}