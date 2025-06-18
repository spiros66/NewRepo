//*************************************************************************************************
// Assembly         : Sillogos v2.0
// Author           : Σπύρος
// Created          : 01-04-2023
//
// Last Modified By : Σπύρος
// Last Modified On : 01-04-2023
// Description      : 
//
// Copyright        : (c) Spiros. All rights reserved.
//*************************************************************************************************

using System;
using System.Globalization;
using System.Windows.Data;

namespace Sillogos.Converters
{
    public class SystemDecimalConverter : IValueConverter
    {
        private readonly char _systemDecimal = GetSystemDecimal();

        private static char GetSystemDecimal()
        {
            // Get the system's decimal separator from the current culture
            return CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert decimal? to string, replacing "." with the system's decimal separator
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return value; // Return null or empty string as-is
            }

            return value.ToString().Replace('.', _systemDecimal);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Convert string to decimal?, replacing the system's decimal separator with "."
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return value; // Return null or empty string as-is
            }

            return value.ToString().Replace(_systemDecimal, '.');
        }
    }
}