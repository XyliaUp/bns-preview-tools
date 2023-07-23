using CUE4Parse.BNS.Exports;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Exports.Material;

using Xylia.Preview.Data.Helper;
using Xylia.Preview.UI.Custom;

namespace Xylia.Preview.UI.FModel.Views;
public class ModelData
{
	public string Name;
	public UObject Export;
	public UAnimSet AnimSet;

	private List<UMaterialInstance> Materials;
	public IEnumerable<string> cols
	{
		set
		{
			Materials = new();
			foreach (var material in value
				 .Where(o => o != null).SelectMany(o => o.Split(','))
				 .Select(FileCache.PakData.LoadObject<UObject>))
			{
				if (material is UMaterialInstance unrealMaterial)
					Materials.Add(unrealMaterial);
			}
		}
	}


	public void Run()
	{
		var view = MyTest.ModelViewer;
		if (!view.TryLoadExport(default, Export)) return;

		Materials?.ForEach(view.Renderer.Swap);

		view.SelectedData = this;
		view.Run();
	}
}