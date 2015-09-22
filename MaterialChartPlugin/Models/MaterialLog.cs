using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Livet;
using ProtoBuf;

namespace MaterialChartPlugin.Models
{
    public class MaterialLog : NotificationObject
    {
        static readonly string roamingDirectoryPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "terry_u16", "MaterialChartPlugin");

        static readonly string exportDirectoryPath = "MaterialChartPlugin";

        static readonly string saveFileName = "materiallog.dat";

        static string FilePath => Path.Combine(roamingDirectoryPath, saveFileName);

        private MaterialChartPlugin plugin;

        #region HasLoaded変更通知プロパティ
        private bool _HasLoaded = false;

        public bool HasLoaded
        {
            get
            { return _HasLoaded; }
            set
            { 
                if (_HasLoaded == value)
                    return;
                _HasLoaded = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        public ObservableCollection<TimeMaterialsPair> History { get; private set; }

        public MaterialLog(MaterialChartPlugin plugin)
        {
            this.plugin = plugin;
        }

        public async Task LoadAsync()
        {
            if (File.Exists(FilePath))
            {
                try
                {
                    using (var stream = File.OpenRead(FilePath))
                    {
                        this.History = await Task.Run(() => Serializer.Deserialize<ObservableCollection<TimeMaterialsPair>>(stream));
                    }
                }
                catch (ProtoException ex)
                {
                    plugin.InvokeNotifyRequested(new Grabacr07.KanColleViewer.Composition.NotifyEventArgs(
                        "MaterialChartPlugin.LoadFailed", "読み込み失敗",
                        "資材データの読み込みに失敗しました。データが破損している可能性があります。"));
                    System.Diagnostics.Debug.WriteLine(ex);
                    this.History = new ObservableCollection<TimeMaterialsPair>();
                }
                catch (IOException ex)
                {
                    plugin.InvokeNotifyRequested(new Grabacr07.KanColleViewer.Composition.NotifyEventArgs(
                        "MaterialChartPlugin.LoadFailed", "読み込み失敗",
                        "資材データの読み込みに失敗しました。必要なアクセス権限がない可能性があります。"));
                    System.Diagnostics.Debug.WriteLine(ex);
                    this.History = new ObservableCollection<TimeMaterialsPair>();
                }
            }
            else
            {
                this.History = new ObservableCollection<TimeMaterialsPair>();
            }
            
            this.HasLoaded = true;
        }

        public async Task SaveAsync()
        {
            try
            {
                if (!Directory.Exists(roamingDirectoryPath))
                {
                    Directory.CreateDirectory(roamingDirectoryPath);
                }

                // オレオレ形式でバイナリ保存とかも考えたけど
                // 今後ネジみたいに新しい資材が入ってくると対応が面倒なのでやめた
                using (var stream = File.OpenWrite(FilePath))
                {
                    await Task.Run(() => Serializer.Serialize(stream, History));
                }
            }
            catch (IOException ex)
            {
                plugin.InvokeNotifyRequested(new Grabacr07.KanColleViewer.Composition.NotifyEventArgs(
                    "MaterialChartPlugin.SaveFailed", "読み込み失敗",
                    "資材データの保存に失敗しました。必要なアクセス権限がない可能性があります。"));
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public async Task ExportAsCsvAsync()
        {
            var csvFileName = CreateCsvFileName(DateTime.Now);
            var csvFilePath = Path.Combine(exportDirectoryPath, csvFileName);

            try
            {
                if (!Directory.Exists(exportDirectoryPath))
                {
                    Directory.CreateDirectory(exportDirectoryPath);
                }

                using (var writer = new StreamWriter(csvFilePath, false, Encoding.UTF8))
                {
                    await writer.WriteLineAsync("時刻,燃料,弾薬,鋼材,ボーキサイト,高速修復材,開発資材,高速建造材,改修資材");

                    foreach (var pair in History)
                    {
                        await writer.WriteLineAsync($"{pair.DateTime},{pair.Fuel},{pair.Ammunition},{pair.Steel},{pair.Bauxite},{pair.RepairTool},{pair.DevelopmentTool},{pair.InstantBuildTool},{pair.ImprovementTool}");
                    }
                }

                plugin.InvokeNotifyRequested(new Grabacr07.KanColleViewer.Composition.NotifyEventArgs(
                    "MaterialChartPlugin.ExportCompleted", "エクスポート完了",
                    $"資材データがエクスポートされました: {csvFilePath}")
                {
                    Activated = () =>
                    {
                        System.Diagnostics.Process.Start("EXPLORER.EXE", $"/select,\"\"{csvFilePath}\"\"");
                    }
                });
            }
            catch (IOException ex)
            {
                plugin.InvokeNotifyRequested(new Grabacr07.KanColleViewer.Composition.NotifyEventArgs(
                    "MaterialChartPlugin.ExportFailed", "エクスポート失敗",
                    "資材データのエクスポートに失敗しました。必要なアクセス権限がない可能性があります。"));
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private string CreateCsvFileName(DateTime dateTime)
        {
            return $"MaterialChartPlugin-{dateTime.ToString("yyMMdd-HHmmssff")}.csv";
        }
    }
}
