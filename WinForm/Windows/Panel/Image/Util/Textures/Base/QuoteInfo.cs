using System.Drawing;

namespace Xylia.Match.Util.Paks.Textures
{
	/// <summary>
	/// 图标关联信息
	/// </summary>
	public class QuoteInfo
	{
		#region 存储信息
		/// <summary>
		/// 对象编号
		/// </summary>
		public int MainId;

		/// <summary>
		/// 对象名称
		/// </summary>
		public string Name;

		/// <summary>
		/// 对象别名
		/// </summary>
		public string Alias;
		#endregion


		#region 图标信息
		public string TextureAlias;

		public short IconIndex;
		#endregion



		/// <summary>
		/// 加工图片
		/// </summary>
		public virtual Bitmap ProcessImage(Bitmap bitmap) => bitmap;
	}
}
