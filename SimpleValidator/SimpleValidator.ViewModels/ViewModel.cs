using System;
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
    public class ViewModel : BaseViewModel
    {
        private ValidationMode ValidationMode { get; }
        public ICommand SaveCommand => new RelayCommand(param => this.SaveSettings(), param => !this.HasErrors);

        // Example property, which validates its value before applying it
        private string _timeRecordInterval;
        public string TimeRecordInterval
        {
            get => this._timeRecordInterval;
            set
            {
                // Validate the value
                ValidateProperty(value, SaveCommand as RelayCommand);
                // Accept the valid value
                this._timeRecordInterval = value;
                OnPropertyChanged();
            }
        }

        private string _timeLimit;
        public string TimeLimit
        {
            get => this._timeLimit;
            set
            {
                // Validate the value
                ValidateProperty(value, SaveCommand as RelayCommand);
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
    }
}