using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Common.Seq;
public enum KeyCommandSeq
{
	[Name("none")]
	None,

	[Name("action1")]
	Action1,

	[Name("action2")]
	Action2,

	[Name("action3")]
	Action3,

	[Name("classic-action1")]
	ClassicAction1,

	[Name("classic-action2")]
	ClassicAction2,

	[Name("classic-action3")]
	ClassicAction3,

	[Name("skill-recommend")]
	SkillRecommend,

	[Name("skill-stance")]
	SkillStance,

	[Name("skill-slot1")]
	SkillSlot1,

	[Name("skill-slot2")]
	SkillSlot2,

	[Name("skill-slot3")]
	SkillSlot3,

	[Name("skill-slot4")]
	SkillSlot4,

	[Name("skill-slot5")]
	SkillSlot5,

	[Name("skill-slot6")]
	SkillSlot6,

	[Name("skill-slot7")]
	SkillSlot7,

	[Name("skill-slot8")]
	SkillSlot8,

	[Name("skill-slot9")]
	SkillSlot9,

	[Name("skill-slot10")]
	SkillSlot10,

	[Name("skill-slot11")]
	SkillSlot11,

	[Name("skill-slot12")]
	SkillSlot12,

	[Name("skill-q")]
	SkillQ,

	[Name("skill-e")]
	SkillE,

	[Name("skill-special-1")]
	SkillSpecial1,

	[Name("skill-special-2")]
	SkillSpecial2,

	[Name("item-slot1")]
	ItemSlot1,

	[Name("item-slot2")]
	ItemSlot2,

	[Name("item-slot3")]
	ItemSlot3,

	[Name("item-slot4")]
	ItemSlot4,

	[Name("extra-skill-slot5")]
	ExtraSkillSlot5,

	[Name("skill-duel-tag")]
	SkillDuelTag,

	[Name("toggle-help")]
	ToggleHelp,

	[Name("toggle-dungeon-guide")]
	ToggleDungeonGuide,

	[Name("toggle-inventory")]
	ToggleInventory,

	[Name("toggle-gem")]
	ToggleGem,

	[Name("toggle-equipped")]
	ToggleEquipped,

	[Name("toggle-skill-book")]
	ToggleSkillBook,

	[Name("toggle-quest-journal")]
	ToggleQuestJournal,

	[Name("toggle-minimap-scale")]
	ToggleMinimapScale,

	[Name("toggle-minimap-visible")]
	ToggleMinimapVisible,

	[Name("toggle-quest-quick-slot")]
	ToggleQuestQuickSlot,

	[Name("toggle-collect-manufacture")]
	ToggleCollectManufacture,

	[Name("toggle-petition")]
	TogglePetition,

	[Name("toggle-option")]
	ToggleOption,

	[Name("toggle-minimap-alpha")]
	ToggleMinimapAlpha,

	[Name("toggle-party-panel")]
	TogglePartyPanel,

	[Name("toggle-party-mark")]
	TogglePartyMark,

	[Name("toggle-party-search")]
	TogglePartySearch,

	[Name("toggle-hud-customize")]
	ToggleHudCustomize,

	[Name("party-match")]
	PartyMatch,

	[Name("manual-targeting")]
	ManualTargeting,

	[Name("strafe-left")]
	StrafeLeft,

	[Name("strafe-right")]
	StrafeRight,

	[Name("dash-forward")]
	DashForward,

	[Name("move-forward")]
	MoveForward,

	[Name("move-backward")]
	MoveBackward,

	[Name("turn-left")]
	TurnLeft,

	[Name("turn-right")]
	TurnRight,

	[Name("jump-up")]
	JumpUp,

	[Name("toggle-walk-run")]
	ToggleWalkRun,

	[Name("social-action1")]
	SocialAction1,

	[Name("social-action2")]
	SocialAction2,

	[Name("social-action3")]
	SocialAction3,

	[Name("social-action4")]
	SocialAction4,

	[Name("social-action5")]
	SocialAction5,

	[Name("social-action6")]
	SocialAction6,

	[Name("social-action7")]
	SocialAction7,

	[Name("social-action8")]
	SocialAction8,

	[Name("social-action9")]
	SocialAction9,

	[Name("social-action10")]
	SocialAction10,

	[Name("toggle-auto-move-forward")]
	ToggleAutoMoveForward,

	[Name("enter-slash-chatting")]
	EnterSlashChatting,

	[Name("enter-normal-chatting")]
	EnterNormalChatting,

	[Name("enter-party-chatting")]
	EnterPartyChatting,

	[Name("enter-local-chatting")]
	EnterLocalChatting,

	[Name("enter-private-chatting")]
	EnterPrivateChatting,

	[Name("toggle-hud")]
	ToggleHud,

	[Name("toggle-light-hud-mode")]
	ToggleLightHudMode,

	[Name("camera-up")]
	CameraUp,

	[Name("camera-down")]
	CameraDown,

	[Name("camera-zoomin")]
	CameraZoomin,

	[Name("camera-zoomout")]
	CameraZoomout,

	[Name("toggle-voice-chat-option")]
	ToggleVoiceChatOption,

	[Name("mark1")]
	Mark1,

	[Name("mark2")]
	Mark2,

	[Name("mark3")]
	Mark3,

	[Name("mark4")]
	Mark4,

	[Name("mark5")]
	Mark5,

	[Name("mark6")]
	Mark6,

	[Name("mark7")]
	Mark7,

	[Name("mark8")]
	Mark8,

	[Name("mark-timer")]
	MarkTimer,

	[Name("mark-timer-immune-break-down")]
	MarkTimerImmuneBreakDown,

	[Name("mark-timer-immune-break-kneel")]
	MarkTimerImmuneBreakKneel,

	[Name("mark-timer-immune-break-stun")]
	MarkTimerImmuneBreakStun,

	[Name("mark-timer-immune-break-knockback")]
	MarkTimerImmuneBreakKnockback,

	[Name("unmark")]
	Unmark,

	[Name("headline-start-timer")]
	HeadlineStartTimer,

	[Name("minimap-depth-plus")]
	MinimapDepthPlus,

	[Name("minimap-depth-minus")]
	MinimapDepthMinus,

	[Name("rotate-camera")]
	RotateCamera,

	[Name("rotate-player")]
	RotatePlayer,

	[Name("reset-camera")]
	ResetCamera,

	[Name("toggle-bns")]
	ToggleBns,

	[Name("notification-accept")]
	NotificationAccept,

	[Name("notification-decline")]
	NotificationDecline,

	[Name("toggle-joypad-ui-mode")]
	ToggleJoypadUiMode,

	[Name("close-floating-panels")]
	CloseFloatingPanels,

	[Name("joypad-esc")]
	JoypadEsc,

	[Name("joypad-set-mouse-position")]
	JoypadSetMousePosition,

	[Name("joypad-toggle-rotate-type")]
	JoypadToggleRotateType,

	[Name("turn-on-social-mode")]
	TurnOnSocialMode,

	[Name("enter-party-search-channel-in-party-match")]
	EnterPartySearchChannelInPartyMatch,

	[Name("toggle-sns")]
	ToggleSns,

	[Name("toggle-powerbook")]
	TogglePowerbook,

	[Name("toggle-skill-powerbook")]
	ToggleSkillPowerbook,

	[Name("toggle-nshop")]
	ToggleNshop,

	[Name("toggle-character-info")]
	ToggleCharacterInfo,

	[Name("toggle-market")]
	ToggleMarket,

	[Name("toggle-money-market")]
	ToggleMoneyMarket,

	[Name("toggle-postbox")]
	TogglePostbox,

	[Name("toggle-tencent-in-game-shop")]
	ToggleTencentInGameShop,

	[Name("toggle-tencent-cross")]
	ToggleTencentCross,

	[Name("toggle-second-password")]
	ToggleSecondPassword,

	[Name("toggle-achievement")]
	ToggleAchievement,

	[Name("toggle-pc-filter-hidden")]
	TogglePcFilterHidden,

	[Name("toggle-guild2")]
	ToggleGuild2,

	[Name("toggle-economy-communication")]
	ToggleEconomyCommunication,

	[Name("cancel-order")]
	CancelOrder,

	[Name("invite-party")]
	InviteParty,

	[Name("duel-start")]
	DuelStart,

	[Name("toggle-friend")]
	ToggleFriend,

	[Name("toggle-wardrobe")]
	ToggleWardrobe,

	[Name("toggle-dragon-jade-shop")]
	ToggleDragonJadeShop,

	[Name("toggle-ranking")]
	ToggleRanking,

	[Name("toggle-ingame-notice")]
	ToggleIngameNotice,

	[Name("change-item-bar-page")]
	ChangeItemBarPage,

	[Name("duel-tag-order-first")]
	DuelTagOrderFirst,

	[Name("duel-tag-order-random")]
	DuelTagOrderRandom,

	[Name("leave-arena-square")]
	LeaveArenaSquare,

	[Name("quick-turn")]
	QuickTurn,

	[Name("joypad-special-function1")]
	JoypadSpecialFunction1,

	[Name("joypad-special-function2")]
	JoypadSpecialFunction2,

	[Name("accelerate-mouse-cursor-speed")]
	AccelerateMouseCursorSpeed,

	[Name("joypad-mouse-left-button")]
	JoypadMouseLeftButton,

	[Name("joypad-mouse-right-button")]
	JoypadMouseRightButton,

	[Name("change-pet-filter-hidden")]
	ChangePetFilterHidden,

	[Name("toggle-damage-meter")]
	ToggleDamageMeter,

	[Name("toggle-item-graph")]
	ToggleItemGraph,

	[Name("duel-camera-fitpawn-new")]
	DuelCameraFitpawnNew,

	[Name("duel-camera-free-new")]
	DuelCameraFreeNew,

	[Name("duel-camera-quater-new")]
	DuelCameraQuaterNew,

	[Name("duel-camera-alpha-new")]
	DuelCameraAlphaNew,

	[Name("duel-camera-beta-new")]
	DuelCameraBetaNew,

	[Name("duel-camera-free-up-new")]
	DuelCameraFreeUpNew,

	[Name("duel-camera-free-down-new")]
	DuelCameraFreeDownNew,

	[Name("toggle-goods-store")]
	ToggleGoodsStore,

	[Name("enter-skill-training-room")]
	EnterSkillTrainingRoom,

	[Name("toggle-arena-entrance")]
	ToggleArenaEntrance,

	[Name("toggle-appearance-pictorial")]
	ToggleAppearancePictorial,

	[Name("toggle-hybrid-mode")]
	ToggleHybridMode,

	[Name("toggle-personal-customize")]
	TogglePersonalCustomize,

	[Name("toggle-context-simple-mode")]
	ToggleContextSimpleMode,

	[Name("toggle-mentoring")]
	ToggleMentoring,

	[Name("toggle-pvp-enable")]
	TogglePvpEnable,

	[Name("nova-equip-skill-quick-slot")]
	NovaEquipSkillQuickSlot,

	[Name("toggle-account-contents")]
	ToggleAccountContents,

	[Name("toggle-contents-journal-2")]
	ToggleContentsJournal2,

	[Name("toggle-fish-collecting")]
	ToggleFishCollecting,

	[Name("toggle-slate")]
	ToggleSlate,

	[Name("toggle-auto-mode")]
	ToggleAutoMode,

	[Name("toggle-auto-mode-option")]
	ToggleAutoModeOption,

	[Name("toggle-auto-mode-enable-item-support")]
	ToggleAutoModeEnableItemSupport,

	[Name("toggle-pvp-data")]
	TogglePvpData,

	[Name("toggle-ride-vehicle")]
	ToggleRideVehicle,

	[Name("toggle-museum")]
	ToggleMuseum,

	[Name("equip-spare-item")]
	EquipSpareItem,

	[Name("toggle-spare-item")]
	ToggleSpareItem,

	[Name("unk-")]
	Unk178,

	[Name("toggle-glyph-inventory")]
	ToggleGlyphInventory,

	[Name("toggle-constellation")]
	ToggleConstellation,

	[Name("toggle-sealed-scroll")]
	ToggleSealedScroll,

	[Name("toggle-inter-battle-field")]
	ToggleInterBattleField,

	[Name("toggle-wanted-mission")]
	ToggleWantedMission,

	[Name("toggle-expedition")]
	ToggleExpedition,
}