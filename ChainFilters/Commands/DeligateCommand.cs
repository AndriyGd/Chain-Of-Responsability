using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChainFilters.Commands
{
    public class DeligateCommand : ICommand
    {
        private readonly Action _action;
        private readonly Predicate<object> _predicate;

        public DeligateCommand(Action action, Predicate<object> predicate)
        {
            _action = action;
            _predicate = predicate;
        }

        public bool CanExecute(object parameter)
        {
            return _predicate?.Invoke(parameter) ?? false;
        }

        public void Execute(object parameter)
        {
            _action?.Invoke();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
