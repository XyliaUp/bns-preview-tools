using System.Collections.Generic;
using System.Linq;
using System.Xml;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Data.Record.ScriptData.Reaction;
using Xylia.Preview.Data.Table.XmlRecord;

namespace Xylia.Preview.Data.Record.ScriptData
{
	[Signal("reaction-set")]
	public sealed class ReactionSet : BaseRecord
	{
		public List<IReaction> Reaction;

		/// <summary>
		/// 概率 0~100, decision 下的所有 ReactionSet 概率和不能超过最大值
		/// </summary>
		public byte Probability;

		/// <summary>
		/// 概率 0~10000, decision 下的所有 ReactionSet 概率和不能超过最大值 
		/// </summary>
		public short Probability10000;



		public override void LoadData(XmlElement data)
		{
			base.LoadData(data);

			this.Reaction = new List<IReaction>();
			foreach (var record in data.SelectNodes("./reaction").OfType<XmlElement>())
			{
				this.Reaction.Add(record.TypeFactory<ReactionType, IReaction>(s => s switch
				{
					ReactionType.AcquireFieldItem => new AcquireFieldItem(),
					ReactionType.ActivateTeleport => new ActivateTeleport(),
					ReactionType.ActResume => new ActResume(),
					ReactionType.AddZoneScore => new AddZoneScore(),
					ReactionType.CopyNpcHate => new CopyNpcHate(),
					ReactionType.Damage => new Damage(),
					ReactionType.DeactivateTeleport => new DeactivateTeleport(),
					ReactionType.DebugPrint => new DebugPrint(),
					ReactionType.DebugTrace => new DebugTrace(),
					ReactionType.DespawnNpc => new DespawnNpc(),
					ReactionType.DespawnNpcGroups => new DespawnNpcGroups(),
					ReactionType.DespawnNpcIndex => new DespawnNpcIndex(),
					ReactionType.DiffNpcHate => new DiffNpcHate(),
					ReactionType.DiffNpcNumber => new DiffNpcNumber(),
					ReactionType.DiffPartyNumber => new DiffPartyNumber(),
					ReactionType.DispelBuff => new DispelBuff(),
					ReactionType.DispelByAttr => new DispelByAttr(),
					ReactionType.DispelByType => new DispelByType(),
					ReactionType.DispelDebuff => new DispelDebuff(),
					ReactionType.Heal => new Heal(),
					ReactionType.HealMax => new HealMax(),
					ReactionType.InOutDetectStart => new InOutDetectStart(),
					ReactionType.InOutDetectStop => new InOutDetectStop(),
					ReactionType.InvokeEffect => new InvokeEffect(),
					ReactionType.Kill => new Kill(),
					ReactionType.NpcFireSpecial => new NpcFireSpecial(),
					ReactionType.NpcTalkFinish => new NpcTalkFinish(),
					ReactionType.PatternStart => new PatternStart(),
					ReactionType.PatternSuccess => new PatternSuccess(),
					ReactionType.PlayCinematic => new PlayCinematic(),
					ReactionType.PlayIndexedSocial => new PlayIndexedSocial(),
					ReactionType.PlaySocial => new PlaySocial(),
					ReactionType.PlaySurroundSocial => new PlaySurroundSocial(),
					ReactionType.RemoveFieldItem => new RemoveFieldItem(),
					ReactionType.ResetNpcAllHate => new ResetNpcAllHate(),
					ReactionType.ResetNpcHate => new ResetNpcHate(),
					ReactionType.ResetStage => new ResetStage(),
					ReactionType.ResetZoneObject => new ResetZoneObject(),
					ReactionType.SetEnvEnable => new SetEnvEnable(),
					ReactionType.SetEnvInitEnable => new SetEnvInitEnable(),
					ReactionType.SetEnvState => new SetEnvState(),
					ReactionType.SetNpcAct => new SetNpcAct(),
					ReactionType.SetNpcAttackable => new SetNpcAttackable(),
					ReactionType.SetNpcBrain => new SetNpcBrain(),
					ReactionType.SetNpcCombatMode => new SetNpcCombatMode(),
					ReactionType.SetNpcFollow => new SetNpcFollow(),
					ReactionType.SetNpcHateOn => new SetNpcHateOn(),
					ReactionType.SetNpcIndexedAct => new SetNpcIndexedAct(),
					ReactionType.SetNpcInteractive => new SetNpcInteractive(),
					ReactionType.SetNpcNumber => new SetNpcNumber(),
					ReactionType.SetPartyNumber => new SetPartyNumber(),
					ReactionType.SetPartyObject => new SetPartyObject(),
					ReactionType.SetPublicRaidEvent => new SetPublicRaidEvent(),
					ReactionType.SetUndying => new SetUndying(),
					ReactionType.SetZoneObject => new SetZoneObject(),
					ReactionType.SpawnFieldItem => new SpawnFieldItem(),
					ReactionType.SpawnNpc => new SpawnNpc(),
					ReactionType.SpawnNpcGroups => new SpawnNpcGroups(),
					ReactionType.SpawnNpcIndex => new SpawnNpcIndex(),
					ReactionType.SpawnRandomNpc => new SpawnRandomNpc(),
					ReactionType.SpawnRandomNpcGroup => new SpawnRandomNpcGroup(),
					ReactionType.TransitNpcCombat => new TransitNpcCombat(),
					ReactionType.TransitNpcCombatIndex => new TransitNpcCombatIndex(),
					ReactionType.Warp => new Warp(),
					ReactionType.WarpParty => new WarpParty(),
					ReactionType.WarpToReentrance => new WarpToReentrance(),

					_ => null
				}));
			}
		}
	}
}