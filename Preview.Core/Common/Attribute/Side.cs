namespace Xylia.Preview.Common.Attribute;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field)]
public class Side : System.Attribute
{
	public Side() => this.SideType = ReleaseSide.Client | ReleaseSide.Server;

	public Side(ReleaseSide sideType) => this.SideType = sideType;



	public ReleaseSide SideType;
}


[Flags]
public enum ReleaseSide
{
	Client,

	Server,
		Achievement = Server + 1,
		Rank        = Server + 2,
}