﻿namespace Xylia.Preview.Data.Models;
public sealed class ItemEvent : ModelElement
{
	public DateTime EventExpirationTime { get; set; }

	public bool IsExpiration => this.EventExpirationTime < DateTime.Now;
}