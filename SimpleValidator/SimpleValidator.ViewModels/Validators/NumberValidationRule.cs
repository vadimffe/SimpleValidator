using System.Globalization;

namespace SimpleValidator.ViewModels.Validators
{
    public class NumberValidationRule : ValidationRule<string>
    {
        private bool ValidateNumber(string number)
        {
            int ignored;
            return int.TryParse(number, out ignored);
        }

        public override ValidationResult Validate(string value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value))
            {
                return ValidationResult.CreateWarning("Field cannot be empty");
            }

            return ValidateNumber(value)
                ? ValidationResult.CreateValid()
                : ValidationResult.CreateError("Value must be number");
        }
    }
}