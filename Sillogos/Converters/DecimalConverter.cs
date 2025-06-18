using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Sillogos.Converters
{
    public class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "";
            return ((decimal)value).ToString(culture); // Use current culture for display
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value as string;
            if (string.IsNullOrEmpty(strValue)) return null;

            // 1. Try parsing with the InvariantCulture (for ".")
            //if (decimal.TryParse(strValue, NumberStyles.Any, CultureInfo.CurrentCulture, out decimal result))
            //{
            //    return result;
            //}

            // 2. If that fails, try with the current culture (for ",")
            if (decimal.TryParse(strValue, NumberStyles.Any, CultureInfo.CurrentCulture, out decimal result))
            {
                return result;
            }

            return DependencyProperty.UnsetValue; // Signal invalid input
        }
    }
}