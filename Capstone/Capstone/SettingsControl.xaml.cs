using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Capstone
{
    /// <summary>
    /// Interaction logic for SettingsControl.xaml
    /// </summary>
    public partial class SettingsControl : UserControl, INotifyPropertyChanged
    {
        public SettingsControl()
        {
            InitializeComponent();
        }

        private SettingsData _settingsData = new SettingsData();
        public SettingsData SettingsData
        {
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

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
	        //Nothing happens, this is already on the settings window*/
        }

        // Clicking the SearchButton in the SettingsWindow seems to activate this method instead...
        private void Button_Click(object sender, RoutedEventArgs e)
        {
	        //SearchButton_Click(sender, e);
        }
        private void SettingsWindowSetup()
        {
	        //This is left blank for now

        }
	}
}
