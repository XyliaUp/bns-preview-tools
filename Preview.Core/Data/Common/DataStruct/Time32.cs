﻿namespace Xylia.Preview.Data.Common.DataStruct;
/// <summary>
///  Represents an instant in time
/// </summary>
public struct Time32(int Ticks)
{
	public int Ticks = Ticks;

	public static Time32 Parse(string input)
	{
		throw new NotImplementedException();
	}
}