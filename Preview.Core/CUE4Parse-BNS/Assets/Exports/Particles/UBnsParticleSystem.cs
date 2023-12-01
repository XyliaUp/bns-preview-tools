using System.Diagnostics;

using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Readers;
using CUE4Parse.UE4.Objects.Core.Math;

using Newtonsoft.Json;

namespace CUE4Parse.BNS.Assets.Exports;
public class UParticleSystem : USerializeObject
{
	public float UpdateTime_FPS;

	public float UpdateTime_Delta;

	/// <summary>
	/// WarmupTime - the time to warm-up the particle system when first rendered	
	/// Warning: WarmupTime is implemented by simulating the particle system for the time requested upon activation. 
	/// This is extremely prone to cause hitches, especially with large particle counts - use with caution.
	/// </summary>
	public float WarmupTime;

	public float WarmupTickRate;


	public UParticleEmitter[] Emitters;

	//public UParticleSystemComponent PreviewComponent;

	//public FRotator ThumbnailAngle;

	//public float ThumbnailDistance;

	//public float ThumbnailWarmup;

	//public UInterpCurveEdSetup CurveEdSetup;

	public float LODDistanceCheckTime;

	public float MacroUVRadius;

	public float[] LODDistances;

	//public int EditorLODSetting;

	//public FParticleSystemLOD[] LODSettings;

	public FBox FixedRelativeBoundingBox;

	public float SecondsBeforeInactive;

	//public FString FloorMesh;

	//public FVector FloorPosition;

	//public FRotator FloorRotation;

	//public float FloorScale;

	//public FVector FloorScale3D;

	//public FColor BackgroundColor;

	public float Delay;

	public float DelayLow;

	public bool bOrientZAxisTowardCamera;

	public bool bUseFixedRelativeBoundingBox;

	public bool bShouldResetPeakCounts;

	public bool bHasPhysics;

	public bool bUseRealtimeThumbnail;

	public bool ThumbnailImageOutOfDate;


	private bool bIsElligibleForAsyncTick;

	private bool bIsElligibleForAsyncTickComputed;

	//public UTexture2D ThumbnailImage;


	public bool bUseDelayRange;

	public bool bAllowManagedTicking;

	public bool bAutoDeactivate;

	public bool bRegenerateLODDuplicate;



	public override void Deserialize(FAssetArchive Ar, long validPos)
	{
		base.Deserialize(Ar, validPos);

		//Emitters = GetOrDefault<ResolvedObject[]>("Emitters");
		LODDistances = GetOrDefault<float[]>(nameof(LODDistances));
		Trace.WriteLine(JsonConvert.SerializeObject(Emitters, Formatting.Indented));
	}
}

public class UBnsParticleSystem : UParticleSystem
{
	
}