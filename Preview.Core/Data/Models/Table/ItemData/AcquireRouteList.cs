using Xylia.Preview.Common.Extension;

namespace Xylia.Preview.Data.Models;
public sealed class AcquireRouteList : ModelElement
{
	public Ref<AcquireRoute>[] AcquireRoute { get; set; }

	public Ref<AcquireRoute>[] AcquireRouteFull { get; set; }


	public override string ToString()
	{
		return string.Join("<br/>", AcquireRoute.SelectNotNull(x => x.Instance));
	}
}