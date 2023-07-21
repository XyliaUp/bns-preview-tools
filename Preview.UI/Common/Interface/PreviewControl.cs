using System.ComponentModel;

using Xylia.Preview.Data.Models.BinData.Table.Record;

namespace Xylia.Preview.Common.Interface;

[DesignTimeVisible(false)]
public partial class PreviewControl : UserControl, IPreview
{
	public virtual bool INVALID() => !this.Visible;

	public virtual void LoadData(BaseRecord record)
	{

	}

	public PreviewControl LoadInfo<TRecord>(TRecord Record) where TRecord : BaseRecord, new()
	{
		if (Record is null) return null;

		this.LoadData(Record);
		return this.INVALID() ? null : this;
	}
}

public interface IPreview
{

}