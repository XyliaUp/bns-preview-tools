using System.ComponentModel;
using System.Runtime.InteropServices;

using CUE4Parse.UE4.Assets.Exports;

using FModel.Views.Snooper.Buffers;

using HZH_Controls.Util;

using ImGuiNET;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

using SixLabors.ImageSharp.Advanced;

using Xylia.Configure;
using Xylia.Preview.Helper;
using Xylia.Preview.UI.FModel.Views;

using Application = System.Windows.Application;
using Keys = OpenTK.Windowing.GraphicsLibraryFramework.Keys;

namespace FModel.Views.Snooper;
public class ModelView : GameWindow
{
	public Renderer Renderer;
	public readonly FramebufferObject Framebuffer;
	private readonly ModelGui _gui;
	private bool _init;

	public List<ModelData> Models;
	public ModelData SelectedData;


	public ModelView(GameWindowSettings gwSettings, NativeWindowSettings nwSettings) : base(gwSettings, nwSettings)
	{
		this.Title = "Model Viewer";

		Framebuffer = new FramebufferObject(ClientSize);
		Renderer = new Renderer(ClientSize.X, ClientSize.Y);

		_gui = new(ClientSize.X, ClientSize.Y);
		_init = false;


		_showFps = ShowFps;
	}

	public bool TryLoadExport(CancellationToken cancellationToken, UObject export)
	{
		Renderer.Load(cancellationToken, export);
		return Renderer.Options.Models.Count > 0;
	}

	public bool TryLoadExport(CancellationToken cancellationToken, ModelData Model = null)
	{
		SelectedData = Model ?? Models.First();
		return TryLoadExport(cancellationToken, SelectedData.Export);
	}

	public unsafe void WindowShouldClose(bool value, bool clear)
	{
		if (clear)
		{
			Renderer.CameraOp.Speed = 0;
			Renderer.Save();
		}

		GLFW.SetWindowShouldClose(WindowPtr, value); // start / stop game loop
		IsVisible = !value;
	}

	public unsafe void WindowShouldFreeze(bool value)
	{
		GLFW.SetWindowShouldClose(WindowPtr, value); // start / stop game loop
		IsVisible = true;
	}

	public override void Run()
	{
		Renderer.Options.SwapMaterial(false);
		Renderer.Options.AnimateMesh(false);
		this.Transform();

		Register.Dispatcher.Invoke(() =>
		{
			WindowShouldClose(false, false);
			base.Run();
		});
	}

	private unsafe void LoadWindowIcon()
	{
		var info = Application.GetResourceStream(new Uri("/Preview.UI;component/Resources/engine.png", UriKind.Relative));
		using var img = SixLabors.ImageSharp.Image.Load<Rgba32>(info.Stream);
		var memoryGroup = img.GetPixelMemoryGroup();
		Memory<byte> array = new byte[memoryGroup.TotalLength * sizeof(Rgba32)];
		var block = MemoryMarshal.Cast<byte, Rgba32>(array.Span);
		foreach (var memory in memoryGroup)
		{
			memory.Span.CopyTo(block);
			block = block[memory.Length..];
		}

		Icon = new WindowIcon(new OpenTK.Windowing.Common.Input.Image(img.Width, img.Height, array.ToArray()));
	}

	protected override void OnLoad()
	{
		if (_init)
		{
			Renderer.Options.SetupModelsAndLights();
			return;
		}

		base.OnLoad();
		CenterWindow();
		LoadWindowIcon();

		GL.ClearColor(OpenTK.Mathematics.Color4.Black);
		GL.Enable(EnableCap.Blend);
		GL.Enable(EnableCap.CullFace);
		GL.Enable(EnableCap.DepthTest);
		GL.Enable(EnableCap.Multisample);
		GL.StencilOp(StencilOp.Keep, StencilOp.Replace, StencilOp.Replace);
		GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

		Framebuffer.Setup();
		Renderer.Setup();
		_init = true;
	}

	protected override void OnRenderFrame(FrameEventArgs args)
	{
		base.OnRenderFrame(args);
		if (!IsVisible)
			return;

		var delta = (float)args.Time;

		ClearWhatHasBeenDrawn(); // clear window background


		_gui.Controller.Update(this, delta);
		_gui.Render(this);

		Framebuffer.Bind(); // switch to viewport background
		ClearWhatHasBeenDrawn(); // clear viewport background

		try
		{
			Renderer.Render(delta);
		}
		catch
		{

		}

		Framebuffer.BindMsaa();
		Framebuffer.Bind(0); // switch to window background

		SwapBuffers();
	}

	private void ClearWhatHasBeenDrawn()
	{
		GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
		GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
	}

	protected override void OnUpdateFrame(FrameEventArgs e)
	{
		base.OnUpdateFrame(e);
		if (!IsVisible || ImGui.GetIO().WantTextInput)
			return;

		var delta = (float)e.Time;
		Renderer.CameraOp.Modify(KeyboardState, delta);

		if (KeyboardState.IsKeyPressed(Keys.Z) &&
			Renderer.Options.TryGetModel(out var selectedModel) &&
			selectedModel.HasSkeleton)
		{
			Renderer.Options.RemoveAnimations();
			Renderer.Options.AnimateMesh(true);
			WindowShouldClose(true, false);
		}
		if (KeyboardState.IsKeyPressed(Keys.Space))
			Renderer.Options.Tracker.IsPaused = !Renderer.Options.Tracker.IsPaused;
		if (KeyboardState.IsKeyPressed(Keys.Delete))
			Renderer.Options.RemoveModel(Renderer.Options.SelectedModel);
		if (KeyboardState.IsKeyPressed(Keys.H))
			WindowShouldClose(true, false);
		if (KeyboardState.IsKeyPressed(Keys.Escape))
			WindowShouldClose(true, true);
	}

	protected override void OnResize(ResizeEventArgs e)
	{
		base.OnResize(e);

		GL.Viewport(0, 0, e.Width, e.Height);

		Framebuffer.WindowResized(e.Width, e.Height);
		Renderer.WindowResized(e.Width, e.Height);

		_gui.Controller.WindowResized(e.Width, e.Height);
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		base.OnClosing(e);
		WindowShouldClose(true, true);
	}




	public void Transform()
	{
		Renderer.Options.TryGetModel(out var model);
		model.Transforms.First().Rotation.Z = 1F;
	}


	public bool _showFps;
	public bool ShowFps
	{
		get => Ini.ReadValue("ModelViewer", "show-fps").ToBool();
		set => Ini.WriteValue("ModelViewer", "show-fps", _showFps = value);
	}
}