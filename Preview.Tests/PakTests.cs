using System.Drawing;
using System.IO;

using CUE4Parse.BNS;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Objects;
using CUE4Parse.UE4.Objects.Core.Math;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Xylia.Configure;

namespace Xylia.Preview.Tests
{
	[TestClass]
	public class PakTests
	{
		//[TestMethod]
		public void GetScene()
		{
			string OutDir = Path.Combine(PathDefine.Desktop, "scene");

			var AssetPath = "BNSR/Content/Art/UI/GameUI/Scene/Game_ToolTip/Game_ToolTipScene.uasset";
			async void Output(string Name, FStructFallback ImageProperty)
			{
				if (ImageProperty is null) return;

				var BaseImageTexture = ImageProperty.GetOrDefault<UObject>("BaseImageTexture");
				var ImageUV = ImageProperty.GetOrDefault<FVector2D>("ImageUV");
				var ImageUVSize = ImageProperty.GetOrDefault<FVector2D>("ImageUVSize");

				string DirPath = Path.Combine(OutDir, Path.GetFileNameWithoutExtension(AssetPath));
				Directory.CreateDirectory(DirPath);

				var data = BaseImageTexture.GetUObject()?.GetImage();
				data?.Clone(new Rectangle((int)ImageUV.X, (int)ImageUV.Y, (int)ImageUVSize.X, (int)ImageUVSize.Y), data.PixelFormat)
					.Save(DirPath + $"\\{Name}.png");
			}

			PakData PakData = new();
			PakData.Initialize();
			foreach (var o in PakData.GetAssetExports(AssetPath))
			{
				if (o.TryGetValue(out FStructFallback BaseImageProperty, "BaseImageProperty")) Output(o.Name, BaseImageProperty);

				else if (o.TryGetValue(out FStructFallback NormalImageProperty, "NormalImageProperty")) Output(o.Name, NormalImageProperty);

				else if (o.TryGetValue(out UScriptArray ExpansionComponentList, "ExpansionComponentList"))
				{
					foreach (var p in ExpansionComponentList.Properties)
					{
						if (p is StructProperty s && s.Value.StructType is FStructFallback fallback)
						{
							Output(o.Name, fallback.GetOrDefault<FStructFallback>("ImageProperty"));
						}
					}
				}
			}
		}

		//[TestMethod]
		public void AssetRegistry()
		{
			PakData pak = new();
			pak.Initialize();
			pak.LoadAssetRegistry();
		}
	}
}