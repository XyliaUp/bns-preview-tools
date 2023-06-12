using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

using Xylia.Extension;

namespace Xylia.Preview.Data.Models.DatData.Third
{
	public static class MySpport
    {
        public static byte[] RSA2 => bnscompression.GetRSAKeyBlob(Convert.FromBase64String("AQAB"),
                Convert.FromBase64String("6frEEJqRXEuy/ttKNKxRZZdvqAgeSi0yDwMzMu4lZhtq4/sbojbQH2zkcsEUz6PJ7Ab9Zty2EuBDO1ZJoYN2Y0i1Pvi+avGGJbwTuHuPag352hxHwVPbBXZ//koxlL4J1J9FQKtEWHBCRkDM7UYVBkCQb5I6k9fEtyJejrzdmgk="),
                Convert.FromBase64String("8mJvLTFUS3w15GT+//Ok7xSZlO2SRtypmhcCrtAFUGDbmjmIT9Wg8Ll353yDGzFxZIVmiMblgdMrRnc1d7pf6Q=="),
                Convert.FromBase64String("9x93N77SSk7vsZdzuS9eutc+zMKxk5fBYqndgK6gm8+mSfKWBm4CCKVWXNeuhIYtsgSOBeix5nYvjkymVpw1IQ=="));

        public static byte[] RSA3 => bnscompression.GetRSAKeyBlob(Convert.FromBase64String("AQAB"),
                 Convert.FromBase64String("4n/9xPwCpn2/TGXY0bCc23xXKdU9iobCl2RCMWTDgz17uh+Jl8W+Jvci+apyTyXDYdQH8nh2SKkUpAYsQy8bA9v8k+ZbYDytp/DAcHKBfY/1ccknJQrWStbzxQwRXSGsmWmY0vwCW2K7iTkWGQbxo0qRG/L10/qDXQLxf7bmyE8="),
                 Convert.FromBase64String("6fXfeI2dhsT173QVhtOpYLqEQazrsf9opL8cps8j6XH5AzGpRDh0PePoXzhWTZA36nbyEJY0yqDrrBVBEQwRnQ=="),
                 Convert.FromBase64String("99Y0G0QkA62/hFBFmg5fI4vdsesCYGMZw+QwhdSJW87Z5fTZ8r8PamYzNudQPeiJhgQgAVjpFBG7K6Um6JRj2w=="));





        public class PackParam
        {
            public string FolderPath;

            public string PackagePath;

            public bool Bit64;

            public byte[] Aes;

            //public byte[] Xor;

            public bnscompression.CompressionLevel CompressionLevel = bnscompression.CompressionLevel.Normal;
        }

        public static void Pack(PackParam param, ConcurrentDictionary<string, byte[]> replaces = null, bool IgnoreExist = false)
        {
            #region Initialize
            if (string.IsNullOrWhiteSpace(param.FolderPath) && string.IsNullOrWhiteSpace(param.PackagePath))
                throw new ArgumentException("请填写dat路径和文件夹路径");

            else if (string.IsNullOrWhiteSpace(param.PackagePath))
            {
                var dir = new DirectoryInfo(param.FolderPath);
                param.PackagePath = dir.Parent.FullName + "\\" + dir.Name + ".dat";
            }

            else if (string.IsNullOrWhiteSpace(param.FolderPath))
                param.FolderPath = Path.GetDirectoryName(param.PackagePath) + @"\Export\" + Path.GetFileNameWithoutExtension(param.PackagePath) + @"\files";
            #endregion

            #region 替换文件
            if (replaces != null)
            {
                Console.WriteLine();

                //获取文件列表
                var FilePath = new BNSDat(param.PackagePath, param.Bit64).FileTableList.Select(Fte => Fte.FilePath).ToList();
                foreach (var replace in replaces)
                {
                    #region 判断需要修改的目标
                    //需要修改的对象
                    var Target = new List<string>();

                    //如果直接包含
                    if (FilePath.Contains(replace.Key)) Target.Add(replace.Key);

                    //进行通配符匹配
                    else if (replace.Key.Contains("*") || replace.Key.Contains("?"))
                    {
                        Target.AddRange(FilePath.Where(f => f.RegexMatch(replace.Key)));
                    }

                    else if (IgnoreExist) Target.Add(replace.Key);

                    if (!Target.Any()) continue;
                    #endregion

                    foreach (var t in Target)
                    {
                        File.WriteAllBytes(param.FolderPath + "\\" + t, replace.Value);
                    }
                }
            }
            #endregion

            #region 压缩文件
            string DllName = nameof(Properties.Resource.bnscompression) + ".dll";
            if (!File.Exists(DllName)) File.WriteAllBytes(DllName, Properties.Resource.bnscompression);

            var rsa = RSA3;
            double value = bnscompression.CreateFromDirectory(param.FolderPath, param.PackagePath, param.Bit64, param.CompressionLevel,
                param.Aes, (uint)param.Aes.Length, rsa, (uint)rsa.Length,
                bnscompression.BinaryXmlVersion.Version4, delegate (string fileName, ulong fileSize)
                {
                    return bnscompression.DelegateResult.Continue;
                });
            #endregion
        }
    }
}