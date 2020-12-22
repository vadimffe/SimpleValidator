using System;
using System.Globalization;
using System.Windows.Controls;

namespace SimpleValidator.ViewModels
{
    public class NumberValidationRule : ValidationRule
    {
        public string numberValidation { get; set; }

        private bool ValidateNumber(string time)
        {
            int ignored;
            return int.TryParse(time, out ignored);
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var text = value as string;
            if (string.IsNullOrEmpty(text))
            {
                return !text.StartsWith(this.numberValidation)
                           ? new ValidationResult(false, $"Must not be empty")
                           : ValidationResult.ValidResult;
            }

            else if (!ValidateNumber(text))
            {
                return !text.StartsWith(this.numberValidation)
                           ? new ValidationResult(false, $"Must be {this.numberValidation} format")
                           : ValidationResult.ValidResult;
            }

            return ValidationResult.ValidResult;
        }
    }
}
