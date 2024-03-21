using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using CUE4Parse.BNS.Assets.Exports;
using Xylia.Preview.Data.Helpers;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Controls.Helpers;
public class ExpansionCollection : Collection<ExpansionComponent>
{
	#region Constructors
	public ExpansionCollection()
	{

	}

	internal ExpansionCollection(IEnumerable<ExpansionComponent> collections)
	{
		// use cloned instead of themselves
		foreach (var component in collections)
		{
			this.Add(component.Clone());
		}
	}
	#endregion

	#region Protected Methods
	protected override void InsertItem(int index, ExpansionComponent item)
	{
		base.InsertItem(index, item);
		dic[item.ExpansionName.Text] = item;
	}

	protected override void RemoveItem(int index)
	{
		var removedItem = this[index];

		base.RemoveItem(index);
		dic.Remove(removedItem.ExpansionName.Text);
	}

	protected override void SetItem(int index, ExpansionComponent item)
	{
		var originalItem = this[index];
		base.SetItem(index, item);

		dic.Remove(originalItem.ExpansionName.Text);
		dic[item.ExpansionName.Text] = item;
	}

	protected override void ClearItems()
	{
		base.ClearItems();
		dic.Clear();
	}

	public ExpansionComponent? this[string name] => dic!.GetValueOrDefault(name);
	#endregion

	#region Private Fields
	private readonly Dictionary<string, ExpansionComponent> dic = [];
	#endregion
}

internal interface IMetaData
{
	/// <summary>
	/// Set widget's text
	/// </summary>
	void UpdateString(string? text);

	/// <summary>
	/// Set widget's tooltip
	/// </summary>
	void UpdateTooltip(string? text);


	public static async void UpdateData(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (e.NewValue is not string s) return;

		var widget = (IMetaData)d;

		// Called by expansion
		if (e.OldValue is StringProperty sp)
		{
			widget.UpdateString(sp.LabelText?.Text);
		}

		// MetaData
		if (!string.IsNullOrEmpty(s))
		{
			// return if in design
			if (DesignerProperties.GetIsInDesignMode(d)) return;

			await Task.Run(() => FileCache.Data.Provider.GetTable<Text>());

			foreach (var meta in s.Split(';'))
			{
				var ls = meta.Split('=', 2);
				if (ls.Length < 2) continue;

				switch (ls[0])
				{
					case "textref": widget.UpdateString(ls[1].GetText()); return;
					case "tooltip": widget.UpdateTooltip(ls[1].GetText()); return;
					case "config": case "width": case "height": return;

					default: throw new NotSupportedException();
				}
			}
		}
	}
}