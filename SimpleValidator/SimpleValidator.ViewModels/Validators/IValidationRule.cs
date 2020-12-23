#region Info

// 2020/12/23  21:42
// SimpleValidator.ViewModels

#endregion

using System.Globalization;

namespace SimpleValidator.ViewModels.Validators
{
  public interface IValidationRule<TProperty> : IValidationRule
  {
    ValidationResult Validate(TProperty timeValue, CultureInfo cultureInfo);
  }
  public interface IValidationRule
  {
    ValidationResult Validate(object timeValue, CultureInfo cultureInfo);
  }
}