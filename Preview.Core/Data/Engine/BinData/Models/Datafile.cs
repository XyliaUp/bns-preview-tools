using Xylia.Preview.Data.Engine.BinData.Helpers;

namespace Xylia.Preview.Data.Engine.BinData.Models;
public class Datafile
{
	public byte DatafileVersion { get; set; }
	public Common.DataStruct.Version ClientVersion { get; set; }
	public DateTime CreatedAt { get; set; }


	public NameTable NameTable { get; set; } 

    public TableCollection Tables { get; set; }

    public bool is64Bit { get; set; }
}