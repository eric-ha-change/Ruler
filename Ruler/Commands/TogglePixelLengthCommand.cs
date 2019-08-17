using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MiP.Ruler.Commands
{
    public class TogglePixelLengthCommand : ICommand
    {
        private readonly MainWindow _window;

        public TogglePixelLengthCommand(MainWindow window)
        {
            _window = window;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _window.TogglePixelLength();
        }

        public event EventHandler CanExecuteChanged;
    }
}
