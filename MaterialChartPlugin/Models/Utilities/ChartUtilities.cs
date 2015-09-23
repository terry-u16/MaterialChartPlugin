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
    }
}
