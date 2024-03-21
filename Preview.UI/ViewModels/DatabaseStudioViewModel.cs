using CommunityToolkit.Mvvm.ComponentModel;
using Xylia.Preview.Data.Engine.Definitions;
using Xylia.Preview.UI.Services;

namespace Xylia.Preview.UI.ViewModels;
internal partial class DatabaseStudioViewModel : ObservableObject
{
	#region ToolBar
	[ObservableProperty]
	private bool _connectStatus;

	[ObservableProperty]
	private TableDefinition _currentDefinition;

	public string SaveDataPath => UserSettings.Default.OutputFolder + "\\data";

	public bool UseImport => UserService.Instance?.Role >= UserRole.Advanced;
	#endregion


	#region SQL Result
	[ObservableProperty]
	internal bool _isGlobalData = false;

	[ObservableProperty]
	private bool _indentText = true;
	#endregion
}