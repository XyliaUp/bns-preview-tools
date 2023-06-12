using System.Collections.Generic;

using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Seq;
using Xylia.Extension;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class ItemCombat : BaseRecord
	{
		[Signal("job-style")]
		public JobStyleSeq JobStyle;


		[Signal("item-skill-1")]
		public ItemSkill ItemSkill1;

		[Signal("item-skill-2")]
		public ItemSkill ItemSkill2;

		[Signal("item-skill-3")]
		public ItemSkill ItemSkill3;

		[Signal("item-skill-4")]
		public ItemSkill ItemSkill4;

		[Signal("item-skill-5")]
		public ItemSkill ItemSkill5;

		[Signal("item-skill-6")]
		public ItemSkill ItemSkill6;

		[Signal("item-skill-7")]
		public ItemSkill ItemSkill7;

		[Signal("item-skill-8")]
		public ItemSkill ItemSkill8;

		[Signal("item-skill-9")]
		public ItemSkill ItemSkill9;

		[Signal("item-skill-10")]
		public ItemSkill ItemSkill10;

		[Signal("item-skill-11")]
		public ItemSkill ItemSkill11;

		[Signal("item-skill-12")]
		public ItemSkill ItemSkill12;

		[Signal("item-skill-13")]
		public ItemSkill ItemSkill13;

		[Signal("item-skill-14")]
		public ItemSkill ItemSkill14;

		[Signal("item-skill-15")]
		public ItemSkill ItemSkill15;

		[Signal("item-skill-16")]
		public ItemSkill ItemSkill16;



		[Signal("item-skill-second-1")]
		public ItemSkill ItemSkillSecond1;

		[Signal("item-skill-second-2")]
		public ItemSkill ItemSkillSecond2;

		[Signal("item-skill-second-3")]
		public ItemSkill ItemSkillSecond3;

		[Signal("item-skill-second-4")]
		public ItemSkill ItemSkillSecond4;

		[Signal("item-skill-second-5")]
		public ItemSkill ItemSkillSecond5;

		[Signal("item-skill-second-6")]
		public ItemSkill ItemSkillSecond6;

		[Signal("item-skill-second-7")]
		public ItemSkill ItemSkillSecond7;

		[Signal("item-skill-second-8")]
		public ItemSkill ItemSkillSecond8;

		[Signal("item-skill-second-9")]
		public ItemSkill ItemSkillSecond9;

		[Signal("item-skill-second-10")]
		public ItemSkill ItemSkillSecond10;

		[Signal("item-skill-second-11")]
		public ItemSkill ItemSkillSecond11;

		[Signal("item-skill-second-12")]
		public ItemSkill ItemSkillSecond12;

		[Signal("item-skill-second-13")]
		public ItemSkill ItemSkillSecond13;

		[Signal("item-skill-second-14")]
		public ItemSkill ItemSkillSecond14;

		[Signal("item-skill-second-15")]
		public ItemSkill ItemSkillSecond15;

		[Signal("item-skill-second-16")]
		public ItemSkill ItemSkillSecond16;


		[Signal("item-skill-third-1")]
		public ItemSkill ItemSkillThird1;

		[Signal("item-skill-third-2")]
		public ItemSkill ItemSkillThird2;

		[Signal("item-skill-third-3")]
		public ItemSkill ItemSkillThird3;

		[Signal("item-skill-third-4")]
		public ItemSkill ItemSkillThird4;

		[Signal("item-skill-third-5")]
		public ItemSkill ItemSkillThird5;

		[Signal("item-skill-third-6")]
		public ItemSkill ItemSkillThird6;

		[Signal("item-skill-third-7")]
		public ItemSkill ItemSkillThird7;

		[Signal("item-skill-third-8")]
		public ItemSkill ItemSkillThird8;

		[Signal("item-skill-third-9")]
		public ItemSkill ItemSkillThird9;

		[Signal("item-skill-third-10")]
		public ItemSkill ItemSkillThird10;

		[Signal("item-skill-third-11")]
		public ItemSkill ItemSkillThird11;

		[Signal("item-skill-third-12")]
		public ItemSkill ItemSkillThird12;

		[Signal("item-skill-third-13")]
		public ItemSkill ItemSkillThird13;

		[Signal("item-skill-third-14")]
		public ItemSkill ItemSkillThird14;

		[Signal("item-skill-third-15")]
		public ItemSkill ItemSkillThird15;

		[Signal("item-skill-third-16")]
		public ItemSkill ItemSkillThird16;



		[Signal("skill-build-up-parent-skill3-id-1")]
		public int SkillBuildUpParentSkill3Id1;

		[Signal("skill-build-up-parent-skill3-id-2")]
		public int SkillBuildUpParentSkill3Id2;

		[Signal("skill-build-up-parent-skill3-id-3")]
		public int SkillBuildUpParentSkill3Id3;

		[Signal("skill-build-up-level-1")]
		public byte SkillBuildUpLevel1;

		[Signal("skill-build-up-level-2")]
		public byte SkillBuildUpLevel2;

		[Signal("skill-build-up-level-3")]
		public byte SkillBuildUpLevel3;

		[Signal("skill-modify-info-group")]
		public SkillModifyInfoGroup SkillModifyInfoGroup;
	

		#region Functions
		public List<ItemSkill> ItemSkills
		{
			get
			{
				var result = new List<ItemSkill>();
				// **********************************************
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkill1]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkill2]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkill3]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkill4]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkill5]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkill6]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkill7]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkill8]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkill9]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkill10]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkill11]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkill12]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkill13]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkill14]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkill15]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkill16]);
				// **********************************************
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillSecond1]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillSecond2]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillSecond3]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillSecond4]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillSecond5]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillSecond6]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillSecond7]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillSecond8]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillSecond9]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillSecond10]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillSecond11]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillSecond12]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillSecond13]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillSecond14]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillSecond15]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillSecond16]);
				// **********************************************
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillThird1]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillThird2]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillThird3]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillThird4]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillThird5]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillThird6]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillThird7]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillThird8]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillThird9]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillThird10]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillThird11]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillThird12]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillThird13]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillThird14]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillThird15]);
				result.AddItem(FileCache.Data.ItemSkill[this.ItemSkillThird16]);

				return result;
			}
		}
		#endregion
	}
}