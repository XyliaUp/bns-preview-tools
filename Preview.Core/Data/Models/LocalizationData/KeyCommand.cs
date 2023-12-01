using SkiaSharp;

using Xylia.Preview.Data.Common.Attribute;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Common.Seq;
using Xylia.Preview.Data.Helpers;

namespace Xylia.Preview.Data.Models;
public sealed class KeyCommand : Record
{
	#region Fields
	public KeyCommandSeq Command;

	[Name("pc-job")]
	public JobSeq PcJob;



	//public Seq Category;

	//[Signal("joypad-category")]
	//public Seq JoypadCategory;

	public Ref<Text> Name;

	[Name("default-keycap")]
	public string DefaultKeycap;

	[Name("modifier-enabled")]
	public bool ModifierEnabled;

	[Name("sort-no")]
	public sbyte SortNo;

	public sbyte Layer;

	[Name("option-sort-no")]
	public short OptionSortNo;

	//[Signal("usable-joypad-mode")]
	//public Seq UsableJoypadMode;

	[Name("joypad-customize-enabled")]
	public bool JoypadCustomizeEnabled;
	#endregion

	#region Methods
	private KeyCap[] GetKeyCaps()
	{
		var result = new List<KeyCap>();

		#region 处理默认组合键
		if (this.DefaultKeycap != null)
		{
			//逗号分隔多个快捷键, 实际未支持处理
			foreach (var o in this.DefaultKeycap.Split(','))
			{
				if (string.IsNullOrWhiteSpace(o) || o == "none") continue;

				if (o.StartsWith("^"))
				{
					result.Add(KeyCap.Cast(KeyCode.Control));
					result.Add(KeyCap.Cast(o[1..]));
				}
				else if (o.StartsWith("~"))
				{
					result.Add(KeyCap.Cast(KeyCode.Alt));
					result.Add(KeyCap.Cast(o[1..]));
				}
				else result.Add(KeyCap.Cast(o));
			}
		}
		#endregion

		return result.ToArray();
	}

	private KeyCap GetKey(int Index) => this.GetKeyCaps().Length >= Index + 1 ? this.GetKeyCaps()[Index] : null;
	public KeyCap Key1 => GetKey(0);
	public KeyCap Key2 => GetKey(1);


	public string GetImage() => this.Key1?.Image;

	public SKBitmap GetIcon() => this.Key1?.Icon;

	public static KeyCommand Cast(KeyCommandSeq KeyCommand) => FileCache.Data.KeyCommand.FirstOrDefault(o => o.Command == KeyCommand);
	#endregion
}