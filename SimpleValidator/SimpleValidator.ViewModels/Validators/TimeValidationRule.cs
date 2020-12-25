using System;
using System.Globalization;
using System.Linq;

namespace SimpleValidator.ViewModels.Validators
{
    public class TimeValidationRule : ValidationRule<string>
    {
        private string timeFormat = "HH:mm";

        // Call this method for compiletime type checking!
        public override ValidationResult Validate(string value, CultureInfo cultureInfo) => IsValidInputStrict(value);

        private bool ValidateTimeStrict(string time)
        {
            return DateTime.TryParseExact(time, timeFormat,
                                          CultureInfo.InvariantCulture,
                                          DateTimeStyles.None,
                                          out _);
        }

        private ValidationResult IsValidInputStrict(string input)
        {
            // Allow empty input
            if (string.IsNullOrWhiteSpace(input))
            {
                return ValidationResult.CreateWarning("Enter a valid time");
            }

            // Test if input is a pure number
            if (!ValidateTimeStrict(input))
            {
                // Test if number consists of max 2 digits
                return ValidationResult.CreateError("Only 2 digit format 'HH:mm' allowed");
            }

            // If value is not a pure number (e.g., contains colon),
            // then check if it's a valid time
            return ValidationResult.CreateValid();
        }

        //private ValidationResult IsValidInput(string input)
        //{
        //    // Allow empty input
        //    if (string.IsNullOrWhiteSpace(input))
        //    {
        //        return ValidationResult.CreateWarning("Enter a valid time");
        //    }

        //    // Test if input is a pure number
        //    if (int.TryParse(input, out _))
        //    {
        //        // Test if number consists of max 2 digits
        //        return input.Length < 3
        //            ? ValidationResult.CreateValid()
        //            : ValidationResult.CreateError("Only 2 digit format 'HH:mm' allowed");
        //    }

        //    // If value is not a pure number (e.g., contains colon),
        //    // then check if it's a valid time
        //    return IsValidTime(input);
        //}

        //private ValidationResult IsValidTime(string input)
        //{
        //    // Test if value is valid time
        //    if (TimeSpan.TryParse(input, new DateTimeFormatInfo(), out _))
        //    {
        //        // Test if only one colon is used (restrict to HH:mm format)
        //        if (input.Count(character => character.Equals(':')) < 2)
        //        {
        //            // Test if successive volons are entered (only interesting for HH:mm:ss format)
        //            if (!input.Contains("::"))
        //            {
        //                return ValidationResult.CreateValid();
        //            }
        //            return ValidationResult.CreateError("Invalid time format: no double colon allowed. Format must be 'HH:mm'.");
        //        }
        //        return ValidationResult.CreateError("Invalid time format: too many groups. Format must be 'HH:mm'.");
        //    }

        //    // Input is not a valid time.
        //    // Test if input ends with a colon
        //    if (!input.EndsWith(":", StringComparison.OrdinalIgnoreCase))
        //    {
        //        return ValidationResult.CreateError("Invalid time format. Format must be 'HH:mm'.");
        //    }

        //    // and if max number of colons reached
        //    return input.Count(character => character.Equals(':')) < 2 ? ValidationResult.CreateValid() : ValidationResult.CreateError("Invalid time format: too many groups. Format must be 'HH:mm'.");
        //}
    }
}