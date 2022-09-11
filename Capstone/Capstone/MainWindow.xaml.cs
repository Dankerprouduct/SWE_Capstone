using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Capstone.Crawlers;
using Capstone.Models;

namespace Capstone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>(); 
        public MainWindow()
        {
            InitializeComponent();
            RunAmazonCrawler();

            DataContext = this;
        }

        public void RunAmazonCrawler()
        {
            
        }

        private void SearchTextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var searchText = ((TextBox) sender).Text;

                this.Dispatcher.Invoke(() =>
                {
                    var amazonCrawler = new AmazonCrawler();
                    var results = Task.Run(() => amazonCrawler.SearchProduct(searchText));

                    Products.Clear();
                    foreach (var result in results.Result)
                    {
                        Products.Add(result);
                    }
                });

            }
        }
    }
}
