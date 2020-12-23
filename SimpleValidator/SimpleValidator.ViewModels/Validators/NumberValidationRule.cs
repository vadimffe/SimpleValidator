using System;
using System.Globalization;
using System.Windows.Controls;

namespace SimpleValidator.ViewModels
{
    public class NumberValidationRule : ValidationRule
    {
        private bool ValidateNumber(string number)
        {
            int ignored;
            return int.TryParse(number, out ignored);
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(false, "Field cannot be empty");
            }

            else if (!ValidateNumber((string)value))
            {
                return new ValidationResult(false, "Value must be number");
            }

            return ValidationResult.ValidResult;
        }
    }
}