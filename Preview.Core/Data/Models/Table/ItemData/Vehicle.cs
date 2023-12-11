using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public class Vehicle : ModelElement
{
	public Ref<VehicleAppearance> Appearance;

	public Ref<ContextScript> ContextScript;
}