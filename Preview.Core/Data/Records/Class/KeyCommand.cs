using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;

namespace Xylia.Preview.Data.Record
{
	public sealed class KeyCommand : BaseRecord
	{
		#region Fields
		[Signal("key-command")]
		public KeyCommandSeq keyCommand;

		[Signal("pc-job")]
		public JobSeq PcJob;



		//public Seq Category;

		//[Signal("joypad-category")]
		//public Seq JoypadCategory;

		public Text Name;

		[Signal("default-keycap")]
		public string DefaultKeycap;

		[Signal("modifier-enabled")]
		public bool ModifierEnabled;

		[Signal("sort-no")]
		public byte SortNo;

		public byte Layer;

		[Signal("option-sort-no")]
		public short OptionSortNo;

		//[Signal("usable-joypad-mode")]
		//public Seq UsableJoypadMode;

		[Signal("joypad-customize-enabled")]
		public bool JoypadCustomizeEnabled;
		#endregion

		#region Functions
		/// <summary>
		/// 获取组合键
		/// </summary>
		/// <returns></returns>
		private KeyCap[] GetKeyCaps()
		{
			var result = new List<KeyCap>();

			#region 处理默认组合键
			if(this.DefaultKeycap != null)
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
		#endregion



		public string GetImage() => this.Key1?.Image;  

		public Bitmap GetIcon() => this.Key1?.Icon;


		public static KeyCommand Cast(KeyCommandSeq KeyCommand) => FileCache.Data.KeyCommand.FirstOrDefault(o => o.keyCommand == KeyCommand);
	}
}