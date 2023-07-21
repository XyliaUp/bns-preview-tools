using System.Windows;

using CUE4Parse.BNS.Exports;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Exports.Material;

using FModel.Framework;
using FModel.Views.Snooper;

using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

using Xylia.Preview.Data.Helper;
using Xylia.Preview.Helper;

namespace Xylia.Preview.UI.Custom;
public class MyTest
{
	private static Snooper _snooper;
	public static Snooper SnooperViewer
	{
		get
		{
			if (_snooper != null) return _snooper;

			var scale = ImGuiController.GetDpiScale();
			var htz = Snooper.GetMaxRefreshFrequency();
			return _snooper = new Snooper(
				new GameWindowSettings { RenderFrequency = htz, UpdateFrequency = htz },
				new NativeWindowSettings
				{
					Size = new OpenTK.Mathematics.Vector2i(
						Convert.ToInt32(SystemParameters.MaximizedPrimaryScreenWidth * .75 * scale),
						Convert.ToInt32(SystemParameters.MaximizedPrimaryScreenHeight * .85 * scale)),
					NumberOfSamples = 4,
					WindowBorder = WindowBorder.Resizable,
					Flags = ContextFlags.ForwardCompatible,
					Profile = ContextProfile.Core,
					Vsync = VSyncMode.Adaptive,
					APIVersion = new Version(4, 6),
					StartVisible = false,
					StartFocused = false,
					Title = "3D Viewer"
				});
		}
	}



	private static ModelView _model;
	public static ModelView ModelViewer
	{
		get
		{
			if (_model != null) return _model;

			return Register.Dispatcher.Invoke(() =>
			{
				var scale = ImGuiController.GetDpiScale();
				var htz = Snooper.GetMaxRefreshFrequency();
				return _model = new ModelView(
					new GameWindowSettings { RenderFrequency = htz, UpdateFrequency = htz },
					new NativeWindowSettings
					{
						Size = new OpenTK.Mathematics.Vector2i(
							Convert.ToInt32(SystemParameters.MaximizedPrimaryScreenWidth * .45 * scale),
							Convert.ToInt32(SystemParameters.MaximizedPrimaryScreenHeight * .85 * scale)),
						NumberOfSamples = 4,
						WindowBorder = WindowBorder.Resizable,
						Flags = ContextFlags.ForwardCompatible,
						Profile = ContextProfile.Core,
						Vsync = VSyncMode.Adaptive,
						APIVersion = new Version(4, 6),
						StartVisible = false,
						StartFocused = false,
					});
			});
		}
	}



	public static bool TestModel(string mesh, params string[] cols)
	{
		var export = FileCache.PakData.LoadObject<UObject>(mesh);

		var snooper = MyTest.ModelViewer;
		if (!snooper.TryLoadExport(default, export)) return false;


		if (cols.Any())
		{
			foreach (var material in cols
				.Where(o => o != null).SelectMany(o => o.Split(','))
				.Select(FileCache.PakData.LoadObject<UObject>))
			{
				if (material is UMaterialInstance unrealMaterial)
				{
					snooper.Renderer.Swap(unrealMaterial);
				}
			}
		}


		return true;
	}

	public static void TestMesh(string mesh, string animset, string material = null)
	{
		var Mesh = FileCache.PakData.LoadObject<UObject>(mesh);
		var AnimSet = FileCache.PakData.LoadObject<UAnimSet>(animset);
		var Material = FileCache.PakData.LoadObject<UMaterialInstance>(material);

		var snooper = MyTest.ModelViewer;
		if (snooper.TryLoadExport(default, Mesh))
		{
			snooper.AnimSet = AnimSet;
			if (Material != null)
				snooper.Renderer.Swap(Material);

			snooper.Run();
		}
	}
}