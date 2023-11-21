using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;

using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Interactivity;

using HandyControl.Tools.Extension;

using Xylia.Preview.Data.Database;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Views.Editor;
[ObservableObject]
public partial class PropertyEditor : System.Windows.Window
{
	#region Fields
	[ObservableProperty]
	private Record source;
	#endregion

	#region Ctr
	public PropertyEditor()
	{
		InitializeComponent();
	}
	#endregion
}