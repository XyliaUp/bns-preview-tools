using System.ComponentModel;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Xylia.Preview.UI.Views.Selector;

[ObservableObject]
public partial class SearcherResult : Window
{
	[ObservableProperty]
	ICollectionView source;

	public SearcherResult()
	{
		InitializeComponent();
	}
}