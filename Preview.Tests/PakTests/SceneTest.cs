using System.Diagnostics;

using CUE4Parse.BNS;
using CUE4Parse.BNS.Objects.Script;
using CUE4Parse.UE4.Assets;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Objects.Engine;
using CUE4Parse.UE4.Objects.UObject;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Xylia.Configure;
using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.Tests.PakTests;

[TestClass]
public class SceneTest
{
	[TestMethod]
	public void Main()
	{
		using GameFileProvider Provider = new(Common.GameFolder);

		#region Blueprint
		var Blueprint = Provider.LoadObject<UWidgetBlueprintGeneratedClass>(@"BNSR/Content/Art/UI/GameUI/Scene/Game_Battle/Game_BattleScene/NovaSkillBar.NovaSkillBar_C");
		var WidgetTree = Blueprint.GetOrDefault<ResolvedObject>("WidgetTree").Load<UWidgetTree>();
		var bAllowTemplate = Blueprint.GetOrDefault<bool>("bAllowTemplate");
		var bValidTemplate = Blueprint.GetOrDefault<bool>("bValidTemplate");
		var bClassRequiresNativeTick = Blueprint.GetOrDefault<bool>("bClassRequiresNativeTick");
		var TemplateAsset = Blueprint.GetOrDefault<FSoftObjectPath>("TemplateAsset");
		var DefaultObject = Blueprint.ClassDefaultObject;
		#endregion


		TemplateAsset.Load().GetOrDefault<ResolvedObject>("WidgetTree").Load<UWidgetTree>().LoadWidget();
	}

	[TestMethod]
	public void GetScene()
	{
		var AssetPath = "BNSR/Content/Art/UI/GameUI/Scene/Game_ItemStore/Game_ItemStoreScene/ItemStore_PossessionPanel.uasset";

		GameFileProvider Provider = new(Common.GameFolder);
		foreach(var o in Provider.LoadAllObjects(AssetPath))
		{
			Debug.WriteLine(o.GetFullName());

			if (o is UBnsCustomWidget widget && widget.StringProperty != null)
				Debug.WriteLine(widget.StringProperty?.LabelText?.Text);
		}
		return;



		string Output = Path.Combine(PathDefine.Desktop, "scene", Path.GetFileNameWithoutExtension(AssetPath));
		Directory.CreateDirectory(Output);

		foreach (var obj in Provider.LoadAllObjects(AssetPath))
		{
			//if (obj.Name != "ItemMapPanel_NavigationList_Column_1_1_Route_11_MainRoute_19_Desc_Arrow") continue;
			if (obj is UBnsCustomWidget widget)
			{
				Debug.WriteLine(widget.GetFullName() + "	" + widget.MetaData);

				//widget.BaseImageProperty?.GetBitmap?.Save(Output + $"/{obj.Name}.png");
				//if (widget.ExpansionComponentList != null)
				//{
				//	//Debug.WriteLine(JsonConvert.SerializeObject(widget, Formatting.Indented));

				//	for (int i = 0; i < widget.ExpansionComponentList.Count; i++)
				//	{
				//		var expansion = widget.ExpansionComponentList[i];
				//		expansion.GetBitmap?.Save(Output + $"/{obj.Name}.{expansion.ExpansionName}.png");
				//	}
				//}
			}
		}
	}



	//[TestMethod]
	public void Create()
	{
		var VfsFolder = "BNSR/Content/Art/UI/GameUI/Scene/";
		var OutputDir = @"F:\Resources\文档\Programming\C#\Xylia\bns\bns-preview-tools\Preview.UI\Art\GameUI\Scene\新建文件夹";

		foreach (var _gamefile in FileCache.Provider.Files)
		{
			var package = _gamefile.Value.Path;
			if (package.Contains(".uasset") && package.StartsWith(VfsFolder, StringComparison.OrdinalIgnoreCase))
			{
				var temp = package.Replace(VfsFolder, null).Split('/');
				if (temp.Length == 2)
				{
					var current = OutputDir + "/" + temp[0];
					Directory.CreateDirectory(current);

					var name = temp[1].Replace(".uasset", null);
					if (name.Contains("_UIPanelList")) continue;
					else if (name.Contains("_Datatable"))
					{
						File.WriteAllText($"{current}/{name}", null);
					}
					else
					{
						File.WriteAllText($"{current}/{name}.xaml", $$"""
							<Window
							        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
							        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
							        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
							        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
							        xmlns:local="clr-namespace:Xylia.Preview.UI.Art.GameUI.Scene.{{temp[0]}}"
								                      x:Class="Xylia.Preview.UI.Art.GameUI.Scene.{{temp[0]}}.{{name}}"
							        mc:Ignorable="d"
							        Title="" Height="450" Width="800" >
							    <Grid>

								</Grid>
							</Window>
							""");
						File.WriteAllText($"{current}/{name}.xaml.cs", $$"""
							namespace Xylia.Preview.UI.Art.GameUI.Scene.{{temp[0]}};
							public partial class {{name}} : Window
							{
								public {{name}}()
								{
							        DataContext = new {{name}}ViewModel();
									InitializeComponent();
								}
							}
							""");

						File.WriteAllText($"{current}/{name}ViewModel.cs", $$"""
							using CommunityToolkit.Mvvm.ComponentModel;

							namespace Xylia.Preview.UI.Art.GameUI.Scene.{{temp[0]}};
							public partial class {{name}}ViewModel : ObservableObject
							{

							}
							""");
					}
				}
			}
		}
	}
}