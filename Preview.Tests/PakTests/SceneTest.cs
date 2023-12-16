using System.Diagnostics;
using CUE4Parse.BNS;
using CUE4Parse.BNS.Assets.Exports;
using CUE4Parse.UE4.Assets.Exports;
using CUE4Parse.UE4.Objects.Engine;
using CUE4Parse.UE4.Objects.UObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.Tests.PakTests;

[TestClass]
public class SceneTest
{
	[TestMethod]
	public void Main()
	{
		using GameFileProvider Provider = new(new Common().GameFolder);
		var AssetPath = "BNSR/Content/Art/UI/GameUI/Scene/Game_Broadcasting/Game_BroadcastingScene.uasset";
		var Blueprint = Provider.LoadAllObjects(AssetPath).OfType<UWidgetBlueprintGeneratedClass>().First();

		var dump = new WidgetDump() { Output = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "scene", Path.GetFileNameWithoutExtension(AssetPath)) };
		dump.LoadBlueprint(Blueprint);
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


public class WidgetDump
{
	public string Output;


	public void LoadBlueprint(UWidgetBlueprintGeneratedClass blueprint, int level = 0)
	{
		var bAllowTemplate = blueprint.GetOrDefault<bool>("bAllowTemplate");
		var bValidTemplate = blueprint.GetOrDefault<bool>("bValidTemplate");
		var bClassRequiresNativeTick = blueprint.GetOrDefault<bool>("bClassRequiresNativeTick");
		var DefaultObject = blueprint.ClassDefaultObject;
		var TemplateAsset = blueprint.GetOrDefault<FSoftObjectPath>("TemplateAsset");
		var WidgetTree = blueprint.GetOrDefault<UWidgetTree>("WidgetTree");

		//WidgetTree = TemplateAsset.Load().GetOrDefault<UWidgetTree>("WidgetTree");  
		this.LoadWidget(WidgetTree.RootWidget.Load(), null, level);
	}

	public void LoadWidget(UObject obj, UBnsCustomBaseWidgetSlot widgetslot, int level)
	{
		WriteLine(level, $"{obj.ExportType}  {obj.Name}");

		if (obj.Template != null)
		{
			LoadBlueprint(obj.Template.Class.Load<UWidgetBlueprintGeneratedClass>(), level);
			return;
		}


		if (widgetslot != null)
		{
			WriteLine(level, JsonConvert.SerializeObject(widgetslot.LayoutData));
		}

		if (obj is UBnsCustomBaseWidget widget)
		{
			WriteLineIf(level, widget.MetaData);
			WriteLineIf(level, widget.StringProperty.LabelText?.Text);


			//Directory.CreateDirectory(Output);
			//widget.BaseImageProperty.Image?.Save(Output + $"/{obj.Name}.png");
			//widget.ExpansionComponentList?.ForEach(expansion =>
			//{
			//	expansion.Image?.Save(Output + $"/{obj.Name}.{expansion.ExpansionName}.png");
			//});
		}

		// children
		var Slots = obj.GetOrDefault<UBnsCustomBaseWidgetSlot[]>("Slots");
		Slots?.Where(slot => slot != null).ForEach(slot => LoadWidget(slot.Content.Load(), slot, level + 1));
	}


	private static void WriteLine(int level, string message)
	{
		Debug.WriteLine(new string('\t', level) + message);
	}

	private static void WriteLineIf(int level, string message)
	{
		if (message != null) WriteLine(level, message);
	}
}