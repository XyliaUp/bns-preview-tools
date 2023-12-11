using System.Diagnostics;

using CUE4Parse.BNS.Assets.Exports;
using CUE4Parse.UE4.Assets.Exports;

using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.FModel.Views;
using Xylia.Preview.UI.Views;

namespace Xylia.Preview.UI.Interactivity;
public class PreviewModel : RecordCommand
{
    public override bool CanExecute(object parameter)
    {
        if (parameter is not Record record) return false;

        return CanExecute(record.Owner.Name);
    }

    public override bool CanExecute(string name) => name switch
    {
        "npc" or
        "item" or
        "pet" or
        "vehicle-appearance" => true,
        _ => false,
    };

    public override async void Execute(Record record)
    {
        List<ModelData> models = new();
        await Load(record, models);

        // show
        var view = MyTest.ModelViewer;
        view.Models = [.. models];
        if (view.TryLoadExport(default)) view.Run();
        else Debug.WriteLine(record?.GetType());
    }

    private static async Task Load(Record record, List<ModelData> models)
    {
        if (record is null) return;

        // accordin table name
        switch (record.Owner.Name)
        {
            case "npc":
                {
                    var appearance = record.Attributes.Get<Record>("appearance");
                    if (appearance is null) return;

                    models.Add(new()
                    {
                        Export = await FileCache.Provider.LoadObjectAsync<UObject>(appearance.Attributes["body-mesh-name"]),
                        Cols = new string[] { appearance.Attributes["body-material-name"] },
                        AnimSet = await FileCache.Provider.LoadObjectAsync<UAnimSet>(record.Attributes["animset"]),
                    });
                }
                break;

            case "item":
                {
                    void LoadModel(string Mesh, string Col = null)
                    {
                        Col ??= Mesh + "-col";
                        models.Add(new ModelData()
                        {
                            DisplayName = Mesh,
                            Export = FileCache.Provider.LoadObject<UObject>(record.Attributes[Mesh]),
                            Cols = new string[] { record.Attributes[Col, 1], record.Attributes[Col, 2], record.Attributes[Col, 3] },
                        });
                    }

                    var MeshId = record.Attributes["mesh-id"];
                    if (!string.IsNullOrEmpty(MeshId))
                    {
                        //"mesh-id"
                        //"mesh-id-2"
                        //"mesh-col-1"
                        //"mesh-col-2"
                        //"mesh-col-3"
                        //"mesh-animset"
                        //"mesh-attach"
                        //"mesh-animtree"

                        LoadModel("mesh-id", "mesh-col");

                        models.Add(new ModelData()
                        {
                            Export = FileCache.Provider.LoadObject<UObject>(record.Attributes["talk-mesh"]),
                            AnimSet = FileCache.Provider.LoadObject<UAnimSet>(record.Attributes["talk-animset"]),
                        });
                    }
                    else
                    {
                        LoadModel("kun-mesh");
                        LoadModel("gon-male-mesh");
                        LoadModel("gon-female-mesh");
                        LoadModel("lyn-male-mesh");
                        LoadModel("lyn-female-mesh");
                        LoadModel("jin-male-mesh");
                        LoadModel("jin-female-mesh");
                        LoadModel("cat-mesh");

                        var temp = models.Where(model => model.Export != null);
                        if (temp.Any())
                        {
                            models = temp.ToList();
                            return;
                        }

                        else if (record.ElDefinition.Name == "weapon")
                        {
                            var pet = record.Attributes.Get<Record>("pet");
                            await Load(pet, models);

                            var equipshow = record.Attributes["equip-show"];
                            if (!string.IsNullOrEmpty(equipshow))
                            {
                                //var EquipShow = FileCache.Pakitem.LoadObject<UShowObject>(equipshow);
                            }
                        }
                        else if (record.ElDefinition.Name == "accessory")
                        {
                            var VehicleDetail = record.Attributes.Get<Record>("vehicle-detail");
                            var VehicleAppearance = VehicleDetail?.Attributes.Get<Record>("appearance");
                            await Load(VehicleAppearance, models);
                        }
                    }

                    break;
                }


            case "pet":
                {
                    models.Add(new()
                    {
                        Export = await FileCache.Provider.LoadObjectAsync<UObject>(record.Attributes["mesh-name"]),
                        AnimSet = await FileCache.Provider.LoadObjectAsync<UAnimSet>(record.Attributes["anim-set-name"]),
                        Cols = [record.Attributes["material-name-1"], record.Attributes["material-name-2"], record.Attributes["material-name-3"]],
                    });
                    break;
                }

            case "vehicle-appearance":
                {
                    models.Add(new ModelData()
                    {
                        Export = FileCache.Provider.LoadObject<UObject>(record.Attributes["mesh-name"]),
                        AnimSet = FileCache.Provider.LoadObject<UAnimSet>(record.Attributes["anim-set-name"]),
                        Cols = [record.Attributes["material-name-1"], record.Attributes["material-name-2"], record.Attributes["material-name-3"]],
                    });
                    break;
                }
        }
    }
}