namespace Xylia.Preview.Data.Models.Sequence;
public enum SkillResult
{
    None,
    Hit,
    Miss,
    Dodge,
    Parry,
    PerfectParry,
    Bounce,
    Counter,
    CriticalHit,
    HitCriticalHit,
    BackHitCriticalHit,
    NotHit,
    All,
    HitCriticalHitParry,
    ParryPerfectParry,
    FrontHitCriticalHit,
    ParryPerfectParryCounter,
    ParryPerfectParryDodge,
    ParryDodge,

	COUNT
}

public enum SkillEventType
{
    None,
    Attack,
    Attacked,
}