﻿using CUE4Parse.BNS.Assets.Exports;
using CUE4Parse.UE4.Objects.UObject;
using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.Abstractions;
using Xylia.Preview.Data.Models.QuestData;
using Xylia.Preview.Data.Models.QuestData.Enums;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public sealed class Quest : ModelElement, IHaveName
{
	#region Fields
	[Side(ReleaseSide.Server)]
	public BroadcastCategory BroadcastCategory { get; set; }

	[Side(ReleaseSide.Server), Repeat(3)]
	public Ref<Achievement>[] ExtraQuestCompleteAchievement { get; set; }

	[Side(ReleaseSide.Server)]
	public Ref<Cinematic> ReplayEpicZoneLeaveCinematic { get; set; }

	public LazyList<Acquisition> Acquisition { get; set; }

	public LazyList<MissionStep> MissionStep { get; set; }

	public LazyList<Completion> Completion { get; set; }

	public LazyList<Transit> Transit { get; set; }
	#endregion


	#region Properties
	public string Text => this.Attributes["name2"]?.GetText();

	public string Title => this.Attributes["group2"]?.GetText();

	public ImageProperty FrontIcon
	{
		get
		{
			bool IsRepeat = Attributes["reset-type"].ToEnum<ResetType>() != ResetType.None;
			var Category = Attributes["category"].ToEnum<Category>();
			var ContentType = Attributes["content-type"].ToEnum<ContentType>();

			string res;
			switch (Category)
			{
				case Category.Epic: res = "Map_Epic_Start"; break;
				case Category.Job: res = "Map_Job_Start"; break;
				case Category.Dungeon: return null;
				case Category.Attraction: res = "Map_attraction_start"; break;
				case Category.TendencySimple: res = "Map_System_start"; break;
				case Category.TendencyTendency: res = "Map_System_start"; break;
				case Category.Mentoring: res = "mento_mentoring_start"; break;
				case Category.Hunting: res = IsRepeat ? "Map_Hunting_repeat_start" : "Map_Hunting_start"; break;
				case Category.Normal:
				{
					//faction quest
					if (Attributes["main-faction"] != null)
					{
						res = IsRepeat ? "Map_Faction_repeat_start" : "Map_Faction_start";
					}
					else
					{
						res = ContentType switch
						{
							ContentType.Festival => IsRepeat ? "Map_Festival_repeat_start" : "Map_Festival_start",
							ContentType.Duel or ContentType.PartyBattle => IsRepeat ? "Map_Faction_repeat_start" : "Map_Faction_start",
							ContentType.SideEpisode => "Map_side_episode_start",
							ContentType.Special => "Map_Job_Start",

							_ => IsRepeat ? "Map_Repeat_start" : "Map_Normal_Start",
						};
					}
				}
				break;

				default: throw new NotImplementedException();
			}

			return new ImageProperty()
			{
				BaseImageTexture = new MyFPackageIndex($"BNSR/Content/Art/UI/GameUI/Resource/GameUI_Map_Indicator/{res}"),
			};
		}
	}
	#endregion
}