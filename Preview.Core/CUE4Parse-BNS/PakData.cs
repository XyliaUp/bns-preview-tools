using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using CUE4Parse.Encryption.Aes;
using CUE4Parse.FileProvider;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Objects.Core.Misc;
using CUE4Parse.UE4.Versions;

using Xylia.Preview.Properties;

namespace CUE4Parse.BNS;

public sealed class PakData : IDisposable
{
	#region FileProvider
	private const string _aesKey = "0xd2e5f7f94e625efe2726b5360c1039ce7cb9abb760a94f37bb15a6dc08741656";


	public DefaultFileProvider _provider;

	private readonly ConcurrentDictionary<string, string> ObjectRef = new(StringComparer.OrdinalIgnoreCase);


	public void Initialize(string GameDirectory = null)
	{
		if (this._provider != null)
			return;

		lock (this._provider = new DefaultFileProvider(GameDirectory ?? CommonPath.GameFolder, SearchOption.AllDirectories, true, new VersionContainer()
		{
			Game = EGame.GAME_BladeAndSoul
		}))
		{
			DateTime dt = DateTime.Now;

			this._provider.Initialize();
			this._provider.SubmitKey(new FGuid(), new FAesKey(_aesKey));

			Debug.WriteLine($"Initialize file provider, rt {(DateTime.Now - dt).Seconds}s");
		}
	}

	public void LoadAssetRegistry()
	{
		DateTime dt = DateTime.Now;

		//Pak0-UFS_A-WindowsNoEditor
		if (_provider.TryCreateReader("BNSR/AssetRegistry.bin", out var archive))
		{
			var AssetRegistry = new CUE4Parse.BNS.AssetRegistry.FAssetRegistryState(archive);
			foreach (var asset in AssetRegistry.PreallocatedAssetDataBuffers)
				ObjectRef[asset.ObjectPath2] = asset.ObjectPath.Text;
		}

		Debug.WriteLine($"Initialize asset registry,rt {(DateTime.Now - dt).Seconds}s");
	}
	#endregion



	#region Functions
	public UObject GetObject(UObject obj) => obj is null ? null : GetObject(obj.GetPathName_Bns());

	public UObject GetObject(string filePath)
	{
		if (string.IsNullOrWhiteSpace(filePath)) return null;
		this.Initialize();

		#region get asset path
		string Ue4Path = null;

		if (filePath.StartsWith("BNSR/Content")) Ue4Path = filePath;
		else if (filePath.StartsWith("/Game")) Ue4Path = "BNSR/Content" + filePath[5..];
		else
		{
			//设定常用替换关系
			//实际通过资源注册表关联, 但载入资源注册表十分耗时
			if (filePath.StartsWith("00008758")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Icon/" + filePath[9..];
			else if (filePath.StartsWith("00021326")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Icon2nd/" + filePath[9..];
			else if (filePath.StartsWith("00052219")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Icon3rd/" + filePath[9..];
			else if (filePath.StartsWith("00078990")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Icon4th/" + filePath[9..];
			else if (filePath.StartsWith("00008130")) Ue4Path = "BNSR/Content/Art/UI/GameUI_BNSR/Resource/GameUI_FontSet_R/" + filePath[9..];
			else if (filePath.StartsWith("00009076")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Window/" + filePath[9..];
			else if (filePath.StartsWith("00009499")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Map_Indicator/" + filePath[9..];
			else if (filePath.StartsWith("00010047")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_ImageSet/" + filePath[9..];
			else if (filePath.StartsWith("00015590")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Tag/" + filePath[9..];
			else if (filePath.StartsWith("00027918")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_Portrait/" + filePath[9..];
			else if (filePath.StartsWith("00033689")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_KeyKap/" + filePath[9..];
			else if (filePath.StartsWith("00043230")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_SkillBookImage/" + filePath[9..];
			else if (filePath.StartsWith("00064443")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_FishIcon/" + filePath[9..];
			else if (filePath.StartsWith("00079972")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_CollectionCard2D/" + filePath[9..];
			else if (filePath.StartsWith("00079973")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_CollectionCard3D/" + filePath[9..];
			else if (filePath.StartsWith("00080271")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_CollectionCard3D2nd/" + filePath[9..];
			else if (filePath.StartsWith("00080646")) Ue4Path = "BNSR/Content/Art/UI/GameUI/Resource/GameUI_CollectionCard3D3rd/" + filePath[9..];
			else if (filePath.StartsWith("MiniMap_", StringComparison.OrdinalIgnoreCase)) Ue4Path = "BNSR/Content/bns/Package/World/GameDesign/commonpackage/" + filePath;
			else
			{
				//使用公共处理
				if (ObjectRef.IsEmpty && true) lock (this.ObjectRef) LoadAssetRegistry();
				if (this.ObjectRef.TryGetValue(filePath, out Ue4Path))
					return GetObject(Ue4Path);

				Debug.WriteLine("bad asset path: " + filePath);
				return null;
			}

			//对于旧版路径, 冒号代表文件夹
			Ue4Path = Ue4Path.Replace('.', '/');
		}
		#endregion

		#region get result
		string AssetPath = Ue4Path.Split('.')[0];
		var exports = GetAssetExports(AssetPath);
		if (exports != null && exports.Any())
		{
			if (!Ue4Path.Contains('.'))
				return exports.First();

			string ObjectName = Ue4Path.Split('.')[1];
			return exports.FirstOrDefault(o => o.Name.Equals(ObjectName, StringComparison.OrdinalIgnoreCase));
		}

		Debug.WriteLine($"get asset failed: {Ue4Path}");
		return null;
		#endregion
	}

	public IEnumerable<UObject> GetAssetExports(string AssetPath)
	{
		if (this._provider.TryFindGameFile(AssetPath, out var file))
		{
			var task = new Task<IEnumerable<UObject>>(() => _provider.LoadPackage(file).GetExports());
			task.Start();
			task.Wait();

			return task.Result;
		}

		return null;
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
				this._provider?.Dispose();
			}

			disposedValue = true;
		}
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
	#endregion
}