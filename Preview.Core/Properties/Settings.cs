using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xylia.Configure;
using Xylia.Extension;

namespace Xylia.Preview.Properties;
public class Settings : INotifyPropertyChanged
{
	internal static Settings Default => new();

	#region PropertyChange
	public event PropertyChangedEventHandler PropertyChanged;

	protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
	{
		if (EqualityComparer<T>.Default.Equals(storage, value))
			return false;

		storage = value;
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		return true;
	}


	protected static string GetValue(string section = "Common", [CallerMemberName] string Name = null)
	{
		if (Name.Contains('_'))
		{
			var split = Name.Split('_', 2);
			section = split[0]; Name = split[1];
		}

		return Ini.Instance.ReadValue(section, Name);
	}

	protected void SetValue(object value, string section = "Common", [CallerMemberName] string Name = null)
	{
		if (Name.Contains('_'))
		{
			var split = Name.Split('_', 2);
			section = split[0]; Name = split[1];
		}

		Ini.Instance.WriteValue(section, Name, value);
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
	}
	#endregion

	#region Common
	public string GameFolder
	{
		get => GetValue();
		set
		{
			if (!Directory.Exists(value)) return;

			SetValue(value);
		}
	}

	public string OutputFolder
	{
		get => GetValue();
		set
		{
			if (!Directory.Exists(value)) return;

			SetValue(value);
			OutputFolderResource ??= value + "\\Paks";
		}
	}

	public string OutputFolderResource { get => GetValue(); set => SetValue(value); }


	public bool UseUserDefinition { get => GetValue().ToBool(); set => SetValue(value); }
	#endregion
}