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

            TimeSpan minimumTimeStep = GetMinimumTimeStep(period);
            double minimumMaterialRatio = GetMinimumMaterialRatio(period);

            // 最初のデータは必ず返す
            yield return log.First();
            TimeMaterialsPair lastData = log.First();

            foreach (var data in log.Skip(1))
            {
                // 前のデータから時刻あるいは資材量が十分離れているデータであれば値を返す
                if (HasSeriouslyChanged(lastData, data, minimumTimeStep, minimumMaterialRatio))
                {
                    yield return data;
                    lastData = data;
                }
            }

            // 直近のデータは必ず返す
            if (!lastData.Equals(log.Last()))
            {
                yield return log.Last();
            }
        }

        private static TimeSpan GetMinimumTimeStep(DisplayedPeriod period)
        {
            // x方向の解像度は150分割くらいにしておく（わりと適当）
            switch (period)
            {
                case DisplayedPeriod.OneDay:
                    return TimeSpan.FromMinutes(10);
                case DisplayedPeriod.OneWeek:
                    return TimeSpan.FromHours(1);
                case DisplayedPeriod.OneMonth:
                    return TimeSpan.FromHours(4);
                case DisplayedPeriod.ThreeMonths:
                    return TimeSpan.FromHours(12);
                case DisplayedPeriod.OneYear:
                    return TimeSpan.FromDays(2);
                case DisplayedPeriod.ThreeYears:
                    return TimeSpan.FromDays(6);
                default:
                    throw new ArgumentException("periodの値が不正です");
            }
        }

        private static double GetMinimumMaterialRatio(DisplayedPeriod period)
        {
            switch (period)
            {
                case DisplayedPeriod.OneDay:
                case DisplayedPeriod.OneWeek:
                    return 0.02;
                case DisplayedPeriod.OneMonth:
                    return 0.05;
                case DisplayedPeriod.ThreeMonths:
                    return 0.08;
                case DisplayedPeriod.OneYear:
                    return 0.12;
                case DisplayedPeriod.ThreeYears:
                    return 0.25;
                default:
                    throw new ArgumentException("periodの値が不正です");
            }
        }

        private static bool HasSeriouslyChanged(TimeMaterialsPair oldData, TimeMaterialsPair newData, TimeSpan minimumTimeStep, double minimumMaterialRatio)
        {
            // 5%くらい変化したら大きく変わったとみてよい
            return newData.DateTime - oldData.DateTime >= minimumTimeStep
                || Math.Abs(newData.Fuel - oldData.Fuel) >= oldData.Fuel * minimumMaterialRatio
                || Math.Abs(newData.Ammunition - oldData.Ammunition) >= oldData.Fuel * minimumMaterialRatio
                || Math.Abs(newData.Steel - oldData.Steel) >= oldData.Steel * minimumMaterialRatio
                || Math.Abs(newData.Bauxite - oldData.Bauxite) >= oldData.Bauxite * minimumMaterialRatio
                || Math.Abs(newData.RepairTool - oldData.RepairTool) >= oldData.RepairTool * minimumMaterialRatio;
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
