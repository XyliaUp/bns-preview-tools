using System.Windows;

using FModel.Framework;
using FModel.Views.Snooper;

using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Xylia.Preview.UI.Views;
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
				new GameWindowSettings { UpdateFrequency = htz },
				new NativeWindowSettings
				{
					ClientSize = new OpenTK.Mathematics.Vector2i(
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

			return Application.Current.Dispatcher.Invoke(() =>
			{
				var scale = ImGuiController.GetDpiScale();
				var htz = Snooper.GetMaxRefreshFrequency();
				return _model = new ModelView(
					new GameWindowSettings { UpdateFrequency = htz },
					new NativeWindowSettings
					{
						ClientSize = new OpenTK.Mathematics.Vector2i(
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

						Title = "Model",
					});
			});
		}
	}
}