using System.ComponentModel;
using System.Runtime.CompilerServices;
using IniParser;
using IniParser.Model;
using Xylia.Preview.Common.Extension;

namespace Xylia.Preview.Properties;
public class Settings : INotifyPropertyChanged
{
	internal static Settings Default { get; } = new();

	public static string ApplicationData => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Xylia");

	protected Settings()
	{
		// prevent exception when save
		Directory.CreateDirectory(ApplicationData);

		ConfigPath = Path.Combine(ApplicationData, "Settings.config");
		Configuration = File.Exists(ConfigPath) ? 
			new FileIniDataParser().ReadFile(ConfigPath) : 
			new IniData();
	}


	#region PropertyChange	 
	protected string ConfigPath;
	protected IniData Configuration;

	public event PropertyChangedEventHandler PropertyChanged;

	protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
	{
		if (EqualityComparer<T>.Default.Equals(storage, value))
			return false;

		storage = value;
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		return true;
	}

	protected string GetValue(string section = "Common", [CallerMemberName] string name = null)
	{
		if (name.Contains('_'))
		{
			var split = name.Split('_', 2);
			section = split[0];
			name = split[1];
		}

		return Configuration[section][name];
	}

	protected void SetValue(object value, string section = "Common", [CallerMemberName] string name = null)
	{
		if (name.Contains('_'))
		{
			var split = name.Split('_', 2);
			section = split[0];
			name = split[1];
		}

		Configuration[section][name] = value?.ToString();
		new FileIniDataParser().WriteFile(ConfigPath, Configuration);

		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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