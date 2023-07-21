using System.Numerics;

using CUE4Parse_Conversion.Animations;

using FModel.Views.Snooper.Models;

using ImGuiNET;

using OpenTK.Windowing.Common;

namespace FModel.Views.Snooper;
public partial class ModelGui : SnimGui
{
	private Model model;
	private ModelView view;

	private bool _viewportFocus = true;

	public ModelGui(int width, int height) : base(width, height)
	{

	}



	public void Render(ModelView s)
	{
		this.view = s;

		Controller.SemiBold();
		DrawDockSpace(s.Size);


		model = s.Renderer.Options.Models.First().Value;

		Draw3DViewport(s);
		DrawNavbar();

		Controller.Render();
	}


	private void Draw3DViewport(ModelView s)
	{
		ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, Vector2.Zero);
		if (ImGui.Begin("3D Viewport", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoSavedSettings))
		{
			var size = new Vector2(s.Size.X, s.Size.Y - 20);
			ImGui.SetWindowPos(new Vector2(0, 20));
			ImGui.SetWindowSize(size);
			ImGui.Image(s.Framebuffer.GetPointer(), size, new Vector2(0, 1), new Vector2(1, 0), Vector4.One);


			if (ImGui.IsMouseDown(ImGuiMouseButton.Left))
			{
				s.CursorState = CursorState.Grabbed;
			}

			if (ImGui.IsMouseDragging(ImGuiMouseButton.Left) && _viewportFocus)
			{
				s.Renderer.CameraOp.Modify(ImGui.GetIO().MouseDelta);
			}

			if (ImGui.IsMouseReleased(ImGuiMouseButton.Left))
			{
				s.CursorState = CursorState.Normal;
			}




			var pos = new Vector2(0, 5);
			void SetAttribute(string Name, object Value = null)
			{
				ImGui.SetCursorPos(pos with { X = 7 });

				var TextColor = new Vector4(.2f, 1.0f, .2f, 1.00f);
				ImGui.TextColored(TextColor, string.Concat(Name, ":", Value));

				pos = ImGui.GetCursorPos();
			}

			SetAttribute("Package", model.Path);
			SetAttribute("Class", model.Type);
			SetAttribute("Object", model.Name);
			pos.Y += 10;

			SetAttribute("Skeleton", model.Skeleton.Name);
			SetAttribute("LOD", s.Renderer.Options.Models.Count);
			SetAttribute("UV Set", model.UvCount);
			SetAttribute("Colors", null);
			SetAttribute("Bones", model.Skeleton.BoneCount);


			if (view._showFps)
			{
				pos.Y += 10;
				ImGui.SetCursorPos(pos with { X = 7 });

				float framerate = ImGui.GetIO().Framerate;
				ImGui.Text($"FPS: {framerate:0}");
			}



			ImGui.End();
		}

		ImGui.PopStyleVar();
	}

	private void DrawNavbar()
	{
		if (!ImGui.BeginMainMenuBar()) return;

		if (ImGui.BeginMenu("Anim Sequence") && view.AnimSet != null)
		{
			_viewportFocus = false;

			foreach (var sequence in view.AnimSet.AnimSequenceMap)
			{
				if (ImGui.MenuItem(sequence.Key))
				{
					SwitchAnimate();
					async void SwitchAnimate()
					{
						var export = await sequence.Value.TryLoadAsync();
						if (export is not null) view.Renderer.Animate(export);
					}

					_viewportFocus = true;
				}
			}

			ImGui.EndMenu();
		}
		else _viewportFocus = true;


		if (ImGui.BeginMenu("Settings"))
		{
			var ShowFps = view._showFps;
			if (ImGui.MenuItem("Show FPS", "", ref ShowFps))
				view.ShowFps = ShowFps;


			ImGui.EndMenu();
		}


		ImGui.EndMainMenuBar();
	}
}