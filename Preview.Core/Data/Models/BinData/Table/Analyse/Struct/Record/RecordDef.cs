using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using BnsBinTool.Core.Definitions;

using Xylia.Preview.Data.Models.BinData.Analyse.Enums;
using Xylia.Preview.Data.Models.BinData.Analyse.Struct.Seq;

namespace Xylia.Preview.Data.Models.BinData.Analyse.Struct.Record
{
	public sealed class RecordDef : ICloneable
    {
        #region Fields
        public int TableIndex;



        public string Alias;

        public bool Deprecated;

        public AttributeType Type;

        public ushort Offset;

        public ushort Size;

        public ushort Repeat;

        public bool IsKey;






        /// <summary>
        /// 类型设定
        /// </summary>
        public List<ushort> Filter = new();

        /// <summary>
        /// 指示是服务端需要属性 （不输入默认 true)
        /// </summary>
        public bool Server { get; set; } = true;

        /// <summary>
        /// 指示是客户端需要属性 （不输入默认 true)
        /// </summary>
        public bool Client { get; set; } = true;

        /// <summary>
        /// 默认值设定
        /// </summary>
        public DefaultInfo DefaultInfo;

        /// <summary>
        /// 错误的处理类型
        /// </summary>
        public ErrorType ErrorType;
        #endregion


        #region 偏移数据
        /// <summary>
        /// 当前结束索引
        /// </summary>
        public int EndIndex => Offset + Size * Repeat;

        /// <summary>
        /// 获取Fields起始偏移
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public int GetOffset(int Index = 0) => Offset + Size * Index;

        public string GetAlias(int Index = 0) => Alias + (Repeat == 1 ? null : "-" + Index);
        #endregion

        #region 输出控制
        /// <summary>
        /// 使用过滤关联限制条件
        /// 激活时，满足配置的关联条件时才会输出
        /// 默认关闭
        /// </summary>
        public Condition.Base.Condition OutCond;

        /// <summary>
        /// 服务端输入条件
        /// </summary>
        public Condition.Base.Condition ValidCond;



        /// <summary>
        /// 指示可以输出
        /// </summary>
        public bool CanOutput { get; set; } = true;

        /// <summary>
        /// 指示可以输入
        /// </summary>
        public bool CanInput { get; set; } = true;

        /// <summary>
        /// 当表存在全局清除设置时，不进行清理
        /// </summary>
        public bool NotRuleDispel { get; set; } = false;

        /// <summary>
        /// 当存在清除标记时，清理当前Fields
        /// </summary>
        public bool RuleDispel { get; set; } = false;

        /// <summary>
        /// 服务端应用属性方式
        /// </summary>
        public ApplyMode ApplyServer;




        /// <summary>
        /// 显示错误信息
        /// </summary>
        public bool UseInfo => ErrorType == ErrorType.Warning || ErrorType == ErrorType.Error;
        #endregion




        #region 数值处理
        /// <summary>
        /// 最大值 (目前仅short类型可用)
        /// </summary>
        public long MaxValue = -1;

        /// <summary>
        /// 最小值
        /// </summary>
        public long MinValue = -1;
        #endregion

        #region 引用信息 (Ref类型)
        public RefInfo RefInfo;

        /// <summary>
        /// 指示是否存在外键信息
        /// 判断有无外键信息或者组信息，然后判断LevelStart是否存在
        /// </summary>
        public bool UseRef => RefInfo?.RefTable != null;
        #endregion

        #region 枚举信息 (Seq类型)
        /// <summary>
        /// 枚举列表
        /// </summary>
        public SeqInfo Seq { get; set; }

        /// <summary>
        /// 是否有枚举
        /// </summary>
        public bool HasSeq => Seq != null;
        #endregion



        #region ICloneable
        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <returns></returns>
        public RecordDef DeepClone()
        {
            using var objectStream = new MemoryStream();

            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(objectStream, this);
            objectStream.Seek(0, SeekOrigin.Begin);
            return formatter.Deserialize(objectStream) as RecordDef;
        }

        public object Clone()
        {
            //rc.OutCond = ((Condition)r.OutCond.Clone());

            return MemberwiseClone();
        }
        #endregion
    }
}