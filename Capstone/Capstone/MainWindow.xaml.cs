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
using System.Diagnostics;
using System.ComponentModel;
using Capstone.Services;
using Microsoft.EntityFrameworkCore;

namespace Capstone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// 
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

	    protected readonly BuggyDbContext _context = new BuggyDbContext();

        private SettingsData _settingsData = new SettingsData();
        public SettingsData SettingsData
        {
            get {
                return _settingsData;
            }
            set {
                _settingsData = value;
                OnPropertyChanged("SettingsData");
            }
        }

        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }
            
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            this.Loaded += (sender, args) =>
            {
	            _context.Database.EnsureCreated(); 
                _context.Products.Load();

                ProductService.Instance = new ProductService(_context); 
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


        private async void SearchTextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                
                var searchText = ((TextBox) sender).Text;
				var products = await GetProducts(searchText);
				products = SortPriceDesc(products);
				this.Dispatcher.Invoke(() =>
				{
					Products.Clear();
					foreach (Product product in products)
					{
						Products.Add(product);
					}
				});

				return;
				this.Dispatcher.Invoke(() =>
                {

					Products.Clear();
					foreach (Product product in products)
					{
						Products.Add(product);
					}
					return;
                    
	                
	                // old way

	                List<Product> resultItems = new List<Product>(); // This stores results from each crawler.
                    //If eBay is included in the search...
                    if (SettingsData.IsEBayChecked)
                    {
                        var eBayCrawler = new EbayCrawler();
                        var results = Task.Run(() => eBayCrawler.SearchProduct(searchText));
                        foreach (Product? product in results.Result)
                        {
                            resultItems.Add(product);
                        }
                    }
                    //If Amazon is included in the search...
                    if (SettingsData.IsAmazonChecked)
                    {
                        var amazonCrawler = new AmazonCrawler();
                        var results = Task.Run(() => amazonCrawler.SearchProduct(searchText));
                        foreach (Product? product in results.Result)
                        {
                            resultItems.Add(product);
                        }
                    }
                    //Sort the results,
                    resultItems = SortPriceDesc(resultItems);

                    //Print them to the screen.
                    Products.Clear();
                    foreach (Product product in resultItems)
                    {
                        Products.Add(product);
                    }
                });

            }
        }

        private async Task<List<Product>> GetProducts(string searchString)
        {
            var products = new List<Product>();
	        foreach (Type type in GetType().Assembly.GetTypes())
	        {

		        if (typeof(IWebCrawler).IsAssignableFrom(type) && type.IsInterface == false)
		        {
			        var instance = Activator.CreateInstance(type);
			        IWebCrawler webCrawler = (IWebCrawler)instance;
			        if(!webCrawler.Enabled)
                        continue;
			        var values = await webCrawler.SearchProduct(searchString);

                    foreach (var p in values)
                    {
                        products.Add(p);

                        p.DateTime = DateTime.Now;
                        await ProductService.Instance.AddProductAsync(p);
                    }
		        }
	        }

	        return products;
        }

        //Takes a list of products and sorts it by price in descending order. This can be used to sort results from any (?) crawler.
        private List<Product> SortPriceDesc(List<Product> products)
        {

            if (products == null || products.Count == 0)
            {
                return new List<Product>();
            }
            List<Product> sortedProducts = new List<Product>();
            Product temp = new Product();
            char[] tempPriceArr;
            double temp1 = 0.0;
            double[] sortPriceArr = new double[products.Count * 2]; //sortPriceArr = [price0, index of price0 in products, ..., priceN, index of priceN in products]
            //Convert price values into decimal values
            for (int i = 0; i < products.Count; i++)
            {
                tempPriceArr = products[i].Price.ToCharArray();
                tempPriceArr = Array.FindAll<char>(tempPriceArr, (c => (char.IsDigit(c) || c == '.')));
                try
                {
                    temp1 = Double.Parse(new string(tempPriceArr));
                    sortPriceArr[2 * i] = temp1;
                    sortPriceArr[2 * i + 1] = i;
                }
                catch (Exception ex) { }
            }

            //Sort the decimal values, since initially the price index = index in the list
            int min = 0; // index of min val
            double temp2 = 0;


            for (int k = 0; k < sortPriceArr.Length; k += 2)
            {
                min = k + 1;

                for (int j = k; j < sortPriceArr.Length; j += 2)
                {
                    if (sortPriceArr[j] < sortPriceArr[min - 1])
                    {
                        min = j + 1;
                    }
                }
                //Add the product at the min index to the sorted products
                sortedProducts.Add(products[(int)sortPriceArr[min]]);
                temp1 = sortPriceArr[k];
                temp2 = sortPriceArr[k + 1];
                sortPriceArr[k] = sortPriceArr[min - 1];
                sortPriceArr[k + 1] = sortPriceArr[min];
                sortPriceArr[min - 1] = temp1;
                sortPriceArr[min] = temp2;
            }
            return sortedProducts;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow w1 = new SettingsWindow()
            {
                SettingsData = this.SettingsData
            };

            //Dimensions
            w1.Height = this.ActualHeight;
            w1.Width = this.ActualWidth;
            w1.Top = this.Top;
            w1.Left = this.Left;

            w1.Show();
            this.Hide();
        }
    }
}
