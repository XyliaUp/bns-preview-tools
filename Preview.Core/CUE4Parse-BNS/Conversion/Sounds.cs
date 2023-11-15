using CUE4Parse.BNS.Assets.Exports;
using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Exports.Sound;
using CUE4Parse.UE4.Objects.UObject;

using CUE4Parse_Conversion.Sounds;
using CUE4Parse_Conversion.Textures;

using Xylia.Preview.Data.Helpers;

namespace CUE4Parse.BNS.Conversion;
public static class Sounds
{
	public static byte[] GetWave(this UObject Object, int ReferenceIdx = 0)
	{
		if (Object is null) return null;
		else if (Object is USoundWave soundWave)
		{
			Object.Decode(true, out var audioFormat, out var data);
			return data;
		}
		else if (Object is USoundCue SoundCue)
		{
			var FirstNode = SoundCue.FirstNode?.Load();
			var ChildNodes = FirstNode.GetOrDefault<ResolvedObject[]>("ChildNodes");
			foreach (var ChildNode in ChildNodes)
			{
				var obj = ChildNode.Load();
				return obj.GetOrDefault<FSoftObjectPath>("SoundWaveAssetPtr").Load().GetWave();
			}
		}
		else if (Object is UShowSoundKey ShowSoundKey) return ShowSoundKey.SoundCue?.Load().GetWave();
		else if (Object is UShowFaceFxKey ShowFaceFxKey) return FileCache.Provider.LoadObject(ShowFaceFxKey.FaceFXAnimSetName).GetWave(ReferenceIdx);
		else if (Object is UShowFaceFxUE4Key ShowFaceFxUE4Key) return ShowFaceFxUE4Key.FaceFXAnimObj.Load().GetWave();
		else if (Object.ExportType == "FaceFXAnim") return Object.GetOrDefault<FSoftObjectPath>("SoundCue").Load().GetWave();
		else if (Object.ExportType == "LegacyFaceFXAnimSet") return Object.GetOrDefault<ResolvedObject[]>("ReferencedSoundCues")[ReferenceIdx].Load().GetWave();
		else if (Object is UShowObject ShowObject)
		{
			if (ShowObject.EventKeys is null) return null;
			foreach (var _eventKey in ShowObject.EventKeys)
			{
				var o = _eventKey.Load();
				if (o.ExportType == "ShowSoundKey") return o.GetWave();
				else if (o.ExportType == "ShowFaceFxUE4Key") return o.GetWave();
				else if (o.ExportType == "ShowFaceFxKey") return o.GetWave(ReferenceIdx);
			}
		}

		return null;
	}
}