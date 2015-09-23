using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using MaterialChartPlugin.Models;

namespace MaterialChartPlugin.Views.Converters
{
    public class DisplayedPeriodToIntervalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var period = (DisplayedPeriod)value;

            switch (period)
            {
                case DisplayedPeriod.OneDay:
                    return 6.0;
                case DisplayedPeriod.OneWeek:
                    return 1.0;
                case DisplayedPeriod.OneMonth:
                    return 1.0;
                case DisplayedPeriod.ThreeMonths:
                    return 2.0;
                case DisplayedPeriod.OneYear:
                    return 3.0;
                case DisplayedPeriod.ThreeYears:
                    return 6.0;
                default:
                    throw new ArgumentException("periodの値が不正です");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
