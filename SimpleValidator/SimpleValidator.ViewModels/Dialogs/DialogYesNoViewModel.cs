using System;
using System.Windows;
using System.Windows.Input;
using SimpleValidator.DialogService;
using SimpleValidator.ViewModels.Commands;

namespace SimpleValidator.ViewModels.Dialogs
{
    public class DialogYesNoViewModel : DialogViewModelBase
    {
        public event EventHandler YesClicked = delegate { };
        public event EventHandler NoClicked = delegate { };

        private ICommand yesCommand = null;
        public ICommand YesCommand
        {
            get { return this.yesCommand; }
            set { this.yesCommand = value; }
        }

        private ICommand noCommand = null;
        public ICommand NoCommand
        {
            get { return this.noCommand; }
            set { this.noCommand = value; }
        }

        public string Message { get; private set; }

        public DialogYesNoViewModel(string message)
        {
            this.Message = message;
            this.yesCommand = new RelayCommand(OnYesClicked);
            this.noCommand = new RelayCommand(OnNoClicked);
        }

        private void OnYesClicked(object parameter)
        {
            this.YesClicked(this, EventArgs.Empty);
            CloseDialog(parameter as Window);
        }

        private void OnNoClicked(object parameter)
        {
            this.NoClicked(this, EventArgs.Empty);
            CloseDialog(parameter as Window);
        }
    }
}
