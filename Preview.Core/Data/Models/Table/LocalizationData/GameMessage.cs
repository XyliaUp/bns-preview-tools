namespace Xylia.Preview.Data.Models;
public sealed class GameMessage : ModelElement
{
	#region Events
	public event EventHandler<string> Headline2Handler;
	#endregion

	#region Methods
	public void Instant()
	{
		Headline2Handler?.Invoke(null, "message");
	}
	#endregion
}