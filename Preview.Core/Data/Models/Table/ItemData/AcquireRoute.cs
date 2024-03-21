using Xylia.Preview.Common.Extension;

namespace Xylia.Preview.Data.Models;
public sealed class AcquireRoute : ModelElement
{
	public Ref<Text>[] RouteText { get; set; }

	public Ref<ModelElement>[] RouteRef { get; set; }

	public override string ToString()
	{
		return string.Join("<br/>", RouteText.SelectNotNull(element => element.Instance));
	}
}