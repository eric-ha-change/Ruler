using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MiP.Ruler.Converters
{
    public static class PixelToPpiConverter
    {
        private static double _inch = 2.54;
        private static double _ppi = 0;

        public static double MillimeterToPixel(double mm)
        {
            double pixel = 0;

            pixel = (mm / 10) * _ppi / _inch;

            return pixel;
        }

        public static double PixelToMillimeter(double pixel)
        {
            double mm = 0;

            mm = (pixel * _inch / _ppi) * 10;

            return mm;
        }

        public static void SetPPI()
        {
            double dimSq = GetSquare(Config.Instance.MonitorDimension);
            double widthSq = GetSquare(SystemParameters.PrimaryScreenWidth);
            double heightSq = GetSquare(SystemParameters.PrimaryScreenHeight);
            double dimRatio = (widthSq + heightSq) / dimSq;
            double widthInchSq = widthSq / dimRatio;
            double heightInchSq = heightSq / dimRatio;
            double widthInch = Math.Sqrt(widthInchSq);
            double heightInch = Math.Sqrt(heightInchSq);
            double widthDpi = SystemParameters.PrimaryScreenWidth / widthInch;
            double heightDpi = SystemParameters.PrimaryScreenHeight / heightInch;
            double pixels = SystemParameters.PrimaryScreenWidth * SystemParameters.PrimaryScreenHeight;
            double dpiSq = pixels / (widthInch * heightInch);
            _ppi = Math.Sqrt(dpiSq);
        }

        private static double GetSquare(double ar)
        {
            return ar * ar;
        }
    }
}
