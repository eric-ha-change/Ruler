using MiP.Ruler.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MiP.Ruler
{
    /// <summary>
    /// MonitorDimension.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MonitorDimension : Window
    {
        public MonitorDimension()
        {
            InitializeComponent();

            textboxInch.Text = Convert.ToString(Config.Instance.MonitorDimension);
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            double md = 24;
            double.TryParse(textboxInch.Text, out md);
            Config.Instance.MonitorDimension = md;
            PixelToPpiConverter.SetPPI();
            Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
