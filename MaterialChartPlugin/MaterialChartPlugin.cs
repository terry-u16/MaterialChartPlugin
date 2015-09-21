using Grabacr07.KanColleViewer.Composition;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialChartPlugin.Views;
using MaterialChartPlugin.ViewModels;
using Livet;

namespace MaterialChartPlugin
{
    [Export(typeof(IPlugin))]
    [ExportMetadata("Guid", "56B66906-608A-4BCC-9FE2-6B3B0093F377")]
    [ExportMetadata("Title", "MaterialChart")]
    [ExportMetadata("Description", "資材の推移を折れ線グラフで表示します。")]
    [ExportMetadata("Version", "0.1.0")]
    [ExportMetadata("Author", "@terry_u16")]
    [Export(typeof(ITool))]
    [Export(typeof(IRequestNotify))]
    public class MaterialChartPlugin : IPlugin, ITool, IRequestNotify
    {
        private ToolViewModel viewModel;

        public event EventHandler<NotifyEventArgs> NotifyRequested;

        public string Name => "Material";

        public void InvokeNotifyRequested(NotifyEventArgs e) => this.NotifyRequested?.Invoke(this, e);

        // タブ表示する度に new されてしまうが、毎回 new しないとグラフが正常に表示されない模様？
        public object View => new ToolView() { DataContext = viewModel };

        static MaterialChartPlugin()
        {
            try
            {
                // このクラスの何らかのメンバーにアクセスされたら読み込み
                // 読み込みに失敗したら例外が投げられてプラグインだけが死ぬ（はず）
                System.Reflection.Assembly.LoadFrom("protobuf-net.dll");
                System.Reflection.Assembly.LoadFrom("Sparrow.Chart.Wpf.40.dll");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw;
            }
        }

        public MaterialChartPlugin()
        {
            viewModel = new ToolViewModel(this);
        }

        public void Initialize()
        {
            viewModel.Initialize();
        }

    }
}
