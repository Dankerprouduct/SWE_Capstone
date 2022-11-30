using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Capstone
{
	/// <summary>
	/// Interaction logic for ItemsUserControl.xaml
	/// </summary>
	public partial class ItemsUserControl : UserControl
	{

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

			SavedProductList.ItemsSource = Products;
		}
		
	}
}
