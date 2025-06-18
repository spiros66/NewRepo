//*************************************************************************************************
// Assembly         : Sillogos v2.0
// Author           : Óðýñïò
// Created          : 01-04-2023
//
// Last Modified By : Óðýñïò
// Last Modified On : 01-04-2023
// Description      : 
//
// Copyright        : (c) Spiros. All rights reserved.
//*************************************************************************************************

using System.Globalization;
using System.Windows.Controls;

namespace Sillogos.Helpers
{
    public class NumericRangeValidator : ValidationRule
    {
        private int _min;
        private int _max;
        private string _errorMessage;
        public NumericRangeValidator()
        {
        }

        public int Min
        {
            get => _min;
            set => _min = value;
        }

        public int Max
        {
            get => _max;
            set => _max = value;
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => _errorMessage = value;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            decimal _value = decimal.Parse(value.ToString(), CultureInfo.CurrentCulture);

            //if (!int.TryParse(value.ToString(), out _value))
            //    return new ValidationResult(false, "Please enter a valid integer value.));
            //else 
            if ((_value < Min) || (_value > Max))
            {
                return new ValidationResult(false,
                    "Please enter value between: " + Min + " and " + Max + ".");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}