using Xylia.Preview.Data.Models;

namespace Xylia.Preview.Data.Common.DataStruct;
public struct Sub<TRecord> where TRecord : Record
{
    public short Type;
    private BnsDatabase Database;

    public Sub(string value, BnsDatabase database)
    {
        Type = short.Parse(value);
		Database = database;
	}
}