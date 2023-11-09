using Xylia.Preview.Data.Engine.BinData.Serialization;
using Xylia.Preview.Data.Engine.Readers;

namespace Xylia.Preview.Data.Common.Abstractions;
public interface IRecordReader
{
    bool Initialize(DatafileArchive reader, bool is64Bit);
    bool Read(DatafileArchive reader, ref RecordMemory recordMemory);
}