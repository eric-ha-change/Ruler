using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MiP.Ruler.Commands
{
    public class MonitorDimensionCommand : ICommand
    {
        private readonly MainWindow _window;

        public MonitorDimensionCommand(MainWindow window)
        {
            _window = window;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MonitorDimension dlg = new MonitorDimension();
            dlg.ShowDialog();

            _window.TogglePixelLength();
        }

        public event EventHandler CanExecuteChanged;
    }
}
