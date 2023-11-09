using SkiaSharp;

using Xylia.Extension;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Engine.BinData.Models;
using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.Data.Models;
public sealed class KeyCap : Record
{
	#region Fields	
	[Name("key-code")]
	public KeyCode KeyCode;

	public Ref<Text> Name;

	[Name("short-name")]
	public Ref<Text> ShortName;

	[Name("scroll-imageset")]
	public string ScrollImageset;

	[Name("scroll-imageset-scale")]
	public float ScrollImagesetScale;
	#endregion


	#region Methods
	public SKBitmap Icon => this.Attributes["icon"].GetIcon();

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