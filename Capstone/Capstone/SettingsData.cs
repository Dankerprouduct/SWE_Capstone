using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Capstone
{
    public class SettingsData : INotifyPropertyChanged
    {
        //Data binding for the settings, so it is saved between windows.
        public SettingsData()
        {
            //
        }
        
        //Filter by a specific retailer (one variable for each).
        bool isAmazonChecked;
        public bool IsAmazonChecked
        {
            get { return isAmazonChecked; }
            set { isAmazonChecked = value; OnPropertyChanged(nameof(IsAmazonChecked)); }
        }

        bool isEBayChecked;
        public bool IsEBayChecked {
            get { return isEBayChecked; }
            set { isEBayChecked = value; OnPropertyChanged(nameof(IsEBayChecked)); }
        }

        bool isCostcoChecked;
        public bool IsCostcoChecked {
            get { return isCostcoChecked; }
            set { isCostcoChecked = value; OnPropertyChanged(nameof(IsCostcoChecked)); }
        }

        bool isWalmartChecked;
        public bool IsWalmartChecked {
            get { return isWalmartChecked; }
            set { isWalmartChecked = value; OnPropertyChanged(nameof(IsWalmartChecked)); }
        }

        bool isTargetChecked;
        public bool IsTargetChecked {
            get { return isTargetChecked; }
            set { isTargetChecked = value; OnPropertyChanged(nameof(IsTargetChecked)); }
        }

        //Should search data be saved and sent to the DB? (settings)
        bool isSaveDataChecked;
        public bool IsSaveDataChecked {
            get { return isSaveDataChecked; }
            set { isSaveDataChecked = value; OnPropertyChanged(nameof(IsSaveDataChecked)); }
        }

        //Is the dark mode enabled? False = light mode (settings)
        bool isDarkModeChecked;
        public bool IsDarkModeChecked
        {
            get { return isDarkModeChecked; }
            set { isDarkModeChecked = value; OnPropertyChanged(nameof(IsSaveDataChecked)); }
        }

        //Does the user want price drop notifications? (settings)
        bool isPriceAlertsEnabled;
        public bool IsPriceAlertsEnabled
        {
            get { return isPriceAlertsEnabled; }
            set { isPriceAlertsEnabled = value; OnPropertyChanged(nameof(IsPriceAlertsEnabled)); }
        }

        //user's phone number (settings)
        string _phone;
        public string Phone
        {
            get { return _phone; } 
            set { _phone = value; OnPropertyChanged(nameof(Phone)); }
        }

        //user's email address (settings)
        string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
