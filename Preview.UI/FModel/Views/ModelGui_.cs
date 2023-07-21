using FModel.Views.Snooper;
namespace FModel.Views.Snooper;
public partial class ModelGui
{
	private void DrawDetails(ModelView s)
	{
		//if (ImGui.Begin("Details", ImGuiWindowFlags.NoSavedSettings | ImGuiWindowFlags.NoDocking))
		//{
		//	ImGui.SetWindowSize(new Vector2(300, 500));
		//	if (ImGui.BeginTabBar("tabbar_details", ImGuiTabBarFlags.None))
		//	{
		//		if (ImGui.BeginTabItem("Anim Sequence"))
		//		{
		//			ImGui.BeginTable("table_sections", 1, ImGuiTableFlags.Resizable | ImGuiTableFlags.NoSavedSettings, ImGui.GetContentRegionAvail());
		//			foreach (var sequence in s.AnimSet.AnimSequenceMap)
		//			{
		//				ImGui.TableNextRow();
		//				ImGui.TableNextColumn();
		//				if (ImGui.Selectable(sequence.Key, false, ImGuiSelectableFlags.SpanAllColumns))
		//				{
		//					var task = new Task<UObject>(sequence.Value.Load);
		//					task.Start();
		//					task.Wait();

		//					s.Renderer.Animate(task.Result);
		//				}
		//			}

		//			ImGui.EndTable();
		//			ImGui.EndTabItem();
		//		}
		//	}

		//	ImGui.End();
		//}
	}
}