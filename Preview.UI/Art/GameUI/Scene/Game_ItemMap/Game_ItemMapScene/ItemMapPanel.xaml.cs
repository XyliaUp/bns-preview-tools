using CommunityToolkit.Mvvm.Input;

namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_ItemMap;
public partial class ItemMapPanel
{
	public ItemMapPanel()
	{
		this.DataContext = this;
		InitializeComponent();
	}

	[RelayCommand]
	private void EquipTypeChange(object value)
	{
		ItemMapPanel_MapField.Update(value);
	}
}