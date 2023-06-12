using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using Xylia.Extension;

namespace Xylia.Preview.Data.Models.BinData.Analyse.Struct
{
    /// <summary>
    /// 默认值信息结构
    /// </summary>
    public class DefaultInfo
    {
        #region Constructor
        public DefaultInfo(string Value, string CorrectVal)
        {
            this.Value = Value?.Trim();
            ConditionValue = CorrectVal?.Trim();

            //特殊规则设定
            if (Value != null && (Value.StartsWith("$") || Value.Contains("{")))
                Load(Value);
        }
        #endregion



        #region Fields
        /// <summary>
        /// 默认值
        /// </summary>
        public string Value;

        /// <summary>
        /// 满足表达式时应用值，优先级高于普通值
        /// </summary>
        public string ConditionValue;

        /// <summary>
        /// 特殊规则类型
        /// </summary>
        public Symbol Symbol { get; set; }

        /// <summary>
        /// 特殊规则内容
        /// </summary>
        public string SymbolContent { get; set; }

        public List<string> Extra { get; set; } = new();

        /// <summary>
        /// 返回是否有效
        /// </summary>
        //public bool IsValid => !this.Value.IsNull() || !this.ConditionValue.IsNull() || Symbol != Symbol.None;
        #endregion

        #region Functions
        public void Load(string Info)
        {
            try
            {
                //去除$特殊符号
                if (Info.StartsWith("$"))
                {
                    Info = Info.Substring(1);

                    var Content = Regex.Replace(Info, @"(.*\()(.*)(\).*)", "$2");
                    string Symbol = Info.Replace(Content, null).Replace("(", null).Replace(")", null);
                    SymbolContent = Content;

                    //尝试直接转换，如果转换失败则为直接引用值方式
                    if (Enum.TryParse<Symbol>(Symbol, true, out var result)) this.Symbol = result;
                    else
                    {
                        this.Symbol = Struct.Symbol.Copy;
                        if (string.IsNullOrWhiteSpace(SymbolContent))
                            Console.WriteLine($"#cRed#[DefaultInfo] 直接引用对象不能为空 {Symbol} ({Info})");
                    }
                }

                //解析为文本表达式
                else if (Info.Contains("{") && Info.Contains("}"))
                {
                    var regex = new Regex(@"\{(.*)\}", RegexOptions.Singleline);  //正则表达式，取中间内容
                    SymbolContent = regex.Match(Info).Groups[1].Value;      //获取原文信息

                    Extra = regex.Replace(" " + Info + " ", "$").Split('$').ToList();
                    Symbol = Symbol.Text;
                }

                SymbolContent = SymbolContent.Trim();
            }
            catch (Exception Exception)
            {
                throw new Exception("处理特殊计算符号失败\n" + Exception);
            }
        }
        #endregion
    }

    /// <summary>
    /// 处理类型
    /// </summary>
    public enum Symbol
    {
        None,

        /// <summary>
        /// 计算满足正则表达式的数量，数值未填写不会计算在内
        /// </summary>
        COUNT,

        /// <summary>
        /// 计算数值累加值，数值为非整数型将会报错
        /// </summary>
        SUM,

        /// <summary>
        /// 文本表达式
        /// </summary>
        Text,

        /// <summary>
        /// 复制指定对象值
        /// </summary>
        Copy,
    }
}
