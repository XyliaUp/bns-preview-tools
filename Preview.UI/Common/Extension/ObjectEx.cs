using System.Diagnostics;

using Xylia.Extension;
using Xylia.Preview.Common.Cast;
using Xylia.Preview.Data.Record;
using Xylia.Preview.GameUI.Controls;
using Xylia.Preview.GameUI.Scene.Game_ToolTip.ItemTooltipPanel;
using Xylia.Preview.GameUI.Scene.Skill;

namespace Xylia.Preview.Common.Extension
{
	public static class ObjectEx
	{
		#region Icon 
		public static Bitmap Icon(this Skill skill) => skill.IconTexture.GetIcon(skill.IconIndex);




		public static ItemIconCell GetObjIcon(this string ObjInfo, string StackCount) => GetObjIcon(ObjInfo, (short)StackCount.ToInt());

		public static ItemIconCell GetObjIcon(this string ObjInfo, short StackCount = 0)
		{
			if (ObjInfo is null) return null;

			var Object = ObjInfo.Contains(':') ? ObjInfo.CastObject() : FileCache.Data.Item[ObjInfo];
			return GetObjIcon(Object, StackCount);
		}

		public static ItemIconCell GetObjIcon(this BaseRecord Object, short StackCount = 0)
		{
			if (Object is null) return null;
			if (Object.Attributes is null) return Object.alias.GetObjIcon(StackCount);


			//get image
			Bitmap Image = null;
			if (Object is Item Item) Image = Item.Icon();
			else if (Object is ItemBrandTooltip ItemBrandTooltip) Image = ItemBrandTooltip.Icon();
			else if (Object is Skill skill) Image = skill.Icon();
			else return null;

			//
			return new ItemIconCell()
			{
				ObjectRef = Object,
				Image = Image,

				StackCount = StackCount,
				ShowStackCount = StackCount != 0,
				ShowStackCountOnlyOne = false,
			};
		}

		public static ItemIconCell GetObjIcon(this Bitmap Image)
		{
			return new ItemIconCell()
			{
				Image = Image,
				ShowStackCount = false,
			};
		}
		#endregion

		#region Preview
		public static void PreviewShow(this BaseRecord obj, IWin32Window window = null)
		{
			if (obj is null) return;

			var thread = new Thread(() =>
			{
				#region	get
				Form ResultFrm = null;
				if (obj is Item item) ResultFrm = new ItemTooltipPanel(item);
				else if (obj is Skill skill) ResultFrm = new SkillFrm(skill);
				else
				{
					Trace.WriteLine($"窗体无效 ({obj})");
					return;
				}
				#endregion


				if (window is null) ResultFrm.ShowDialog();
				else ResultFrm.ShowDialog(window);
			});

			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();
		}
		#endregion
	}
}