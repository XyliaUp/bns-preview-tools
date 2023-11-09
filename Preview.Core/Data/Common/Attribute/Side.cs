namespace Xylia.Preview.Data.Common.Attribute;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field)]
public class Side : System.Attribute
{
    public Side() => SideType = ReleaseSide.Client | ReleaseSide.Server;

    public Side(ReleaseSide sideType) => SideType = sideType;



    public ReleaseSide SideType;
}


[Flags]
public enum ReleaseSide
{
    Client,

    Server,
    Achievement = Server + 1,
    Rank = Server + 2,
}