using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialChartPlugin.Views.Converters
{
    public class SolidBrushToTransparentBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var borderBlush = value as SolidColorBrush;
            if (borderBlush == null)
                return null;

            var fillBrush = new SolidColorBrush();
            var color = borderBlush.Color;
            color.A = (byte)(color.A * double.Parse(parameter as string));
            fillBrush.Color = color;
            return fillBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
