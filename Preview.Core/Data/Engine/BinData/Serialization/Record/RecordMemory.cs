using System.Runtime.InteropServices;

namespace Xylia.Preview.Data.Engine.BinData.Serialization;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct RecordMemory
{
    public byte* DataBegin;
    public byte* StringBufferBegin;
    public int DataSize;
    public int StringBufferSize;
    public short Type;
}