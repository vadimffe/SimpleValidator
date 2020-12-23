﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using SimpleValidator.ViewModels.Commands;
using SimpleValidator.ViewModels.Validators;
using ValidationResult = SimpleValidator.ViewModels.Validators.ValidationResult;

namespace SimpleValidator.ViewModels
{
    public class ViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        private ValidationMode ValidationMode { get; set; }
        public ICommand SaveCommand => new RelayCommand(param => this.SaveSettings());

        // Example property, which validates its value before applying it
        private string _timeRecordInterval;
        public string TimeRecordInterval
        {
            get => this._timeRecordInterval;
            set
            {
                // Validate the value
                if (ValidateProperty(value))
                {
                    // Accept the valid value
                    this._timeRecordInterval = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _timeLimit;
        public string TimeLimit
        {
            get => this._timeLimit;
            set
            {
                // Validate the value
                ValidateProperty(value);
                this._timeLimit = value;
                OnPropertyChanged();
            }
        }

        // Constructor
        public ViewModel()
        {
            this.ValidationMode = ValidationMode.AllowInvalidInput;

            this._timeRecordInterval = "1";
            this._timeLimit = "12:30";

            this.Errors = new Dictionary<string, List<string>>();
            this.ValidationRules = new Dictionary<string, List<IValidationRule>>();

            // Create a Dictionary of validation rules for fast lookup. 
            // Each property name of a validated property maps to one or more ValidationRule.
            this.ValidationRules.Add(nameof(this.TimeRecordInterval), new List<IValidationRule>() { new NumberValidationRule() });
            this.ValidationRules.Add(nameof(this.TimeLimit), new List<IValidationRule>() { new TimeValidationRule() });
        }

        private void SaveSettings()
        {
            if (!HasErrors)
                Debug.WriteLine("Save");
        }

        // Validation method. 
        // Is called from each property which needs to validate its value.
        // Because the parameter 'propertyName' is decorated with the 'CallerMemberName' attribute.
        // this parameter is automatically generated by the compiler. 
        // The caller only needs to pass in the 'propertyValue', if the caller is the target property's set method.
        public bool ValidateProperty<TValue>(TValue propertyValue, [CallerMemberName] string propertyName = null)
        {
            // Clear previous errors of the current property to be validated 
            this.Errors.Remove(propertyName);
            OnErrorsChanged(propertyName);

            if (this.ValidationRules.TryGetValue(propertyName, out List<IValidationRule> propertyValidationRules))
            {
                // Apply all the rules that are associated with the current property and validate the property's value
                if (this.ValidationMode == ValidationMode.SuppressInvalidInput)
                {
                    return propertyValidationRules
                        .Select(validationRule => validationRule.Validate(propertyValue, CultureInfo.CurrentCulture))
                        .All(result => result.Status != ValidationResultStatus.Error);
                }

                List<ValidationResult> invalidResults = propertyValidationRules
                    .Select(validationRule => validationRule.Validate(propertyValue, CultureInfo.CurrentCulture))
                    .Where(result => result.Status == ValidationResultStatus.Error || result.Status == ValidationResultStatus.Warning)
                    .ToList();

                invalidResults.ForEach(
                    invalidResult => AddError(
                        propertyName,
                        invalidResult.Message,
                        invalidResult.Status == ValidationResultStatus.Warning));

                return invalidResults.All(invalidResult => invalidResult.Status != ValidationResultStatus.Error);
            }

            // No rules found for the current property
            return true;
        }

        // Adds the specified error to the errors collection if it is not 
        // already present, inserting it in the first position if 'isWarning' is 
        // false. Raises the ErrorsChanged event if the Errors collection changes. 
        // A property can have multiple errors.
        public void AddError(string propertyName, string errorMessage, bool isWarning = false)
        {
            if (!this.Errors.TryGetValue(propertyName, out List<string> propertyErrors))
            {
                propertyErrors = new List<string>();
                this.Errors[propertyName] = propertyErrors;
            }

            if (!propertyErrors.Contains(errorMessage))
            {
                if (isWarning)
                {
                    // Move warnings to the end
                    propertyErrors.Add(errorMessage);
                }
                else
                {
                    propertyErrors.Insert(0, errorMessage);
                }
                OnErrorsChanged(propertyName);
            }
        }

        public bool PropertyHasErrors(string propertyName) =>
            this.Errors.TryGetValue(propertyName, out List<string> propertyErrors) && propertyErrors.Any();

        #region INotifyDataErrorInfo implementation

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        // Returns all errors of a property. If the argument is 'null' instead of the property's name, 
        // then the method will return all errors of all properties.
        public IEnumerable GetErrors(string propertyName)
          => string.IsNullOrWhiteSpace(propertyName)
            ? this.Errors.SelectMany(entry => entry.Value)
            : this.Errors.TryGetValue(propertyName, out List<string> errors)
              ? errors
              : new List<string>();

        // Returns if the view model has any invalid property
        public bool HasErrors => this.Errors.Any();

        #endregion

        protected virtual void OnErrorsChanged(string propertyName)
        {
            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        // Maps a property name to a list of errors that belong to this property
        private Dictionary<String, List<String>> Errors { get; }

        // Maps a property name to a list of ValidationRules that belong to this property
        private Dictionary<String, List<IValidationRule>> ValidationRules { get; }
    }
}