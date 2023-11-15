using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Assets.Objects.Properties;
using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Objects.UObject;

namespace CUE4Parse.BNS.Assets.Exports;
public class UAnimSet : USerializeObject
{
	public bool bAnimRotationOnly;
	public bool bServiced;
	public Dictionary<string, FSoftObjectPath> AnimSequenceMap;  //Dictionary<FName, FSoftObjectPath>

	public FName PreviewSkelMeshName;

	public FName[] AdjustAniScaleBoneNames;
	public FName[] PhysicalHitBoneNames;
	public FName[] TrackBoneNames;

	public FName[] UseMovementBoneNames;
	public FName[] UseTranslationBoneNames;


	public ResolvedObject[] RefAnimSets;



	public override void Deserialize(FAssetArchive Ar, long validPos)
	{
		base.Deserialize(Ar, validPos);

		bAnimRotationOnly = GetOrDefault<bool>(nameof(bAnimRotationOnly));
		bServiced = GetOrDefault<bool>(nameof(bServiced));

		AdjustAniScaleBoneNames = GetOrDefault<FName[]>(nameof(AdjustAniScaleBoneNames));
		PhysicalHitBoneNames = GetOrDefault<FName[]>(nameof(PhysicalHitBoneNames));
		PreviewSkelMeshName = GetOrDefault<FName>(nameof(PreviewSkelMeshName));
		TrackBoneNames = GetOrDefault<FName[]>(nameof(TrackBoneNames));

		UseMovementBoneNames = GetOrDefault<FName[]>(nameof(UseMovementBoneNames));
		UseTranslationBoneNames = GetOrDefault<FName[]>(nameof(UseTranslationBoneNames));




		AnimSequenceMap = new();
		var temp = GetOrDefault<UScriptMap>(nameof(AnimSequenceMap));
		if (temp != null)
		{
			foreach (var map in temp.Properties)
			{
				var key = ((NameProperty)map.Key).Value.Text;
				var value = ((SoftObjectProperty)map.Value).Value;

				AnimSequenceMap[key] = value;
			}
		}


		RefAnimSets = GetOrDefault<ResolvedObject[]>(nameof(RefAnimSets));
		var temp2 = GetOrDefault<UScriptMap>("RefAnimSequenceMap");
		if (temp2 != null)
		{
			foreach (var map in temp2.Properties)
			{
				var key = ((NameProperty)map.Key).Value.Text;
				var value = ((SoftObjectProperty)map.Value).Value;

				AnimSequenceMap[key] = value;
			}
		}
	}
}