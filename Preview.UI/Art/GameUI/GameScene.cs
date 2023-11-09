using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Xylia.Preview.UI;
/// <summary>
/// base class of game scene
/// </summary>
public abstract class GameScene : Window, INotifyPropertyChanged
{
	public GameScene()
	{

	}


	#region	PropertyChange

	public event PropertyChangedEventHandler PropertyChanged;

	protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
	{
		if (EqualityComparer<T>.Default.Equals(storage, value))
			return false;

		storage = value;
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		return true;
	}
	#endregion
}