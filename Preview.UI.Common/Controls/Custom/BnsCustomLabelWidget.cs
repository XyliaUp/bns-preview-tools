using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Xylia.Preview.Data.Models;
using Xylia.Preview.UI.Controls.Primitives;
using Xylia.Preview.UI.Documents;
using Xylia.Preview.UI.Documents.Primitives;
using Xylia.Preview.UI.Extensions;

namespace Xylia.Preview.UI.Controls;
/// <summary>
/// BnsCustomLabelWidget element displays text.
/// </summary>
public class BnsCustomLabelWidget : BnsCustomBaseWidget, IContentHost
{
	#region Public Properties
	public Dictionary<int, Timer> Timers { get; } = [];

	public static CopyMode CopyMode { get; set; }
	#endregion

	#region Dependency Properties
	private static readonly Type Owner = typeof(BnsCustomLabelWidget);

	/// <summary>
	/// DependencyProperty for <see cref="Text" /> property.
	/// </summary>
	public static readonly DependencyProperty TextProperty = Owner.Register("Text", string.Empty,
		FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, OnTextChanged);

	/// <summary>
	/// The Text property defines the content (text) to be displayed.
	/// </summary>
	[Localizability(LocalizationCategory.Text)]
	public string Text
	{
		get { return (string)GetValue(TextProperty); }
		set { SetValue(TextProperty, value); }
	}

	public static readonly DependencyProperty ArgumentsProperty = Owner.Register("Arguments", new TextArguments(),
		 FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits , OnArgumentsChanged);

	public TextArguments Arguments
	{
		get { return (TextArguments)GetValue(ArgumentsProperty); }
		set { SetValue(ArgumentsProperty, _container.Arguments = value); }
	}
	#endregion


	#region Protected Methods
	protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
	{
		base.OnMouseDoubleClick(e);

		var raw = String?.LabelText.Text;
		var text = CopyMode switch
		{
			CopyMode.None => null,
			CopyMode.Trimmed => TextContainer.Cut(TextExtension.Replace(raw, Arguments)),
			CopyMode.Regular => TextExtension.Replace(raw, Arguments),
			CopyMode.Original => raw,

			_ => throw new NotSupportedException(),
		};

		if (string.IsNullOrWhiteSpace(text)) return;
		Clipboard.SetText(text);
	}

	protected override Size MeasureOverride(Size constraint)
	{
		// Measure content size, then update if value is invalid
		RenderSize = constraint; 
		var size = DrawString(null, String, MetaData);
		if (double.IsInfinity(constraint.Width)) constraint.Width = size.Width;
		if (double.IsInfinity(constraint.Height)) constraint.Height = size.Height;

		return base.MeasureOverride(constraint);
	}
	#endregion

	#region Private Methods
	private static void OnArgumentsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var widget = (BnsCustomBaseWidget)d;
		widget.OnContainerChanged(widget, new EventArgs());
	}

	private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		var widget = (BnsCustomLabelWidget)d;
		var text = (string)e.NewValue;

		widget.UpdateString(text);
	}
	#endregion


	#region IContentHost
	//-------------------------------------------------------------------
	//
	//  IContentHost
	//
	//-------------------------------------------------------------------
	protected sealed override HitTestResult? HitTestCore(PointHitTestParameters hitTestParameters)
	{
		ArgumentNullException.ThrowIfNull(hitTestParameters);

		var r = new Rect(new Point(), RenderSize);
		return r.Contains(hitTestParameters.HitPoint) ?
			new PointHitTestResult(this, hitTestParameters.HitPoint) : null;
	}

	IInputElement IContentHost.InputHitTest(Point point)
	{
		if (point.X < 0 || point.Y < 0) return this;

		IInputElement ie = null;

		// retrieve IInputElement from the hit position.
		ie = _container.Document.InputHitTest(point);

		// If nothing has been hit, assume that element itself has been hit.
		return ie ?? this;
	}

	public ReadOnlyCollection<Rect>? GetRectangles(ContentElement child)
	{
		ArgumentNullException.ThrowIfNull(child);

		if (child is BaseElement e) return new ReadOnlyCollection<Rect>([e.FinalRect]);

		return null;
	}

	public IEnumerator<IInputElement> HostedElements
	{
		get
		{
			// Return enumerator created from document
			return this._container.Document.Children.GetEnumerator();
		}
	}

	void IContentHost.OnChildDesiredSizeChanged(UIElement child) => this.OnChildDesiredSizeChanged(child);
	#endregion
}

/// <summary>
/// Control the text copy mode of <see cref="BnsCustomLabelWidget"/>
/// </summary>
public enum CopyMode
{
	None,

	/// <summary>
	/// Text after replacing parameters and remove other XML parameters 
	/// </summary>
	Trimmed,

	/// <summary>
	/// Text after replacing parameters
	/// </summary>
	Regular,

	/// <summary>
	/// original text
	/// </summary>
	Original,
}