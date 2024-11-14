using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KPZ_Lab2.Commands
{
     public class Command : ICommand
     {
          public Action<object> ExecuteDelegate { get; set; }
          public Func<object, bool> CanExecuteDelegate { get; set; }

          public event EventHandler CanExecuteChanged
          {
               add { CommandManager.RequerySuggested += value; }
               remove { CommandManager.RequerySuggested -= value; }
          }

          public Command(Action<object> execute, Func<object, bool> canExecute = null)
          {
                ExecuteDelegate = execute;
                CanExecuteDelegate = canExecute;
          }

          public bool CanExecute(object parameter)
          {
               return CanExecuteDelegate == null || CanExecuteDelegate(parameter);
          }

          public void Execute(object parameter)
          {
                ExecuteDelegate(parameter);
          }
     }
}
