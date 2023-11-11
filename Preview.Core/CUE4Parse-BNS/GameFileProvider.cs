using System.Collections.Concurrent;
using System.Diagnostics;

using CUE4Parse.Encryption.Aes;
using CUE4Parse.FileProvider;
using CUE4Parse.FileProvider.Objects;
using CUE4Parse.FileProvider.Vfs;
using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Objects.Core.Misc;
using CUE4Parse.UE4.Versions;
using CUE4Parse.Utils;

namespace CUE4Parse.BNS;
public sealed class GameFileProvider : DefaultFileProvider, IDisposable
{
	private static bool _register;
	internal const string _aesKey = "0xd2e5f7f94e625efe2726b5360c1039ce7cb9abb760a94f37bb15a6dc08741656";
	private const string gameName = "BNSR";
	private readonly ConcurrentDictionary<string, string> ObjectRef = new(StringComparer.OrdinalIgnoreCase);

	public GameFileProvider(string GameDirectory, bool LoadOnInit = false) : base(
		GameDirectory, SearchOption.AllDirectories, true,
		new() { Game = EGame.GAME_BladeAndSoul })
	{
		this.Initialize();
		this.SubmitKey(new FGuid(), new FAesKey(_aesKey));
		this.LoadLocalization(ELanguage.Chinese);

		// register game custom class
		if (!_register)
		{
			_register = true;
			ObjectTypeRegistry.RegisterEngine(typeof(GameFileProvider).Assembly);
		}

		// load asset registry
		if (LoadOnInit) LoadAssetRegistry();
	}

	#region FixPath
	public string FixPath(string path, bool useRegistry)
	{
		if (string.IsNullOrEmpty(path)) return null;
		if (!path.Contains('/') && !path.Contains('\\'))
		{
			// common replace rule
			// actually associated through the AssetRegistry, but loading is time-consuming
			string Ue4Path;
			if (path.StartsWith("00008758")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Icon/" + path[9..];
			else if (path.StartsWith("00021326")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Icon2nd/" + path[9..];
			else if (path.StartsWith("00052219")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Icon3rd/" + path[9..];
			else if (path.StartsWith("00078990")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Icon4th/" + path[9..];
			else if (path.StartsWith("00008130")) Ue4Path = "BNSR/Content/Art/UI/GameUI_BNSR/Resource/GameUI_FontSet_R/" + path[9..];
			else if (path.StartsWith("00009076")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Window/" + path[9..];
			else if (path.StartsWith("00009499")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Map_Indicator/" + path[9..];
			else if (path.StartsWith("00010047")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_ImageSet_R/" + path[9..];
			else if (path.StartsWith("00015590")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Tag/" + path[9..];
			else if (path.StartsWith("00027869")) Ue4Path = "BNSR/Content/Art/FX/01_Source/05_SF/FXUI_03/" + path[9..];
			else if (path.StartsWith("00027918")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Portrait/" + path[9..];
			else if (path.StartsWith("00033689")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_KeyKap/" + path[9..];
			else if (path.StartsWith("00043230")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_SkillBookImage/" + path[9..];
			else if (path.StartsWith("00064443")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_FishIcon/" + path[9..];
			else if (path.StartsWith("00079972")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_CollectionCard2D/" + path[9..];
			else if (path.StartsWith("00079973")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_CollectionCard3D/" + path[9..];
			else if (path.StartsWith("00080271")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_CollectionCard3D2nd/" + path[9..];
			else if (path.StartsWith("00080646")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_CollectionCard3D3rd/" + path[9..];
			else if (path.StartsWith("MiniMap_", StringComparison.OrdinalIgnoreCase)) Ue4Path = "BNSR/Content/bns/Package/World/GameDesign/commonpackage/" + path;
			else if (!useRegistry) return null;
			else
			{
				lock (ObjectRef) if (ObjectRef.IsEmpty) LoadAssetRegistry();

				return ObjectRef.TryGetValue(path, out path) ? FixPath(path, true) : null;
			}

			Ue4Path = FixPath(Ue4Path.Replace('.', '/'), true);
			return string.Concat(Ue4Path, ".", Ue4Path.Split('/')[^1]);
		}

		return FixPath(path).SubstringBeforeLast(".uasset");
	}

	public override string FixPath(string path, StringComparison comparisonType)
	{
		path = path.Replace('\\', '/');
		if (path[0] == '/')
			path = path[1..];
		var lastPart = path.SubstringAfterLast('/');
		// This part is only for FSoftObjectPaths and not really needed anymore internally, but it's still in here for user input
		if (lastPart.Contains('.') && lastPart.SubstringBefore('.') == lastPart.SubstringAfter('.'))
			path = string.Concat(path.SubstringBeforeLast('/'), "/", lastPart.SubstringBefore('.'));
		if (path[^1] != '/' && !lastPart.Contains('.'))
			path += "." + GameFile.Ue4PackageExtensions[0];

		var ret = path;
		var root = path.SubstringBefore('/');
		var tree = path.SubstringAfter('/');
		if (root.Equals("Game", comparisonType) || root.Equals("Engine", comparisonType))
		{
			var gameName = root.Equals("Engine", comparisonType) ? "Engine" : InternalGameName;
			var root2 = tree.SubstringBefore('/');
			if (root2.Equals("Config", comparisonType) ||
				root2.Equals("Content", comparisonType) ||
				root2.Equals("Plugins", comparisonType))
			{
				ret = string.Concat(gameName, '/', tree);
			}
			else
			{
				ret = string.Concat(gameName, "/Content/", tree);
			}
		}

		return comparisonType == StringComparison.OrdinalIgnoreCase ? ret.ToLowerInvariant() : ret;
	}
	#endregion

	#region Load Methods
	public override UObject LoadObject(string objectPath) => Task.Run(() => LoadObjectAsync(objectPath)).Result;

	public override T LoadObject<T>(string objectPath) => Task.Run(() => LoadObjectAsync<T>(objectPath)).Result;

	public override async Task<UObject> LoadObjectAsync(string objectPath)
	{
		objectPath = FixPath(objectPath, true);
		if (objectPath is null) return null;

		try
		{
			return await base.LoadObjectAsync(objectPath);
		}
		catch
		{
			return null;
		}
	}

	public override async Task<T> LoadObjectAsync<T>(string objectPath)
	{
		return (await LoadObjectAsync(objectPath)) as T;
	}


	private void LoadAssetRegistry()
	{
		if (!TryCreateReader("BNSR/AssetRegistry.bin", out var archive))
			throw new FileNotFoundException();


		ObjectRef.Clear();
		var dt = DateTime.Now;

		var AssetRegistry = new AssetRegistry.FAssetRegistryState(archive);
		foreach (var asset in AssetRegistry.PreallocatedAssetDataBuffers)
		{
			ObjectRef[asset.ObjectPath2] = asset.ObjectPath.Text;
		}

		string msg = $"Initialize asset registry, rt {(DateTime.Now - dt).Seconds}s";
		Debug.WriteLine(msg);
	}
	#endregion


	void IDisposable.Dispose()
	{
		ObjectRef?.Clear();

		// 内存回收有问题，懒得提issue
		(Files as FileProviderDictionary)?.Clear();
		base.Dispose();
	}
}

[Flags]
public enum LoadMode
{
	Default = 0x0000,
	LoadOnInit = 0x0001,
}