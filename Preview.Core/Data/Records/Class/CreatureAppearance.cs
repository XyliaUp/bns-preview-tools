using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public sealed class CreatureAppearance : BaseRecord
{
	[Signal("body-mesh-name")]
	public string BodyMeshName;

	[Signal("voice-set-name")]
	public string VoiceSetName;


	[Signal("body-material-name")]
	public string BodyMaterialName;
}