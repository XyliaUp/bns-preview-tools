using System.ComponentModel;
using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Models.Sequence;
public enum StanceSeq
{
    [Name("stance-none")]
    StanceNone,

    [Name("npc기본자세")]
    Npc기본자세,

    [Name("천검세")]
    [Description("剑术")]
    천검세,

    [Name("검령세")]
    [Description("御剑")]
    검령세,

    [Name("단타")]
    [Description("体术")]
    단타,

    [Name("연계")]
    [Description("烈虎拳")]
    연계,

    [Name("체술")]
    [Description("冰虎拳")]
    체술,

    [Name("납도")]
    [Description("拔剑")]
    납도,

    [Name("경기공")]
    [Description("气功")]
    경기공,

    [Name("연기공")]
    [Description("柔气功")]
    연기공,

    [Name("강기공")]
    [Description("刚气功")]
    강기공,

    [Name("스나이퍼모드")]
    스나이퍼모드,

    [Name("건너모드")]
    건너모드,

    [Name("파괴")]
    [Description("毁灭")]
    파괴,

    [Name("보호")]
    [Description("怜悯")]
    보호,

    [Name("철벽")]
    [Description("金刚不坏")]
    철벽,

    [Name("소환")]
    [Description("追随")]
    소환,

    [Name("교란")]
    [Description("扰乱")]
    교란,

    [Name("은신")]
    [Description("隐身")]
    은신,

    [Name("암살")]
    암살,

    [Name("검술")]
    [Description("剑术")]
    검술,

    [Name("어검")]
    [Description("御剑")]
    어검,

    [Name("발도")]
    [Description("拔剑")]
    발도,

    [Name("npc자세1")]
    Npc자세1,

    [Name("npc자세2")]
    Npc자세2,

    [Name("npc자세3")]
    Npc자세3,

    [Name("소환수-follow")]
    [Description("追随")]
    소환수Follow,

    [Name("소환수-command-1")]
    [Description("战斗")]
    소환수Command1,

    [Name("소환수-flying")]
    소환수Flying,

    [Name("주술")]
    [Description("幻术")]
    주술,

    [Name("강림")]
    [Description("降临")]
    강림,

    [Name("유권")]
    [Description("拳击")]
    유권,

    [Name("기공")]
    [Description("气击")]
    기공,

    [Name("냉정")]
    [Description("冷静")]
    냉정,

    [Name("광기")]
    [Description("狂暴")]
    광기,

    [Name("장궁")]
    장궁,

    [Name("정령궁")]
    정령궁,

    [Name("찌르기")]
    [Description("毁灭")]
    찌르기,

    [Name("베기")]
    [Description("毁灭")]
    베기,

    [Name("청뢰")]
    [Description("毁灭")]
    청뢰,

    [Name("혈뢰")]
    [Description("毁灭")]
    혈뢰,

    [Name("어검세")]
    [Description("御剑")]
    어검세,

    [Name("정수")]
    정수,

    [Name("역수")]
    역수,

    [Name("한손역수")]
    한손역수,

    [Name("쌍월")]
    쌍월,

    [Name("뇌신")]
    뇌신,

    [Name("침식")]
    침식,

    [Name("npc주시자세1")]
    Npc주시자세1,

    [Name("npc주시자세2")]
    Npc주시자세2,

    [Name("npc주시자세3")]
    Npc주시자세3,


    //新增的, 未确认排序
    간이연주,
    우퍼,
    연주,

    권총모드,
    런처모드,
}