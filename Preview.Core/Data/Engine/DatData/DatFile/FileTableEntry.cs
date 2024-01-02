﻿using System.Text;

namespace Xylia.Preview.Data.Engine.DatData;
public class FileTableEntry
{
	#region Fields
	public string FilePath;
	public byte Unknown_001;
	public byte Unknown_002;
	public bool IsCompressed;
	public bool IsEncrypted;

	public long FileDataOffset;        // (relative) offset
	public long FileDataSizeSheared;   // without padding for AES
	public long FileDataSizeStored;
	public long FileDataSizeUnpacked;

	public byte[] Padding;
	#endregion

	#region Constructor
	private BNSDat Owner { get; init; }

	internal FileTableEntry(BNSDat owner, DataArchive archive)
	{
		Owner = owner;

		var FilePathLength = (int)archive.ReadLongInt();
		FilePath = Encoding.Unicode.GetString(archive.ReadBytes(FilePathLength * 2));

		Unknown_001 = archive.Read<byte>();
		IsCompressed = archive.Read<bool>();
		IsEncrypted = archive.Read<bool>();
		Unknown_002 = archive.Read<byte>();
		FileDataSizeUnpacked = archive.ReadLongInt();
		FileDataSizeSheared = archive.ReadLongInt();
		FileDataSizeStored = archive.ReadLongInt();
		FileDataOffset = archive.ReadLongInt();
		Padding = archive.ReadBytes(60);
	}

	internal FileTableEntry(BNSDat owner, string path, byte[] data)
	{
		Owner = owner;

		FilePath = path;
		IsCompressed = true;
		IsEncrypted = true;
		Unknown_001 = 2;
		Unknown_002 = 0;
		Padding = new byte[60];
		Data = data;
	}
	#endregion

	#region DATA
	internal DataArchive DataArchive { get; set; }

	private byte[] _data;

	public byte[] Data
	{
		set
		{
			_data = value;
			DataArchive = null;
		}
		get
		{
			if (DataArchive != null)
			{
				var buffer = BNSDat.Unpack(DataArchive.CreateStream().ToArray(), FileDataSizeStored, FileDataSizeSheared, FileDataSizeUnpacked, IsEncrypted, IsCompressed, Owner.Params.AES_KEY);

				if (FilePath.EndsWith(".xml") || FilePath.EndsWith(".x16"))
				{
					var Xml = new BXML_CONTENT(Owner.Params.XOR_KEY);
					Xml.Read(buffer);

					_data = Xml.ConvertToString();
				}
				else _data = buffer;
			}

			return _data;
		}
	}
	#endregion



	public void WriteHeader(BinaryWriter writer, bool Is64bit, CompressionLevel level, ref long FileDataOffset)
	{
		if (DataArchive is null)
		{
			var data = _data;
			if (FilePath.EndsWith(".xml") || FilePath.EndsWith(".x16"))
			{
				var bns_xml = new BXML_CONTENT(Owner.Params.XOR_KEY);
				bns_xml.ConvertFrom(data);
				data = bns_xml.Write();
			}

			FileDataSizeUnpacked = data.Length;
			DataArchive = new DataArchive(BNSDat.Pack(data, FileDataSizeUnpacked, out FileDataSizeSheared, out FileDataSizeStored, IsEncrypted, IsCompressed, level, Owner.Params.AES_KEY), Is64bit);
		}

		byte[] _filePath = Encoding.Unicode.GetBytes(FilePath);
		if (Is64bit) writer.Write((long)FilePath.Length);
		else writer.Write(FilePath.Length);
		writer.Write(_filePath);

		writer.Write(Unknown_001);
		writer.Write(IsCompressed);
		writer.Write(IsEncrypted);
		writer.Write(Unknown_002);

		if (Is64bit)
		{
			writer.Write((long)FileDataSizeUnpacked);
			writer.Write((long)FileDataSizeSheared);
			writer.Write((long)FileDataSizeStored);
			writer.Write((long)FileDataOffset);
		}
		else
		{
			writer.Write((int)FileDataSizeUnpacked);
			writer.Write((int)FileDataSizeSheared);
			writer.Write((int)FileDataSizeStored);
			writer.Write((int)FileDataOffset);
		}

		writer.Write(Padding);
		FileDataOffset += FileDataSizeStored;
	}
}