﻿using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class ClosetGroup : Record
{
	public short SortNo;

	public Ref<Item> ChargeOfItemForInstantPayment;
	public Ref<Item> ItemToBePaid;

	public bool CheckEquipCharacteristics;
}