using System.Windows.Controls;
using System.Windows.Input;

using CUE4Parse.BNS.Assets.Exports;
using CUE4Parse.BNS.Conversion;

using HandyControl.Data;

using Newtonsoft.Json;
using Xylia.Preview.UI.Audio;
using Xylia.Preview.UI.ViewModels;

namespace Xylia.Preview.UI.Views;
public partial class ShowObjectPlayer
{
	#region Ctor
	ShowObjectPlayerViewModel _viewModel;
	AudioPlayer audioPlayer;

	public UShowObject Source
	{
		get => _viewModel.ShowObject;
		set => _viewModel.ShowObject = value;
	}

	public ShowObjectPlayer()
	{
		DataContext = _viewModel = new ShowObjectPlayerViewModel();
		InitializeComponent();


		//string path = @"BNSR/Content/Art/FX/01_Source/05_SF/BM_EquipShow_01/Item_EquipShow_Constellation01";
		////path = @"BNSR/Content/bns/Package/World/GameDesign/commonpackage/ShowData/indun/soc_etc_all_insdungeun/ME_ChungGakABoss_0005_soc_voice";

		//_viewModel.ShowObject = FileCache.Provider.LoadObject<UShowObject>(path);
	}
	#endregion


	#region Methods
	private void OnSearchStarted(object sender, FunctionEventArgs<string> e)
	{

	}

	private void OnSelectedItem(object sender, SelectionChangedEventArgs e)
	{
		if (ObjectList.SelectedItem is not ShowKeyBase value) return;

		TextEditor.Text = JsonConvert.SerializeObject(value, Formatting.Indented);
	}

	private void OnDoubleClick(object sender, MouseButtonEventArgs e)
	{
		if (ObjectList.SelectedItem is not ShowKeyBase value) return;

		var wave = value.GetWave();
		if (wave != null)
		{
			if (audioPlayer is null)
			{
				audioPlayer = new();
				audioPlayer.Closed += (_, _) => audioPlayer = null;
				audioPlayer.Show();
			}

			audioPlayer.Load(new AudioFile(wave, "ogg")
			{
				Name = value.Name,
			});
		}
	}
	#endregion
}