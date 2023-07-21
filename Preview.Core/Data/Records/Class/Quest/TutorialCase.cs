using System.ComponentModel;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Record;
using Xylia.Preview.Data.Record.QuestData.Enums;

using static Xylia.Preview.Data.Record.Item;

namespace Xylia.Preview.Data.Record.QuestData;
public partial class TutorialCase : BaseRecord
{
    #region Base
    [DefaultValue(100)]
    public byte Prob = 100;

    [Side(ReleaseSide.Client)]
    [Signal("mapunit"), Repeat(10)]
    public MapUnit[] MapUnit;


    [DefaultValue(-1)]
    [Signal("range-max")]
    public short RangeMax = -1;

    [DefaultValue(-1)]
    [Signal("range-min")]
    public short RangeMin = -1;

    [Signal("progress-mission")]
    public ProgressMission ProgressMission = ProgressMission.N;

    [Signal("valid-zone-1")]
    public Zone ValidZone1;

    [Signal("valid-zone-2")]
    public Zone ValidZone2;


    [Side(ReleaseSide.Server)]
    public string Zone;

    [Side(ReleaseSide.Server)]
    [Signal("quest-decision")]
    public string QuestDecision;

    public virtual List<string> AttractionObject { get; }
    #endregion

    #region Sub
    public sealed class AcquireItem : TutorialCase
    {

    }

    public sealed class EquipItem : TutorialCase
    {
        [Side(ReleaseSide.Client)]
        [Signal("item-category")]
        public GameCategory2Seq ItemCategory;

        [Side(ReleaseSide.Client)]
        public Item Item;
    }

    public sealed class UseItem : TutorialCase
    {
        [Side(ReleaseSide.Client)]
        public string Item;
    }

    public sealed class GrowItem : TutorialCase
    {
        [Side(ReleaseSide.Client)]
        [Signal("material-item")]
        public Item MaterialItem;

        [Side(ReleaseSide.Client)]
        [Signal("primary-item")]
        public Item PrimaryItem;
    }

    public sealed class TransformItem : TutorialCase
    {

    }

    public sealed class PickUpFielditem : TutorialCase
    {

    }

    public sealed class PickDownFielditem : TutorialCase
    {

    }

    public sealed class Targeting : TutorialCase
    {

    }

    public sealed class TalkStart : TutorialCase
    {

    }

    public sealed class WindowOpen : TutorialCase
    {
        [Signal("window-type")]
        [Side(ReleaseSide.Client)]
        public WindowTypeSeq WindowType;

        public enum WindowTypeSeq
        {
            Inverntory,

            [Signal("quest-journal")]
            QuestJournal,

            Skill,

            Sandbox,

            Auction,

            [Signal("cash-shop")]
            CashShop,

            Wardrobe,

            [Signal("account-contents")]
            AccountContents,
        }



        [Signal("window-open-way")]
        [Side(ReleaseSide.Client)]
        public WindowOpenWaySeq WindowOpenWay;

        public enum WindowOpenWaySeq
        {
            None,

            [Signal("by-npc-seller-button")]
            ByNpcSellerButton,
        }
    }

    public sealed class CompleteSelfRevival : TutorialCase
    {

    }

    public sealed class NpcBleeding : TutorialCase
    {

    }

    public sealed class PcBleeding : TutorialCase
    {
        [DefaultValue(null)]
        public byte Percent;
    }

    public sealed class Exhausted : TutorialCase
    {

    }

    public sealed class Attacked : TutorialCase
    {

    }

    public sealed class AcquireSp : TutorialCase
    {

    }

    public sealed class Skill : TutorialCase
    {

    }

    public sealed class SkillSequence : TutorialCase
    {
        [Signal("object"), Repeat(15)]
        public string[] Object;

        [Signal("object2"), Repeat(15)]
        public string[] Object2;

        [Side(ReleaseSide.Client)]
        [Signal("skill-sequence")]
        public string skillSequence;
    }

    public sealed class SkillTraining : TutorialCase
    {

    }

    public sealed class QuestTrackingPosition : TutorialCase
    {

    }

    public sealed class RepairWithCampfire : TutorialCase
    {
        [Side(ReleaseSide.Client)]
        public string Item;
    }

    public sealed class Teleport : TutorialCase
    {

    }

    public sealed class ExpandInventory : TutorialCase
    {

    }

    public sealed class GemCompose : TutorialCase
    {
        [Side(ReleaseSide.Client)]
        [Signal("primary-item")]
        public Item PrimaryItem;

        [Side(ReleaseSide.Client)]
        [Signal("material-item")]
        public Item MaterialItem;
    }

    public sealed class GemDecompose : TutorialCase
    {
        [Side(ReleaseSide.Client)]
        public Item Item;
    }

    public sealed class WeaponGem : TutorialCase
    {
        [Side(ReleaseSide.Client)]
        public Item Weapon;

        [Side(ReleaseSide.Client)]
        public Item Gem;
    }

    public sealed class Airdash : TutorialCase
    {
        [Side(ReleaseSide.Client)]
        public string Object2;

        [Side(ReleaseSide.Client)]
        [Signal("env-response")]
        public string EnvResponse;


        public override List<string> AttractionObject => new() { Object2 };
    }

    public sealed class DetachWeaponGem : TutorialCase
    {
        [Side(ReleaseSide.Client)]
        public Item Weapon;

        [Side(ReleaseSide.Client)]
        public Item Gem;
    }

    public sealed class EnlargeMiniMap : TutorialCase
    {

    }

    public sealed class TransparentMiniMap : TutorialCase
    {

    }

    public sealed class ResurrectingSummoned : TutorialCase
    {

    }

    public sealed class MoveToPosition : TutorialCase
    {
        [Side(ReleaseSide.Client)]
        [Signal("link-npc")]
        public Npc LinkNpc;

        [Side(ReleaseSide.Client)]
        [Signal("location-x")]
        public float LocationX;

        [Side(ReleaseSide.Client)]
        [Signal("location-y")]
        public float LocationY;

        [Side(ReleaseSide.Client)]
        [Signal("approach-range")]
        public float ApproachRange;
    }

    public sealed class UseHeartCount : TutorialCase
    {

    }

    public sealed class ChargeHeartCount : TutorialCase
    {

    }

    public sealed class TeleportZone : TutorialCase
    {
        [Side(ReleaseSide.Client)]
        [Signal("teleport-id")]
        public Teleport TeleportID;
    }
    #endregion
}