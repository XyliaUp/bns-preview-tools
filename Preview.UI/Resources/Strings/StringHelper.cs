using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows;
using HandyControl.Tools;
using Xylia.Preview.Common.Attributes;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Engine.DatData;
using Xylia.Preview.UI.ViewModels;

namespace Xylia.Preview.UI;
/// <summary>
/// Text Controller
/// </summary>
public partial class StringHelper : ResourceDictionary
{
	#region Constructor
	public static StringHelper? Current { get; private set; }

	public StringHelper()
	{
		Current = this;
		Language = UserSettings.Default.Language;
	}
	#endregion

	#region CultureInfo
	private CultureInfo CultureInfo = CultureInfo.CurrentCulture;

	protected virtual string BasePath => "Xylia.Preview.UI.Resources.Strings.Strings";

	public ELanguage Language
	{
		get => EnumerateLanguages().FirstOrDefault(x => x.GetAttribute<NameAttribute>().Name == CultureInfo.Name);
		set
		{
			// culture
			if (value != ELanguage.None)
			{
				var name = value.GetAttribute<NameAttribute>()?.Name;
				if (name != null) CultureInfo = new CultureInfo(name);
			}

			// resource
			ConfigHelper.Instance.SetLang(CultureInfo.Name);
			var manager = new ResourceManager(BasePath, Assembly.GetEntryAssembly());
			foreach (DictionaryEntry entry in manager.GetResourceSet(CultureInfo, true, true)!)
			{
				var resourceKey = entry.Key.ToString()!;
				var resource = entry.Value;

				this[resourceKey] = resource;
			}
		}
	}
	#endregion


	#region Static Methods
	internal static IEnumerable<ELanguage> EnumerateLanguages() => Enum.GetValues<ELanguage>().Where(x => x.GetAttribute<NameAttribute>() != null);

	/// <summary>
	/// Gets text and replaces the format item in a specified string with the string representation of a corresponding object in a specified array.
	/// </summary>
	/// <param name="key">Target text resource key</param>
	/// <param name="args">An object array that contains zero or more objects to format.</param>
	/// <returns></returns>
	public static string Get(string key, params object[] args)
	{
		ArgumentNullException.ThrowIfNull(Current);

		if (Current![key] is string s)
		{
			return string.Format(s, args);
		}

		return key;
	}
	#endregion
}