using MiP.Ruler;
using System;
using System.Windows.Input;

namespace MIR.Ruler.Commands
{
    public class ToggleRelativeDisplayCommand : ICommand
    {
        private readonly MainWindow _window;

        public event EventHandler CanExecuteChanged;

        public ToggleRelativeDisplayCommand(MainWindow window)
        {
            _window = window;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _window.ToggleRelativeDisplay();
        }
    }
}