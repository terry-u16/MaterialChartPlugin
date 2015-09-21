using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using Livet.EventListeners;
using MetroTrilithon.Mvvm;
using MaterialChartPlugin.Models;
using MaterialChartPlugin.Models.Settings;
using MaterialChartPlugin.Models.Utilities;
using System.Reactive.Linq;
using System.Windows;

namespace MaterialChartPlugin.ViewModels
{
    public class ToolViewModel : ViewModel
    {
        private MaterialChartPlugin plugin;

        public MaterialManager materialManager { get; }

        public int Fuel => materialManager.Fuel;

        public int Ammunition => materialManager.Ammunition;

        public int Steel => materialManager.Steel;

        public int Bauxite => materialManager.Bauxite;

        public int RepairTool => materialManager.RepairTool;

        #region FuelSeries変更通知プロパティ
        private ObservableCollection<ChartPoint> _FuelSeries = new ObservableCollection<ChartPoint>();

        public ObservableCollection<ChartPoint> FuelSeries
        {
            get
            { return _FuelSeries; }
            set
            {
                if (_FuelSeries == value)
                    return;
                _FuelSeries = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region AmmunitionSeries変更通知プロパティ
        private ObservableCollection<ChartPoint> _AmmunitionSeries = new ObservableCollection<ChartPoint>();

        public ObservableCollection<ChartPoint> AmmunitionSeries
        {
            get
            { return _AmmunitionSeries; }
            set
            {
                if (_AmmunitionSeries == value)
                    return;
                _AmmunitionSeries = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region SteelSeries変更通知プロパティ
        private ObservableCollection<ChartPoint> _SteelSeries = new ObservableCollection<ChartPoint>();

        public ObservableCollection<ChartPoint> SteelSeries
        {
            get
            { return _SteelSeries; }
            set
            {
                if (_SteelSeries == value)
                    return;
                _SteelSeries = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region BauxiteSeries変更通知プロパティ
        private ObservableCollection<ChartPoint> _BauxiteSeries = new ObservableCollection<ChartPoint>();

        public ObservableCollection<ChartPoint> BauxiteSeries
        {
            get
            { return _BauxiteSeries; }
            set
            {
                if (_BauxiteSeries == value)
                    return;
                _BauxiteSeries = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region RepairToolSeries変更通知プロパティ
        private ObservableCollection<ChartPoint> _RepairToolSeries = new ObservableCollection<ChartPoint>();

        public ObservableCollection<ChartPoint> RepairToolSeries
        {
            get
            { return _RepairToolSeries; }
            set
            {
                if (_RepairToolSeries == value)
                    return;
                _RepairToolSeries = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region StorableLimitSeries変更通知プロパティ
        private ObservableCollection<ChartPoint> _StorableLimitSeries;

        public ObservableCollection<ChartPoint> StorableLimitSeries
        {
            get
            { return _StorableLimitSeries; }
            set
            {
                if (_StorableLimitSeries == value)
                    return;
                _StorableLimitSeries = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region XMin変更通知プロパティ
        private DateTime _XMin;

        public DateTime XMin
        {
            get
            { return _XMin; }
            set
            {
                if (_XMin == value)
                    return;
                _XMin = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region XMax変更通知プロパティ
        private DateTime _XMax;

        public DateTime XMax
        {
            get
            { return _XMax; }
            set
            {
                if (_XMax == value)
                    return;
                _XMax = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region YMin変更通知プロパティ
        private double _YMin;

        public double YMin
        {
            get
            { return _YMin; }
            set
            {
                if (_YMin == value)
                    return;
                _YMin = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region YMax1変更通知プロパティ
        private double _YMax1;

        public double YMax1
        {
            get
            { return _YMax1; }
            set
            {
                if (_YMax1 == value)
                    return;
                _YMax1 = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region YMax2変更通知プロパティ
        private double _YMax2;

        public double YMax2
        {
            get
            { return _YMax2; }
            set
            {
                if (_YMax2 == value)
                    return;
                _YMax2 = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region XInterval変更通知プロパティ
        private TimeSpan _XInterval;

        public TimeSpan XInterval
        {
            get
            { return _XInterval; }
            set
            {
                if (_XInterval == value)
                    return;
                _XInterval = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        // TODO: YIntervalバインディング
        #region YInterval1変更通知プロパティ
        private double _YInterval1;

        public double YInterval1
        {
            get
            { return _YInterval1; }
            set
            {
                if (_YInterval1 == value)
                    return;
                _YInterval1 = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region YInterval2変更通知プロパティ
        private double _YInterval2;

        public double YInterval2
        {
            get
            { return _YInterval2; }
            set
            {
                if (_YInterval2 == value)
                    return;
                _YInterval2 = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region DateTimeLabelFormat変更通知プロパティ
        private string _DateTimeLabelFormat;

        public string DateTimeLabelFormat
        {
            get
            { return _DateTimeLabelFormat; }
            set
            {
                if (_DateTimeLabelFormat == value)
                    return;
                _DateTimeLabelFormat = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region MinorTicksCount変更通知プロパティ
        private int _MinorTicksCount;

        public int MinorTicksCount
        {
            get
            { return _MinorTicksCount; }
            set
            {
                if (_MinorTicksCount == value)
                    return;
                _MinorTicksCount = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region XAxisVisibility変更通知プロパティ
        private Visibility _XAxisVisibility = Visibility.Visible;

        public Visibility XAxisVisibility
        {
            get
            { return _XAxisVisibility; }
            set
            {
                if (_XAxisVisibility == value)
                    return;
                _XAxisVisibility = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        public IReadOnlyCollection<DisplayViewModel<DisplayedPeriod>> DisplayedPeriods { get; }

        PropertyChangedEventListener managerChangedListener;

        PropertyChangedEventListener logChangedListener;

        public ToolViewModel(MaterialChartPlugin plugin)
        {
            this.plugin = plugin;

            this.materialManager = new MaterialManager(plugin);

            this.DisplayedPeriods = new List<DisplayViewModel<DisplayedPeriod>>()
            {
                DisplayViewModel.Create(DisplayedPeriod.OneDay, "1日"),
                DisplayViewModel.Create(DisplayedPeriod.OneWeek, "1週間"),
                DisplayViewModel.Create(DisplayedPeriod.OneMonth, "1ヶ月"),
                DisplayViewModel.Create(DisplayedPeriod.ThreeMonths, "3ヶ月"),
                DisplayViewModel.Create(DisplayedPeriod.OneYear, "1年"),
                DisplayViewModel.Create(DisplayedPeriod.ThreeYears, "3年")
            };

            managerChangedListener = new PropertyChangedEventListener(materialManager)
                    {
                        { nameof(materialManager.Fuel),  (_,__) => RaisePropertyChanged(nameof(Fuel)) },
                        { nameof(materialManager.Ammunition),  (_,__) => RaisePropertyChanged(nameof(Ammunition)) },
                        { nameof(materialManager.Steel),  (_,__) => RaisePropertyChanged(nameof(Steel)) },
                        { nameof(materialManager.Bauxite),  (_,__) => RaisePropertyChanged(nameof(Bauxite)) },
                        { nameof(materialManager.RepairTool),  (_,__) => RaisePropertyChanged(nameof(RepairTool)) },
                        {
                            nameof(materialManager.IsAvailable),
                            (_,__) => ChartSettings.DisplayedPeriod.Subscribe(___ => RefleshData()).AddTo(this)
                        }
                    };

        }

        public async void Initialize()
        {
            await materialManager.Initialize();

            var history = materialManager.Log.History;

            // データ初期読み込み
            logChangedListener = new PropertyChangedEventListener(materialManager.Log)
                {
                    { nameof(materialManager.Log.HasLoaded), (_, __) => RefleshData() }
                };

            // データ更新
            Observable.FromEvent<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>
                (h => (sender, e) => h(e), h => history.CollectionChanged += h, h => history.CollectionChanged -= h)
                .Where(_ => materialManager.Log.HasLoaded)
                .Throttle(TimeSpan.FromMilliseconds(50))
                .Subscribe(_ => UpdateData(history.Last()));
        }

        public void UpdateData(TimeMaterialsPair data)
        {
            SetXAxis(data);
            SetMaterialYAxis(data);
            SetRepairToolYAxis(data);
            AddChartData(data);
        }

        /// <summary>
        /// グラフのデータをアップデートします。
        /// </summary>
        public void RefleshData()
        {
            // 描画すべきデータがなかったら何もしない
            if (materialManager.Log.History
                .Within(ChartSettings.DisplayedPeriod)
                .ThinOut(ChartSettings.DisplayedPeriod).Count() == 0)
                return;

            var neededData = materialManager.Log.History
                .Within(ChartSettings.DisplayedPeriod)
                .ThinOut(ChartSettings.DisplayedPeriod)
                .ToArray();

            SetXAxis(neededData[neededData.Length - 1]);
            SetMaterialYAxis(neededData);
            SetRepairToolYAxis(neededData);
            RefleshChartData(neededData);
        }

        private void AddChartData(TimeMaterialsPair data)
        {
            Application.Current.Dispatcher.Invoke(
                () =>
                {
                    FuelSeries.Add(new ChartPoint(data.DateTime, data.Fuel));
                    AmmunitionSeries.Add(new ChartPoint(data.DateTime, data.Ammunition));
                    SteelSeries.Add(new ChartPoint(data.DateTime, data.Steel));
                    BauxiteSeries.Add(new ChartPoint(data.DateTime, data.Bauxite));
                    RepairToolSeries.Add(new ChartPoint(data.DateTime, data.RepairTool));
                });

            var currentDateTime = data.DateTime;

            var storableLimit = new ObservableCollection<ChartPoint>();
            storableLimit.Add(new ChartPoint(currentDateTime - TimeSpan.FromDays(365 * 3),
                materialManager.StorableMaterialLimit));
            storableLimit.Add(new ChartPoint(currentDateTime, materialManager.StorableMaterialLimit));
            this.StorableLimitSeries = storableLimit;
        }

        /// <summary>
        /// チャートにバインディングされたObservableCollectionのデータをリフレッシュします。
        /// </summary>
        /// <param name="neededData"></param>
        private void RefleshChartData(TimeMaterialsPair[] neededData)
        {

            var fuel = new ObservableCollection<ChartPoint>();
            var ammunition = new ObservableCollection<ChartPoint>();
            var steel = new ObservableCollection<ChartPoint>();
            var bauxite = new ObservableCollection<ChartPoint>();
            var repairTool = new ObservableCollection<ChartPoint>();
            var storableLimit = new ObservableCollection<ChartPoint>();

            foreach (var data in neededData)
            {
                fuel.Add(new ChartPoint(data.DateTime, data.Fuel));
                ammunition.Add(new ChartPoint(data.DateTime, data.Ammunition));
                steel.Add(new ChartPoint(data.DateTime, data.Steel));
                bauxite.Add(new ChartPoint(data.DateTime, data.Bauxite));
                repairTool.Add(new ChartPoint(data.DateTime, data.RepairTool));
            }

            var currentDateTime = neededData[neededData.Length - 1].DateTime;

            storableLimit.Add(new ChartPoint(currentDateTime - TimeSpan.FromDays(365 * 3),
                materialManager.StorableMaterialLimit));
            storableLimit.Add(new ChartPoint(currentDateTime, materialManager.StorableMaterialLimit));

            this.FuelSeries = fuel;
            this.AmmunitionSeries = ammunition;
            this.SteelSeries = steel;
            this.BauxiteSeries = bauxite;
            this.RepairToolSeries = repairTool;
            this.StorableLimitSeries = storableLimit;
        }

        /// <summary>
        /// X軸の設定を行います。
        /// </summary>
        private void SetXAxis(TimeMaterialsPair newData)
        {
            // 不可視にしないと更新の度描画するせいか死ぬほど遅くなる
            XAxisVisibility = Visibility.Collapsed;

            // X軸
            DateTimeLabelFormat = ChartUtilities.GetDateTimeFormat(ChartSettings.DisplayedPeriod);
            XMin = newData.DateTime - ChartSettings.DisplayedPeriod.Value.ToTimeSpan();
            XMax = newData.DateTime;
            XInterval = ChartUtilities.GetInterval(ChartSettings.DisplayedPeriod);
            // バインディングされたこいつの値を更新すると死ぬ（原因不明）
            //MinorTicksCount = ChartUtilities.GetMynorTicks(ChartSettings.DisplayedPeriod);

            XAxisVisibility = Visibility.Visible;
        }

        /// <summary>
        /// 資材グラフのY軸の設定を行います。
        /// </summary>
        /// <param name="newData"></param>
        private void SetMaterialYAxis(TimeMaterialsPair newData)
        {
            var yMaxValue1 = Math.Max(Math.Max(Math.Max(Math.Max((int)(YMax1 * 0.99), newData.Fuel),
                newData.Ammunition), newData.Steel), newData.Bauxite);
            var interval = ChartUtilities.GetInterval(0, yMaxValue1);
            YMin = 0;

            // Y軸のメモリ数が6個を超えるとRaisePropertyChanged()内でSparrowChartが応答しなくなる
            var hasIntervalUpdated = false;
            var yMax = ChartUtilities.GetYAxisMax(yMaxValue1, interval);

            if (yMax / YInterval1 >= 6)
            {
                YInterval1 = interval;
                hasIntervalUpdated = true;
            }
            YMax1 = yMax;

            if (!hasIntervalUpdated)
                YInterval1 = interval;

            //YMax1 = ChartUtilities.GetYAxisMax(yMaxValue1, interval);
            //YInterval1 = interval;
        }

        /// <summary>
        /// 資材グラフのY軸の設定を行います。
        /// </summary>
        /// <param name="neededData"></param>
        private void SetMaterialYAxis(TimeMaterialsPair[] neededData)
        {
            var yMaxValue1 = neededData.Max(p => Math.Max(Math.Max(Math.Max(p.Fuel, p.Ammunition), p.Steel), p.Bauxite));
            var interval = ChartUtilities.GetInterval(0, yMaxValue1);
            YMin = 0;

            // Y軸のメモリ数が6個を超えるとRaisePropertyChanged()内でSparrowChartが応答しなくなる
            var hasIntervalUpdated = false;
            var yMax = ChartUtilities.GetYAxisMax(yMaxValue1, interval);

            if (yMax / YInterval1 >= 6)
            {
                YInterval1 = interval;
                hasIntervalUpdated = true;
            }
            YMax1 = yMax;

            if (!hasIntervalUpdated)
                YInterval1 = interval;


            //YMax1 = ChartUtilities.GetYAxisMax(yMaxValue1, interval);
            //YInterval1 = interval;
        }

        /// <summary>
        /// 高速修復材グラフのY軸の設定を行います。
        /// </summary>
        /// <param name="newData"></param>
        private void SetRepairToolYAxis(TimeMaterialsPair newData)
        {
            var yMaxValue2 = Math.Max((int)(YMax2 * 0.99), newData.RepairTool);
            var interval = ChartUtilities.GetInterval(0, yMaxValue2);

            var hasIntervalUpdated = false;
            var yMax = ChartUtilities.GetYAxisMax(yMaxValue2, interval);

            if (yMax / YInterval2 >= 6)
            {
                YInterval2 = interval;
                hasIntervalUpdated = true;
            }
            YMax2 = yMax;

            if (!hasIntervalUpdated)
                YInterval2 = interval;


            //YMax2 = ChartUtilities.GetYAxisMax(yMaxValue2, interval);
            //YInterval2 = interval;
        }

        /// <summary>
        /// 高速修復材グラフのY軸の設定を行います。
        /// </summary>
        /// <param name="neededData"></param>
        private void SetRepairToolYAxis(TimeMaterialsPair[] neededData)
        {
            var yMaxValue2 = neededData.Max(p => p.RepairTool);
            var interval = ChartUtilities.GetInterval(0, yMaxValue2);

            var hasIntervalUpdated = false;
            var yMax = ChartUtilities.GetYAxisMax(yMaxValue2, interval);

            if (yMax / YInterval2 >= 6)
            {
                YInterval2 = interval;
                hasIntervalUpdated = true;
            }
            YMax2 = yMax;

            if (!hasIntervalUpdated)
                YInterval2 = interval;

            //YMax2 = ChartUtilities.GetYAxisMax(yMaxValue2, yInterval2);
            //YInterval2 = yInterval2;
        }

        public async void ExportAsCsv()
        {
            await materialManager.Log.ExportAsCsvAsync();
        }
    }

}
