using System;

using Xylia.Attribute;
using Xylia.Preview.Data.Models.BinData.Analyse.Struct.Condition.Base;

namespace Xylia.Preview.Data.Models.BinData.Analyse.Struct.Condition.Subclass
{
    public class Compare : ValidCondition
    {
        public Compare(Op Operation = Op.eq)
        {
            this.Operation = Operation;
        }


        #region Fields
        public int TargetValue;

        /// <summary>
        /// 操作符号
        /// </summary>
        public Op Operation;
        #endregion



        #region Functions
        public override void LoadParams(params string[] Params)
        {
            base.LoadParams(Params);

            if (Params.Length < 3) throw new ArgumentException(GetType().Name + "条件必须有三个参数");
            else if (Params.Length >= 3 && int.TryParse(Params[2]?.Trim(), out var tmp)) TargetValue = tmp;
        }

        protected override bool IsMeet(IHash Hash, bool ExistTarget)
        {
            #region 获取当前实例的对象值
            int CurValue = 0;  //后续可能会调整为判断Fields的默认值

            //如果存在目标
            if (ExistTarget)
            {
                //当前值可以转换为整数型
                if (int.TryParse(Hash[TargetAlias], out int Result)) CurValue = Result;
                else
                {
                    // Trace.WriteLine();
                }
            }
            #endregion

            #region 返回结果
            return Operation switch
            {
                Op.eq => TargetValue == CurValue,
                Op.neq => TargetValue != CurValue,
                Op.ge => TargetValue <= CurValue,
                Op.gt => TargetValue < CurValue,
                Op.le => TargetValue >= CurValue,
                Op.lt => TargetValue > CurValue,

                _ => throw new NotImplementedException("未支持的操作符号")
            };
            #endregion
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) return false;

            //如果目标别名相同，检测是否存在目标值
            var Cond2 = obj as Compare;
            return TargetValue == Cond2.TargetValue;
        }

        public override int GetHashCode() => base.GetHashCode() ^ TargetValue.GetHashCode();
        #endregion
    }

    public enum Op
    {
        /// <summary>
        /// 相等
        /// </summary>
        eq,

        /// <summary>
        /// 不相等
        /// </summary>
        neq,

        /// <summary>
        /// 大于
        /// </summary>
        gt,

        /// <summary>
        /// 大于等于
        /// </summary>
        ge,

        /// <summary>
        /// 小于
        /// </summary>
        lt,

        /// <summary>
        /// 小于等于
        /// </summary>
        le,
    }
}