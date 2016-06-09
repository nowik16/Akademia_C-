using System.Windows.Media;
using System.Windows.Shapes;

namespace Nowicki_PGS
{                                                       //Tworzenie i wygląd naszej elipsy
    class Elipsa
    {
        public SolidColorBrush fillBrush = new SolidColorBrush() { Color = Colors.Red };
        public SolidColorBrush borderBrush = new SolidColorBrush() { Color = Colors.Black };

        public Ellipse CreateAnEllipse(int height, int width)
        { 
            return new Ellipse()
            {
                Height = height,
                Width = width,
                StrokeThickness = 1,
                Stroke = borderBrush,
                Fill = fillBrush
            };
        }
    }
}
