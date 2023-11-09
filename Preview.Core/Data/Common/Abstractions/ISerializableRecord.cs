namespace Xylia.Preview.Data.Common.Abstractions;
public interface ISerializableRecord : IRecord
{
    unsafe ushort Serialize(byte* buffer, StreamWriter stringWriter);
}