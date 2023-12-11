﻿using System.Text;

using Xylia.Preview.Data.Common.Attribute;

namespace Xylia.Preview.Data.Engine.BinData.Models;
public sealed class TableWriterSettings
{
	internal static readonly TableWriterSettings DefaultSettings = new() 
	{
		ReleaseSide = ReleaseSide.Client,
		Encoding = Encoding.UTF8,
	};


	public ReleaseSide ReleaseSide { get; set; }

	public Encoding Encoding { get; set; }
}