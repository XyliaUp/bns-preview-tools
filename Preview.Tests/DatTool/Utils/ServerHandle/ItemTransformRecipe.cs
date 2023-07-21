using System.Xml;

using Xylia.Preview.Tests.DatTool.Utils.ServerHandle;

namespace Xylia.Preview.Tests.DatTool.Utils.ServerHandle;

public class ItemTransformRecipe : TableHandle
{
	protected override void Fix(XmlElement record)
	{
		var Warning = record.Attributes["warning"]?.Value;
		if (Warning is "lower" or "lower-gemslotreset")
		{
			record.SetAttribute("random-result", "lower");
		}




		bool IsSure = Warning is null or "gemslotreset" or "delete-particle" or "delete-design";

		for (int i = 1; i <= 8; i++)
		{
			if (record.Attributes["fixed-ingredient-" + i] != null && record.Attributes["fixed-ingredient-stack-count-" + i] is null)
			{
				record.SetAttribute("fixed-ingredient-stack-count-" + i, "1");
			}
		}


		bool UseRandom = record.Attributes["use-random"]?.Value == "y";
		if (UseRandom)
		{
			if (record.Attributes["random-item-success-probability"] != null)
				return;

			int RandomMax = 20;
			for (int i = 1; i <= RandomMax; i++)
			{
				var attr = record.Attributes[$"random-item-{i}"];
				if (attr != null)
				{
					record.SetAttribute($"random-item-stack-count-{i}", "1");
				}
				else if (i != 1)
				{
					const int q = 3;
					int TotalCount = i - 1;
					int TotalWeight = 1 * (1 - (int)Math.Pow(q, TotalCount)) / (1 - q);

					//概率权重和需要超过 1000
					int ExtraWeight = 0;
					if (TotalWeight < 1000)
						ExtraWeight = (int)Math.Ceiling((decimal)(1000 - TotalWeight) / TotalCount);

					for (int x = 1; x <= TotalCount; x++)
					{
						int Weight = 1 * (int)Math.Pow(q, x - 1) + ExtraWeight;
						record.SetAttribute($"random-item-select-prop-weight-{x}", Weight.ToString());
					}

					//最大值 100
					record.SetAttribute($"random-item-success-probability", IsSure ? "100" : "20");
					record.SetAttribute($"random-item-total-count", TotalCount.ToString());

					break;
				}
				else break;
			}
		}
		else
		{
			if (record.Attributes["normal-item-success-probability"] == null)
			{
				int MormalMax = 10;
				for (int i = 1; i <= MormalMax; i++)
				{
					var attr = record.Attributes[$"normal-item-{i}"];
					if (attr != null)
					{
						record.SetAttribute($"normal-item-stack-count-{i}", "1");
					}
					else if (i != 1)
					{
						record.SetAttribute($"normal-item-success-probability", IsSure ? "1000" : "190");
						record.SetAttribute($"normal-item-select-count", (i - 1).ToString());
						record.SetAttribute($"normal-item-total-count", (i - 1).ToString());

						break;
					}
					else break;
				}
			}

			if (record.Attributes["rare-item-success-probability"] == null)
			{
				int RareMax = 10;
				for (int i = 1; i <= RareMax; i++)
				{
					var attr = record.Attributes[$"rare-item-{i}"];
					if (attr != null)
					{
						record.SetAttribute($"rare-item-stack-count-{i}", "1");
					}
					else if (i != 1)
					{
						record.SetAttribute($"rare-item-success-probability", IsSure ? "1000" : "190");
						record.SetAttribute($"rare-item-select-count", (i - 1).ToString());
						record.SetAttribute($"rare-item-total-count", (i - 1).ToString());
						break;
					}
					else break;
				}
			}
		}
	}
}