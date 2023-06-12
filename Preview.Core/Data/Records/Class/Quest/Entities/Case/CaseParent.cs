using System.Collections.Generic;
using System.Linq;
using System.Xml;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Record.QuestData.Case;
using Xylia.Preview.Data.Record.QuestData.TutorialCase;
using Xylia.Preview.Data.Table.XmlRecord;

namespace Xylia.Preview.Data.Record.QuestData
{
	/// <summary>
	/// Case类型的母对象
	/// </summary>
	public abstract class CaseParent : BaseRecord
	{
		[Signal("case")]
		public List<CaseBase> Case;

		[Signal("tutorial-case")]
		public List<TutorialCaseBase> TutorialCase;

		[Signal("basic-reward")]
		public List<BasicReward> BasicReward;

		[Signal("fixed-reward")]
		public List<FixedReward> FixedReward;

		[Signal("optional-reward")]
		public List<OptionalReward> OptionalReward;

		[Signal("acquisition-loss")]
		public List<AcquisitionLoss> AcquisitionLoss;

		[Signal("completion-loss")]
		public List<CompletionLoss> CompletionLoss;


		public override void LoadData(XmlElement data)
		{
			base.LoadData(data);


			Case = new();
			TutorialCase = new();

			foreach (var record in data.SelectNodes("./case").OfType<XmlElement>())
			{
				Case.Add(record.TypeFactory<CaseType, CaseBase>(s => s switch
				{
					CaseType.AcquireSummoned => new AcquireSummoned(),
					CaseType.Approach => new Approach(),
					CaseType.AttractionPopup => new AttractionPopup(),
					CaseType.BattleRoyal => new BattleRoyal(),
					CaseType.CompleteQuest => new CompleteQuest(),
					CaseType.ConvoyArrived => new ConvoyArrived(),
					CaseType.ConvoyFailed => new ConvoyFailed(),
					CaseType.DuelFinish => new DuelFinish(),
					CaseType.EnterPortal => new EnterPortal(),
					CaseType.EnterZone => new EnterZone(),
					CaseType.EnvEntered => new EnvEntered(),
					CaseType.JoinFaction => new JoinFaction(),
					CaseType.Killed => new Killed(),
					CaseType.Loot => new Loot(),
					CaseType.Manipulate => new Manipulate(),
					CaseType.NpcBleedingOccured => new NpcBleedingOccured(),
					CaseType.NpcManipulate => new NpcManipulate(),
					CaseType.PartyBattle => new PartyBattle(),
					CaseType.PartyBattleAction => new PartyBattleAction(),
					CaseType.PcSocial => new PcSocial(),
					CaseType.PickUpFielditem => new Case.PickUpFielditem(),
					CaseType.PublicRaid => new Case.PublicRaid(),
					CaseType.Skill => new Case.Skill(),
					CaseType.Talk => new Talk(),
					CaseType.TalkToItem => new TalkToItem(),
					CaseType.TalkToSelf => new TalkToSelf(),

					_ => null
				}));
			}

			foreach (var record in data.SelectNodes("./tutorial-case").OfType<XmlElement>())
			{
				TutorialCase.Add(record.TypeFactory<TutorialCaseType, TutorialCaseBase>(s => s switch
				{
					TutorialCaseType.AcquireItem => new AcquireItem(),
					TutorialCaseType.AcquireSp => new AcquireSp(),
					TutorialCaseType.Airdash => new Airdash(),
					TutorialCaseType.Attacked => new Attacked(),
					TutorialCaseType.ChargeHeartCount => new ChargeHeartCount(),
					TutorialCaseType.CompleteSelfRevival => new CompleteSelfRevival(),
					TutorialCaseType.DetachWeaponGem => new DetachWeaponGem(),
					TutorialCaseType.EnlargeMiniMap => new EnlargeMiniMap(),
					TutorialCaseType.TransparentMiniMap => new TransparentMiniMap(),
					TutorialCaseType.EquipItem => new EquipItem(),
					TutorialCaseType.Exhausted => new Exhausted(),
					TutorialCaseType.ExpandInventory => new ExpandInventory(),
					TutorialCaseType.GemCompose => new GemCompose(),
					TutorialCaseType.GemDecompose => new GemDecompose(),
					TutorialCaseType.GrowItem => new GrowItem(),
					TutorialCaseType.MoveToPosition => new MoveToPosition(),
					TutorialCaseType.NpcBleeding => new NpcBleeding(),
					TutorialCaseType.PcBleeding => new PcBleeding(),
					TutorialCaseType.PickDownFielditem => new PickDownFielditem(),
					TutorialCaseType.PickUpFielditem => new TutorialCase.PickUpFielditem(),
					TutorialCaseType.QuestTrackingPosition => new QuestTrackingPosition(),
					TutorialCaseType.RepairWithCampfire => new RepairWithCampfire(),
					TutorialCaseType.ResurrectingSummoned => new ResurrectingSummoned(),
					TutorialCaseType.Skill => new TutorialCase.Skill(),
					TutorialCaseType.SkillSequence => new SkillSequence(),
					TutorialCaseType.SkillTraining => new SkillTraining(),
					TutorialCaseType.TalkStart => new TalkStart(),
					TutorialCaseType.Targeting => new Targeting(),
					TutorialCaseType.Teleport => new TutorialCase.Teleport(),
					TutorialCaseType.TeleportZone => new TeleportZone(),
					TutorialCaseType.TransformItem => new TransformItem(),
					TutorialCaseType.UseHeartCount => new UseHeartCount(),
					TutorialCaseType.UseItem => new UseItem(),
					TutorialCaseType.WeaponGem => new WeaponGem(),
					TutorialCaseType.WindowOpen => new WindowOpen(),

					_ => null
				}));
			}
		}
	}
}