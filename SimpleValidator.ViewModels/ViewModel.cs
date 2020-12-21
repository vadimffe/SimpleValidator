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
        public ICommand StopManualTimerCommand => new RelayCommand(param => this.SaveSettings());

        private void SaveSettings()
        {
            if (ValidateTime(Username))
                MessageBox.Show("Save");
        }

        public string Error { get { return null; } }
        private string _username;

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        public bool ValidateTime(string time)
        {
            DateTime ignored;
            return DateTime.TryParseExact(time, "HH:mm",
                                          CultureInfo.InvariantCulture,
                                          DateTimeStyles.None,
                                          out ignored);
        }

        public string this[string name]
        {
            get
            {
                string result = null;

                switch (name)
                {
                    case "Username":
                        if (string.IsNullOrWhiteSpace(Username))
                            result = "Username cannot be empty";
                        else if (!ValidateTime(Username))
                            result = "Username must be a minimum of 5 characters.";
                        break;
                }

                if (ErrorCollection.ContainsKey(name))
                    ErrorCollection[name] = result;
                else if (result != null)
                    ErrorCollection.Add(name, result);

                OnPropertyChanged(nameof(ErrorCollection));
                return result;
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                OnPropertyChanged(ref _username, value);
            }
        }

    }
}

