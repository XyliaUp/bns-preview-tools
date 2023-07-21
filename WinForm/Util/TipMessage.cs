using System.Diagnostics;
using System.Xml;

using Xylia.Extension;

namespace Xylia.Match.Util;
public class TipMessage
{
	readonly Dictionary<int, TipInfo> Tips = new();

	readonly int TotalWeight = 0;	

	public TipMessage()
	{
		var doc = new XmlDocument();
		doc.LoadXml(Resources.Resources.Tips);
		foreach (XmlNode item in doc.SelectNodes("*/record"))
		{
			var tip = new TipInfo()
			{
				Text = item.Attributes["text"]?.Value,
				Type = item.Attributes["module"]?.Value.ToEnum<TipInfo.type>() ?? default,
			};


			int proweight = (item.Attributes["proweight"]?.Value).ToInt();
			if (proweight <= 0) proweight = 1;

			Tips.Add(TotalWeight, tip);
			TotalWeight += proweight;
		}
	}


	public string GetNext
	{
		get
		{
			int random = new Random().Next(0, TotalWeight);
			int TempWeight = 0;

			foreach (var tip in Tips)
			{
				if (random < TempWeight) return tip.Value.Text;
				else TempWeight += tip.Key;
			}

			throw new Exception("bad tip");
		}
	}
}


public class TipInfo
{
	public string Text;
	public type Type;

	public enum type
	{
		None,

		Prop,

		Quest,
	}
}
