using MetroTrilithon.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialChartPlugin.Models.Settings
{
    public class SettingsProviders
    {
        public static string RoamingFilePath { get; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "terry_u16", "MaterialChartPlugin", "Settings.xaml");

        public static string LocalFilePath { get; } = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "terry_u16", "MaterialChartPlugin", "Settings.xaml");

        public static ISerializationProvider Roaming { get; } = new FileSettingsProvider(RoamingFilePath);

        public static ISerializationProvider Local { get; } = new FileSettingsProvider(LocalFilePath);
    }
}
