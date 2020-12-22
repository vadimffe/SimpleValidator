using System;
using System.Globalization;
using System.Windows.Controls;

namespace SimpleValidator.ViewModels
{
    public class TimeValidationRule : ValidationRule
    {
        public string timeValidation { get; set; }

        private bool ValidateTime(string time)
        {
            DateTime ignored;
            return DateTime.TryParseExact(time, "HH:mm",
                                          CultureInfo.InvariantCulture,
                                          DateTimeStyles.None,
                                          out ignored);
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var text = value as string;
            if (string.IsNullOrEmpty(text))
            {
                return !text.StartsWith(this.timeValidation)
                           ? new ValidationResult(false, $"Must not be empty")
                           : ValidationResult.ValidResult;
            }

            else if (!ValidateTime(text))
            {
                return !text.StartsWith(this.timeValidation)
                           ? new ValidationResult(false, $"Must be {this.timeValidation} format")
                           : ValidationResult.ValidResult;
            }

            return ValidationResult.ValidResult;
        }
    }
}