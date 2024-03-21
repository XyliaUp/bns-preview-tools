using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Xylia.Preview.Data.Models;

namespace Xylia.Preview.UI.Extensions;
public class TextResource : MarkupExtension, IValueConverter
{
    #region Constructorss
    public TextResource()
    {

    }

    public TextResource(string? path)
    {
        Path = new PropertyPath(path);
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        #region Converter
        var provideValueTargetService = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
        if (provideValueTargetService == null) return null;

        if (provideValueTargetService.TargetObject is not FrameworkElement ||
            provideValueTargetService.TargetProperty is not DependencyProperty)
            return this;
        #endregion

        #region binding
        Binding binding = new()
        {
            Path = Path,
            XPath = XPath,
            Mode = BindingMode.OneWay,
            UpdateSourceTrigger = UpdateSourceTrigger,
            Converter = this
        };

        if (RelativeSource != null) binding.RelativeSource = RelativeSource;
        if (ElementName != null) binding.ElementName = ElementName;
        if (Source != null) binding.Source = Source;

        return binding.ProvideValue(serviceProvider);
        #endregion
    }
    #endregion

    #region Binding Members
    /// <summary>
    /// The source path (for CLR bindings).
    /// </summary>
    public object? Source { get; set; }

    /// <summary>
    /// The source path (for CLR bindings).
    /// </summary>
    public PropertyPath? Path { get; set; }

    /// <summary>
    /// The XPath path (for XML bindings).
    /// </summary>
    [DefaultValue(null)]
    public string? XPath { get; set; }

    /// <summary>
    /// Update type
    /// </summary>
    [DefaultValue(UpdateSourceTrigger.Default)]
    public UpdateSourceTrigger UpdateSourceTrigger { get; set; }

    /// <summary>
    /// Description of the object to use as the source, relative to the target element.
    /// </summary>
    [DefaultValue(null)]
    public RelativeSource? RelativeSource { get; set; }

    /// <summary>
    /// Name of the element to use as the source
    /// </summary>
    [DefaultValue(null)]
    public string? ElementName { get; set; }
    #endregion

    #region IValueConverter
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value.GetText();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
    #endregion
}