using System.Globalization;
using System.Windows.Data;
using System;
using JetBrains.Annotations;

namespace AppBaseToolkit.Converters;

[PublicAPI]
public class NullToBooleanConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
    {
        return value == null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}