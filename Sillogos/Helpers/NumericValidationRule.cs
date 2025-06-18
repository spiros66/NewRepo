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

using System.Windows.Controls;

namespace Sillogos.Helpers
{
    public class NumericValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            decimal d;
            if (decimal.TryParse(value.ToString(), out d))
                return new ValidationResult(true, null);

            return new ValidationResult(false, "Please enter a valid numeric value.");
        }
    }
}