using System;
using System.Diagnostics;
using System.Windows.Input;

namespace SimpleValidator.ViewModels.Commands
{
    public class RelayCommand : ICommand
    {
        #region Fields 
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;
        #endregion // Fields 
        #region Constructors 
        public RelayCommand(Action<object> execute) : this(execute, null) { }
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this._execute = execute; this._canExecute = canExecute;
        }
        #endregion // Constructors 
        #region ICommand Members 
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return this._canExecute == null ? true : this._canExecute(parameter);
        }

        public void InvalidateCommand() => this.InternalCanExecuteChanged?.Invoke(this, EventArgs.Empty);

    private event EventHandler InternalCanExecuteChanged;
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                this.InternalCanExecuteChanged += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                this.InternalCanExecuteChanged -= value;
            }
        }

        public void Execute(object parameter) { this._execute(parameter); }
        #endregion // ICommand Members 
    }
}
