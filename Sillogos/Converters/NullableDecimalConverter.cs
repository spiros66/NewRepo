using System;
using System.Globalization;
using System.Windows.Data;

namespace Sillogos.Converters
{
    public class NullableDecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert decimal? to string using the current culture
            if (value is decimal decimalValue)
            {
                return decimalValue.ToString(culture);
            }
            return string.Empty; // Return empty string for null
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert string to decimal? using the current culture
            if (value is string stringValue)
            {
                if (string.IsNullOrEmpty(stringValue))
                {
                    return null; // Return null for empty string
                }

                if (decimal.TryParse(stringValue, NumberStyles.Any, culture, out decimal result))
                {
                    return result; // Return parsed decimal
                }
            }
            return null; // Return null for invalid input
        }
    }
}