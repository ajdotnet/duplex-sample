using System;
using System.Windows.Input;

namespace AJ.DuplexClient.ViewModels
{
    /// <summary>Command class for delegates.</summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public class RelayCommand : ICommand
    {
        Action<object> _execute;
        Func<object, bool> _canExecute;
        bool? _isEnabled;

        /// <summary>Gets or sets whether the command is enabled.</summary>
        /// <value>
        /// The value determining whether the command is enabled;
        /// if set to <c>null</c>, the callback will be used.
        /// </value>
        public bool? IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                NotifyCanExecuteChanged();
            }
        }

        /// <summary>Occurs when changes occur that affect whether or not the command should execute.</summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>Initializes a new instance of the <see cref="RelayCommand" /> class.</summary>
        /// <param name="execute">The delegate executed when the comand is invoked.</param>
        /// <param name="canExecute">The delegate used to determine if the command is enabled;
        /// Note: <see cref="IsEnabled" /> takes precedence.</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// if <see cref="IsEnabled" /> is set, it determines the result. If it is not set,
        /// <see cref="_canExecute" /> is called. If <see cref="_canExecute" /> is <c>null</c>, the
        /// result is true.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data
        /// to be passed, this object can be set to null.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            var e = this.IsEnabled;
            if (e.HasValue)
                return e.Value;
            if (_canExecute != null)
                return _canExecute(parameter);
            return true;
        }

        /// <summary>Defines the method to be called when the command is invoked.</summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data
        /// to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
                _execute(parameter);
        }

        /// <summary>Notifies that "can execute" has changed.</summary>
        public void NotifyCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
