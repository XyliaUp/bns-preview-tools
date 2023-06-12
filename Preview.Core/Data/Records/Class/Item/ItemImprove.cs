﻿using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record
{
	public sealed class ItemImprove : BaseRecord
	{
		public byte Level;

		[Signal("success-option-list-id")]
		public int SuccessOptionListId;
	}
}