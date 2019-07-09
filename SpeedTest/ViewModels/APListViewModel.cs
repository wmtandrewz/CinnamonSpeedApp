using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using SpeedTest.Interfaces;
using SpeedTest.Models;
using Xamarin.Forms;

namespace SpeedTest.ViewModels
{
    public class APListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand UpdateResultsCommand { get; }

        private ObservableCollection<APModel> _scanResults;
        public ObservableCollection<APModel> ScanResults
        {
            get
            {
                return _scanResults;
            }
            set
            {
                _scanResults = value;
                OnPropertyChanged("ScanResults");
            }
        }

        public APListViewModel()
        {
            UpdateResultsCommand = new Command(async () => await LoadAPList());
        }

        private async Task LoadAPList()
        {

            var scanList = DependencyService.Get<IWiFiStat>().GetAvailableSSIDList();

            List<APModel> apModelList = new List<APModel>();

            if (scanList != null)
            {
                foreach (var item in scanList)
                {
                    var model = new APModel
                    {
                        Ssid = !string.IsNullOrEmpty(item.Ssid) ? item.Ssid : "(Hidden SSID)",
                        Bssid = item.Bssid,
                        Frequency = item.Frequency.ToString() + " MHz",
                        Capabilities = item.Capabilities?.Split(']')[0] + "]",
                        Strength = item.Level.ToString() + " dBm",
                        Distance = "~ " + Math.Round(GetDistance(item.Frequency, item.Level), 2).ToString() + " m"
                    };

                    apModelList.Add(model);
                }
            }

            Device.BeginInvokeOnMainThread(() =>
            {
                ScanResults = new ObservableCollection<APModel>(apModelList);
            });

        }



        private double GetDistance(int freq, int rssi)
        {
            double exp = (27.55 - (20 * Math.Log10(freq)) + Math.Abs(rssi)) / 20.0;
            return Math.Pow(10.0, exp);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
