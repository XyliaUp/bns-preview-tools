namespace Xylia.Preview.Data.Models;
public sealed class AcquireRouteList : ModelElement
{
	public Ref<AcquireRoute>[] AcquireRoute { get; set; }

	public Ref<AcquireRoute>[] AcquireRouteFull { get; set; }


	public override string ToString()
	{
		return AcquireRoute.Select(element => element.Instance)
			.Where(element => element != null)
			.Aggregate("", (a, n) => a + n.ToString() + "\n")
			.TrimEnd('\n');
	}
}