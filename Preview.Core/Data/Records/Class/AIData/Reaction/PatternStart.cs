﻿using Xylia.Preview.Common.Attribute;

namespace Xylia.Preview.Data.Record.ReactionClass;

/// <summary>
/// 挑战模式阶段开始
/// </summary>
[Signal("pattern-start")]
public sealed class PatternStart : Reaction
{
	public sbyte Index;
}