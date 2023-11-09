using OpenTK.Windowing.Desktop;

using Xylia.Preview.UI.FModel.Views;

namespace FModel.Views.Snooper;

public class ModelView : Snooper
{
	public ModelView(GameWindowSettings gwSettings, NativeWindowSettings nwSettings) : base(gwSettings, nwSettings)
	{
		_gui = new ModelGui(ClientSize.X, ClientSize.Y);
	}



	public List<ModelData> Models;
	public ModelData SelectedData;

	public bool TryLoadExport(CancellationToken cancellationToken, ModelData Model = null)
	{
		SelectedData = Model ?? Models.First();
		return TryLoadExport(cancellationToken, SelectedData.Export);
	}

	public void Transform()
	{
		Renderer.Options.TryGetModel(out var model);
		model.Transforms.First().Rotation.Z = 1F;
	}
}