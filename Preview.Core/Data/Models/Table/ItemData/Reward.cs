namespace Xylia.Preview.Data.Models;
public sealed class Reward : ModelElement
{
	#region Fields
	public Ref<Item>[] FixedItem { get; set; }
	public short[] FixedItemMin { get; set; }
	public short[] FixedItemMax { get; set; }
	public Ref<Item>[] Group3Item { get; set; }
	public Ref<Item>[] SelectedItem { get; set; }
	public short[] SelectedItemCount { get; set; }
	public sbyte SelectedItemAssuredCount { get; set; }
	public Ref<Item>[] RandomItem { get; set; }
	#endregion


	#region Methods
	protected internal override void LoadHiddenField()
	{
		if (Attributes["rare-item-1"] != null && Attributes["rare-item-1-max"] is null)
		{
			this.Attributes["rare-item-1-max"] = 1;
			this.Attributes["rare-item-1-min"] = 1;

			for (int i = 1; i <= 40; i++)
			{
				var item = this.Attributes["rare-item-" + i];
				if (item != null) this.Attributes["rare-item-prob-weight-" + i] = 50;
			}
		}

		if (Attributes["group-3-item-1"] != null && Attributes["group-3-probability"] is null)
		{
			Attributes["group-3-probability"] = 1000;

			for (int i = 1; i <= 35; i++)
			{
				if (Attributes["group-3-item-" + i] != null)
				{
					Attributes["group-3-item-prob-weight-" + i] = 3;
				}
			}
		}

		if (Attributes["group-1-2-probability"] is null)
		{
			bool UseGroup1 = false;
			bool UseGroup2 = false;

			if (Attributes["group-2-item-1"] != null)
			{
				Attributes["group-2-assured-count"] = 1;
				UseGroup2 = true;
			}

			if (Attributes["group-1-item-1"] != null)
			{
				Attributes["group-1-assured-count"] = 1;
				UseGroup1 = true;
			}

			if (UseGroup1 || UseGroup2)
			{
				Attributes["group-1-2-probability"] = 100;

				if (UseGroup1) Attributes["group-1-prob-weight"] = UseGroup1 && UseGroup2 ? 50 : 100;
				if (UseGroup2) Attributes["group-2-prob-weight"] = UseGroup1 && UseGroup2 ? 50 : 100;
			}
		}
	}
	#endregion
}