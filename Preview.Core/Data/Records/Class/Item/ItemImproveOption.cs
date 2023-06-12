using System.Collections.Generic;
using System.Linq;

using Xylia.Preview.Common.Attribute;
using Xylia.Extension;
using Xylia.Preview.Common.Seq;
using Xylia.Preview.Common.Extension;

namespace Xylia.Preview.Data.Record
{
	public sealed class ItemImproveOption : BaseRecord
	{
		public byte Level;

		public MainAbility Ability;

		[Signal("ability-value")]
		public int AbilityValue;

		public Effect Effect;

		[Signal("effect-description")]
		public string EffectDescription;

		[Signal("skill-modify-info-group-1")]
		public SkillModifyInfoGroup SkillModifyInfoGroup1;

		[Signal("skill-modify-info-group-2")]
		public SkillModifyInfoGroup SkillModifyInfoGroup2;

		[Signal("skill-modify-info-group-3")]
		public SkillModifyInfoGroup SkillModifyInfoGroup3;

		[Signal("skill-modify-info-group-4")]
		public SkillModifyInfoGroup SkillModifyInfoGroup4;

		[Signal("skill-modify-info-group-5")]
		public SkillModifyInfoGroup SkillModifyInfoGroup5;

		[Signal("skill-modify-info-group-6")]
		public SkillModifyInfoGroup SkillModifyInfoGroup6;

		[Signal("skill-modify-info-group-7")]
		public SkillModifyInfoGroup SkillModifyInfoGroup7;

		[Signal("skill-modify-info-group-8")]
		public SkillModifyInfoGroup SkillModifyInfoGroup8;

		[Signal("skill-modify-info-group-9")]
		public SkillModifyInfoGroup SkillModifyInfoGroup9;

		[Signal("skill-modify-info-group-10")]
		public SkillModifyInfoGroup SkillModifyInfoGroup10;

		public string Additional;

		[Signal("draw-option-icon")]
		public string DrawOptionIcon;


		#region Functions
		/// <summary>
		/// 获取显示文本
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			string AdditionalText = Additional.GetText();

			//获取效果类型部分提示
			if (!string.IsNullOrWhiteSpace(this.EffectDescription)) return $"{this.EffectDescription.GetText()}{AdditionalText}";

			//获取Property加成部分提示
			if (this.Ability != MainAbility.None) return this.Ability.GetName(this.AbilityValue) + AdditionalText;


			//获取武功加成部分提示
			List<string> ResultInfo = new();
			//ResultInfo.AddItem(FileCacheData.Data.SkillModifyInfoGroup[this.SkillModifyInfoGroup1]?.ToString());
			//ResultInfo.AddItem(FileCacheData.Data.SkillModifyInfoGroup[this.SkillModifyInfoGroup2]?.ToString());
			//ResultInfo.AddItem(FileCacheData.Data.SkillModifyInfoGroup[this.SkillModifyInfoGroup3]?.ToString());
			//ResultInfo.AddItem(FileCacheData.Data.SkillModifyInfoGroup[this.SkillModifyInfoGroup4]?.ToString());
			//ResultInfo.AddItem(FileCacheData.Data.SkillModifyInfoGroup[this.SkillModifyInfoGroup5]?.ToString());
			ResultInfo.AddItem(FileCache.Data.SkillModifyInfoGroup[this.SkillModifyInfoGroup6]?.ToString());
			ResultInfo.AddItem(FileCache.Data.SkillModifyInfoGroup[this.SkillModifyInfoGroup7]?.ToString());
			ResultInfo.AddItem(FileCache.Data.SkillModifyInfoGroup[this.SkillModifyInfoGroup8]?.ToString());
			ResultInfo.AddItem(FileCache.Data.SkillModifyInfoGroup[this.SkillModifyInfoGroup9]?.ToString());
			ResultInfo.AddItem(FileCache.Data.SkillModifyInfoGroup[this.SkillModifyInfoGroup10]?.ToString());


			if (!ResultInfo.Any()) return null;
			return ResultInfo.Aggregate("<font name=\"00008130.UI.Label_Green03_12\">", (sum, now) => sum + "<br/>" + now) + "</font>";
		}
		#endregion
	}
}