using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using System.Diagnostics;

namespace Capstone
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void Buggy_Startup(object sender, StartupEventArgs e)
        {
            //base.OnStartup(e);
            //These are the initial settings when the app first starts
            SettingsData initialSettings = new SettingsData()
            {
                IsAmazonChecked     = false,         //As we implement more crawlers, we can change the default settings here
                IsEBayChecked       = false,
                IsCostcoChecked     = false,
                IsTargetChecked     = true,
                IsWalmartChecked    = false,
                IsSaveDataChecked   = true
            };

            MainWindow mw = new MainWindow();
            mw.SettingsData = initialSettings;
            this.MainWindow = mw;
            mw.DataContext = initialSettings;

            mw.Show();
        }
    }
}
