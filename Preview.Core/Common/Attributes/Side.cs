namespace Xylia.Preview.Common.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Property)]
public class Side : Attribute
{
    public Side() => SideType = ReleaseSide.Client | ReleaseSide.Server;

    public Side(ReleaseSide sideType) => SideType = sideType;



    public ReleaseSide SideType;
}


[Flags]
public enum ReleaseSide
{
    None = -1,
    Client,

    Server,
    Achievement = Server + 1,
    Rank = Server + 2,
}