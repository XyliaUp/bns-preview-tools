using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Models;
public class Vehicle : Record
{
	public Ref<VehicleAppearance> Appearance;

	public Ref<ContextScript> ContextScript;
}