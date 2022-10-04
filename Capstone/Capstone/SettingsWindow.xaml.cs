using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Capstone
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public bool isAmazonChecked { get; set; }
        public bool isEBayChecked { get; set; }
        public SettingsWindow()
        {
            InitializeComponent();
            if (this.isEBayChecked==true)
            {
                SettingsEBayCheckBox.IsChecked = true;
            }
            if (this.isAmazonChecked==true)
            {
                SettingsAmazonCheckBox.IsChecked = true;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w1 = new MainWindow();

            w1.Height = this.ActualHeight;
            w1.Width = this.ActualWidth;

            w1.Top = this.Top;
            w1.Left = this.Left;

            w1.isAmazonChecked = this.isAmazonChecked;
            w1.isEBayChecked = this.isEBayChecked;

            w1.Show();
            this.Hide();
        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            //Nothing happens, this is already on the settings window*/
        }

        // Clicking the SearchButton in the SettingsWindow seems to activate this method instead...
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SearchButton_Click(sender, e);
        }

        //Amazon checkbox events
        private void SettingsAmazonCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            isAmazonChecked = true;
        }
        private void SettingsAmazonCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            isAmazonChecked = false;
        }

        //Ebay checkbox events
        private void SettingsEBayCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            isEBayChecked = true;
        }
        private void SettingsEBayCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            isEBayChecked = false;
        }
    }
}
