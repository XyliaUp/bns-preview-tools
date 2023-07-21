using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Xylia.Preview.Data.Models.DatData;
public class BXML_CONTENT
{
    #region XOR
    public byte[] XOR_KEY;

    void Xor(byte[] buffer, int size)
    {
        if (Version == 3)
        {
            XOR_KEY ??= KeyInfo.XOR_KEY_2021;

            for (int i = 0; i < size; i++)
            {
                buffer[i] = (byte)(buffer[i] ^ XOR_KEY[i % XOR_KEY.Length]);
            }
        }
        else if (Version == 4)
        {
            XOR_KEY = KeyInfo.XOR_KEY_2014;

            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    buffer[k] = seedTransform((byte)(buffer[k] ^ seedTransform(XOR_KEY[k % XOR_KEY.Length])));
                }
            }
        }
        else
        {
            throw new Exception($"Unsupported version: {Version}");
        }
    }

    static byte rol(byte original, int bits)
    {
        return (byte)(original << bits | original >> 8 - bits);
    }

    static byte seedTransform(byte c)
    {
        byte original = c;
        original = rol(original, 4);
        original = (byte)(original ^ c);
        original = (byte)(original & 0x33);
        original = (byte)(original ^ c);
        original = rol(original, 1);
        byte original2 = original;
        original = (byte)(original & 0x55);
        original2 = rol(original2, 2);
        original2 = (byte)(original2 & 0xAA);
        return (byte)(original2 | original);
    }


    public BXML_CONTENT(byte[] XorKey) => XOR_KEY = XorKey;
    #endregion

    #region Fields
    public readonly XmlDocument Nodes = new();
    int AutoID;

    byte[] Signature;                 // 8 byte
    uint Version;                     // 4 byte
    int FileSize;                     // 4 byte
    byte[] Padding;                   // 64 byte
    bool Unknown;                     // 1 byte
    string OriginalPath;
    #endregion

    #region Functions
    public void Read(Stream iStream, BXML_TYPE iType)
    {
        if (iType == BXML_TYPE.BXML_PLAIN)
        {
            Signature = "LMXBOSLB"u8.ToArray();
            Version = 3;
            FileSize = 85;
            Padding = new byte[64];
            Unknown = true;
            OriginalPath = "";

            // NOTE: keep whitespace text nodes (to be compliant with the whitespace TEXT_NODES in bns xml)
            // no we don't keep them, we remove them because it is cleaner
            Nodes.PreserveWhitespace = true;
            Nodes.Load(iStream);

            // get original path from first comment node
            XmlComment comment = Nodes.DocumentElement.ChildNodes.OfType<XmlComment>().FirstOrDefault();
            if (comment != null)
            {
                OriginalPath = comment.InnerText?.Trim();

                //original path not the real node for doc
                if (Nodes.PreserveWhitespace && comment.NextSibling.NodeType == XmlNodeType.Whitespace)
                {
                    //Nodes.DocumentElement.RemoveChild(comment.NextSibling);

                    XmlWhitespace whitespace = (XmlWhitespace)comment.NextSibling;
                    whitespace.Value = new Regex("\r\n").Replace(whitespace.Value, "", 1).TrimStart(' ');
                }

                Nodes.DocumentElement.RemoveChild(comment);
            }
        }

        else if (iType == BXML_TYPE.BXML_BINARY)
        {
            BinaryReader br = new(iStream);
            br.BaseStream.Position = 0;

            Signature = br.ReadBytes(8);
            Version = br.ReadUInt32();
            FileSize = br.ReadInt32();
            Padding = br.ReadBytes(64);
            Unknown = br.ReadByte() == 1;
            OriginalPath = ReadText(br);
            AutoID = 1;
            ReadNode(iStream);

            // add original path as first comment node
            Nodes.DocumentElement.PrependChild(Nodes.CreateComment(" " + OriginalPath + " "));
            Nodes.PrependChild(Nodes.CreateXmlDeclaration("1.0", "utf-8", null));

            if (FileSize != iStream.Position)
            {
                throw new Exception(string.Format("Filesize Mismatch, expected size was {0} while actual size was {1}.", FileSize, iStream.Position));
            }
        }
    }

    public void Write(Stream oStream, BXML_TYPE oType)
    {
        if (oType == BXML_TYPE.BXML_PLAIN) Nodes.Save(oStream);

        else if (oType == BXML_TYPE.BXML_BINARY)
        {
            BinaryWriter bw = new(oStream);
            bw.Write(Signature);
            bw.Write(Version);
            bw.Write(FileSize);
            bw.Write(Padding);
            bw.Write(Unknown);
            WriteText(bw, OriginalPath);

            AutoID = 1;
            WriteNode(oStream);

            FileSize = (int)oStream.Position;
            oStream.Position = 12;
            bw.Write(FileSize);
        }
    }


    private void ReadNode(Stream iStream, XmlNode parent = null)
    {
        XmlNode node = null;
        BinaryReader br = new BinaryReader(iStream);
        int Type = 1;
        if (parent != null)
        {
            Type = br.ReadInt32();
        }

        KeyValuePair<string, string>[] attributes = null;
        switch (Type)
        {
            case 1:
                {
                    node = Nodes.CreateElement("Text");

                    int ParameterCount = br.ReadInt32();
                    attributes = new KeyValuePair<string, string>[ParameterCount];

                    for (int i = 0; i < ParameterCount; i++)
                    {
                        string Name = ReadText(br);
                        string Value = ReadText(br);

                        attributes[i] = new KeyValuePair<string, string>(Name, Value);
                    }
                }
                break;

            case 2:
                {
                    var Text = ReadText(br);
                    node = Nodes.CreateTextNode(Text);
                }
                break;

            default: throw new Exception($"Unknown XML Node Type: " + Type);
        }


        bool Closed = br.ReadByte() == 1;
        string Tag = ReadText(br);

        if (Type == 1)
        {
            node = Nodes.CreateElement(Tag);
            foreach (var attr in attributes)
            {
                ((XmlElement)node).SetAttribute(attr.Key, attr.Value);
            }
        }

        int ChildCount = br.ReadInt32();
        AutoID = br.ReadInt32();
        AutoID++;

        for (int i = 0; i < ChildCount; i++)
            ReadNode(iStream, node);


        if (parent != null) parent.AppendChild(node);
        else Nodes.AppendChild(node);
    }

    private bool WriteNode(Stream oStream, XmlNode parent = null)
    {
        BinaryWriter bw = new(oStream);
        XmlNode node = parent ?? Nodes.DocumentElement;

        int Type = 1;
        switch (node.NodeType)
        {
            case XmlNodeType.Element:
                Type = 1;
                break;
            case XmlNodeType.Text:
            case XmlNodeType.Whitespace:
                Type = 2;
                break;
            case XmlNodeType.Comment: return false;
            default: throw new Exception($"Unknown XML Node Type: " + node.NodeType);
        }

        if (parent != null)
            bw.Write(Type);


        string TagName = "";
        switch (Type)
        {
            case 1:
                {
                    TagName = node.Name;

                    int OffsetAttributeCount = (int)oStream.Position;
                    int AttributeCount = 0;
                    bw.Write(AttributeCount);

                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        WriteText(bw, attribute.Name);
                        WriteText(bw, attribute.Value);

                        AttributeCount++;
                    }

                    int OffsetCurrent = (int)oStream.Position;
                    oStream.Position = OffsetAttributeCount;
                    bw.Write(AttributeCount);
                    oStream.Position = OffsetCurrent;
                    break;
                }
            case 2:
                {
                    WriteText(bw, node.Value);
                    break;
                }
        }


        bool Closed = true;
        bw.Write(Closed);

        //Tag
        WriteText(bw, TagName);


        int OffsetChildCount = (int)oStream.Position;
        int ChildCount = 0;

        bw.Write(ChildCount);
        bw.Write(AutoID);
        AutoID++;

        foreach (XmlNode child in node.ChildNodes)
        {
            if (WriteNode(oStream, child))
                ChildCount++;
        }

        int OffsetCurrent2 = (int)oStream.Position;
        oStream.Position = OffsetChildCount;
        bw.Write(ChildCount);
        oStream.Position = OffsetCurrent2;
        return true;
    }


    private string ReadText(BinaryReader reader)
    {
        int size = reader.ReadInt32();
        byte[] buffer = reader.ReadBytes(size * 2);
        Xor(buffer, buffer.Length);

        return Encoding.Unicode.GetString(buffer);
    }

    private void WriteText(BinaryWriter writer, string Text)
    {
        int size = Text.Length;
        writer.Write(size);

        byte[] buffer = Encoding.Unicode.GetBytes(Text);
        Xor(buffer, buffer.Length);
        writer.Write(buffer);
    }
    #endregion




    public enum BXML_TYPE
    {
        BXML_PLAIN,
        BXML_BINARY,
        BXML_UNKNOWN
    };

    public static BXML_TYPE DetectType(Stream iStream)
    {
        int offset = (int)iStream.Position;
        iStream.Position = 0;
        byte[] Signature = new byte[13];

        iStream.Read(Signature, 0, 13);
        iStream.Position = offset;

        if (BitConverter.ToString(Signature).Replace("-", "").Replace("00", "").Contains(BitConverter.ToString("<?xml"u8.ToArray()).Replace("-", "")))
        {
            return BXML_TYPE.BXML_PLAIN;
        }
        else if (Signature[7] == 'B' && Signature[6] == 'L' && Signature[5] == 'S' && Signature[4] == 'O' && Signature[3] == 'B' && Signature[2] == 'X' && Signature[1] == 'M' && Signature[0] == 'L')
        {
            return BXML_TYPE.BXML_BINARY;
        }

        return BXML_TYPE.BXML_UNKNOWN;
    }

    public static MemoryStream Convert(Stream iStream, BXML_TYPE oType, byte[] XorKey = null)
    {
        BXML_TYPE iType = DetectType(iStream);

        var oStream = new MemoryStream();
        if (iType == BXML_TYPE.BXML_PLAIN && oType == BXML_TYPE.BXML_BINARY ||
            iType == BXML_TYPE.BXML_BINARY && oType == BXML_TYPE.BXML_PLAIN)
        {
            var bns_xml = new BXML_CONTENT(XorKey);
            bns_xml.Read(iStream, iType);
            bns_xml.Write(oStream, oType);
        }
        else iStream.CopyTo(oStream);

        return oStream;
    }
}