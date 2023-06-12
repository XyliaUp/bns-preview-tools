using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Ionic.Zlib;

using Xylia.Extension;
using Xylia.Preview.Data.Models.DatData.DatDetect;

using static Xylia.Preview.Data.Models.DatData.BXML_CONTENT;
using static Xylia.Preview.Data.Models.DatData.Third.MySpport;

namespace Xylia.Preview.Data.Models.DatData
{
	public sealed class BNSDat
    {
        #region Constructor
        public bool Bit64;

        public KeyInfo KeyInfo = new();


        private BNSDat()
        {

        }

        public BNSDat(string DatPath, bool? Is64 = null, byte[] AES = null)
        {
            if (AES != null) KeyInfo.AES_KEY.Add(AES);
            Bit64 = Is64 ?? DatPath.Judge64Bit();

            Read(DatPath, Bit64);
        }
        #endregion

        #region DatInfo
        /// <summary>
        /// 2020年5月引入的认证字符
        /// </summary>
        public byte[] Auth;

        public byte[] Signature;
        public static byte[] Magic => new byte[8] { (byte)'U', (byte)'O', (byte)'S', (byte)'E', (byte)'D', (byte)'A', (byte)'L', (byte)'B' };


        public uint Version;

        public byte[] Unknown_001;
        public byte[] Unknown_002;

        public int FileDataSizePacked;

        public bool IsCompressed;
        public bool IsEncrypted;

        public int FileTableSizePacked;
        public int FileTableSizeUnpacked;
        public byte[] FileTableUnpacked;

        public int OffsetGlobal;

        /// <summary>
        /// 文件列表
        /// </summary>
        public List<BPKG_FTE> FileTableList = new();

        public IEnumerable<BPKG_FTE> EnumerateFiles(string searchPattern)
        {
            searchPattern = searchPattern
                .Replace("/", "\\")
                .Replace("\\", "\\\\")
                .Replace("*", ".*?");

            return FileTableList.Where(o => o.FilePath.RegexMatch(searchPattern));
        }
        #endregion


        #region Functions
        private void Read(string FileName, bool Is64)
        {
            if (!File.Exists(FileName))
                return;
            //throw new ArgumentException("请选择文件路径");

            #region 读取基础数据
            using var br = new BinaryReader(new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            Signature = br.ReadBytes(8);
            Version = br.ReadUInt32();
            Unknown_001 = br.ReadBytes(5);
            FileDataSizePacked = Is64 ? (int)br.ReadInt64() : br.ReadInt32();
            var FileCount = Is64 ? (int)br.ReadInt64() : br.ReadInt32();
            IsCompressed = br.ReadBoolean();
            IsEncrypted = br.ReadBoolean();

            // Update 200429
            if (Version == 3)
            {
                Auth = br.ReadBytes(128);
            }


            Unknown_002 = br.ReadBytes(62);

            FileTableSizePacked = Is64 ? (int)br.ReadInt64() : br.ReadInt32();
            FileTableSizeUnpacked = Is64 ? (int)br.ReadInt64() : br.ReadInt32();

            byte[] FileTablePacked = br.ReadBytes(FileTableSizePacked);

            //不要相信数值，请读取当前流位置
            OffsetGlobal = Is64 ? (int)br.ReadInt64() : br.ReadInt32();
            OffsetGlobal = (int)br.BaseStream.Position;
            #endregion

            #region 读取文件描述信息
            byte[] FileTableUnpacked = Unpack(FileTablePacked, FileTableSizePacked, FileTableSizePacked, FileTableSizeUnpacked, IsEncrypted, IsCompressed, KeyInfo);
            FileTablePacked = null;

            using BinaryReader br2 = new(new MemoryStream(FileTableUnpacked));
            FileTableUnpacked = null;

            //读取文件描述信息
            FileTableList = new List<BPKG_FTE>();
            for (int i = 0; i < FileCount; i++)
                FileTableList.Add(new BPKG_FTE(br2, Is64, OffsetGlobal) { KeyInfo = KeyInfo });

            //读取文件数据
            Parallel.ForEach(FileTableList, BPKG_FTE =>
            {
                lock (br)
                {
                    br.BaseStream.Position = BPKG_FTE.FileDataOffset;
                    BPKG_FTE.Data = br.ReadBytes(BPKG_FTE.FileDataSizeStored);
                }
            });
            #endregion
        }

        public static void Pack(PackParam param)
        {
            ArgumentNullException.ThrowIfNull(param.Aes);
            var Level = (byte)(3 * (byte)param.CompressionLevel);


            #region 获取文件数据
            List<BPKG_FTE> list = new();
            foreach (string File in Directory.EnumerateFiles(param.FolderPath, "*", SearchOption.AllDirectories))
            {
                list.Add(new BPKG_FTE()
                {
                    FilePath = File,
                    Unknown_001 = 2,
                    IsCompressed = true,
                    IsEncrypted = true,
                    Unknown_002 = 0,
                    FileDataSizeUnpacked = 0,
                    Padding = new byte[60],
                });
            }

            Parallel.ForEach(list, _file =>
            {
                using FileStream fis = new(_file.FilePath, FileMode.Open);

                var tmp = new MemoryStream();
                if (_file.FilePath.EndsWith(".xml") || _file.FilePath.EndsWith(".x16")) tmp = Convert(fis, BXML_TYPE.BXML_BINARY);
                else fis.CopyTo(tmp);

                // 原始数据
                var originalData = tmp.ToArray();
                _file.FileDataSizeUnpacked = originalData.Length;
                tmp.Close();
                tmp = null;

                // 转换数据
                _file.FilePath = _file.FilePath.Replace(param.FolderPath, "").TrimStart('\\');
                _file.Data = Pack(originalData, _file.FileDataSizeUnpacked, out _file.FileDataSizeSheared, out _file.FileDataSizeStored, _file.IsEncrypted, _file.IsCompressed, Level, param.Aes);

                originalData = null;
            });
            #endregion


            #region 生成头部数据
            var is64 = param.Bit64;

            BinaryWriter bw = new(new MemoryStream());
            bw.Write(Magic);

            int Version = 3;
            bw.Write(Version);

            byte[] Unknown_001 = new byte[5] { 0, 0, 0, 0, 0 };
            bw.Write(Unknown_001);

            // size
            int FileDataSizePacked = 0;
            if (is64) bw.Write((long)FileDataSizePacked);
            else bw.Write(FileDataSizePacked);

            // count
            if (is64) bw.Write((long)list.Count);
            else bw.Write(list.Count);


            bool IsCompressed = true;
            bw.Write(IsCompressed);
            bool IsEncrypted = true;
            bw.Write(IsEncrypted);


            if (Version == 3)
            {
                var Auth = RSA3;
                bw.Write(new byte[128]);
            }

            byte[] Unknown_002 = new byte[62];
            bw.Write(Unknown_002);
            #endregion

            #region 生成BKG数据
            int FileDataOffset = 0;
            BinaryWriter mosTable = new(new MemoryStream());
            foreach (BPKG_FTE item in list)
            {
                item.FileDataOffset = FileDataOffset;
                byte[] FilePath = Encoding.Unicode.GetBytes(item.FilePath);


                if (is64) mosTable.Write((long)item.FilePath.Length);
                else mosTable.Write(item.FilePath.Length);

                mosTable.Write(FilePath);
                mosTable.Write(item.Unknown_001);
                mosTable.Write(item.IsCompressed);
                mosTable.Write(item.IsEncrypted);
                mosTable.Write(item.Unknown_002);

                if (is64) mosTable.Write((long)item.FileDataSizeUnpacked);
                else mosTable.Write(item.FileDataSizeUnpacked);

                if (is64) mosTable.Write((long)item.FileDataSizeSheared);
                else mosTable.Write(item.FileDataSizeSheared);

                if (is64) mosTable.Write((long)item.FileDataSizeStored);
                else mosTable.Write(item.FileDataSizeStored);

                if (is64) mosTable.Write((long)item.FileDataOffset);
                else mosTable.Write(item.FileDataOffset);

                FileDataOffset += item.FileDataSizeStored;


                mosTable.Write(item.Padding);
            }

            int FileTableSizeUnpacked = (int)mosTable.BaseStream.Length;
            int FileTableSizeSheared = FileTableSizeUnpacked;
            int FileTableSizePacked = FileTableSizeUnpacked;

            var buffer_unpacked = mosTable.BaseStream.ToBytes();
            mosTable.Dispose();
            mosTable = null;

            var buffer_packed = Pack(buffer_unpacked, FileTableSizeUnpacked, out FileTableSizeSheared, out FileTableSizePacked, IsEncrypted, IsCompressed, Level, param.Aes);
            buffer_unpacked = null;


            if (is64) bw.Write((long)FileTableSizePacked);
            else bw.Write(FileTableSizePacked);

            if (is64) bw.Write((long)FileTableSizeUnpacked);
            else bw.Write(FileTableSizeUnpacked);

            long Pos = bw.BaseStream.Position;

            bw.Write(buffer_packed);
            buffer_packed = null;
            #endregion

            #region 生成内容数据
            int OffsetGlobal = (int)bw.BaseStream.Position + (is64 ? 8 : 4);
            if (is64) bw.Write((long)OffsetGlobal);
            else bw.Write(OffsetGlobal);

            foreach (BPKG_FTE item in list)
            {
                bw.Write(item.Data);
                item.Data = null;
            }

            FileDataSizePacked = (int)(bw.BaseStream.Length - Pos);
            bw.BaseStream.Position = 17;
            bw.Write((long)FileDataSizePacked);
            #endregion



            #region 最后处理
            FileStream fileStream = null;

            try
            {
                fileStream = new(param.PackagePath, FileMode.Open);
            }
            catch
            {
                fileStream = new(param.PackagePath + ".tmp", FileMode.Open);
                Console.WriteLine("由于文件目前正在占用，已变更为创建临时文件。待退出游戏后将.tmp后缀删除即可。\n");
            }

            bw.BaseStream.Position = 0;
            bw.BaseStream.CopyTo(fileStream);
            bw.Dispose();
            bw = null;

            fileStream.Close();
            fileStream = null;

            Console.WriteLine("完成");
            #endregion
        }
        #endregion

        #region Functions
        public static byte[] Decrypt(byte[] buffer, int size, byte[] AES)
        {
            // AES requires buffer to consist of blocks with 16 bytes (each)
            // expand last block by padding zeros if required...
            // -> the encrypted data in BnS seems already to be aligned to blocks

            int AES_BLOCK_SIZE = AES.Length;
            int sizePadded = size + AES_BLOCK_SIZE;

            byte[] output = new byte[sizePadded];
            byte[] tmp = new byte[sizePadded];
            buffer.CopyTo(tmp, 0);
            buffer = null;

            Rijndael aes = Rijndael.Create();
            aes.Mode = CipherMode.ECB;

            ICryptoTransform decrypt = aes.CreateDecryptor(AES, new byte[16]);

            decrypt.TransformBlock(tmp, 0, sizePadded, output, 0);
            tmp = output;
            output = new byte[size];
            Array.Copy(tmp, 0, output, 0, size);
            tmp = null;

            return output;
        }

        public static byte[] Encrypt(byte[] buffer, int size, out int sizePadded, byte[] AESKey)
        {
            #region 获取AES信息
            byte[] AES = AESKey ?? KeyInfo.AES_2020_05;

            int AES_BLOCK_SIZE = AES.Length;
            #endregion

            #region 执行加密
            sizePadded = size + (AES_BLOCK_SIZE - size % AES_BLOCK_SIZE);

            byte[] output = new byte[sizePadded];
            byte[] temp = new byte[sizePadded];


            Array.Copy(buffer, 0, temp, 0, buffer.Length);
            buffer = null;

            Rijndael aes = Rijndael.Create();
            aes.Mode = CipherMode.ECB;

            ICryptoTransform encrypt = aes.CreateEncryptor(AES, new byte[16]);
            encrypt.TransformBlock(temp, 0, sizePadded, output, 0);

            temp = null;
            return output;
            #endregion
        }

        public static byte[] Deflate(byte[] buffer, int sizeDecompressed)
        {
            byte[] tmp = ZlibStream.UncompressBuffer(buffer);
            if (tmp.Length != sizeDecompressed)
            {
                byte[] tmp2 = new byte[sizeDecompressed];
                Array.Copy(tmp, 0, tmp2, 0, Math.Min(sizeDecompressed, tmp.Length));

                tmp = tmp2;
                tmp2 = null;
            }

            return tmp;
        }

        public static byte[] Inflate(byte[] buffer, int sizeDecompressed, byte? compressionLevel = null)
        {
            if (sizeDecompressed == 0)
                sizeDecompressed = buffer.Length;

            var output = new MemoryStream();

            ZlibStream zs = new(output, CompressionMode.Compress, (CompressionLevel)(compressionLevel ?? 6), true);
            zs.Write(buffer, 0, sizeDecompressed);
            zs.Flush();
            zs.Close();

            return output.ToArray();
        }

        public static byte[] Unpack(byte[] buffer, int sizeStored, int sizeSheared, int sizeUnpacked, bool isEncrypted, bool isCompressed, KeyInfo KeyInfo)
        {
            byte[] output = buffer;

            #region 判断正确密钥
            if (KeyInfo.Correct != null) Callback(buffer, sizeStored, sizeSheared, sizeUnpacked, isEncrypted, isCompressed, KeyInfo.Correct, out output);
            else
            {
                foreach (byte[] AES in KeyInfo.AES_KEY)
                {
                    if (Callback(buffer, sizeStored, sizeSheared, sizeUnpacked, isEncrypted, isCompressed, AES, out output))
                    {
                        KeyInfo.Correct = AES;
                        break;
                    }
                }
            }
            #endregion

            return output;
        }

        public static bool Callback(byte[] buffer, int sizeStored, int sizeSheared, int sizeUnpacked, bool isEncrypted, bool isCompressed, byte[] key, out byte[] output)
        {
            output = buffer;

            try
            {
                if (isEncrypted) output = Decrypt(buffer, sizeStored, key);
                if (isCompressed) output = Deflate(output, sizeUnpacked);

                //既不加密，也不压缩 -> 返回原始副本	
                if (!isEncrypted && !isCompressed)
                {
                    output = new byte[sizeUnpacked];
                    Array.Copy(buffer, 0, output, 0, Math.Min(sizeSheared, sizeUnpacked));
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static byte[] Pack(byte[] buffer, int sizeUnpacked, out int sizeSheared, out int sizeStored, bool encrypt, bool compress, byte compressionLevel, byte[] Key)
        {
            byte[] output = buffer;
            buffer = null;

            sizeStored = sizeSheared = sizeUnpacked;

            //如果是压缩过的
            if (compress)
            {
                output = Inflate(output, sizeUnpacked, compressionLevel);

                sizeSheared = output.Length;
                sizeStored = output.Length;
            }

            if (encrypt)
            {
                output = Encrypt(output, output.Length, out sizeStored, Key);
            }

            return output;
        }
        #endregion
    }
}