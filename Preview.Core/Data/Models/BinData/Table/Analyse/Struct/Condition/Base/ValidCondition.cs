namespace Xylia.Preview.Data.Models.BinData.Analyse.Struct.Condition.Base
{
    public class ValidCondition : Condition
    {
        public override bool Invalid => string.IsNullOrWhiteSpace(TargetAlias);
    }
}
