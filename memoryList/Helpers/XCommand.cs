using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace memoryList
{
    public class XCommand : ICommand
    {
        // No support for this, we don't even hold reference to subscribers
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;
        private bool _isExecuting;

        public XCommand(Action execute, Func<object, bool> canExecute = null)
            : this(_ => execute(), canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }
        }

        public XCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public XCommand(Func<Task> execute, Func<object, bool> canExecute = null)
            : this(_ => execute(), canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }
        }

        public XCommand(Func<object, Task> execute, Func<object, bool> canExecute = null)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            _execute = async x =>
            {
                // Prevents concurrent executions 
                if (_isExecuting)
                {
                    return;
                }

                _isExecuting = true;

                await execute(x);

                _isExecuting = false;
            };

            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute != null ? _canExecute(parameter) : true;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }

    public class XCommand<T> : ICommand
    {
        // No support for this, we don't even hold reference to subscribers
        public event EventHandler CanExecuteChanged
        {
            add { }
            remove { }
        }

        private readonly Action<T> _execute;
        private bool _isExecuting;

        public XCommand(Action<T> execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public XCommand(Func<T, Task> execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }

            _execute = async x =>
            {
                // Prevents concurrent executions 
                if (_isExecuting)
                {
                    return;
                }

                _isExecuting = true;

                await execute(x);

                _isExecuting = false;
            };
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
