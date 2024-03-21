using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using CUE4Parse.BNS.Assets.Exports;
using CUE4Parse.UE4.Objects.Core.i18N;
using SkiaSharp.Views.WPF;
using Xylia.Preview.Common.Extension;
using Xylia.Preview.UI.Controls.Helpers;
using Xylia.Preview.UI.Converters;
using Xylia.Preview.UI.Documents;
using Xylia.Preview.UI.Documents.Primitives;
using Xylia.Preview.UI.Extensions;

namespace Xylia.Preview.UI.Controls.Primitives;
public abstract class BnsCustomBaseWidget : UserWidget, IMetaData
{
	#region Constructors
	internal BnsCustomBaseWidget()
	{
		// Create TextContainer associated with it
		_container = new TextContainer(this);
		_container.ChangedHandler += new EventHandler(OnContainerChanged);

		SetCurrentValue(ExpansionComponentListProperty, new ExpansionCollection());
		SetCurrentValue(StringProperty, new StringProperty());
	}
	#endregion

	#region DependencyProperty 
	private static readonly Type Owner = typeof(BnsCustomBaseWidget);
	public static readonly DependencyProperty BaseImagePropertyProperty = Owner.Register<ImageProperty>(nameof(BaseImageProperty), null);
	public static readonly DependencyProperty StringProperty = Owner.Register<StringProperty>(nameof(String), null, callback: OnStringChanged);
	public static readonly DependencyProperty MetaDataProperty = Owner.Register(nameof(MetaData), string.Empty, callback: IMetaData.UpdateData);

	public ImageProperty BaseImageProperty
	{
		get { return (ImageProperty)GetValue(BaseImagePropertyProperty); }
		set { SetValue(BaseImagePropertyProperty, value); }
	}

	public StringProperty String
	{
		get { return (StringProperty)GetValue(StringProperty); }
		set { SetValue(StringProperty, value); }
	}

	public string MetaData
	{
		get { return (string)GetValue(MetaDataProperty); }
		set { SetValue(MetaDataProperty, value); }
	}
	#endregion

	#region ExpansionComponent
	public static DependencyProperty ExpansionComponentListProperty = Owner.Register<ExpansionCollection>(nameof(ExpansionComponentList));
	public static DependencyProperty ExpansionProperty = Owner.Register<ObservableCollection<string>>(nameof(Expansion), default,
		FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, callback: OnExpansionChanged);

	private static void OnExpansionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var widget = (BnsCustomBaseWidget)d;
		if (e.NewValue is ObservableCollection<string> newIdCollection)
			newIdCollection.CollectionChanged += (_, _) => widget.InvalidateVisual();
	}

	public ExpansionCollection ExpansionComponentList
	{
		get { return (ExpansionCollection)GetValue(ExpansionComponentListProperty); }
		set { SetValue(ExpansionComponentListProperty, value); }
	}

	[TypeConverter(typeof(CollectionConvert<string>))]
	public ObservableCollection<string> Expansion
	{
		get { return (ObservableCollection<string>)GetValue(ExpansionProperty); }
		set { SetValue(ExpansionProperty, value); }
	}
	#endregion

	#region StringProperty
	internal readonly TextContainer _container;

	private static void OnStringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var widget = (BnsCustomBaseWidget)d;
		var value = (StringProperty)e.NewValue;

		widget.OnStringChanged(value);
		if (value != null) value.PropertyChanged += (_, _) => widget.OnStringChanged(value);
	}

	protected void OnStringChanged(StringProperty p)
	{
		// TODO: raise event
		OnContainerChanged(this, EventArgs.Empty);
	}

	internal void OnContainerChanged(object? sender, EventArgs e)
	{
		InvalidateMeasure();
		InvalidateArrange();
		InvalidateVisual();
	}

	//  IMetaData
	public void UpdateString(string? text)
	{
		this.String ??= new StringProperty();
		this.String.LabelText = new FText(text);
		this.OnStringChanged(String);
	}

	void IMetaData.UpdateTooltip(string? text)
	{
		this.ToolTip = text;
	}
	#endregion


	#region Protected Methods
	protected override Rect ArrangeChildren(UIElement child, Size constraint)
	{
		var rect = base.ArrangeChildren(child, constraint);

		// support for scroll
		if (child is not BnsCustomScrollBarWidget)
		{
			rect.X -= ScrollOffset.X;
			rect.Y -= ScrollOffset.Y;
		}

		return rect;
	}

	protected override void OnRender(DrawingContext dc)
	{
		// Using the Background brush, draw a rectangle that fills the render bounds of the widget.
		var background = Background;
		if (background != null) dc.DrawRectangle(background, null, new Rect(RenderSize));

		// Draw BaseImage & String 
		DrawImage(dc, BaseImageProperty);
		DrawString(dc, String, MetaData);

		#region ExpansionComponent
		if (!ExpansionComponentList.IsEmpty())
		{
			IEnumerable<ExpansionComponent> expansions;
			if (Expansion is null) expansions = [ExpansionComponentList.First()];
			else expansions = ExpansionComponentList.Where(e => Expansion.Contains(e.ExpansionName.PlainText));

			foreach (var e in expansions)
			{
				if (e.ExpansionType == ExpansionComponent.Type_IMAGE)
				{
					DrawImage(dc, e.ImageProperty);
				}
				else if (e.ExpansionType == ExpansionComponent.Type_STRING)
				{
					DrawString(dc, e.StringProperty, e.MetaData);
				}
				else Debug.Assert(string.IsNullOrEmpty(e.ExpansionType.PlainText));
			}
		}
		#endregion
	}


	protected void DrawImage(DrawingContext? ctx, ImageProperty p)
	{
		if (p is null) return;

		// layout
		var size = p.Measure(RenderSize.Parse(), out var source);
		if (source != null)
		{
			var pos = LayoutData.ComputeOffset(RenderSize, size, p.HorizontalAlignment, p.VerticalAlignment, p.Offset);
			ctx?.DrawImage(source.ToWriteableBitmap(), new Rect(pos, new Size(size.X, size.Y)));
		}
	}

	protected Size DrawString(DrawingContext? ctx, StringProperty p, string MetaData)
	{
		if (p is null) return default;

		var document = this._container.Document = new Paragraph() { FontSet = p.fontset };
		IMetaData.UpdateData(document, new(StringProperty, p, MetaData));
		BaseElement.InheritDependency(this, document);

		// layout
		var size = document.Measure(RenderSize);
		var pos = LayoutData.ComputeOffset(RenderSize, size.Parse(), p.HorizontalAlignment, p.VerticalAlignment, p.ClippingBound);

		if (ctx != null)
		{
			document.Arrange(new Rect(pos, size));
			document.OnRender(ctx);
		}

		return size;
	}
	#endregion
}