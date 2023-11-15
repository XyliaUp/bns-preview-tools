using System.Windows.Input;
using System.Windows.Media;

using CommunityToolkit.Mvvm.Input;

using Xylia.Extension;
using Xylia.Preview.Data.Common.Seq;

namespace Xylia.Preview.UI.Art.GameUI.Scene.Game_ItemMap;
public partial class Game_ItemMapScene : GameScene
{
	public Game_ItemMapScene()
	{
		InitializeComponent();
	}


	#region Properties
	double scale { get; set; } = 1;
	#endregion

	#region Methods
	[RelayCommand]
	private void EquipTypeChange(object value)
	{
		ItemMapPanel_MapField.EquipType = value.ToString().ToEnum<EquipType>();
		Scroller.ScrollToRightEnd();
	}

	private void PanelMouseWheel(object sender, MouseWheelEventArgs e)
	{
		if (!Keyboard.IsKeyDown(Key.LeftCtrl)) return;

		if (e.Delta < 0)
		{
			if (scale <= 0.75) return;
			scale -= 0.1;
		}
		else
		{
			if (scale >= 1.25) return;
			scale += 0.1;
		}

		ItemMapPanel.LayoutTransform = new ScaleTransform() { ScaleX = scale, ScaleY = scale, };
		e.Handled = true;
	}
	#endregion
}