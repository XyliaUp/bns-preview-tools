using BnsBinTool.Core.Models;

using Xylia.Extension;

namespace Xylia.Preview.Data.Models.DatData;
public static class Extension
{
	#region Extract
	public static void Extract(this BNSDat bNSDat, string OutPath)
	{
		Parallel.ForEach(bNSDat.FileTable, df =>
		{
			string path = Path.Combine(OutPath, df.FilePath);
			Directory.CreateDirectory(Path.GetDirectoryName(path));

			df.Decrypt();
			File.WriteAllBytes(path, df.Data);
		});

		GC.Collect();
	}

	public static Datafile ExtractBin(this BNSDat bNSDat)
	{
		var file = bNSDat.FileTable.Find(f => f.FilePath.RegexMatch(".*?.bin"));
		ArgumentNullException.ThrowIfNull(file);

		file.Decrypt();
		var datafile = Datafile.ReadFromBytes(file.Data, is64Bit: bNSDat.Bit64);

		file.Dispose();
		return datafile;
	}
	#endregion


	#region Compress
	public static void CompressFiles(this BNSDat bNSDat, Dictionary<string, byte[]> Replaces, byte compression = 9)
	{
		throw new NotImplementedException();

		//#region 生成数据段
		//BinaryWriter DataWriter = new(new MemoryStream());
		//for (int i = 0; i < bNSDat.FileCount; i++)
		//{
		//	#region 获取修改目标
		//	var _file = bNSDat.FileTableList[i];
		//	string TargetFile = null;
		//	if (_file.FilePath != null)
		//	{
		//		if (Replaces.ContainsKey(_file.FilePath)) TargetFile = _file.FilePath;
		//		else if (_file.FilePath.Contains(".bin") && Replaces.ContainsKey(".*?.bin")) TargetFile = ".*?.bin";
		//	}
		//	#endregion

		//	//未修改时读取内部原数据
		//	if (TargetFile is null)
		//	{
		//		lock (bNSDat.DatInfo.br)
		//		{
		//			bNSDat.DatInfo.br.BaseStream.Position = _file.FileDataOffset;
		//			if (bNSDat.DatInfo.br.BaseStream.Position < _file.FileDataOffset)
		//				throw new Exception("数据异常");

		//			var buffer_packed = bNSDat.DatInfo.br.ReadBytes(_file.FileDataSizeStored);

		//			_file.FileDataOffset = (int)DataWriter.BaseStream.Position;
		//			DataWriter.Write(buffer_packed);
		//		}
		//	}
		//	else
		//	{
		//		//获取传递的数据
		//		byte[] OriginalData = Replaces[TargetFile];

		//		//对于文本类型需要进行XOR处理
		//		if (_file.FilePath.EndsWith(".xml") || _file.FilePath.EndsWith(".x16"))
		//		{
		//			//BXML bXML = new BXML(bNSDat.KeyInfo.XOR_KEY);
		//			var fis = new MemoryStream(OriginalData);
		//			OriginalData = BNSDat.Convert(fis, BXML.DetectType(fis), BXML_TYPE.BXML_BINARY, bNSDat.KeyInfo.XOR_KEY).ToArray();
		//		}


		//		_file.FileDataSizeUnpacked = OriginalData.Length;

		//		var buffer_packed = BNSDat.Pack(OriginalData, _file.FileDataSizeUnpacked, out _file.FileDataSizeSheared, out _file.FileDataSizeStored, _file.IsEncrypted, _file.IsCompressed, compression, bNSDat.KeyInfo.Correct);
		//		OriginalData = null;

		//		_file.FileDataOffset = (int)DataWriter.BaseStream.Position;
		//		DataWriter.Write(buffer_packed);
		//		buffer_packed = null;

		//		Console.WriteLine($"已修改 { _file.FilePath }");
		//	}
		//}
		//#endregion

		//#region 存储文件
		//var data = Test(bNSDat, bNSDat.FileTableList, DataWriter, bNSDat.Bit64, compression);

		//try
		//{
		//	File.WriteAllBytes(bNSDat.DatPath, data);
		//}
		//catch
		//{
		//	File.WriteAllBytes(bNSDat.DatPath + ".tmp", data);
		//	Console.WriteLine("\n由于文件目前正在占用，已变更为创建临时文件。待退出游戏后将.tmp后缀删除即可。\n");
		//}

		//data = null;
		//GC.Collect();
		//#endregion
	}
	#endregion
}