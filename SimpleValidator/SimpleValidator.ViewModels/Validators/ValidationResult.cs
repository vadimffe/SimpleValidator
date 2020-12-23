#region Info

// 2020/12/23  21:49
// SimpleValidator.ViewModels

#endregion

namespace SimpleValidator.ViewModels.Validators
{
  public readonly struct ValidationResult
  {
    public static ValidationResult CreateWarning(string message) => new ValidationResult(ValidationResultStatus.Warning, message);
    public static ValidationResult CreateError(string message) => new ValidationResult(ValidationResultStatus.Error, message);
    public static ValidationResult CreateInfo(string message) => new ValidationResult(ValidationResultStatus.Info, message);
    public static ValidationResult CreateValid() => new ValidationResult(ValidationResultStatus.Valid, string.Empty);

    private ValidationResult(ValidationResultStatus status) : this(status, string.Empty)
    {
    }

    private ValidationResult(ValidationResultStatus status, string message)
    {
      this.Status = status;
      this.Message = message;
    }

    public ValidationResultStatus Status { get; }
    public string Message { get; }
    public bool IsValid => this.Status == ValidationResultStatus.Valid;
  }
}