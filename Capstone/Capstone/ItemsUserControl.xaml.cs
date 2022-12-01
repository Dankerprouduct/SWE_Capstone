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
using LiveChartsCore.Kernel.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WPF;
using SkiaSharp;

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
						DataLabelsSize = 15,
						DataLabelsPaint = new SolidColorPaint(SKColors.White),
						DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Top,
						DataLabelsFormatter = (point) => point.PrimaryValue.ToString("C"),
						Values = _currentValues,
						IsHoverable = false,
						Fill = null,
						
					}
				};
			}
		}
		
		private static double[] _currentValues = new double[] {};
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
			foreach (var axis in PriceChart.YAxes)
			{
				axis.ShowSeparatorLines = false;
			}

			SavedProductList.SelectionChanged += (sender, args) =>
			{
				_currentValues = GatherValues().Result.ToArray();
				OnPropertyChanged(nameof(Series));
			};


			UserProductService.Instance.ProductsUpdated += (sender, args) =>
			{
				OnPropertyChanged(nameof(Products));
				
			};
		}

		private async Task<List<double>> GatherValues()
		{
			var products = await ProductService.Instance.GetProducts();

			var currentItem = (SavedProduct) SavedProductList.SelectedItem;
			if (currentItem == null)
				return new List<double>();
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

		private void RemoveItem(object sender, RoutedEventArgs e)
		{
			var button = sender as Button;
			var tag = button.Tag as SavedProduct;

			if (tag != null)
			{
				UserProductService.Instance.RemoveItem(tag);
			}
		}
	}
}
