using System.ComponentModel;
using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Struct;

namespace Xylia.Preview.Data.Record;
public abstract class Reaction : BaseRecord
{
    [DefaultValue(0)]
    public byte Probability;


    #region Sub
    [Signal("act-resume")]
    public sealed class ActResume : Reaction
    {
        public Script_obj Target;
    }

    [Signal("copy-npc-hate")]
    public sealed class CopyNpcHate : Reaction
    {
        public Script_obj Opponent;

        public Script_obj Target;
    }
    #endregion
}