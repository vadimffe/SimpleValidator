using System;
using System.Globalization;
using System.Windows.Controls;

namespace SimpleValidator.ViewModels
{
    public class TimeValidationRule : ValidationRule
    {
        private string timeFormat = "HH:mm";
        
        private bool ValidateTime(string time)
        {
            return DateTime.TryParse(time,
                       CultureInfo.InvariantCulture,
                       DateTimeStyles.None,
                       out _);
        }
        
        // Call this method for compiletime type checking!
        public ValidationResult Validate(string timeValue, CultureInfo cultureInfo) => Validate(timeValue, cultureInfo);
        
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var text = value as string;
            return string.IsNullOrEmpty(text)
                ? new ValidationResult(false, $"Must not be empty")
                : ValidateTime(text)
                    ? ValidationResult.ValidResult
                    : new ValidationResult(false, $"Must be {this.timeFormat} format");
        }
    }
}