#region Info

// 2020/12/23  21:57
// SimpleValidator.ViewModels

#endregion

using System.Globalization;

namespace SimpleValidator.ViewModels.Validators
{
  public abstract class ValidationRule<TPropertyValue> : IValidationRule<TPropertyValue>, IValidationRule
  {
    public abstract ValidationResult Validate(TPropertyValue value, CultureInfo cultureInfo);

    #region Implementation of IValidationRule

    /// <inheritdoc />
    ValidationResult IValidationRule.Validate(object timeValue, CultureInfo cultureInfo) => Validate((TPropertyValue) timeValue, cultureInfo);

    #endregion Implementation of IValidationRule
  }
}