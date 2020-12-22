using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace SimpleValidator.ViewModels
{
    public class ViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        private readonly PropertyErrors errors;

        public ICommand SaveCommand => new RelayCommand(param => this.SaveSettings());

        public ViewModel()
        {
            this.errors = new PropertyErrors(this, this.OnErrorsChanged);

            //this.TimeLimit = "12:23";

            //this.TimeRecordInterval = "11";
        }

        private void SaveSettings()
        {
            if (!HasErrors)
                MessageBox.Show("Save");
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private string _timeLimit;
        public string TimeLimit
        {
            get { return this._timeLimit; }
            set
            {
                if (value == this._timeLimit)
                {
                    return;
                }

                this._timeLimit = value;
                this.errors.Clear(nameof(this.TimeLimit));
                OnPropertyChanged();
            }
        }

        private string _timeRecordInterval;
        public string TimeRecordInterval
        {
            get { return this._timeRecordInterval; }
            set
            {
                if (value == this._timeRecordInterval)
                {
                    return;
                }

                this._timeRecordInterval = value;
                this.errors.Clear(nameof(this.TimeRecordInterval));
                OnPropertyChanged();
            }
        }

        public bool HasErrors => this.errors.HasErrors;

        public IEnumerable GetErrors(string propertyName) => this.errors.GetErrors(propertyName);

        protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
            this.ErrorsChanged?.Invoke(this, e);
        }
    }
}