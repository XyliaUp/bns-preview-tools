﻿using Xylia.Preview.Data.Common.Interface;
using Xylia.Preview.Data.Common.DataStruct;
using Xylia.Preview.Data.Engine.BinData.Models;

namespace Xylia.Preview.Data.Models;
public sealed class Dungeon : Record, IAttraction
{
	public string Alias;

	public sbyte UiTextGrade;
	public Ref<Text> DungeonName2;
	public Ref<Text> DungeonDesc;


	#region Interface
	public string Text => this.DungeonName2.GetText();

	public string GetDescribe() => this.DungeonDesc.GetText();
	#endregion
}