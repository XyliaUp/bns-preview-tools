using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public class Vehicle : Record
{
	public string Alias;


	public Ref<VehicleAppearance> Appearance;

	public Ref<ContextScript> ContextScript;
}