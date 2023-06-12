using Xylia.Attribute;
using Xylia.Preview.Data.Models.BinData.Analyse.Struct.Condition.Base;

namespace Xylia.Preview.Data.Models.BinData.Analyse.Struct.Condition.Subclass
{
    public class Exist : ValidCondition
    {
        #region Constructor
        public Exist(bool Flag) => this.Flag = Flag;

        /// <summary>
        /// 是否存在
        /// </summary>
        public bool Flag = true;
        #endregion


        #region Functions
        protected override bool IsMeet(IHash Hash, bool ExistTarget) => Flag && ExistTarget || !Flag && !ExistTarget;
        #endregion
    }
}
