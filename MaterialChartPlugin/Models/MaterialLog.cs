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
using Grabacr07.KanColleWrapper;
using MaterialChartPlugin.Models.Utilities;

namespace MaterialChartPlugin.Models
{
    public class MaterialLog : NotificationObject
    {
        static readonly string localDirectoryPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "terry_u16", "MaterialChartPlugin");

        public static readonly string ExportDirectoryPath = "MaterialChartPlugin";

        static readonly string saveFileName = "materiallog.dat";

        static string SaveFilePath => Path.Combine(localDirectoryPath, saveFileName);

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
            await LoadAsync(SaveFilePath, null);
        }

        private async Task LoadAsync(string filePath, Action onSuccess)
        {
            this.HasLoaded = false;

            if (File.Exists(filePath))
            {
                try
                {
                    using (var stream = File.OpenRead(filePath))
                    {
                        var loadedHistory = await Task.Run(() => Serializer.Deserialize<ObservableCollection<TimeMaterialsPair>>(stream));
                        // 現在の資材データと読み込んだ資材データとをマージ
                        this.History = new ObservableCollection<TimeMaterialsPair>(
                            loadedHistory.Union(History, x => x.DateTime).OrderBy(x => x.DateTime));
                    }
                    onSuccess?.Invoke();
                }
                catch (ProtoException ex)
                {
                    plugin.InvokeNotifyRequested(new Grabacr07.KanColleViewer.Composition.NotifyEventArgs(
                        "MaterialChartPlugin.LoadFailed", "読み込み失敗",
                        "資材データの読み込みに失敗しました。データが破損している可能性があります。"));
                    System.Diagnostics.Debug.WriteLine(ex);
                    if (this.History == null)
                        this.History = new ObservableCollection<TimeMaterialsPair>();
                }
                catch (IOException ex)
                {
                    plugin.InvokeNotifyRequested(new Grabacr07.KanColleViewer.Composition.NotifyEventArgs(
                        "MaterialChartPlugin.LoadFailed", "読み込み失敗",
                        "資材データの読み込みに失敗しました。必要なアクセス権限がない可能性があります。"));
                    System.Diagnostics.Debug.WriteLine(ex);
                    if (this.History == null)
                        this.History = new ObservableCollection<TimeMaterialsPair>();
                }
            }
            else
            {
                if (this.History == null)
                    this.History = new ObservableCollection<TimeMaterialsPair>();
            }

            this.HasLoaded = true;
        }

        public async Task SaveAsync()
        {
            try
            {
                await SaveAsync(localDirectoryPath, SaveFilePath, null);
            }
            catch (IOException ex)
            {
                plugin.InvokeNotifyRequested(new Grabacr07.KanColleViewer.Composition.NotifyEventArgs(
                    "MaterialChartPlugin.SaveFailed", "読み込み失敗",
                    "資材データの保存に失敗しました。必要なアクセス権限がない可能性があります。"));
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private async Task SaveAsync(string directoryPath, string filePath, Action onSuccess)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // オレオレ形式でバイナリ保存とかも考えたけど
                // 今後ネジみたいに新しい資材が入ってくると対応が面倒なのでやめた
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await Task.Run(() => Serializer.Serialize(stream, History));
                }

                onSuccess?.Invoke();
            }
            catch (IOException ex)
            {
                plugin.InvokeNotifyRequested(new Grabacr07.KanColleViewer.Composition.NotifyEventArgs(
                    "MaterialChartPlugin.SaveFailed", "保存失敗",
                    "資材データの保存に失敗しました。必要なアクセス権限がない可能性があります。"));
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public async Task ExportAsCsvAsync()
        {
            var csvFileName = CreateCsvFileName(DateTime.Now);
            var csvFilePath = Path.Combine(ExportDirectoryPath, csvFileName);

            try
            {
                if (!Directory.Exists(ExportDirectoryPath))
                {
                    Directory.CreateDirectory(ExportDirectoryPath);
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
                    "MaterialChartPlugin.CsvExportCompleted", "エクスポート完了",
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

        public async Task ImportAsync(string filePath)
        {
            await LoadAsync(filePath, async () =>
                {
                    plugin.InvokeNotifyRequested(new Grabacr07.KanColleViewer.Composition.NotifyEventArgs(
                        "MaterialChartPlugin.ImportComplete", "インポート完了",
                        "資材データのインポートに成功しました。"));

                    var materials = KanColleClient.Current.Homeport.Materials;
                    History.Add(new TimeMaterialsPair(DateTime.Now, materials.Fuel, materials.Ammunition, materials.Steel,
                        materials.Bauxite, materials.InstantRepairMaterials, materials.DevelopmentMaterials,
                        materials.InstantBuildMaterials, materials.ImprovementMaterials));

                    await SaveAsync();
                }
            );

        }

        public async Task ExportAsync()
        {
            var fileName = CreateExportedFileName(DateTime.Now);
            var filePath = Path.Combine(ExportDirectoryPath, fileName);
            await SaveAsync(ExportDirectoryPath, filePath, () =>
                plugin.InvokeNotifyRequested(new Grabacr07.KanColleViewer.Composition.NotifyEventArgs(
                    "MaterialChartPlugin.ExportComplete", "エクスポート完了",
                    $"資材データがエクスポートされました: {filePath}")
                {
                    Activated = () =>
                    {
                        System.Diagnostics.Process.Start("EXPLORER.EXE", $"/select,\"\"{filePath}\"\"");
                    }
                })
            );
        }

        private string CreateCsvFileName(DateTime dateTime)
        {
            return $"MaterialChartPlugin-{dateTime.ToString("yyMMdd-HHmmssff")}.csv";
        }

        private string CreateExportedFileName(DateTime dateTime)
        {
            return $"MaterialChartPlugin-BackUp-{dateTime.ToString("yyMMdd-HHmmssff")}.dat";
        }
    }
}
