﻿using System;
using System.Globalization;
using System.Windows.Data;
using JetBrains.Annotations;

namespace AppBaseToolkit.Converters
{
    [PublicAPI]
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool result && result)
            {
                return System.Windows.Visibility.Visible;
            }

            return System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}