using CUE4Parse.BNS.Exports;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Assets.Exports.Material;

using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.UI.Views;

namespace Xylia.Preview.UI.FModel.Views;
public class ModelData
{
	public string DisplayName;
	public UObject Export;
	public UAnimSet AnimSet;

	private List<UMaterialInstance> Materials;
	public IEnumerable<string> Cols
	{
		set
		{
			Materials = new();
			foreach (var material in value.Split(','))
			{
				var export = FileCache.Provider.LoadObject(material);
				if (export is UMaterialInstance unrealMaterial)
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