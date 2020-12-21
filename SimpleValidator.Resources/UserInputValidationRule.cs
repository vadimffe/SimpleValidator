using System.Globalization;
using System.Windows.Controls;

namespace SimpleValidator.Resources
{
    public class UserInputValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!(value is string userInput))
            {
                return new ValidationResult(false, "Value must be of type string.");
            }

            if (!userInput.StartsWith("@"))
            {
                return new ValidationResult(false, "Input must start with '@'.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
