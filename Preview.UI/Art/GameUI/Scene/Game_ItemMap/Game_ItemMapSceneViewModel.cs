using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Xylia.Extension;
using Xylia.Preview.Data.Common.Seq;

namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_ItemMap;
public partial class Game_ItemMapSceneViewModel : ObservableObject
{
	[ObservableProperty]
	public EquipType equipType;

	[RelayCommand]
	private void EquipTypeChange(object value) => EquipType = value.ToString().ToEnum<EquipType>();
}