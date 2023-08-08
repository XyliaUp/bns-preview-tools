using Xylia.Extension;
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record;

[AliasRecord]
public sealed class CreatureAppearance : BaseRecord
{
	public RaceSeq Race;

	public SexSeq Sex;

	public FPath FaceAnimSetName;

	public FPath AnimTreeName;

	public FPath FaceMeshName;

	public FPath BodyMeshName;

	public FPath VoiceSetName;

	public FPath NpcDialogueSet;

	public FPath AttachEffect;

	public FPath BodyMaterialName;

	public FPath FaceMaterialName;

	public FPath Npcattach1;

	public FPath Npcattach2;

	public FPath NpcattachMaterial1;

	public FPath NpcattachMaterial2;

	public bool EnablePhysbrst;

	public FPath UniqueSoundcue;

	public float UniqueSoundculldist;

	public float UniqueSoundfadetime;

	public float SoundAttenuationScale;

	public float SoundVolumeScale;

	[Repeat(92)]
	public sbyte[] Param8;

	public float DecalRadius;

	public bool PcCustomize;
}

public class Param8
{
	#region Constructor
	const int SIZE = 92;
	public sbyte[] Data;

	public Param8(sbyte[] data) => this.Data = data;
	public Param8(string data) => this.Data = data.ToBytes().Select(b => (sbyte)b).ToArray();
	#endregion


	public static bool operator ==(Param8 a, Param8 b)
	{
		if (SIZE != a.Data.Length || SIZE != b.Data.Length)
			throw new InvalidDataException();

		var flag = true;
		for (int i = 0; i < SIZE; i++)
		{
			if (a.Data[i] != b.Data[i])
			{
				flag = false;
				Trace.WriteLine($"param-{i + 1} ({a.Data[i]} <> {b.Data[i]})");
			}
		}

		return flag;
	}
	public static bool operator !=(Param8 a, Param8 b)
	{
		return !(a == b);
	}
	public override bool Equals(object other) => other is Param8 param8 && this == param8;
	public override int GetHashCode() => Data.GetHashCode();

	public static implicit operator Param8(sbyte[] data) => new(data);

	public static implicit operator Param8(string data) => new(data);
}