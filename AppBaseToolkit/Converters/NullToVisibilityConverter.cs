using System.Globalization;
using System.Windows.Data;
using System;
using JetBrains.Annotations;

namespace AppBaseToolkit.Converters;

[PublicAPI]
public class NullToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value == null 
            ? System.Windows.Visibility.Visible 
            : System.Windows.Visibility.Collapsed;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}