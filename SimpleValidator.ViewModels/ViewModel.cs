using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace SimpleValidator.ViewModels
{
    public class ViewModel : ObservableObject, IDataErrorInfo
    {
        private string MyDefaultTime = "22:32";
        private string MyDefaultInterval = "3";
        public ICommand StopManualTimerCommand => new RelayCommand(param => this.SaveSettings());

        private void SaveSettings()
        {
            if (ValidateTime(TimeLimit) && ValidateTime(TimeRecordInterval))
                MessageBox.Show("Save");
        }

        public ViewModel()
        {
            this.TimeLimit = this.MyDefaultTime;
            this.TimeRecordInterval = this.MyDefaultInterval;
        }

        public string Error { get { return null; } }

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        public bool ValidateTime(string time)
        {
            DateTime ignored;
            return DateTime.TryParseExact(time, "HH:mm",
                                          CultureInfo.InvariantCulture,
                                          DateTimeStyles.None,
                                          out ignored);
        }
        public bool ValidateNumber(string time)
        {
            int ignored;
            return int.TryParse(time, out ignored);
        }

        public string this[string name]
        {
            get
            {
                string result = null;

                switch (name)
                {
                    case "TimeLimit":
                        if (string.IsNullOrWhiteSpace(TimeLimit))
                            result = "This field cannot be empty";
                        else if (!ValidateTime(TimeLimit))
                            result = "This field can contain only time format HH:mm";
                        break;

                    case "TimeRecordInterval":
                        if (string.IsNullOrWhiteSpace(this.TimeRecordInterval))
                        {
                            result = "This field cannot be empty";
                        }
                        else if (!this.ValidateNumber(this.TimeRecordInterval))
                        {
                            result = "Field can contain only numbers";
                        }

                        break;
                }

                if (this.ErrorCollection.ContainsKey(name))
                    this.ErrorCollection[name] = result;
                else if (result != null)
                    ErrorCollection.Add(name, result);

                this.OnPropertyChanged(nameof(this.ErrorCollection));
                return result;
            }
        }

        private string _timeRecordInterval;
        public string TimeRecordInterval
        {
            get { return this._timeRecordInterval; }
            set
            {
                OnPropertyChanged(ref this._timeRecordInterval, value);
            }
        }

        private string _timeLimit;
        public string TimeLimit
        {
            get { return this._timeLimit; }
            set
            {
                OnPropertyChanged(ref this._timeLimit, value);
            }
        }

    }
}

