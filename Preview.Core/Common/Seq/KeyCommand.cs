using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Common.Seq
{
	/// <summary>
	/// 按键命令
	/// </summary>
	public enum KeyCommandSeq
	{
		[Signal("none")]
		None,

		[Signal("action1")]
		Action1,

		[Signal("action2")]
		Action2,

		[Signal("action3")]
		Action3,

		[Signal("classic-action1")]
		ClassicAction1,

		[Signal("classic-action2")]
		ClassicAction2,

		[Signal("classic-action3")]
		ClassicAction3,

		[Signal("skill-recommend")]
		SkillRecommend,

		[Signal("skill-stance")]
		SkillStance,

		[Signal("skill-slot1")]
		SkillSlot1,

		[Signal("skill-slot2")]
		SkillSlot2,

		[Signal("skill-slot3")]
		SkillSlot3,

		[Signal("skill-slot4")]
		SkillSlot4,

		[Signal("skill-slot5")]
		SkillSlot5,

		[Signal("skill-slot6")]
		SkillSlot6,

		[Signal("skill-slot7")]
		SkillSlot7,

		[Signal("skill-slot8")]
		SkillSlot8,

		[Signal("skill-slot9")]
		SkillSlot9,

		[Signal("skill-slot10")]
		SkillSlot10,

		[Signal("skill-slot11")]
		SkillSlot11,

		[Signal("skill-slot12")]
		SkillSlot12,

		[Signal("skill-q")]
		SkillQ,

		[Signal("skill-e")]
		SkillE,

		[Signal("skill-special-1")]
		SkillSpecial1,

		[Signal("skill-special-2")]
		SkillSpecial2,

		[Signal("item-slot1")]
		ItemSlot1,

		[Signal("item-slot2")]
		ItemSlot2,

		[Signal("item-slot3")]
		ItemSlot3,

		[Signal("item-slot4")]
		ItemSlot4,

		[Signal("extra-skill-slot5")]
		ExtraSkillSlot5,

		[Signal("skill-duel-tag")]
		SkillDuelTag,

		[Signal("toggle-help")]
		ToggleHelp,

		[Signal("toggle-dungeon-guide")]
		ToggleDungeonGuide,

		[Signal("toggle-inventory")]
		ToggleInventory,

		[Signal("toggle-gem")]
		ToggleGem,

		[Signal("toggle-equipped")]
		ToggleEquipped,

		[Signal("toggle-skill-book")]
		ToggleSkillBook,

		[Signal("toggle-quest-journal")]
		ToggleQuestJournal,

		[Signal("toggle-minimap-scale")]
		ToggleMinimapScale,

		[Signal("toggle-minimap-visible")]
		ToggleMinimapVisible,

		[Signal("toggle-quest-quick-slot")]
		ToggleQuestQuickSlot,

		[Signal("toggle-collect-manufacture")]
		ToggleCollectManufacture,

		[Signal("toggle-petition")]
		TogglePetition,

		[Signal("toggle-option")]
		ToggleOption,

		[Signal("toggle-minimap-alpha")]
		ToggleMinimapAlpha,

		[Signal("toggle-party-panel")]
		TogglePartyPanel,

		[Signal("toggle-party-mark")]
		TogglePartyMark,

		[Signal("toggle-party-search")]
		TogglePartySearch,

		[Signal("toggle-hud-customize")]
		ToggleHudCustomize,

		[Signal("party-match")]
		PartyMatch,

		[Signal("manual-targeting")]
		ManualTargeting,

		[Signal("strafe-left")]
		StrafeLeft,

		[Signal("strafe-right")]
		StrafeRight,

		[Signal("dash-forward")]
		DashForward,

		[Signal("move-forward")]
		MoveForward,

		[Signal("move-backward")]
		MoveBackward,

		[Signal("turn-left")]
		TurnLeft,

		[Signal("turn-right")]
		TurnRight,

		[Signal("jump-up")]
		JumpUp,

		[Signal("toggle-walk-run")]
		ToggleWalkRun,

		[Signal("social-action1")]
		SocialAction1,

		[Signal("social-action2")]
		SocialAction2,

		[Signal("social-action3")]
		SocialAction3,

		[Signal("social-action4")]
		SocialAction4,

		[Signal("social-action5")]
		SocialAction5,

		[Signal("social-action6")]
		SocialAction6,

		[Signal("social-action7")]
		SocialAction7,

		[Signal("social-action8")]
		SocialAction8,

		[Signal("social-action9")]
		SocialAction9,

		[Signal("social-action10")]
		SocialAction10,

		[Signal("toggle-auto-move-forward")]
		ToggleAutoMoveForward,

		[Signal("enter-slash-chatting")]
		EnterSlashChatting,

		[Signal("enter-normal-chatting")]
		EnterNormalChatting,

		[Signal("enter-party-chatting")]
		EnterPartyChatting,

		[Signal("enter-local-chatting")]
		EnterLocalChatting,

		[Signal("enter-private-chatting")]
		EnterPrivateChatting,

		[Signal("toggle-hud")]
		ToggleHud,

		[Signal("toggle-light-hud-mode")]
		ToggleLightHudMode,

		[Signal("camera-up")]
		CameraUp,

		[Signal("camera-down")]
		CameraDown,

		[Signal("camera-zoomin")]
		CameraZoomin,

		[Signal("camera-zoomout")]
		CameraZoomout,

		[Signal("toggle-voice-chat-option")]
		ToggleVoiceChatOption,

		[Signal("mark1")]
		Mark1,

		[Signal("mark2")]
		Mark2,

		[Signal("mark3")]
		Mark3,

		[Signal("mark4")]
		Mark4,

		[Signal("mark5")]
		Mark5,

		[Signal("mark6")]
		Mark6,

		[Signal("mark7")]
		Mark7,

		[Signal("mark8")]
		Mark8,

		[Signal("mark-timer")]
		MarkTimer,

		[Signal("mark-timer-immune-break-down")]
		MarkTimerImmuneBreakDown,

		[Signal("mark-timer-immune-break-kneel")]
		MarkTimerImmuneBreakKneel,

		[Signal("mark-timer-immune-break-stun")]
		MarkTimerImmuneBreakStun,

		[Signal("mark-timer-immune-break-knockback")]
		MarkTimerImmuneBreakKnockback,

		[Signal("unmark")]
		Unmark,

		[Signal("headline-start-timer")]
		HeadlineStartTimer,

		[Signal("minimap-depth-plus")]
		MinimapDepthPlus,

		[Signal("minimap-depth-minus")]
		MinimapDepthMinus,

		[Signal("rotate-camera")]
		RotateCamera,

		[Signal("rotate-player")]
		RotatePlayer,

		[Signal("reset-camera")]
		ResetCamera,

		[Signal("toggle-bns")]
		ToggleBns,

		[Signal("notification-accept")]
		NotificationAccept,

		[Signal("notification-decline")]
		NotificationDecline,

		[Signal("toggle-joypad-ui-mode")]
		ToggleJoypadUiMode,

		[Signal("close-floating-panels")]
		CloseFloatingPanels,

		[Signal("joypad-esc")]
		JoypadEsc,

		[Signal("joypad-set-mouse-position")]
		JoypadSetMousePosition,

		[Signal("joypad-toggle-rotate-type")]
		JoypadToggleRotateType,

		[Signal("turn-on-social-mode")]
		TurnOnSocialMode,

		[Signal("enter-party-search-channel-in-party-match")]
		EnterPartySearchChannelInPartyMatch,

		[Signal("toggle-sns")]
		ToggleSns,

		[Signal("toggle-powerbook")]
		TogglePowerbook,

		[Signal("toggle-skill-powerbook")]
		ToggleSkillPowerbook,

		[Signal("toggle-nshop")]
		ToggleNshop,

		[Signal("toggle-character-info")]
		ToggleCharacterInfo,

		[Signal("toggle-market")]
		ToggleMarket,

		[Signal("toggle-money-market")]
		ToggleMoneyMarket,

		[Signal("toggle-postbox")]
		TogglePostbox,

		[Signal("toggle-tencent-in-game-shop")]
		ToggleTencentInGameShop,

		[Signal("toggle-tencent-cross")]
		ToggleTencentCross,

		[Signal("toggle-second-password")]
		ToggleSecondPassword,

		[Signal("toggle-achievement")]
		ToggleAchievement,

		[Signal("toggle-pc-filter-hidden")]
		TogglePcFilterHidden,

		[Signal("toggle-guild2")]
		ToggleGuild2,

		[Signal("toggle-economy-communication")]
		ToggleEconomyCommunication,

		[Signal("cancel-order")]
		CancelOrder,

		[Signal("invite-party")]
		InviteParty,

		[Signal("duel-start")]
		DuelStart,

		[Signal("toggle-friend")]
		ToggleFriend,

		[Signal("toggle-wardrobe")]
		ToggleWardrobe,

		[Signal("toggle-dragon-jade-shop")]
		ToggleDragonJadeShop,

		[Signal("toggle-ranking")]
		ToggleRanking,

		[Signal("toggle-ingame-notice")]
		ToggleIngameNotice,

		[Signal("change-item-bar-page")]
		ChangeItemBarPage,

		[Signal("duel-tag-order-first")]
		DuelTagOrderFirst,

		[Signal("duel-tag-order-random")]
		DuelTagOrderRandom,

		[Signal("leave-arena-square")]
		LeaveArenaSquare,

		[Signal("quick-turn")]
		QuickTurn,

		[Signal("joypad-special-function1")]
		JoypadSpecialFunction1,

		[Signal("joypad-special-function2")]
		JoypadSpecialFunction2,

		[Signal("accelerate-mouse-cursor-speed")]
		AccelerateMouseCursorSpeed,

		[Signal("joypad-mouse-left-button")]
		JoypadMouseLeftButton,

		[Signal("joypad-mouse-right-button")]
		JoypadMouseRightButton,

		[Signal("change-pet-filter-hidden")]
		ChangePetFilterHidden,

		[Signal("toggle-damage-meter")]
		ToggleDamageMeter,

		[Signal("toggle-item-graph")]
		ToggleItemGraph,

		[Signal("duel-camera-fitpawn-new")]
		DuelCameraFitpawnNew,

		[Signal("duel-camera-free-new")]
		DuelCameraFreeNew,

		[Signal("duel-camera-quater-new")]
		DuelCameraQuaterNew,

		[Signal("duel-camera-alpha-new")]
		DuelCameraAlphaNew,

		[Signal("duel-camera-beta-new")]
		DuelCameraBetaNew,

		[Signal("duel-camera-free-up-new")]
		DuelCameraFreeUpNew,

		[Signal("duel-camera-free-down-new")]
		DuelCameraFreeDownNew,

		[Signal("toggle-goods-store")]
		ToggleGoodsStore,

		[Signal("enter-skill-training-room")]
		EnterSkillTrainingRoom,

		[Signal("toggle-arena-entrance")]
		ToggleArenaEntrance,

		[Signal("toggle-appearance-pictorial")]
		ToggleAppearancePictorial,

		[Signal("toggle-hybrid-mode")]
		ToggleHybridMode,

		[Signal("toggle-personal-customize")]
		TogglePersonalCustomize,

		[Signal("toggle-context-simple-mode")]
		ToggleContextSimpleMode,

		[Signal("toggle-mentoring")]
		ToggleMentoring,

		[Signal("toggle-pvp-enable")]
		TogglePvpEnable,

		[Signal("nova-equip-skill-quick-slot")]
		NovaEquipSkillQuickSlot,

		[Signal("toggle-account-contents")]
		ToggleAccountContents,

		[Signal("toggle-contents-journal-2")]
		ToggleContentsJournal2,

		[Signal("toggle-fish-collecting")]
		ToggleFishCollecting,

		[Signal("toggle-slate")]
		ToggleSlate,

		[Signal("toggle-auto-mode")]
		ToggleAutoMode,

		[Signal("toggle-auto-mode-option")]
		ToggleAutoModeOption,

		[Signal("toggle-auto-mode-enable-item-support")]
		ToggleAutoModeEnableItemSupport,

		[Signal("toggle-pvp-data")]
		TogglePvpData,

		[Signal("toggle-ride-vehicle")]
		ToggleRideVehicle,

		[Signal("toggle-museum")]
		ToggleMuseum,

		[Signal("equip-spare-item")]
		EquipSpareItem,

		[Signal("toggle-spare-item")]
		ToggleSpareItem,

		[Signal("unk-")]
		Unk178,

		[Signal("toggle-glyph-inventory")]
		ToggleGlyphInventory,

		[Signal("toggle-constellation")]
		ToggleConstellation,

		[Signal("toggle-sealed-scroll")]
		ToggleSealedScroll,

		[Signal("toggle-inter-battle-field")]
		ToggleInterBattleField,

		[Signal("toggle-wanted-mission")]
		ToggleWantedMission,

		[Signal("toggle-expedition")]
		ToggleExpedition,
	}
}