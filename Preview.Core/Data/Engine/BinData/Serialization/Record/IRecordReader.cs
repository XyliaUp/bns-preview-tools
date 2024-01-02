namespace Xylia.Preview.Data.Engine.BinData.Serialization;
internal interface IRecordReader
{
    bool Initialize(DataArchive reader , bool is64Bit);

    bool Read(DataArchive reader, ref RecordMemory recordMemory);
}