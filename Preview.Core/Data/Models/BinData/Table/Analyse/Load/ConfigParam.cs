using System.Collections.Generic;
using Xylia.Preview.Data.Models.BinData.Analyse.Struct.Seq;

namespace Xylia.Preview.Data.Models.BinData.Analyse.Load
{
    /// <summary>
    /// 配置信息
    /// </summary>
    public sealed class ConfigParam
    {
        public Dictionary<string, SeqInfo> PublicSeq;
    }
}