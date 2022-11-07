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
using System.ComponentModel;
using System.Diagnostics;

namespace Capstone
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window, INotifyPropertyChanged
    {
        private SettingsData _settingsData = new SettingsData();
        public SettingsData SettingsData { 
            get
            {
                return _settingsData;
            }
            set
            {
                if (_settingsData != value)
                {
                    _settingsData = value;
                    OnPropertyChanged("SettingsData");
                }
            } 
        }
        public SettingsWindow()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w1 = new MainWindow()
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
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            //Nothing happens, this is already on the settings window*/
        }

        // Clicking the SearchButton in the SettingsWindow seems to activate this method instead...
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SearchButton_Click(sender, e);
        }
        private void SettingsWindowSetup(bool IsDarkModeEnabled)
        {
            if (IsDarkModeEnabled)
            {
                ///SolidColorBrush s = (SolidColorBrush)this.FindResource("BackgroundColor");
                /// s = new SolidColorBrush(Color "Orange"); //  i dont get it how to fix /?????
                /*Border b = (Border)this.Template.FindName("BORDERCONTROL", this);
        b.Background = new SolidColorBrush(Colors.Yellow);*/
            }

        }

        //Button click events for adjusting screen resolution
        private void Res640_480_Click(object sender, RoutedEventArgs e)
        {
            this.Height = 480;
            this.Width = 640;
        }
        private void Res800_480_Click(object sender, RoutedEventArgs e)
        {
            this.Height = 480;
            this.Width = 800;
        }
        private void Res800_600_Click(object sender, RoutedEventArgs e)
        {
            this.Height = 600;
            this.Width = 800;
        }
        private void Res1024_768_Click(object sender, RoutedEventArgs e)
        {
            this.Height = 768;
            this.Width = 1024;
        }
        private void Res1280_720_Click(object sender, RoutedEventArgs e)
        {
            this.Height = 720;
            this.Width = 1280;
        }
        private void Res1366_768_Click(object sender, RoutedEventArgs e)
        {
            this.Height = 768;
            this.Width = 1366;
        }
        private void Res1920_1080_Click(object sender, RoutedEventArgs e)
        {
            this.Height = 1080;
            this.Width = 1920;
        }
        private void Res1920_1200_Click(object sender, RoutedEventArgs e)
        {
            this.Height = 1200;
            this.Width = 1920;
        }
        /*SolidColorBrush MyBrush = Brushes.AliceBlue;
         // Set the value
        Application.Current.Resources["DynamicBG"] = MyBrush;*/
    }
}
