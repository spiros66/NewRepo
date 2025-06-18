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
using System.Windows.Controls;

namespace Sillogos.Helpers
{
    public class DropdownValidator : ValidationRule
    {
        private string _defaultValue;
        private string _errorMessage;

        public DropdownValidator()
        {
        }

        public string DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || value.ToString().Equals("0", StringComparison.CurrentCulture) || string.IsNullOrEmpty(value.ToString()))
                return new ValidationResult(false, _errorMessage);
            else
                return new ValidationResult(true, null);
        }
    }
}