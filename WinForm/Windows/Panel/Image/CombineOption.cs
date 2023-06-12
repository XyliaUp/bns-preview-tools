using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

using Xylia.Extension;

using static Xylia.Preview.Resources.Resource_BNSR;

namespace Xylia.Match.Windows.Panel
{
	/// <summary>
	/// 合成器
	/// </summary>
	public sealed class ItemImageCompose
	{
		#region Events & Delegates
		public event EmptyHandler RefreshHandle;
		public void Refresh() => this.RefreshHandle?.Invoke();
		#endregion

		#region Functions
		public Bitmap GradeImage;

		public ImageInfo BottomLeft;

		public ImageInfo TopRight;

		public Bitmap Icon;

		public Bitmap DrawICON(double? Ratio = null)
		{
			Bitmap Temp = new(GradeImage);

			//比例缩放
			if (Ratio != null) Temp = Temp.Thumbnail((double)Ratio);

			if (Icon != null) Temp = Temp.Combine(Icon, DrawLocation.Centre);


			if (BottomLeft?.bitmap != null)
			{
				var tmp = BottomLeft.bitmap;
				if (Ratio != null) tmp = BottomLeft.bitmap.Thumbnail((double)Ratio);

				Temp = Temp.Combine(tmp, DrawLocation.BottomLeft);
			}

			if (TopRight?.bitmap != null) Temp = Temp.Combine(TopRight.bitmap, DrawLocation.TopRight);


			return Temp;
		}
		#endregion
	}

	public class CombineOption
	{
		static ComponentResourceManager resources = new(typeof(IconPage));


		public static List<GradeInfo> Grades = new()
		{
			{ new(1, resources.GetString("Grade_1")) },
			{ new(2, resources.GetString("Grade_2")) },
			{ new(3, resources.GetString("Grade_3")) },
			{ new(4, resources.GetString("Grade_4")) },
			{ new(5, resources.GetString("Grade_5")) },
			{ new(6, resources.GetString("Grade_6")) },
			{ new(7, resources.GetString("Grade_7")) },
			{ new(8, resources.GetString("Grade_8")) },
			{ new(9, resources.GetString("Grade_9")) },
		};

		public static List<ImageInfo> BLImage = new()
		{
			{ new(resources.GetString("NONE"), null) },
			{ new(resources.GetString("BL_1"), Weapon_Lock_04) },
			{ new(resources.GetString("BL_2"), Weapon_Lock_05) },
			{ new(resources.GetString("BL_3"),  unuseable_lock) },
			{ new(resources.GetString("BL_4"),  unuseable_lock_2) },
			{ new(resources.GetString("BL_5"), Weapon_Lock_03) },
			{ new(resources.GetString("BL_6"), Weapon_Lock_06) },
			{ new(resources.GetString("BL_7"),  Weapon_Lock_01) },
			{ new(resources.GetString("BL_8"),  Weapon_Lock_02) },
		};

		public static List<ImageInfo> TRImage = new()
		{
			{ new(resources.GetString("NONE"), null) },
			{ new(resources.GetString("Trade_Market"), SlotItem_marketBusiness) },
			{ new(resources.GetString("Trade_Private"), SlotItem_privateSale) },
		};
	}





	public sealed class GradeInfo
	{
		public GradeInfo(byte ItemGrade, string Name)
		{
			this.Name = Name;
			this.ItemGrade = ItemGrade;
		}


		public string Name;

		public byte ItemGrade = 1;
	}

	public sealed class ImageInfo
	{
		public ImageInfo(string Name, Bitmap bitmap)
		{
			this.Name = Name;
			this.bitmap = bitmap;
		}


		public string Name;

		public Bitmap bitmap;
	}
}