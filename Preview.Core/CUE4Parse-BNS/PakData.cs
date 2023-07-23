using System.Collections.Concurrent;

using CUE4Parse.Encryption.Aes;
using CUE4Parse.FileProvider;
using CUE4Parse.FileProvider.Vfs;
using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Objects.Core.Misc;
using CUE4Parse.UE4.Versions;

using Xylia.Preview.Properties;

namespace CUE4Parse.BNS;
public sealed class PakData : IDisposable
{
	#region FileProvider
	public PakData(string GameDirectory = null)
	{
		_gameDirectory = GameDirectory ?? CommonPath.GameFolder;

		// register game custom class
		if (!_register)
		{
			_register = true;
			ObjectTypeRegistry.RegisterEngine(typeof(PakData).Assembly);
		}
	}

	private static bool _register;
	private readonly string _gameDirectory;
	private const string _aesKey = "0xd2e5f7f94e625efe2726b5360c1039ce7cb9abb760a94f37bb15a6dc08741656";
	private const string gameName = "BNSR";
	private readonly ConcurrentDictionary<string, string> ObjectRef = new(StringComparer.OrdinalIgnoreCase);


	private DefaultFileProvider _provider;
	public DefaultFileProvider Provider
	{
		private set => _provider = value;
		get
		{
			if (_provider != null)
				return _provider;

			lock (_provider = new DefaultFileProvider(_gameDirectory, SearchOption.AllDirectories, true, new() { Game = EGame.GAME_BladeAndSoul }))
			{
				DateTime dt = DateTime.Now;

				_provider.Initialize();
				_provider.SubmitKey(new FGuid(), new FAesKey(_aesKey));

				//_provider.LoadLocalization(ELanguage.English); 

				Debug.WriteLine($"Initialize file provider, rt {(DateTime.Now - dt).Seconds}s");

				// init-load
				if (Settings.LoadMode.HasFlag(LoadMode.LoadOnInit))
				{
					LoadAssetRegistry();
				}
			}

			return _provider;
		}
	}

	public void LoadAssetRegistry()
	{
		if (!Provider.TryCreateReader("BNSR/AssetRegistry.bin", out var archive))
			throw new FileNotFoundException();


		ObjectRef.Clear();
		var dt = DateTime.Now;

		var AssetRegistry = new AssetRegistry.FAssetRegistryState(archive);
		foreach (var asset in AssetRegistry.PreallocatedAssetDataBuffers)
			ObjectRef[asset.ObjectPath2] = asset.ObjectPath.Text;

		string msg = $"Initialize asset registry, rt {(DateTime.Now - dt).Seconds}s";
		Console.WriteLine(msg);
		Debug.WriteLine(msg);
	}
	#endregion


	#region Functions
	public string FixPath(string path, bool mode = true)
	{
		if (path.Contains("/Content/", StringComparison.OrdinalIgnoreCase)) return path;
		if (path.StartsWith("Game", StringComparison.OrdinalIgnoreCase)) return string.Concat(Provider.InternalGameName, "/Content", path[4..]);
		if (path.StartsWith("/Game", StringComparison.OrdinalIgnoreCase)) return string.Concat(Provider.InternalGameName, "/Content", path[5..]);



		string Ue4Path;

		// common replace rule
		// actually associated through the AssetRegistry, but loading is time-consuming
		if (path.StartsWith("00008758")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Icon/" + path[9..];
		else if (path.StartsWith("00021326")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Icon2nd/" + path[9..];
		else if (path.StartsWith("00052219")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Icon3rd/" + path[9..];
		else if (path.StartsWith("00078990")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Icon4th/" + path[9..];
		else if (path.StartsWith("00008130")) Ue4Path = "BNSR/Content/Art/UI/GameUI_BNSR/Resource/GameUI_FontSet_R/" + path[9..];
		else if (path.StartsWith("00009076")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Window/" + path[9..];
		else if (path.StartsWith("00009499")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Map_Indicator/" + path[9..];
		else if (path.StartsWith("00010047")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_ImageSet_R/" + path[9..];
		else if (path.StartsWith("00015590")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Tag/" + path[9..];
		else if (path.StartsWith("00027918")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Portrait/" + path[9..];
		else if (path.StartsWith("00033689")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_KeyKap/" + path[9..];
		else if (path.StartsWith("00043230")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_SkillBookImage/" + path[9..];
		else if (path.StartsWith("00064443")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_FishIcon/" + path[9..];
		else if (path.StartsWith("00079972")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_CollectionCard2D/" + path[9..];
		else if (path.StartsWith("00079973")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_CollectionCard3D/" + path[9..];
		else if (path.StartsWith("00080271")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_CollectionCard3D2nd/" + path[9..];
		else if (path.StartsWith("00080646")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_CollectionCard3D3rd/" + path[9..];
		else if (path.StartsWith("MiniMap_", StringComparison.OrdinalIgnoreCase)) Ue4Path = "BNSR/Content/bns/Package/World/GameDesign/commonpackage/" + path;
		else if (!mode) return null;
		else
		{
			lock (ObjectRef) if (ObjectRef.IsEmpty) LoadAssetRegistry();

			return ObjectRef.TryGetValue(path, out path) ? FixPath(path) : null;
		}


		Ue4Path = FixPath(Ue4Path.Replace('.', '/'));
		return string.Concat(Ue4Path, ".", Ue4Path.Split('/')[^1]);
	}

	public T LoadObject<T>(string filePath) where T : UObject
	{
		if (string.IsNullOrWhiteSpace(filePath)) return null;

		var path = FixPath(filePath);
		if (!Provider.TryLoadObject(path, out var export))
			Debug.WriteLine($"get object failed: {path}");

		return export as T;
	}
	#endregion


	#region IDispose
	private bool disposedValue;

	private void Dispose(bool disposing)
	{
		if (!disposedValue)
		{
			if (disposing)
			{
				// 内存回收有问题，懒得提issue
				(_provider?.Files as FileProviderDictionary)?.Clear();
 
				_provider?.Dispose();
				_provider = null;

				ObjectRef?.Clear();
			}

			disposedValue = true;
			GC.Collect();
		}
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
	#endregion
}