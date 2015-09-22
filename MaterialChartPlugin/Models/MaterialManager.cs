using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using Livet.EventListeners;
using Grabacr07.KanColleWrapper;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.ComponentModel;
using MetroTrilithon.Mvvm;

namespace MaterialChartPlugin.Models
{
    public class MaterialManager : NotificationObject
    {
        private MaterialChartPlugin plugin;

        public int Fuel => KanColleClient.Current.Homeport.Materials.Fuel;

        public int Ammunition => KanColleClient.Current.Homeport.Materials.Ammunition;

        public int Steel => KanColleClient.Current.Homeport.Materials.Steel;

        public int Bauxite => KanColleClient.Current.Homeport.Materials.Bauxite;

        public int RepairTool => KanColleClient.Current.Homeport.Materials.InstantRepairMaterials;

        /// <summary>
        /// 備蓄可能な資材量の上限を表します。
        /// </summary>
        public int StorableMaterialLimit => KanColleClient.Current.Homeport.Admiral.Level * 250 + 750;

        public MaterialLog Log { get; private set; }

        #region IsAvailable変更通知プロパティ
        private bool _IsAvailable = false;

        public bool IsAvailable
        {
            get
            { return _IsAvailable; }
            set
            { 
                if (_IsAvailable == value)
                    return;
                _IsAvailable = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        PropertyChangedEventListener listener;

        public MaterialManager(MaterialChartPlugin plugin)
        {
            this.plugin = plugin;

            this.Log = new MaterialLog(plugin);

            KanColleClient.Current
                // KanColleClientのIsStartedがtrueに変更されたら資材データの購読を開始
                .Subscribe(nameof(KanColleClient.IsStarted), () =>
                {
                    var materials = KanColleClient.Current.Homeport.Materials;
                    var adomiral = KanColleClient.Current.Homeport.Admiral;
                    listener = new PropertyChangedEventListener(materials)
                    {
                        { nameof(materials.Fuel),  (_,__) => RaisePropertyChanged(nameof(Fuel)) },
                        { nameof(materials.Ammunition),  (_,__) => RaisePropertyChanged(nameof(Ammunition)) },
                        { nameof(materials.Steel),  (_,__) => RaisePropertyChanged(nameof(Steel)) },
                        { nameof(materials.Bauxite),  (_,__) => RaisePropertyChanged(nameof(Bauxite)) },
                        { nameof(materials.InstantRepairMaterials),  (_,__) => RaisePropertyChanged(nameof(RepairTool)) },
                        { nameof(adomiral.Level), (_, __) => RaisePropertyChanged(nameof(StorableMaterialLimit)) }
                    };

                    // 資材のロギング
                    Observable.FromEvent<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                        h => (sender, e) => h(e),
                        h => materials.PropertyChanged += h,
                        h => materials.PropertyChanged -= h)
                        // プロパティ名が一致しているか調べて
                        .Where(e => IsObservedPropertyName(e.PropertyName))
                        // まとめて通知が来るので10ms待機して
                        .Throttle(TimeSpan.FromMilliseconds(10))
                        // 処理
                        .Subscribe(async _ =>
                        {
                            if (Log.HasLoaded)
                            {
                                Log.History.Add(new TimeMaterialsPair(DateTime.Now, Fuel, Ammunition, Steel, Bauxite, RepairTool,
                                    materials.DevelopmentMaterials, materials.InstantBuildMaterials, materials.ImprovementMaterials));
                                await Log.SaveAsync();
                            }
                        });

                    this.IsAvailable = true;

                }, false);
        }

        /// <summary>
        /// 監視対象のプロパティ名と一致しているかを調べます。
        /// </summary>
        /// <param name="propertyName">変更が通知されたプロパティ名</param>
        /// <returns></returns>
        private bool IsObservedPropertyName(string propertyName)
        {
            var materials = KanColleClient.Current.Homeport.Materials;
            return propertyName == nameof(materials.Fuel) || propertyName == nameof(materials.Ammunition)
                || propertyName == nameof(materials.Steel) || propertyName == nameof(materials.Bauxite)
                || propertyName == nameof(materials.InstantRepairMaterials);
        }

        public async Task Initialize()
        {
            await Log.LoadAsync();
        }
    }
}
