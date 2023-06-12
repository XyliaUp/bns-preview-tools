
using Xylia.Preview.Common.Attribute;


namespace Xylia.Preview.Data.Record.QuestData.TutorialCase
{
	public sealed class DetachWeaponGem : TutorialCaseBase
	{
		[Side(ReleaseSide.Client)]
		public string Weapon;

		[Side(ReleaseSide.Client)]
		public string Gem;
	}

	public sealed class WeaponGem : TutorialCaseBase
	{
		[Side(ReleaseSide.Client)]
		public string Weapon;

		[Side(ReleaseSide.Client)]
		public string Gem;
	}
}