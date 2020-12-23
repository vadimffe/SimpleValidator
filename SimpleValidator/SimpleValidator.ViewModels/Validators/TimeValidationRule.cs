using System;
using System.Globalization;
using System.Windows.Controls;

namespace SimpleValidator.ViewModels
{
    public class TimeValidationRule : ValidationRule
    {
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
            if (string.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(false, "Field cannot be empty");
            }

            else if (!ValidateTime((string)value))
            {
                return new ValidationResult(false, "Value must be HH:mm format");
            }

            return ValidationResult.ValidResult;
        }
    }
}