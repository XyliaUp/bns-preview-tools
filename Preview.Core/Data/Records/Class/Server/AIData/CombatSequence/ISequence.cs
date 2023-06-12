using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Arg;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Data.Record.CombatSequenceData.Action;

namespace Xylia.Preview.Data.Record.CombatSequenceData
{
	/// <summary>
	/// 战斗序列类
	/// </summary>
	public abstract class ISequence : BaseRecord
    {
        #region Fields
        public List<IAction> Actions = new();


        public short ID;

        [Signal("attack-limit")]
        public byte AttackLimit;

        [Signal("range-attack-limit")]
        public byte RangeAttackLimit;

        [Signal("combat-distance")]
        public byte CombatDistance;

        [DefaultValue(1)]
        [Signal("max-count")]
        public short MaxCount;

        [Signal("script-debug-enable")]
        public bool ScriptDebugEnable;


        /// <summary>
        /// 普通序列过程中调用特殊序列
        /// </summary>
        [Signal("special-1")] public byte Special1;
        [Signal("special-2")] public byte Special2;
        [Signal("special-3")] public byte Special3;
        [Signal("special-4")] public byte Special4;
        [Signal("special-5")] public byte Special5;
        #endregion

        #region Functions
        public override void LoadData(XmlElement xe)
        {
            base.LoadData(xe);

            //Special1、Special2存在默认值, 当定义 Special1 后 Special2 会默认为空
            if(this.Special1 != 0)    
                this.Special2 = 0;


            this.Actions = new List<IAction>();
            var Actions = xe.SelectNodes("./action");
            for (int i = 0; i < Actions.Count; i++)
            {
                var ActionNode = (XmlElement)Actions[i];
                this.Actions.Add(ActionNode.ActionFactory());
            }
        }
        #endregion
    }


    public sealed class Normal : ISequence
    {
        [Signal("change-min-msec")]
        public byte ChangeMinMsec;

        [Signal("except-summoned")]
        public bool ExceptSummoned;

        [Signal("hide-seq-start")]
        public bool HideSeqStart;

        [Signal("seq-start-social")]
        public byte SeqStartSocial;

        [Signal("setup-position")]
        public bool SetupPosition;

        [Signal("setup-dir")]
        public Direction SetupDir;
    }

    public sealed class Special : ISequence
    {
        /// <summary>
        /// 条件
        /// </summary>
        [Signal("condition-1")]
        public Flag Condition1;

        /// <summary>
        /// 条件
        /// </summary>
        [Signal("condition-2")]
        public Flag Condition2;

        /// <summary>
        /// 条件对象 默认是 event:npc-combat-action:target
        /// </summary>
        [Signal("condition-subscription-1")]
        public Script_obj ConditionSubscription1;

        /// <summary>
        /// 条件对象
        /// </summary>
        [Signal("condition-subscription-2")]
        public Script_obj ConditionSubscription2;




        [Signal("start-delay")]
        public int StartDelay;

        [Signal("min-delay")]
        public int MinDelay;
    }
}