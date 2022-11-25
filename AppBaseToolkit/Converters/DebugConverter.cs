using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace AppBaseToolkit.Converters
{
    public class DebugConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        /// <inheritdoc />
        public object? Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                Debug.WriteLine("DebugConverter: input object is null");
            else
                Debug.WriteLine($"DebugConverter: input object has type {value.GetType().Name} value={value}");
            return value;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        #endregion
    }
}
