namespace Xylia.Preview.Common.Tag;
public abstract class ITag
{
	#region Properties
	public PointF Start;

	public PointF Finish;



	public bool Check(Point Position)
	{
		if (Finish.X >= Start.X)
		{
			if (Position.X < Start.X || Position.X > Finish.X) return false;
			if (Position.Y < Start.Y || Position.Y > Finish.Y) return false;

			return true;
		}
		else
		{
			if (Position.Y < Start.Y || Position.Y > Finish.Y) return false;

			// TODO: X inline check

			return true;
		}
	}
	#endregion



	//List<ExecuteUnit> Units;



	#region Event
	public EventHandler ClickEvent;
	#endregion
}