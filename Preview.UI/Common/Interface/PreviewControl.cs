using System.ComponentModel;
using System.Windows.Forms;

using Xylia.Preview.Data.Record;

namespace Xylia.Preview.Common.Interface
{
	[DesignTimeVisible(false)]
	public partial class PreviewControl : UserControl, IPreview
	{
		/// <summary>
		/// 指示是无效控件
		/// </summary>
		/// <returns></returns>
		public virtual bool INVALID() => !this.Visible;

		/// <summary>
		/// Load Data
		/// </summary>
		/// <param name="record"></param>
		public virtual void LoadData(BaseRecord record)
		{

		}


		/// <summary>
		/// 通过 IRecord 对象Load 组件
		/// </summary>
		/// <typeparam name="TRecord"></typeparam>
		/// <param name="Record"></param>
		/// <returns></returns>
		public PreviewControl LoadInfo<TRecord>(TRecord Record) where TRecord : BaseRecord, new()
		{
			if (Record is null) return null;

			this.LoadData(Record);

			//判断是否有效
			return this.INVALID() ? null : this;
		}
	}


	/// <summary>
	/// 预览组件统一接口
	/// </summary>
	public interface IPreview
	{
		
	}
}