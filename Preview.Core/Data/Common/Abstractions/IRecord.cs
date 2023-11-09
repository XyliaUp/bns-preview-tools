using Xylia.Preview.Data.Common.DataStruct;

namespace Xylia.Preview.Data.Common.Abstractions;
public interface IRecord
{
	Ref Ref { get; set; }
	TRef TRef => new TRef(TableType, Ref.Id, Ref.Variant);
	short TableType { get; }
}