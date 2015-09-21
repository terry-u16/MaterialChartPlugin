using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialChartPlugin.Models
{
    /// <summary>
    /// グラフの表示するデータの期間を示す識別子を定義します。
    /// </summary>
    public enum DisplayedPeriod
    {
        /// <summary>
        /// 1日分のデータを表示します。
        /// </summary>
        OneDay,

        /// <summary>
        /// 1週間分のデータを表示します。
        /// </summary>
        OneWeek,

        /// <summary>
        /// 1ヶ月分のデータを表示します。
        /// </summary>
        OneMonth,

        /// <summary>
        /// 3ヶ月分のデータを表示します。
        /// </summary>
        ThreeMonths,

        /// <summary>
        /// 1年分のデータを表示します。
        /// </summary>
        OneYear,

        /// <summary>
        /// 3年分のデータを表示します。
        /// </summary>
        ThreeYears
    }
}
