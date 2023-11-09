using System.Diagnostics;

using Xylia.Extension;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;

namespace Xylia.Preview.Data.Models;
public sealed class CreatureAppearance : Record
{
	public string Alias;


	public RaceSeq Race;

	public SexSeq Sex;

	public ObjectPath FaceAnimSetName;

	public ObjectPath AnimTreeName;

	public ObjectPath FaceMeshName;

	public ObjectPath BodyMeshName;

	public ObjectPath VoiceSetName;

	public ObjectPath NpcDialogueSet;

	public ObjectPath AttachEffect;

	public ObjectPath BodyMaterialName;

	public ObjectPath FaceMaterialName;

	public ObjectPath Npcattach1;

	public ObjectPath Npcattach2;

	public ObjectPath NpcattachMaterial1;

	public ObjectPath NpcattachMaterial2;

	public bool EnablePhysbrst;

	public ObjectPath UniqueSoundcue;

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
	public static bool operator !=(Param8 a, Param8 b) => !(a == b);

	public override bool Equals(object other) => other is Param8 param8 && this == param8;
	public override int GetHashCode() => Data.GetHashCode();

	public static implicit operator Param8(sbyte[] data) => new(data);

	public static implicit operator Param8(string data) => new(data);
}