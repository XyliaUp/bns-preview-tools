using System.ComponentModel;

using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Common.Seq;
public enum StanceSeq
{
	[Signal("stance-none")]
	StanceNone,

	[Signal("npc기본자세")]
	Npc기본자세,

	[Signal("천검세")]
	[Description("剑术")]
	천검세,

	[Signal("검령세")]
	[Description("御剑")]
	검령세,

	[Signal("단타")]
	[Description("体术")]
	단타,

	[Signal("연계")]
	[Description("烈虎拳")]
	연계,

	[Signal("체술")]
	[Description("冰虎拳")]
	체술,

	[Signal("납도")]
	[Description("拔剑")]
	납도,

	[Signal("경기공")]
	[Description("气功")]
	경기공,

	[Signal("연기공")]
	[Description("柔气功")]
	연기공,

	[Signal("강기공")]
	[Description("刚气功")]
	강기공,

	[Signal("스나이퍼모드")]
	스나이퍼모드,

	[Signal("건너모드")]
	건너모드,

	[Signal("파괴")]
	[Description("毁灭")]
	파괴,

	[Signal("보호")]
	[Description("怜悯")]
	보호,

	[Signal("철벽")]
	[Description("金刚不坏")]
	철벽,

	[Signal("소환")]
	[Description("追随")]
	소환,

	[Signal("교란")]
	[Description("扰乱")]
	교란,

	[Signal("은신")]
	[Description("隐身")]
	은신,

	[Signal("암살")]
	암살,

	[Signal("검술")]
	[Description("剑术")]
	검술,

	[Signal("어검")]
	[Description("御剑")]
	어검,

	[Signal("발도")]
	[Description("拔剑")]
	발도,

	[Signal("npc자세1")]
	Npc자세1,

	[Signal("npc자세2")]
	Npc자세2,

	[Signal("npc자세3")]
	Npc자세3,

	[Signal("소환수-follow")]
	[Description("追随")]
	소환수Follow,

	[Signal("소환수-command-1")]
	[Description("战斗")]
	소환수Command1,

	[Signal("소환수-flying")]
	소환수Flying,

	[Signal("주술")]
	[Description("幻术")]
	주술,

	[Signal("강림")]
	[Description("降临")]
	강림,

	[Signal("유권")]
	[Description("拳击")]
	유권,

	[Signal("기공")]
	[Description("气击")]
	기공,

	[Signal("냉정")]
	[Description("冷静")]
	냉정,

	[Signal("광기")]
	[Description("狂暴")]
	광기,

	[Signal("장궁")]
	장궁,

	[Signal("정령궁")]
	정령궁,

	[Signal("찌르기")]
	[Description("毁灭")]
	찌르기,

	[Signal("베기")]
	[Description("毁灭")]
	베기,

	[Signal("청뢰")]
	[Description("毁灭")]
	청뢰,

	[Signal("혈뢰")]
	[Description("毁灭")]
	혈뢰,

	[Signal("어검세")]
	[Description("御剑")]
	어검세,

	[Signal("정수")]
	정수,

	[Signal("역수")]
	역수,

	[Signal("한손역수")]
	한손역수,

	[Signal("쌍월")]
	쌍월,

	[Signal("뇌신")]
	뇌신,

	[Signal("침식")]
	침식,

	[Signal("npc주시자세1")]
	Npc주시자세1,

	[Signal("npc주시자세2")]
	Npc주시자세2,

	[Signal("npc주시자세3")]
	Npc주시자세3,


	//新增的, 未确认排序
	간이연주,
	우퍼,
	연주,

	권총모드,
	런처모드,
}