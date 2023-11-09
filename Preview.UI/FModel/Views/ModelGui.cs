using System.Numerics;

using CUE4Parse.BNS.Conversion;

using FModel.Views.Snooper.Models;

using ImGuiNET;

using OpenTK.Windowing.Common;

using Xylia.Preview.UI.ViewModels;

namespace FModel.Views.Snooper;
public partial class ModelGui : SnimGui
{
	private ModelView view;
	private bool _viewportFocus = true;
	private DateTime lastTime = default;

	public ModelGui(int width, int height) : base(width, height)
	{

	}


	public override void Render(Snooper s)
	{
		this.view = s as ModelView;

		Controller.SemiBold();
		DrawDockSpace(s.Size);

		Draw3DViewport();
		DrawNavbar();

		Controller.Render();
	}

	private void Draw3DViewport()
	{
		ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, Vector2.Zero);
		if (ImGui.Begin("3D Viewport", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoSavedSettings))
		{
			var size = new Vector2(view.Size.X, view.Size.Y - 20);
			ImGui.SetWindowPos(new Vector2(0, 20));
			ImGui.SetWindowSize(size);
			ImGui.Image(view.Framebuffer.GetPointer(), size, new Vector2(0, 1), new Vector2(1, 0), Vector4.One);


			if (ImGui.IsMouseDown(ImGuiMouseButton.Left))
			{
				view.CursorState = CursorState.Grabbed;
			}

			if (ImGui.IsMouseDragging(ImGuiMouseButton.Left) && _viewportFocus)
			{
				view.Renderer.CameraOp.Modify(ImGui.GetIO().MouseDelta);
			}

			if (ImGui.IsMouseReleased(ImGuiMouseButton.Left))
			{
				view.CursorState = CursorState.Normal;
			}




			var pos = new Vector2(0, 5);
			void SetAttribute(string Name, object Value = null)
			{
				ImGui.SetCursorPos(pos with { X = 7 });

				var TextColor = new Vector4(.2f, 1.0f, .2f, 1.00f);
				ImGui.TextColored(TextColor, string.Concat(Name, ":", Value));

				pos = ImGui.GetCursorPos();
			}

			var model = view.Renderer.Options.Models.First().Value;
			SetAttribute("Package", model.Path);
			SetAttribute("Class", model.Type);
			SetAttribute("Object", model.Name);
			pos.Y += 10;

			SetAttribute("Skeleton", (model as SkeletalModel).Skeleton.Name);
			SetAttribute("LOD", view.Renderer.Options.Models.Count);
			SetAttribute("UV Set", model.UvCount);
			SetAttribute("Colors", null);
			SetAttribute("Bones", (model as SkeletalModel).Skeleton.BoneCount);

			if (true)
			{
				pos.Y += 10;
				ImGui.SetCursorPos(pos with { X = 7 });

				float framerate = ImGui.GetIO().Framerate;
				ImGui.Text($"FPS: {framerate:0}");
			}

			if (lastTime != default && (DateTime.Now - lastTime).TotalSeconds < 5)
			{
				pos.Y += 10;
				ImGui.SetCursorPos(pos with { X = 7 });
				ImGui.Text($"Extract files successful.");
			}


			ImGui.End();
		}

		ImGui.PopStyleVar();
	}

	private void DrawNavbar()
	{
		if (!ImGui.BeginMainMenuBar()) return;

		#region Model
		if (view.Models != null && ImGui.BeginMenu("Model"))
		{
			_viewportFocus = false;
			foreach (var model in view.Models)
			{
				if (ImGui.MenuItem(model.DisplayName))
				{
					view.Renderer.Options = new Options();   //clear model 
					view.TryLoadExport(default, model);

					view.Renderer.Options.SetupModelsAndLights();
					view.Transform();

					_viewportFocus = true;
				}
			}

			ImGui.EndMenu();
		}
		else _viewportFocus = true;
		#endregion

		#region Anim 
		if (view.SelectedData.AnimSet != null && ImGui.BeginMenu("Anim Sequence"))
		{
			_viewportFocus = false;
			foreach (var sequence in view.SelectedData.AnimSet.AnimSequenceMap)
			{
				if (ImGui.MenuItem(sequence.Key))
				{
					SwitchAnimate();
					async void SwitchAnimate()
					{
						var export = await sequence.Value.TryLoadAsync();
						if (export != null) view.Renderer.Animate(export);
					}

					_viewportFocus = true;
				}
			}

			ImGui.EndMenu();
		}
		else _viewportFocus = true;
		#endregion


		#region Settings 
		if (ImGui.BeginMenu("Settings"))
		{
			//if (ImGui.MenuItem("Show FPS", "", ref ShowFps))
			//	view.ShowFps = ShowFps;

			if (ImGui.MenuItem("Extract"))
			{
				lastTime = DateTime.Now;
				new Exporter(UserSettings.Default.OutputFolder_Resource).Run(view.SelectedData.Export);
			}


			ImGui.EndMenu();
		}
		#endregion

		ImGui.EndMainMenuBar();
	}
}