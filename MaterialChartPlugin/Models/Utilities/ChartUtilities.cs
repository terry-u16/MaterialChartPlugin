using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialChartPlugin.Models.Utilities
{
    public static class ChartUtilities
    {
        public static int GetYAxisMax(int maxValue, int interval)
        {
            maxValue = maxValue > 0 ? maxValue : 1;
            return interval * (maxValue / interval + 1);
        }

        public static int GetInterval(int min, int max)
        {
            // グラフの数値軸目盛を自動算出するアルゴリズム: いげ太のブログ
            // http://igeta.cocolog-nifty.com/blog/2007/11/graph_scale.html
            // を参考に作成

            if (max <= min)
                throw new ArgumentException();

            int difference = max - min; // 最上位桁値
            int shift = 1;              // 桁上げ倍率

            while (difference >= 10)
            {
                difference /= 10;
                shift *= 10;
            }

            if (difference >= 5)
                return shift * 2;
            else if (difference >= 2)
                return shift;
            else
                return shift * 4 / 10;
        }

        public static TimeSpan GetInterval(DisplayedPeriod period)
        {
            switch (period)
            {
                case DisplayedPeriod.OneDay:
                    return TimeSpan.FromHours(6);
                case DisplayedPeriod.OneWeek:
                    return TimeSpan.FromDays(1);
                case DisplayedPeriod.OneMonth:
                    return TimeSpan.FromDays(7.5);
                case DisplayedPeriod.ThreeMonths:
                    return TimeSpan.FromDays(30);
                case DisplayedPeriod.OneYear:
                    return TimeSpan.FromDays(91.25);
                case DisplayedPeriod.ThreeYears:
                    return TimeSpan.FromDays(365);
                default:
                    throw new ArgumentException("periodの値が不正です");
            }
        }

        public static int GetMynorTicks(DisplayedPeriod period)
        {
            switch (period)
            {
                case DisplayedPeriod.OneDay:
                    return 5;
                case DisplayedPeriod.OneWeek:
                    return 3;
                case DisplayedPeriod.OneMonth:
                    return 6;
                case DisplayedPeriod.ThreeMonths:
                    return 2;
                case DisplayedPeriod.OneYear:
                    return 2;
                case DisplayedPeriod.ThreeYears:
                    return 3;
                default:
                    throw new ArgumentException("periodの値が不正です");
            }
        }

        public static string GetDateTimeFormat(DisplayedPeriod period)
        {
            switch (period)
            {
                case DisplayedPeriod.OneDay:
                    return "M/d H:mm";
                case DisplayedPeriod.OneWeek:
                case DisplayedPeriod.OneMonth:
                    return "M/d";
                case DisplayedPeriod.ThreeMonths:
                case DisplayedPeriod.OneYear:
                case DisplayedPeriod.ThreeYears:
                    return "yyyy/M/d";
                default:
                    throw new ArgumentException("periodの値が不正です");
            }
        }
    }
}
