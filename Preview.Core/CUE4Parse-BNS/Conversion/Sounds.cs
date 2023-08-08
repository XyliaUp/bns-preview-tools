using CUE4Parse.BNS.Exports;
using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Exports.Sound;
using CUE4Parse.UE4.Objects.UObject;

using CUE4Parse_Conversion.Sounds;
using CUE4Parse_Conversion.Textures;

using Xylia.Extension;
using Xylia.Preview.Data.Helper;

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
		else if (Object.ExportType == "SoundCue")
		{
			var FirstNode = Object.GetOrDefault<ResolvedObject>("FirstNode").Load();
			var ChildNodes = FirstNode.GetOrDefault<ResolvedObject[]>("ChildNodes");
			foreach (var ChildNode in ChildNodes)
			{
				var obj = ChildNode.Load();
				return obj.GetOrDefault<FSoftObjectPath>("SoundWaveAssetPtr").Load().GetWave();
			}
		}
		else if (Object.ExportType == "ShowSoundKey") return Object.GetOrDefault<ResolvedObject>("SoundCue").Load().GetWave();
		else if (Object is UShowFaceFxKey ShowFaceFxKey) return FileCache.Provider.LoadObject(ShowFaceFxKey.FaceFXAnimSetName).GetWave(ReferenceIdx);
		else if (Object.ExportType == "ShowFaceFxUE4Key") return Object.GetOrDefault<FSoftObjectPath>("FaceFXAnimObj").Load().GetWave();
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