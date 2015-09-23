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
    public class DisplayedPeriodToIntervalTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var period = (DisplayedPeriod)value;

            switch (period)
            {
                case DisplayedPeriod.OneDay:
                    return DateTimeIntervalType.Hours;
                case DisplayedPeriod.OneWeek:
                    return DateTimeIntervalType.Days;
                case DisplayedPeriod.OneMonth:
                    return DateTimeIntervalType.Weeks;
                case DisplayedPeriod.ThreeMonths:
                    return DateTimeIntervalType.Weeks;
                case DisplayedPeriod.OneYear:
                    return DateTimeIntervalType.Months;
                case DisplayedPeriod.ThreeYears:
                    return DateTimeIntervalType.Months;
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
