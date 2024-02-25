namespace Xylia.Preview.Data.Models;
public sealed class AcquireRoute : ModelElement
{
	public Ref<Text>[] RouteText { get; set; }

	public Ref<ModelElement>[] RouteRef { get; set; }


	public override string ToString()
	{
		return RouteText.Select(element => element.Instance)
			.Where(element => element != null)
			.Aggregate("", (a, n) => a + n.GetText() + "\n")
			.TrimEnd('\n');
	}
}