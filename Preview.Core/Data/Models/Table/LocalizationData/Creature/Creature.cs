using System.Runtime.InteropServices;

using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models.Creature;

[StructLayout(LayoutKind.Sequential)]
public class Creature
{
    public short WorldId;

    public RaceSeq Race;

    public SexSeq Sex;

    public Job Job;

    [Repeat(92)]
    public sbyte[] Appearance { get; set; }



    public string Name;

    public short GeoZone;

    public int X;

    public int Y;

    public int Z;

    public sbyte Yaw;

    public sbyte Level;

    public int Exp;

    public sbyte MasteryLevel;

    public long MasteryExp;

    public long Hp;

    public sbyte GuardGauge;

    public int Money;

    public int MoneyDiff;
}


public sealed class Npc : Creature
{
    //farthest-target 最远目标
    //nearest-target 最近目标
    //min-hate-target 最小仇恨目标
    //top-hate-target 最大仇恨目标
    //last-target 最后目标
}

public sealed class Pc : Creature
{

}