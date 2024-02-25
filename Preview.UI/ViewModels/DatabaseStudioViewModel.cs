using CommunityToolkit.Mvvm.ComponentModel;
using Xylia.Preview.Data.Engine.Definitions;

namespace Xylia.Preview.UI.ViewModels;
internal partial class DatabaseStudioViewModel : ObservableObject
{
	#region ToolBar
	[ObservableProperty]
	private bool _connectStatus;

	[ObservableProperty]
	private TableDefinition _currentDefinition;

	public string SaveDataPath => UserSettings.Default.OutputFolder + "\\data";
	#endregion

	#region SQL Result
	[ObservableProperty]
	private bool _indentText = true;
	#endregion
}