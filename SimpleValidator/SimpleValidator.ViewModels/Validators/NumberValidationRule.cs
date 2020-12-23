using System;
using System.Globalization;
using System.Windows.Controls;

namespace SimpleValidator.ViewModels
{
    public class NumberValidationRule : ValidationRule
    {
        private bool ValidateNumber(string number)
        {
            return int.TryParse(number, out _);
        }

        // Call this method for compiletime type checking!
        public ValidationResult Validate(string timeValue, CultureInfo cultureInfo)
        {
            return Validate(timeValue, cultureInfo);
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var text = value as string;
            return string.IsNullOrEmpty(text)
                ? new ValidationResult(false, $"Must not be empty")
                : ValidateNumber(text)
                    ? ValidationResult.ValidResult
                    : new ValidationResult(false, $"Must be number");
        }
    }
}