using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
using Capstone.Models;
using Capstone.Services;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace Capstone
{
	/// <summary>
	/// Interaction logic for ItemsUserControl.xaml
	/// </summary>
	public partial class ItemsUserControl : UserControl, INotifyPropertyChanged
	{

		public ISeries[] Series
		{
			get
			{
				return new ISeries[]
				{
					new LineSeries<double>
					{
						Values = _currentValues,
						Fill = null
					}
				};
			}
		}

		private static double[] _currentValues = new double[] {0, 1, 2, 3, 4, 5};
		public ObservableCollection<SavedProduct> Products
		{
			get
			{
				var products = UserProductService.Instance.Products;
				return new ObservableCollection<SavedProduct>(products);
			}
		}

		public ItemsUserControl()
		{
			InitializeComponent();
			UserProductService.Instance = new UserProductService();

			DataContext = this;
			SavedProductList.ItemsSource = Products;

			SavedProductList.SelectionChanged += (sender, args) =>
			{
				_currentValues = GatherValues().Result.ToArray();
				OnPropertyChanged(nameof(Series));
			};
		}

		private async Task<List<double>> GatherValues()
		{
			var products = await ProductService.Instance.GetProducts();

			var currentItem = (SavedProduct) SavedProductList.SelectedItem;
			var items = products.Where(x => x.Name == currentItem.ProductName);

			var values = new List<double>();
			foreach (var item in items)
			{
				try
				{
					Debug.WriteLine($"price: {item.Price}");
					var i = item.Price.Replace('$', ' ');
					Debug.WriteLine($"new value: {i}");
					var value = double.Parse(i);

					values.Add(value);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}


			return values;
		}

		private void SavedProductList_OnSelected(object sender, RoutedEventArgs e)
		{

		}

		private void SavedProductList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(field, value)) return false;
			field = value;
			OnPropertyChanged(propertyName);
			return true;
		}
	}
}
