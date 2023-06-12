using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Xylia.Preview.Data.Models.BinData.Analyse.Struct.Seq
{
	public class SeqInfo<SeqCell> : List<SeqCell>, ICloneable where SeqCell : Seq.SeqCell
    {
        #region Constructor
        public SeqInfo() : base()
        {

        }

        public SeqInfo(IEnumerable<SeqCell> collection) : base(collection)
        {

        }
        #endregion

        #region 哈希表
        private readonly Hashtable ht_alias = new(StringComparer.Create(CultureInfo.InvariantCulture, true));

        private readonly Hashtable ht_id = new();
        #endregion


        #region Fields
        /// <summary>
        /// 字典名
        /// </summary>
        public string Name;

        /// <summary>
        /// 默认值
        /// </summary>
        public SeqCell DefaultCell;


        /// <summary>
        /// 错误缓存信息
        /// KEY: 错误代码  VALUE: 错误详细信息
        /// </summary>
        public ConcurrentDictionary<string, string> MsgCache = new();
        #endregion



        #region Functions
        public new void Add(SeqCell item)
        {
            if (item.Key >= 0 && !string.IsNullOrWhiteSpace(item.Alias))
            {
                if (!ht_alias.ContainsKey(item.Alias)) ht_alias.Add(item.Alias, item);
                else LogWriter.WriteLine($"== CRASH == 枚举Initialize错误，别名重复 ({Name} => {item.Alias})");
            }

            if (!ht_id.ContainsKey(item.Key)) ht_id.Add(item.Key, item);
            else LogWriter.WriteLine($"== CRASH == 枚举Initialize错误，主键重复 [{Name} => {item.Alias} ({item.Key})]");

            base.Add(item);
        }

        public new void AddRange(IEnumerable<SeqCell> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));

            foreach (var item in items) Add(item);
        }

        public new bool Remove(SeqCell item)
        {
            if (ht_alias.ContainsKey(item.Alias)) ht_alias.Remove(item.Alias);
            if (ht_id.ContainsKey(item.Key)) ht_id.Remove(item.Key);

            return base.Remove(item);
        }

        public new void Clear()
        {
            base.Clear();

            ht_id.Clear();
            ht_alias.Clear();
        }


        public bool ContainsAlias(string alias) => ht_alias.ContainsKey(alias);

        public bool ContainsKey(short key) => ht_id.ContainsKey(key);

        public SeqCell GetCell(string Alias) => ht_alias.ContainsKey(Alias) ? ht_alias[Alias] as SeqCell : null;

        public SeqCell GetCell(short Key) => ht_id.ContainsKey(Key) ? ht_id[Key] as SeqCell : null;



        /// <summary>
        /// 通过数值获取对象信息
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string GetValue(short Key)
        {
            if (Count == 0) return Key.ToString();

            #region 返回枚举查询结果
            var SeqCell = GetCell(Key);
            if (SeqCell != null) return SeqCell.Alias;
            #endregion

            #region 获取失败处理
            //由于大部分0都是作为空值的，所以不应输出枚举错误提示
            //bool UseTip = Key is short @short && @short != 0;
            //(Val is long @long && @long != 0) ||
            //(Val is short @short && @short != 0) ||
            //(Val is byte @byte && @byte != 0) ||
            //(Val is sbyte @sbyte && @sbyte != 0);
            if (Key != 0)
            {
                string ErrorKEY = ErrorCode.LACK_SEQ_META + "_" + Key;
                if (!MsgCache.ContainsKey(ErrorKEY))
                {
                    string ErrorMsg = $"枚举 {Name} 中不包含: {Key}";
                    Console.WriteLine(ErrorMsg);
                    LogWriter.WriteLine(ErrorMsg);

                    MsgCache[ErrorKEY] = ErrorMsg;
                }
            }

            //如果要求返回主键，则返回Null
            //其他情况返回查询值
            return Key.ToString();
            #endregion
        }

        /// <summary>
        /// 获取主键
        /// </summary>
        /// <param name="Alias"></param>
        /// <param name="Key"></param>
        /// <param name="ShowMsg"></param>
        /// <returns></returns>
        public bool GetKey(string Alias, out int Key, bool ShowMsg = true)
        {
            var KeyInfo = GetCell(Alias);
            if (KeyInfo != null)
            {
                Key = KeyInfo.Key;
                return true;
            }
            else if (ShowMsg)
            {
                string ErrorKEY = ErrorCode.LACK_SEQ_META + "_" + Alias;
                if (!MsgCache.ContainsKey(ErrorKEY))
                {
                    string ErrorMsg = $"获取枚举信息失败 ({Name} => {Alias})";
                    Console.WriteLine(ErrorMsg);
                    LogWriter.WriteLine(ErrorMsg);

                    MsgCache[ErrorKEY] = ErrorMsg;
                }
            }

            Key = default;
            return false;
        }
        #endregion


        #region ICloneable
        public SeqCell DeepClone()
        {
            using var objectStream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(objectStream, this);
            objectStream.Seek(0, SeekOrigin.Begin);

            return formatter.Deserialize(objectStream) as SeqCell;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
        #endregion
    }

    /// <summary>
    /// 枚举信息
    /// </summary>
    public sealed class SeqInfo : SeqInfo<SeqCell> { }


    public enum ErrorCode
    {
        /// <summary>
        /// 缺少枚举成员
        /// </summary>
        LACK_SEQ_META,

        /// <summary>
        /// 缺少类型成员
        /// </summary>
        LACK_TYPE_META,
    }
}
