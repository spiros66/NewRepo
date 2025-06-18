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
	public class RequiredFieldValidator : ValidationRule
	{
		private string _errorMessage;
		public string ErrorMessage
		{
			get { return _errorMessage; }
			set { _errorMessage = value; }
		}

		public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
		{
			if (value == null || string.IsNullOrEmpty(value.ToString()))
				return new ValidationResult(false, _errorMessage);
			else
				return new ValidationResult(true, null);

		}
	}
}