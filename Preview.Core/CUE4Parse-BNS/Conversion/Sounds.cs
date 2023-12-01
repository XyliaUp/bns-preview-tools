using CUE4Parse.BNS.Assets.Exports;
using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Exports.Sound;
using CUE4Parse.UE4.Assets.Exports.Sound.Node;
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
		else if (Object is USoundWave SoundWave)
		{
			Object.Decode(true, out var audioFormat, out var data);
			return data;
		}
		else if (Object is USoundCue SoundCue)
		{
			var FirstNode = SoundCue.FirstNode?.Load();
			return FirstNode.GetWave();
		}
		else if (Object is USoundNode SoundNode)
		{
			if (SoundNode is USoundNodeWavePlayer wavePlayer)
			{
				return wavePlayer.SoundWave.Load().GetWave();
			}
			else
			{
				var ChildNodes = SoundNode.ChildNodes;
				return ChildNodes.Select(x => x.Load().GetWave()).FirstOrDefault(x => x != null);
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
				if (o is UShowSoundKey) return o.GetWave();
				else if (o is UShowFaceFxKey) return o.GetWave(ReferenceIdx);
				else if (o is UShowFaceFxUE4Key) return o.GetWave();
			}
		}

		return null;
	}
}