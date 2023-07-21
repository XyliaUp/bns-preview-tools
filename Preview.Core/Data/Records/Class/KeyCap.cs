using Xylia.Extension;
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Helper;

namespace Xylia.Preview.Data.Record;
public sealed class KeyCap : BaseRecord
{
	#region Fields	
	[Signal("key-code")]
	public KeyCode KeyCode;

	public Text Name;

	[Signal("short-name")]
	public Text ShortName;

	[Signal("scroll-imageset")]
	public string ScrollImageset;

	[Signal("scroll-imageset-scale")]
	public float ScrollImagesetScale;
	#endregion


	#region Functions
	public Bitmap Icon => this.Attributes["icon"].GetIcon();

	public string Image => this.Attributes["image"].GetText();



	public static KeyCode GetKeyCode(string o)
	{
		if (o == "SPACEBAR") return KeyCode.Space;

		return o.ToEnum<KeyCode>();
	}

	public static KeyCap Cast(string KeyCode) => Cast(GetKeyCode(KeyCode));

	public static KeyCap Cast(KeyCode KeyCode) => FileCache.Data.KeyCap[(short)KeyCode];
	#endregion
}