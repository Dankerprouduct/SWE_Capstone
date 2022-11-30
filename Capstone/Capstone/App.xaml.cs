using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using System.Diagnostics;
using Capstone.Services;
using Microsoft.Extensions.DependencyInjection;
using Capstone.Models;
using Microsoft.EntityFrameworkCore;

namespace Capstone
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void Buggy_Startup(object sender, StartupEventArgs e)
        {

			//_context.Database.EnsureCreated();
			//_context.Products.Load();
			//_context.UserProducts.Add(new SavedProduct()
			//{
			// ProductID = 0,
			// ProductName = "test"
			//});

			//ProductService.Instance = new ProductService(_context);
			//UserProductService.Instance = new UserProductService(_context);

            //base.OnStartup(e);
            //These are the initial settings when the app first starts
            SettingsData initialSettings = new SettingsData()
            {
                IsAmazonChecked     = true,         //As we implement more crawlers, we can change the default settings here
                IsEBayChecked       = true,
                IsCostcoChecked     = false,
                IsTargetChecked     = false,
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
