using System.ComponentModel;
using System.IO;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Tools.Extension;

using Ookii.Dialogs.Wpf;
using Xylia.Preview.Data.Helpers.Output;
using Xylia.Preview.UI.GameUI.Scene.Game_Auction;
using Xylia.Preview.UI.Helpers.Output.Items;
using Xylia.Preview.UI.Views;
using Xylia.Preview.UI.Views.Selector;

namespace Xylia.Preview.UI.ViewModels;
public partial class ItemPageViewModel : ObservableObject
{
	#region List
	[ObservableProperty]
	bool onlyUpdate;

	[ObservableProperty]
	string itemListPath;


	[RelayCommand]
	public void OpenSettings()
	{
		new SettingsView().ShowDialog();
	}

	[RelayCommand]
	private void BrowerItemList()
	{
		var dialog = new VistaOpenFileDialog() { Filter = @"|*.chv|All files|*.*" };
		if (dialog.ShowDialog() == true) ItemListPath = dialog.FileName;
	}

	[RelayCommand]
	private async Task CreateItemList()
	{
		#region Check
		if (!Directory.Exists(UserSettings.Default.GameFolder)) throw new WarningException("Must to set game directory");
		else if (!Directory.Exists(UserSettings.Default.OutputFolder)) throw new WarningException("Must to set output directory");
		else if (OnlyUpdate == true && !File.Exists(ItemListPath)) throw new WarningException("Select a valid item list");
		#endregion

		#region Load
		var fileMode = await Dialog.Show<FileModeDialog>().GetResultAsync<FileModeDialog.FileMode>();
		if (fileMode == FileModeDialog.FileMode.None) return;

		int span = 0;
		using var Out = new ItemOut() { OnlyUpdate = this.OnlyUpdate };
		await Task.Run(() =>
		{
			var startTime = DateTime.Now;

			Out.LoadCache(ItemListPath);
			Out.GetData();
			if (Out.Count == 0)
			{
				Growl.Error("没有新增的物品");
				return;
			}

			Out.Start(startTime, fileMode == FileModeDialog.FileMode.Xlsx);
			span = (int)(DateTime.Now - startTime).TotalSeconds;
		});
		#endregion


		Growl.Success(new GrowlInfo()
		{
			Message = StringHelper.Get("ItemList_TaskCompleted", Out.Count, span),
			StaysOpen = true,
		});
	}
	#endregion

	#region Preview
	[RelayCommand]
	private void Preview(string name)
	{
		name = $"Xylia.Preview.UI.GameUIne.{name}";

		var type = Type.GetType(name);
		if (type is null) return;

		(Activator.CreateInstance(type) as System.Windows.Window)?.Show();
	}

	[RelayCommand]
	private void PreviewItem(string rule)
	{
		var scene = new Game_AuctionScene();
		scene.NameFilter = rule;
		scene.Show();
	}

	public static async void StartOutput<T>() where T : OutSet, new()
	{
		var instance = new T();

		var save = new VistaSaveFileDialog
		{
			Filter = "Excel Files|*.xlsx",
			FileName = $"{instance.Name} ({DateTime.Now:yyyyMM}).xlsx",
		};
		if (save.ShowDialog() != true) return;

		DateTime dt = DateTime.Now;
		await instance.Output(new FileInfo(save.FileName));

		Growl.Success(StringHelper.Get("ItemList_TaskCompleted2", 0, (DateTime.Now - dt).TotalSeconds));
	}
	#endregion
}