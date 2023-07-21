using System.IO;
using System.Text;

using NPOI.XSSF;

using static Xylia.Preview.Data.Models.DatData.BXML_CONTENT;

namespace Xylia.Preview.Data.Models.DatData;
public class BPKG_FTE
{
    #region Fields
    public string FilePath;

    public byte Unknown_001;
    public byte Unknown_002;

    public bool IsCompressed;
    public bool IsEncrypted;


    public int FileDataOffset;        // (relative) offset
    public int FileDataSizeSheared;   // without padding for AES
    public int FileDataSizeStored;
    public int FileDataSizeUnpacked;

    public byte[] Padding;
    #endregion

    #region Constructor
    public BPKG_FTE()
    {

    }

    public BPKG_FTE(BinaryReader reader, bool is64, int OffsetGlobal = 0)
    {
        int FilePathLength = (int)(is64 ? reader.ReadInt64() : reader.ReadInt32());

        FilePath = Encoding.Unicode.GetString(reader.ReadBytes(FilePathLength * 2));
        Unknown_001 = reader.ReadByte();
        IsCompressed = reader.ReadByte() == 1;
        IsEncrypted = reader.ReadByte() == 1;
        Unknown_002 = reader.ReadByte();
        FileDataSizeUnpacked = (int)(is64 ? reader.ReadInt64() : reader.ReadInt32());
        FileDataSizeSheared = (int)(is64 ? reader.ReadInt64() : reader.ReadInt32());
        FileDataSizeStored = (int)(is64 ? reader.ReadInt64() : reader.ReadInt32());
        FileDataOffset = (int)(is64 ? reader.ReadInt64() : reader.ReadInt32()) + OffsetGlobal;
        Padding = reader.ReadBytes(60);
    }
    #endregion



    #region DATA
    public KeyInfo KeyInfo;


    public byte[] Data;


    BXML_CONTENT _xml;

    public BXML_CONTENT Xml
    {
        private set => _xml = value;
        get
        {
            if (_xml is null) Decrypt();

            return _xml;
        }
    }



    public void Decrypt()
    {
        byte[] buffer_unpacked = BNSDat.Unpack(Data, FileDataSizeStored, FileDataSizeSheared, FileDataSizeUnpacked, IsEncrypted, IsCompressed, KeyInfo);

        if (FilePath.EndsWith(".xml") || FilePath.EndsWith(".x16"))
        {
            Xml = new BXML_CONTENT(KeyInfo.XOR_KEY);
            Xml.Read(new MemoryStream(buffer_unpacked), BXML_TYPE.BXML_BINARY);

            var oStream = new MemoryStream();
            Xml.Write(oStream, BXML_TYPE.BXML_PLAIN);
            Data = oStream.ToArray();
        }
        else Data = buffer_unpacked;
    }
    #endregion
}