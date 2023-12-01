using System.ComponentModel;
using System.Windows.Data;

using CommunityToolkit.Mvvm.ComponentModel;

using CUE4Parse.BNS.Assets.Exports;

namespace Xylia.Preview.UI.ViewModels;
public class ShowObjectPlayerViewModel : ObservableObject
{
	#region Properties 
	UShowObject _showObject;

	public UShowObject ShowObject
	{
		get => _showObject;
		set
		{
			SetProperty(ref _showObject, value);
			if (value is null) return;

			// load
			var EventKeys = ShowObject.EventKeys.Select(x => x.Load());

			// create view
			Source = CollectionViewSource.GetDefaultView(EventKeys);
			OnPropertyChanged(nameof(Source));
		}
	}

	public ICollectionView Source { get; private set; }
	#endregion
}