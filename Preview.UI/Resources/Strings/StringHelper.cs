using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Xylia.Preview.UI.Resources;
public sealed class StringHelper : ResourceDictionary
{
	#region Constructor
	public static StringHelper Instance { get; set; }

	public StringHelper()
	{
		Instance = this;
		CultureInfo = CultureInfo.CurrentCulture;
	}
	#endregion

	#region CultureInfo
	private CultureInfo _cultureInfo;

	public CultureInfo CultureInfo
	{
		get => _cultureInfo;
		set
		{
			_cultureInfo = value;

			this.Clear();

			var manager = new ResourceManager("Xylia.Preview.UI.Resources.Strings.Strings", Assembly.GetExecutingAssembly());
			foreach (DictionaryEntry entry in manager.GetResourceSet(value, true, true))
			{
				string resourceKey = entry.Key.ToString();
				object resource = entry.Value;

				this.Add(resourceKey, resource);
			}
		}
	}
	#endregion
}