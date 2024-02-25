using CUE4Parse.BNS.Assets.Exports;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models.Sequence;

namespace Xylia.Preview.Data.Models;
public sealed class KeyCap : ModelElement
{
	#region Methods
	public KeyCode KeyCode => this.Attributes["key-code"].ToEnum<KeyCode>();

	public ImageProperty Icon => IconTexture.Parse(Attributes.Get<string>("icon"));

	public string Image => this.Attributes["image"].GetText();


	public static KeyCode GetKeyCode(string o)
	{
		if (o == "SPACEBAR") return KeyCode.Space;

		return o.ToEnum<KeyCode>();
	}

	public static KeyCap Cast(string KeyCode) => Cast(GetKeyCode(KeyCode));

	public static KeyCap Cast(KeyCode KeyCode) => FileCache.Data.Get<KeyCap>().FirstOrDefault(x => x.KeyCode == KeyCode) ;
	#endregion
}