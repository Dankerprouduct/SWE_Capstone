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
        public SettingsData()
        {
            //
        }
        //Data binding for the settings so it is saved between windows.
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

        bool isSaveDataChecked;
        public bool IsSaveDataChecked {
            get { return isSaveDataChecked; }
            set { isSaveDataChecked = value; OnPropertyChanged(nameof(IsSaveDataChecked)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
