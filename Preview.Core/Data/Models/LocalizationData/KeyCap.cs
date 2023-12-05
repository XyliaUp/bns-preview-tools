using SkiaSharp;

using Xylia.Extension;
using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.Data.Models;
public sealed class KeyCap : Record
{
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