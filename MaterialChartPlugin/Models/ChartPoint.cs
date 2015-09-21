using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialChartPlugin.Models
{
    public class ChartPoint
    {
        public DateTime X { get; set; }
        public double Y { get; set; }

        public ChartPoint()
        {
        }

        public ChartPoint(DateTime x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return $"X={X},Y={Y}";
        }
    }
}
