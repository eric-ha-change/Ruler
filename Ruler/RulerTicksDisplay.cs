using MiP.Ruler.Converters;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MiP.Ruler
{
    public class RulerTicksDisplay : Canvas
    {
        public enum RulerPositionValues
        {
            Left,
            Top,
            Right,
            Bottom
        }

        public RulerTicksDisplay()
        {
            Loaded += LeftRuler_Loaded;
        }

        public RulerPositionValues RulerPosition { get; set; }

        public void LeftRuler_Loaded(object sender, RoutedEventArgs e)
        {
            if (Config.Instance.ShowMillimeter)
            {
                if (RulerPosition == RulerPositionValues.Top)
                    DrawMillimeterRuler(SystemParameters.PrimaryScreenWidth, 0, RulerPositionValues.Top);

                if (RulerPosition == RulerPositionValues.Bottom)
                    DrawMillimeterRuler(SystemParameters.PrimaryScreenWidth, Height, RulerPositionValues.Bottom);

                if (RulerPosition == RulerPositionValues.Left)
                    DrawMillimeterRuler(SystemParameters.PrimaryScreenHeight, 0, RulerPositionValues.Left);

                if (RulerPosition == RulerPositionValues.Right)
                    DrawMillimeterRuler(SystemParameters.PrimaryScreenHeight, Width, RulerPositionValues.Right);
            }
            else
            {
                if (RulerPosition == RulerPositionValues.Top)
                    DrawRuler(SystemParameters.PrimaryScreenWidth, 0, SetTickPosition, SetTickTextPositionHorizontal);

                if (RulerPosition == RulerPositionValues.Bottom)
                    DrawRuler(SystemParameters.PrimaryScreenWidth, Height, SetTickPosition, SetTickTextPositionHorizontal);

                if (RulerPosition == RulerPositionValues.Left)
                    DrawRuler(SystemParameters.PrimaryScreenHeight, 0, SetTickPositionSwapped, SetTickTextPositionVertical);

                if (RulerPosition == RulerPositionValues.Right)
                    DrawRuler(SystemParameters.PrimaryScreenHeight, Width, SetTickPositionSwapped, SetTickTextPositionVertical);
            }
        }

        private void DrawRuler(double pixelCount, double tickOffset, 
            Action<Line, double, double, double, double> setTickPosition, 
            Action<TextBlock, int> setTickTextPosition)
        {
            var isLeftTop = RulerPosition == RulerPositionValues.Left || RulerPosition == RulerPositionValues.Top;

            for (var position = 0; position < pixelCount + 2; position += 2)
            {
                var length = GetLength(position);

                var tick = new Line
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 1.0
                };

                setTickPosition(tick, position-1, position-1, tickOffset - length, tickOffset + length);
                
                if (isLeftTop && position > 0 && position%100 == 0)
                {
                    var tickText = new TextBlock {Text = position.ToString("0")};
                    Children.Add(tickText);
                    tickText.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                    setTickTextPosition(tickText, position-1);
                }
                
                Children.Add(tick);
            }
        }

        public void DrawMillimeterRuler(double pixelCount, double tickOffset, RulerPositionValues pos)
        {
            var isLeftTop = RulerPosition == RulerPositionValues.Left || RulerPosition == RulerPositionValues.Top;
            var isTopBottom = pos == RulerPositionValues.Top || pos == RulerPositionValues.Bottom;

            int count = (int)(PixelToPpiConverter.PixelToMillimeter(pixelCount));

            for (int position = 0; position < count; position++)
            {
                var length = GetLength(position);

                var tick = new Line
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 1.0
                };

                if (isTopBottom)
                {
                    SetTickPosition(tick, PixelToPpiConverter.MillimeterToPixel(position),
                    PixelToPpiConverter.MillimeterToPixel(position), tickOffset - length, tickOffset + length);
                }
                else
                {
                    SetTickPositionSwapped(tick, PixelToPpiConverter.MillimeterToPixel(position),
                    PixelToPpiConverter.MillimeterToPixel(position), tickOffset - length, tickOffset + length);
                }

                if (isLeftTop && position > 0 && position % 10 == 0)
                {
                    var tickText = new TextBlock { Text = position.ToString("0") };
                    Children.Add(tickText);
                    tickText.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                    if (isTopBottom)
                    {
                        SetTickTextPositionHorizontal(tickText, (int)PixelToPpiConverter.MillimeterToPixel(position));
                    }
                    else
                    {
                        SetTickTextPositionVertical(tickText, (int)PixelToPpiConverter.MillimeterToPixel(position));
                    }
                }

                Children.Add(tick);
            }
        }

        public static void SetTickTextPositionVertical(TextBlock tickText, int position)
        {
            var top = position - tickText.DesiredSize.Height/2;
            SetLeft(tickText, 12);
            SetTop(tickText, top);
        }

        public static void SetTickTextPositionHorizontal(TextBlock tickText, int position)
        {
            var left = position - tickText.DesiredSize.Width/2;
            SetTop(tickText, 12);
            SetLeft(tickText, left);
        }

        public static void SetTickPosition(Line tick, double x1, double x2, double y1, double y2)
        {
            tick.X1 = x1;
            tick.X2 = x2;
            tick.Y1 = y1;
            tick.Y2 = y2;
        }

        public void SetTickPositionSwapped(Line tick, double x1, double x2, double y1, double y2)
        {
            SetTickPosition(tick, y1, y2, x1, x2);
        }

        private static int GetLength(int pos)
        {
            var length = 4;

            if (pos%10 == 0)
                length = 8;

            if (pos%50 == 0)
                length = 12;

            return length;
        }
    }
}