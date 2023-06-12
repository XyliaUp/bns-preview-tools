using System;

using Xylia.Attribute;
using Xylia.Extension;
using Xylia.Preview.Data.Models.BinData.Analyse.Struct.Condition.Base;

namespace Xylia.Preview.Data.Models.BinData.Analyse.Struct.Condition.Subclass
{
    public class Equal : ValidCondition
    {
        #region Fields
        public string TargetValue;
        #endregion


        #region Functions
        public override void LoadParams(params string[] Params)
        {
            base.LoadParams(Params);

            if (Params.Length < 3) throw new ArgumentException(GetType().Name + "条件必须有三个参数");
            else if (Params.Length >= 3) TargetValue = Params[2]?.Trim();
        }

        protected override bool IsMeet(IHash Hash, bool ExistTarget)
        {
            //如果是逻辑类型
            if (TargetValue.ToBool(out bool r))
            {
                //如果目标存在，判断两者值是否一致
                if (ExistTarget) return Hash[TargetAlias].ToBool() == r;

                //假设逻辑型默认值为 N
                else return !r;
            }

            //其他情况对比类型
            else return ExistTarget && Hash[TargetAlias].MyEquals(TargetValue);
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) return false;

            //如果目标别名相同，检测是否存在目标值
            var Cond2 = obj as Equal;

            if (string.IsNullOrWhiteSpace(TargetValue) && string.IsNullOrWhiteSpace(Cond2.TargetValue)) return true;  //如果目标值不存在，返回相同
            return Equals(TargetValue, Cond2.TargetValue);  //返回目标值是否相同
        }

        public override int GetHashCode() => base.GetHashCode() ^ TargetValue.GetHashCode();
        #endregion
    }
}
