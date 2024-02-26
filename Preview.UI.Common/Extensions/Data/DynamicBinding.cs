﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Xylia.Preview.UI.Extensions;

public class DynamicBinding : MarkupExtension
{
    #region Helper properties
    public static object GetDynamicBindingKeyHelper(DependencyObject obj)
    {
        return obj.GetValue(DynamicBindingKeyHelperProperty);
    }

    public static void SetDynamicBindingKeyHelper(DependencyObject obj, object value)
    {
        obj.SetValue(DynamicBindingKeyHelperProperty, value);
    }

    // Using a DependencyProperty as the backing store for DynamicBindingKeyHelper.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DynamicBindingKeyHelperProperty =
        DependencyProperty.RegisterAttached("DynamicBindingKeyHelper", typeof(object), typeof(DynamicBinding), new PropertyMetadata(null, ResourceKeyChanged));

    static void ResourceKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not FrameworkElement target ||
            e.NewValue is not Tuple<object, DependencyProperty> newVal)
            return;

        var dp = newVal.Item2;

        if (newVal.Item1 == null)
        {
            target.SetValue(dp, dp.GetMetadata(target).DefaultValue);
            return;
        }

        target.SetResourceReference(dp, newVal.Item1);
    }
    #endregion

    #region Constructorss
    public DynamicBinding()
    {

    }

    public DynamicBinding(string path)
    {
        Path = new PropertyPath(path);
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var provideValueTargetService = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
        if (provideValueTargetService == null) return null;

        if (provideValueTargetService.TargetObject != null &&
            provideValueTargetService.TargetObject.GetType().FullName == "System.Windows.SharedDp")
            return this;

        if (provideValueTargetService.TargetObject is not FrameworkElement targetObject ||
            provideValueTargetService.TargetProperty is not DependencyProperty targetProperty)
            return null;

        #region binding
        Binding binding = new Binding
        {
            Path = Path,
            XPath = XPath,
            Mode = Mode,
            UpdateSourceTrigger = UpdateSourceTrigger,
            Converter = Converter,
            ConverterParameter = ConverterParameter,
            ConverterCulture = ConverterCulture,
            FallbackValue = FallbackValue
        };

        if (RelativeSource != null) binding.RelativeSource = RelativeSource;
        if (ElementName != null) binding.ElementName = ElementName;
        if (Source != null) binding.Source = Source;
        #endregion

        var multiBinding = new MultiBinding
        {
            Converter = new HelperConverter(this),
            ConverterParameter = targetProperty,
        };

        multiBinding.Bindings.Add(binding);
        multiBinding.NotifyOnSourceUpdated = true;

        targetObject.SetBinding(DynamicBindingKeyHelperProperty, multiBinding);

        return null;
    }
    #endregion

    #region Binding Members
    /// <summary>
    /// The source path (for CLR bindings).
    /// </summary>
    public object Source { get; set; }

    /// <summary>
    /// The source path (for CLR bindings).
    /// </summary>
    public PropertyPath Path { get; set; }

    /// <summary>
    /// The XPath path (for XML bindings).
    /// </summary>
    [DefaultValue(null)]
    public string XPath { get; set; }

    /// <summary>
    /// Binding mode
    /// </summary>
    [DefaultValue(BindingMode.Default)]
    public BindingMode Mode { get; set; }

    /// <summary>
    /// Update type
    /// </summary>
    [DefaultValue(UpdateSourceTrigger.Default)]
    public UpdateSourceTrigger UpdateSourceTrigger { get; set; }

    /// <summary>
    /// The Converter to apply
    /// </summary>
    [DefaultValue(null)]
    public IValueConverter Converter { get; set; }

    /// <summary>
    /// The parameter to pass to converter.
    /// </summary>
    /// <value></value>
    [DefaultValue(null)]
    public object ConverterParameter { get; set; }

    /// <summary>
    /// Culture in which to evaluate the converter
    /// </summary>
    [DefaultValue(null)]
    [TypeConverter(typeof(CultureInfoIetfLanguageTagConverter))]
    public CultureInfo ConverterCulture { get; set; }

    /// <summary>
    /// Description of the object to use as the source, relative to the target element.
    /// </summary>
    [DefaultValue(null)]
    public RelativeSource RelativeSource { get; set; }

    /// <summary>
    /// Name of the element to use as the source
    /// </summary>
    [DefaultValue(null)]
    public string ElementName { get; set; }
    #endregion

    #region BindingBase Members
    /// <summary>
    /// Value to use when source cannot provide a value
    /// </summary>
    /// <remarks>
    /// Initialized to DependencyProperty.UnsetValue; if FallbackValue is not set, BindingExpression
    /// will return target property's default when Binding cannot get a real value.
    /// </remarks>
    public object FallbackValue { get; set; }

    /// <summary>
    /// Resource key prefix
    /// </summary>
    public string Prefix { get; set; }
    #endregion

    #region Nested types
    private class HelperConverter(DynamicBinding owner) : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            object key = owner.Prefix + values[0];
            return Tuple.Create(key, (DependencyProperty)parameter);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}

public class Binding : System.Windows.Data.Binding
{
    #region Constructorss
    public Binding() : base()
    {

    }

    public Binding(string path) : base(path)
    {

    }

    public Binding(DependencyObject source, string path) : base(path)
    {
        Source = source;
    }
    #endregion
}