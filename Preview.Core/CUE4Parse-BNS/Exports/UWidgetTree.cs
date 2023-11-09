using System.Diagnostics;

using CUE4Parse.BNS.Objects.Script;
using CUE4Parse.UE4.Assets.Readers;

using Newtonsoft.Json;

namespace CUE4Parse.UE4.Assets.Exports;
public class UWidgetTree : UObject
{
	public ResolvedObject RootWidget;   // UWidget

	public override void Deserialize(FAssetArchive Ar, long validPos)
	{
		base.Deserialize(Ar, validPos);
		RootWidget = GetOrDefault<ResolvedObject>(nameof(RootWidget));
	}


	public void LoadWidget()
	{
		Trace.WriteLine(GetFullName());
		LoadWidget(RootWidget.Load<UWidget>());
	}

	public static void LoadWidget(UObject widget, UBnsCustomBaseWidgetSlot widgetslot = null)
	{
		Trace.WriteLine($"\t{widget.ExportType}  {widget.Name}");
		if (widgetslot != null) Trace.WriteLine($"\t\t{JsonConvert.SerializeObject(widgetslot.LayoutData)}");




		{
			var Slot = widget.GetOrDefault<ResolvedObject>("Slot")?.Load<UBnsCustomBaseWidgetSlot>();
			//if (Slot != null) Trace.WriteLine(JsonConvert.SerializeObject(Slot.LayoutData, Formatting.Indented));
		}

		foreach (var Slot in widget.GetOrDefault("Slots", Array.Empty<ResolvedObject>()))
		{
			var slot = Slot.Load<UBnsCustomBaseWidgetSlot>();
			var content = slot.Content.Load();

			LoadWidget(content, slot);
		}

		if (widget.GetOrDefault<ResolvedObject>("WidgetTree")?.Load() is UWidgetTree WidgetTree)
		{
			LoadWidget(WidgetTree);
		}
	}
}