using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows;
using HandyControl.Tools;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.UI.ViewModels;

namespace Xylia.Preview.UI;
/// <summary>
/// Text Controller
/// </summary>
public sealed class StringHelper : ResourceDictionary
{
	#region Constructor
	public StringHelper()
	{
		Instance = this;
		Language = UserSettings.Default.Language;
	}
	#endregion

	#region CultureInfo
	private CultureInfo _cultureInfo;

	public ELanguage Language
	{
		get => EnumerateLanguages().FirstOrDefault(x => x.GetAttribute<NameAttribute>().Name == _cultureInfo.Name);
		set
		{
			// culture
			if (value != ELanguage.None)
			{
				var name = value.GetAttribute<NameAttribute>()?.Name;
				if (name != null) _cultureInfo = new CultureInfo(name);
			}

			if (_cultureInfo is null)
			{
				_cultureInfo ??= CultureInfo.CurrentCulture;
				UserSettings.Default.Language = Language;
			}


			// resource
			this.Clear();

			ConfigHelper.Instance.SetLang(_cultureInfo.Name);
			var manager = new ResourceManager("Xylia.Preview.UI.Resources.Strings.Strings", Assembly.GetExecutingAssembly());
			foreach (DictionaryEntry entry in manager.GetResourceSet(_cultureInfo, true, true))
			{
				string resourceKey = entry.Key.ToString();
				object resource = entry.Value;

				this.Add(resourceKey, resource);
			}
		}
	}
	#endregion


	#region Methods
	internal static IEnumerable<ELanguage> EnumerateLanguages() => Enum.GetValues<ELanguage>().Where(x => x.GetAttribute<NameAttribute>() != null);


	public static StringHelper Instance { get; private set; }

	public static string Get(string key, params object[] args)
	{
		if (Instance[key] is string s)
		{
			return string.Format(s, args);
		}

		return key;
	}


	// add bns text help?
	#endregion
}