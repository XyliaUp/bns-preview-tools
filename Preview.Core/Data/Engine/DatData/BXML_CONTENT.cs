using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Xylia.Preview.Data.Engine.DatData;
public class BXML_CONTENT
{
	#region Fields
	private XmlDocument Nodes = new();

	int AutoID;
	byte[] Signature = "LMXBOSLB"u8.ToArray();
	uint Version;
	int FileSize;
	byte[] Padding;
	bool Unknown;
	string OriginalPath;
	#endregion

	#region XOR	  
	public BXML_CONTENT(byte[] XorKey) => XOR_KEY = XorKey;


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
	#endregion


	#region Methods
	public void Read(byte[] buffer)
	{
		using var br = new BinaryReader(new MemoryStream(buffer));
		Signature = br.ReadBytes(8);
		Version = br.ReadUInt32();
		FileSize = br.ReadInt32();
		Padding = br.ReadBytes(64);
		Unknown = br.ReadByte() == 1;
		OriginalPath = ReadText(br);
		AutoID = 1;
		ReadNode(br, Nodes);

		// add original path as first comment node
		Nodes.DocumentElement.PrependChild(Nodes.CreateComment(" " + OriginalPath + " "));
		Nodes.PrependChild(Nodes.CreateXmlDeclaration("1.0", "utf-8", null));

		// check size
		if (FileSize != br.BaseStream.Position)
			throw new Exception(string.Format("Filesize Mismatch, expected size was {0} while actual size was {1}.", FileSize, br.BaseStream.Position));
	}

	private void ReadNode(BinaryReader br, XmlNode parent)
	{
		#region Type
		XmlNode node;

		int Type = 1;
		if (parent.NodeType != XmlNodeType.Document) Type = br.ReadInt32();
		#endregion

		#region Tag
		switch (Type)
		{
			// Element
			case 1:
			{
				int ParameterCount = br.ReadInt32();
				var attributes = new KeyValuePair<string, string>[ParameterCount];
				for (int i = 0; i < ParameterCount; i++)
				{
					string Name = ReadText(br);
					string Value = ReadText(br);

					attributes[i] = new KeyValuePair<string, string>(Name, Value);
				}


				bool Closed = br.ReadByte() == 1;
				string Tag = ReadText(br);

				node = Nodes.CreateElement(Tag);
				foreach (var attr in attributes)
					((XmlElement)node).SetAttribute(attr.Key, attr.Value);
			}
			break;

			// Text
			case 2:
			{
				var Text = ReadText(br);
				node = Nodes.CreateTextNode(Text);

				bool Closed = br.ReadByte() == 1;
				string Tag = ReadText(br);
			}
			break;

			default: throw new Exception($"Unknown XML Node Type: " + Type);
		}
		#endregion

		#region Child
		int ChildCount = br.ReadInt32();
		AutoID = br.ReadInt32();

		for (int i = 0; i < ChildCount; i++)
			ReadNode(br, node);

		parent.AppendChild(node);
		#endregion
	}

	private string ReadText(BinaryReader reader)
	{
		int size = reader.ReadInt32();
		byte[] buffer = reader.ReadBytes(size * 2);
		Xor(buffer, buffer.Length);

		return Encoding.Unicode.GetString(buffer);
	}


	public byte[] Write()
	{
		using var ms = new MemoryStream();
		using var bw = new BinaryWriter(ms);
		bw.Write(Signature);
		bw.Write(Version);
		bw.Write(FileSize);
		bw.Write(Padding);
		bw.Write(Unknown);
		WriteText(bw, OriginalPath);

		AutoID = 1;
		WriteNode(bw, Nodes.DocumentElement);

		// set size
		FileSize = (int)ms.Position;
		bw.BaseStream.Position = 12;
		bw.Write(FileSize);

		return ms.ToArray();
	}

	private bool WriteNode(BinaryWriter writer, XmlNode node)
	{
		#region Type
		int Type;
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

		if (writer is null) return true;
		if (node != Nodes.DocumentElement) writer.Write(Type);
		#endregion

		#region Tag
		string TagName = "";
		switch (Type)
		{
			case 1:
			{
				TagName = node.Name;

				int ParameterCount = node.Attributes.Count;
				writer.Write(ParameterCount);

				foreach (XmlAttribute attribute in node.Attributes)
				{
					WriteText(writer, attribute.Name);
					WriteText(writer, attribute.Value);
				}
			}
			break;

			case 2:
			{
				TagName = "text";
				WriteText(writer, node.Value);
			}
			break;
		}

		bool Closed = true;
		writer.Write(Closed);

		//Tag
		WriteText(writer, TagName);
		#endregion

		#region Child
		var children = new List<XmlNode>();
		foreach (XmlNode child in node.ChildNodes)
		{
			if (WriteNode(null, child))
				children.Add(child);
		}


		writer.Write(children.Count);
		writer.Write(AutoID++);

		foreach (XmlNode child in children)
			WriteNode(writer, child);
		#endregion

		return true;
	}

	private void WriteText(BinaryWriter writer, string Text)
	{
		int size = Text.Length;
		writer.Write(size);

		byte[] buffer = Encoding.Unicode.GetBytes(Text);
		Xor(buffer, buffer.Length);
		writer.Write(buffer);
	}


	/// <summary>
	/// convert from <see cref="BXML_CONTENT"/> to text buffer
	/// </summary>
	/// <returns></returns>
	public byte[] ConvertToString()
	{
		using var oStream = new MemoryStream();
		using var sw = new StreamWriter(oStream, new UTF8Encoding(false));

		this.Nodes.Save(sw);
		sw.Close();

		return oStream.ToArray();
	}

	/// <summary>
	/// convert to <see cref="BXML_CONTENT"/>
	/// </summary>
	/// <param name="source">source text buffer</param>
	public void ConvertFrom(byte[] source)
	{
		Signature = "LMXBOSLB"u8.ToArray();
		Version = 3;
		Padding = new byte[64];
		Unknown = true;
		OriginalPath = "";

		// NOTE: keep whitespace text nodes (to be compliant with the whitespace TEXT_NODES in bns xml)
		// no we don't keep them, we remove them because it is cleaner
		Nodes = new();
		Nodes.PreserveWhitespace = true;
		Nodes.Load(new MemoryStream(source));

		// get original path from first comment node
		XmlComment comment = Nodes.DocumentElement.ChildNodes.OfType<XmlComment>().FirstOrDefault();
		if (comment != null)
		{
			OriginalPath = comment.InnerText?.Trim();

			//original path not the real node for doc
			if (Nodes.PreserveWhitespace && comment.NextSibling.NodeType == XmlNodeType.Whitespace)
			{
				XmlWhitespace whitespace = (XmlWhitespace)comment.NextSibling;
				whitespace.Value = new Regex("\n").Replace(whitespace.Value, "", 1).TrimStart(' ');
			}

			Nodes.DocumentElement.RemoveChild(comment);
		}
	}
	#endregion
}