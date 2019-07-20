using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SpeedTest.Globals;
using SpeedTest.Interfaces;
using SpeedTest.Models;
using SpeedTest.Services;
using SpeedTest.Views;
using Xamarin.Forms;
using SpeedTest.Helpers;
using System.Linq;

namespace SpeedTest.ViewModels
{
    public class ResultViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SaveButtonCommand { get; }

        private GeoIPmodel _geoIPBindingModel;
        private INavigation Navigation;
        private Position _myPosition;
        private string _ssid = String.Empty;
        private string _bssid = String.Empty;
        private SpeedModel _currentSpeedModel;
        private string _roomNumber = Constants.RoomNumber;
        private string _userName = Settings.UserName;
        private string _clientIPAddress = string.Empty;
        private string _strength = String.Empty;
        public double _distance;
        public double _channel;
        private string _hotelName = Constants.HotelName;
        private bool _isResultVisible = false;
        private bool _isIndicatorVisible = true;
        private bool _isVisibleButtons = false;
        private bool _isRunningIndicator = true;


        public SpeedModel CurrentSpeedModel
        {
            get
            {
                return _currentSpeedModel;
            }
            set
            {
                _currentSpeedModel = value;
                OnPropertyChanged("CurrentSpeedModel");
            }
        }

        public Position MyPosition
        {
            get
            {
                return _myPosition;
            }
            set
            {
                _myPosition = value;
                OnPropertyChanged("MyPosition");
            }
        }

        public string HotelName
        {
            get
            {
                return _hotelName;
            }

            set
            {
                _hotelName = value;
                OnPropertyChanged("HotelName");
            }
        }

        public string SSID
        {
            get
            {
                return _ssid;
            }
            set
            {
                _ssid = value;
                OnPropertyChanged("SSID");
            }
        }

        public string BSSID
        {
            get
            {
                return _bssid;
            }
            set
            {
                _bssid = value;
                OnPropertyChanged("BSSID");
            }
        }

        public string Strength
        {
            get
            {
                return _strength;
            }
            set
            {
                _strength = value;
                OnPropertyChanged("Strength");
            }
        }

        public double Distance
        {
            get
            {
                return _distance;
            }
            set
            {
                _distance = value;
                OnPropertyChanged("Distance");
            }
        }

        public double Channel
        {
            get
            {
                return _channel;
            }
            set
            {
                _channel = value;
                OnPropertyChanged("Channel");
            }
        }

        public string RoomNumber
        {
            get
            {
                return _roomNumber;
            }
            set
            {
                _roomNumber = value;
                OnPropertyChanged("RoomNumber");
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }

        public string ClientIPAddress
        {
            get
            {
                return _clientIPAddress;
            }
            set
            {
                _clientIPAddress = value;
                OnPropertyChanged("ClientIPAddress");
            }
        }

        public bool IsResultVisible
        {
            get
            {
                return _isResultVisible;
            }
            set
            {
                _isResultVisible = value;
                OnPropertyChanged("IsResultVisible");
            }
        }

        public bool IsVisibleButtons
        {
            get
            {
                return _isVisibleButtons;
            }
            set
            {
                _isVisibleButtons = value;
                OnPropertyChanged("IsVisibleButtons");
            }
        }

        public bool IsIndicatorVisible
        {
            get
            {
                return _isIndicatorVisible;
            }
            set
            {
                _isIndicatorVisible = value;

                IsVisibleButtons = !value;

                OnPropertyChanged("IsIndicatorVisible");
            }
        }

        public bool IsRunningIndicator
        {
            get
            {
                return _isRunningIndicator;
            }
            set
            {
                _isRunningIndicator = value;
                OnPropertyChanged("IsRunningIndicator");
            }
        }

        

        public ResultViewModel(INavigation navigation)
        {
            this.Navigation = navigation;
            SaveButtonCommand = new Command(async()=> await SaveButtonClicked());

            RetriveGeoIPdata();
            GetCurrentPosition();
            GetWifiSSID();

        }



        private async Task SaveButtonClicked()
        {

            if (IsResultVisible)
            {
                IsResultVisible = false;
                IsRunningIndicator = true;
                IsIndicatorVisible = true;

                var res = await SendToPostResults();

                if (res)
                {
                    await Application.Current.MainPage.DisplayAlert("Success!", "Speed Results have been saved successfully.", "OK");
                    IsRunningIndicator = false;
                    IsIndicatorVisible = false;

                    MessagingCenter.Send<ResultViewModel>(this, "close");
                    Constants.RoomNumber = string.Empty;
                    await Navigation.PopAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Failed!", $"Speed Test Results had been already saved for Room {RoomNumber}", "OK");
                    IsRunningIndicator = false;
                    IsIndicatorVisible = false;
                    IsResultVisible = true;
                }
            }
        }

        private async  Task<bool> SendToPostResults()
        {

            try
            {
                //Strength

                int SigStrength = 0;

                if (Strength.Contains("dBm"))
                {
                    SigStrength = Convert.ToInt32(Strength.Split(' ')[0]);
                }

                //Ping
                double ping = 0;

                if (CurrentSpeedModel.PingSpeed.Contains("ms"))
                {
                    ping = Convert.ToDouble(CurrentSpeedModel.PingSpeed.Split(' ')[0]);
                }
                else if (CurrentSpeedModel.PingSpeed.Contains("S"))
                {
                    ping = Convert.ToDouble(CurrentSpeedModel.PingSpeed.Split(' ')[0]) * 1000;
                }

                //Download

                double down = 0;

                if (CurrentSpeedModel.DownloadSpeed.Contains("Kbps"))
                {
                    down = Convert.ToDouble(CurrentSpeedModel.DownloadSpeed.Split(' ')[0]);
                }
                else if (CurrentSpeedModel.DownloadSpeed.Contains("Mbps"))
                {
                    down = Convert.ToDouble(CurrentSpeedModel.DownloadSpeed.Split(' ')[0]) * 1024;
                }

                //Upload

                double up = 0;

                if (CurrentSpeedModel.UploadSpeed.Contains("Kbps"))
                {
                    up = Convert.ToDouble(CurrentSpeedModel.UploadSpeed.Split(' ')[0]);
                }
                else if (CurrentSpeedModel.UploadSpeed.Contains("Mbps"))
                {
                    up = Convert.ToDouble(CurrentSpeedModel.UploadSpeed.Split(' ')[0]) * 1024;
                }


                ResultModel resultModel = new ResultModel();

                resultModel.Id = 0;
                resultModel.HodelCode = Constants.HotelCode;
                resultModel.RoomNumber = RoomNumber;
                resultModel.Date = DateTime.Now;
                resultModel.Time = DateTime.Now.TimeOfDay;
                resultModel.Ssid = SSID;
                resultModel.Ip = ClientIPAddress;
                resultModel.Longtitude = MyPosition.Longitude.ToString();
                resultModel.Latitude = MyPosition.Latitude.ToString();
                resultModel.Username = UserName;
                resultModel.ApMacAddress = BSSID;
                resultModel.WiFiStrength = SigStrength;
                resultModel.Ping = ping;
                resultModel.Download = down;
                resultModel.Upload = up;
                resultModel.Distance = Distance;
                resultModel.Channel = Channel;
         
                if (resultModel != null)
                {
                    return await ApiPOSTservices.POSTResultData(resultModel);
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void RetriveGeoIPdata()
        {
            IsRunningIndicator = true;
            IsIndicatorVisible = true;
            IsResultVisible = false;

            CurrentSpeedModel = Constants.CurrentSpeedModel;
            RoomNumber = Constants.RoomNumber;

            var responce = await ApiGETservices.GetIP();

            if (!string.IsNullOrEmpty(responce))
            {
                if (!responce.Contains("Error"))
                {
                    ClientIPAddress = JsonConvert.DeserializeObject<string>(responce);
                }
            }


        }

        private async void GetCurrentPosition()
        {

            try
            {
                if (IsLocationAvailable())
                {
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 1;

                    MyPosition = await locator.GetPositionAsync(TimeSpan.FromMilliseconds(5000));

                    Debug.WriteLine("Position Status: {0}", MyPosition.Timestamp);
                    Debug.WriteLine("Position Latitude: {0}", MyPosition.Latitude);
                    Debug.WriteLine("Position Longtitude: {0}", MyPosition.Longitude);

                    IsResultVisible = true;
                    IsRunningIndicator = false;
                    IsIndicatorVisible = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("error" + ex);

                IsResultVisible = true;
                IsRunningIndicator = false;
                IsIndicatorVisible = false;
            }

        }

        private bool IsLocationAvailable()
        {
            return CrossGeolocator.IsSupported && CrossGeolocator.Current.IsGeolocationAvailable;
        }

        private async void GetWifiSSID()
        {

            if (Device.RuntimePlatform == "Android")
            {
                try
                {
                    SSID = DependencyService.Get<IWiFiStat>().GetSSIDname();
                    BSSID = DependencyService.Get<IWiFiStat>().GetBSSID();

                    var RSSI = DependencyService.Get<IWiFiStat>().GetSignalStrength();

                    Strength = RSSI >= (-67) ? RSSI.ToString() + " dBm (Excellent)" :
                               RSSI >= (-70) ? RSSI.ToString() + " dBm (Good)" : RSSI.ToString() + " dBm (Bad)";

                    await Task.Delay(1000);

                    Device.BeginInvokeOnMainThread(() => {
                        GetDistance();
                    });
                        
                }
                catch (Exception)
                {

                }
            }
            else
            {
                SSID = "Not Supported in iOS";
                BSSID = "Not Supported in iOS";
                Strength = "Not Supported in iOS";
            }

        }

        private void GetDistance()
        {
            var RSSI = DependencyService.Get<IWiFiStat>().GetSignalStrength();
            var Frequenzy = DependencyService.Get<IWiFiStat>().GetFrequenzy();

            double exp = (27.55 - (20 * Math.Log10(Frequenzy)) + Math.Abs(RSSI)) / 20.0;
            var distance =  Math.Pow(10.0, exp);

            var channels = Constants.WiFiChannels;
            var channelNumber = 0;
            for (int i = 0; i < channels.Length; i++)
            {
                if(channels[i] <= Frequenzy)
                {
                    channelNumber = i+1;
                }
                else
                {
                    break;
                }
            }

            Channel = channelNumber;
            Distance = Math.Round(distance, 2);

            Debug.WriteLine(Math.Round(distance, 2)+"m"+$" : Channel {channelNumber}");
        }
    }
}
