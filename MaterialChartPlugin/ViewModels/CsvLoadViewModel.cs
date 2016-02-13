using System;
using System.Collections.Generic;
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
using Reactive.Bindings;

namespace MaterialChartPlugin.ViewModels
{
    public class CsvLoadViewModel : ViewModel
    {
        public ReactiveProperty<string> FilePath { get; private set; }

        public CsvLoadViewModel(MaterialManager manager)
        {
            FilePath = new ReactiveProperty<string>(string.Empty);
        }
    }
}
