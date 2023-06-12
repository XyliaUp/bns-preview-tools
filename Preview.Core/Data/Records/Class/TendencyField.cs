﻿using Xylia.Preview.Common.Attribute;
using Xylia.Preview.Common.Interface;

namespace Xylia.Preview.Data.Record
{
	[AliasRecord]
	public sealed class TendencyField : BaseRecord, Attraction
	{
		[Signal("tendency-field-name2")]
		public Text TendencyFieldName2;

		[Signal("tendency-field-desc")]
		public Text TendencyFieldDesc;

		[Signal("reward-summary")]
		public AttractionRewardSummary RewardSummary;

		[Signal("ui-text-grade")]
		public byte UiTextGrade;


		#region Interface Functions
		public string GetName() => this.TendencyFieldName2.GetText();

		public string GetDescribe() => this.TendencyFieldDesc.GetText();
		#endregion
	}
}