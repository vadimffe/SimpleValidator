using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace SimpleValidator.ViewModels
{
    public class TimeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var text = value as string;
            return IsValidInput(text, out string errorMessage)
              ? ValidationResult.ValidResult
              : new ValidationResult(false, errorMessage);
        }
        private bool IsValidInput(string input, out string errorMessage)
        {
            errorMessage = string.Empty;
            // Allow empty input
            if (string.IsNullOrWhiteSpace(input))
            {
                return true;
            }
            // Test if input is a pure number
            if (int.TryParse(input, out _))
            {
                // Test if number consists of max 2 digits
                return input.Length < 3;
            }
            // If value is not a pure number (e.g., contains colon),
            // then check if it's a valid time
            return IsValidTime(input, out errorMessage);
        }
        private bool IsValidTime(string input, out string errorMessage)
        {
            errorMessage = string.Empty;
            // Test if value is valid time
            if (TimeSpan.TryParse(input, new DateTimeFormatInfo(), out _))
            {
                // Test if only one colon is used (restrict to HH:mm format)
                if (input.Count(character => character.Equals(':')) < 2)
                {
                    // Test if successive volons are entered (only interesting for HH:mm:ss format)
                    if (!input.Contains("::"))
                    {
                        return true;
                    }
                }
            }
            // Input is not a valid time.
            // Test if input ends with a colon (user is in the ,iddle of an input)
            if (!input.EndsWith(":", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            // and if max number of colons reached
            return input.Count(character => character.Equals(':')) < 2;
        }
    }
}