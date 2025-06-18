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
    public class RegExValidator : ValidationRule
    {
        private string _regex;
        private string _errorMessage;
        public RegExValidator()
        {
        }

        public string RegularExpression
        {
            get => _regex;
            set => _regex = value;
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set => _errorMessage = value;
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(_regex);
            bool isValid = reg.IsMatch(value.ToString());
            if (isValid)
                return new ValidationResult(true, null);
            else
                return new ValidationResult(false, _errorMessage);
        }
    }
}