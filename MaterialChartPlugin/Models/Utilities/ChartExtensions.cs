using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialChartPlugin.Models.Utilities
{
    static class ChartDataExtensions
    {
        /// <summary>
        /// チャート描画のパフォーマンスを改善するため、データの間引きを行います。
        /// </summary>
        /// <param name="log">資材の時系列データ</param>
        /// <param name="period">描画対象期間</param>
        /// <returns></returns>
        public static IEnumerable<TimeMaterialsPair> ThinOut(this IEnumerable<TimeMaterialsPair> log, DisplayedPeriod period)
        {
            if (log.Count() == 0)
                yield break;

            DateTime lastUpdated = DateTime.MinValue;
            TimeMaterialsPair lastData = log.First();
            TimeSpan minimumStep;

            // データ数を最大でもだいたい150点くらいに抑える（わりと適当）
            switch (period)
            {
                case DisplayedPeriod.OneDay:
                    minimumStep = TimeSpan.FromMinutes(10);
                    break;
                case DisplayedPeriod.OneWeek:
                    minimumStep = TimeSpan.FromHours(1);
                    break;
                case DisplayedPeriod.OneMonth:
                    minimumStep = TimeSpan.FromHours(4);
                    break;
                case DisplayedPeriod.ThreeMonths:
                    minimumStep = TimeSpan.FromHours(12);
                    break;
                case DisplayedPeriod.OneYear:
                    minimumStep = TimeSpan.FromDays(2);
                    break;
                case DisplayedPeriod.ThreeYears:
                    minimumStep = TimeSpan.FromDays(6);
                    break;
                default:
                    throw new ArgumentException("periodの値が不正です");
            }

            foreach (var data in log)
            {
                // 前のデータから時刻が十分離れているデータであれば値を返す
                if (data.DateTime - lastUpdated >= minimumStep)
                {
                    yield return data;
                    lastUpdated = data.DateTime;
                    lastData = data;
                }
            }

            // 直近のデータは必ず返す
            if (!lastData.Equals(log.Last()))
            {
                yield return log.Last();
            }
        }

        /// <summary>
        /// 指定された期間内のデータのみをフィルタリングします。
        /// </summary>
        /// <param name="log">資材の時系列データ</param>
        /// <param name="period">描画対象期間</param>
        /// <returns></returns>
        public static IEnumerable<TimeMaterialsPair> Within(this IEnumerable<TimeMaterialsPair> log, DisplayedPeriod period)
        {
            if (log.Count() == 0)
                yield break;

            TimeSpan periodSpan = period.ToTimeSpan();

            // 期間の直前のデータはとっておく
            if (log.Any(d => DateTime.Now - d.DateTime > periodSpan))
            {
                yield return log.Last(d => DateTime.Now - d.DateTime > periodSpan);
            }

            if (log.Any(d => DateTime.Now - d.DateTime <= periodSpan))
            {
                foreach (var data in log.Where(d => DateTime.Now - d.DateTime <= periodSpan))
                {
                    yield return data;
                }
            }
        }

        public static TimeSpan ToTimeSpan(this DisplayedPeriod period)
        {
            switch (period)
            {
                case DisplayedPeriod.OneDay:
                    return TimeSpan.FromDays(1);
                case DisplayedPeriod.OneWeek:
                    return TimeSpan.FromDays(7);
                case DisplayedPeriod.OneMonth:
                    return TimeSpan.FromDays(30);
                case DisplayedPeriod.ThreeMonths:
                    return TimeSpan.FromDays(90);
                case DisplayedPeriod.OneYear:
                    return TimeSpan.FromDays(365);
                case DisplayedPeriod.ThreeYears:
                    return TimeSpan.FromDays(365 * 3);
                default:
                    throw new ArgumentException("periodの値が不正です");
            }
        }
    }
}
