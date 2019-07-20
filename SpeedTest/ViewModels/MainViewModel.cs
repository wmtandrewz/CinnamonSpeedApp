using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using SpeedTest.Globals;
using SpeedTest.Models;
using SpeedTest.Services;
using SpeedTest.Views;
using Xamarin.Forms;
using Plugin.Permissions.Abstractions;
using SpeedTest.Utils;
using System.Threading;
using System.Collections.ObjectModel;
using SpeedTest.CustomRenders;
using SpeedTest.Helpers;

namespace SpeedTest.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _pingSpeed = string.Empty;
        private string _downloadSpeed = string.Empty;
        private string _uploadSpeed = string.Empty;
        private bool _isLoading = true;
        private bool _indicatorVisible = true;
        private bool _isVisibleButtons = true;
        private string _roomNumber = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand WebNavigatingCommand { get; }
        public ICommand WebNavigatedCommand { get; }
        public ICommand ChangeHotelCommand { get; }
        public ICommand ResultButtonCommand { get; }
        public ICommand RefreshButtonCommand { get; }
        public ICommand LogoutClickedCommand { get; }
        public ICommand APClickedCommand { get; }
        public ICommand StartButtonCommand { get; }
        public ICommand PageOnAppearingCommand { get; }


        private WebViewer SpeedTestClientView { get; set; }

        private INavigation Navigation;
        private string _hotelName = string.IsNullOrEmpty(Settings.DefaultHotel) ? Constants.HotelName : Settings.DefaultHotel;
        private bool _isEnabledButtons = true;
        private bool _isAnimationVisible;
        private bool _isRefreshVisible = false;
        private string _startButtonText = "Start";
        private string _startButtonColor = "#086100";
        private bool _isVisibleStart = false;
        private bool RepeatTimer = true;

        public string HotelName
        {
            get
            {
                return _hotelName;
            }

            set
            {
                _hotelName = value;
                Constants.HotelName = value;
                Settings.DefaultHotel = value;
                OnPropertyChanged("HotelName");
            }
        }

        public string PingSpeed
        {
            get
            {
                return _pingSpeed;
            }

            set
            {
                _pingSpeed = value;
                OnPropertyChanged("PingSpeed");
            }
        }

        public string DownloadSpeed
        {
            get
            {
                return _downloadSpeed;
            }

            set
            {
                _downloadSpeed = value;
                OnPropertyChanged("DownloadSpeed");
            }
        }

        public string UploadSpeed
        {
            get
            {
                return _uploadSpeed;
            }

            set
            {
                _uploadSpeed = value;
                OnPropertyChanged("UploadSpeed");
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
                Constants.RoomNumber = value;
                OnPropertyChanged("RoomNumber");
            }
        }

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }

            set
            {
                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        public bool IndicatorVisible
        {
            get
            {
                return _indicatorVisible;
            }

            set
            {
                _indicatorVisible = value;
                IsAnimationVisible = !value;
                OnPropertyChanged("IndicatorVisible");
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
                IsRefreshVisible = value;
                IsAnimationVisible = !value;

                OnPropertyChanged("IsVisibleButtons");
            }
        }

        public bool IsVisibleStart
        {
            get
            {
                return _isVisibleStart;
            }

            set
            {
                _isVisibleStart = value;

                OnPropertyChanged("IsVisibleStart");
            }
        }

        public bool IsEnabledButtons
        {
            get
            {
                return _isEnabledButtons;
            }

            set
            {
                _isEnabledButtons = value;
                OnPropertyChanged("IsEnabledButtons");
            }
        }

        public bool IsAnimationVisible
        {
            get
            {
                return _isAnimationVisible;
            }

            set
            {
                _isAnimationVisible = value;
                OnPropertyChanged("IsAnimationVisible");
            }
        }

        public bool IsRefreshVisible
        {
            get
            {
                return _isRefreshVisible;
            }

            set
            {
                _isRefreshVisible = value;
                OnPropertyChanged("IsRefreshVisible");
            }
        }

        public string StartButtonText
        {
            get
            {
                return _startButtonText;
            }

            set
            {
                _startButtonText = value;
                OnPropertyChanged("StartButtonText");
            }
        }

        public string StartButtonColor
        {
            get
            {
                return _startButtonColor;
            }

            set
            {
                _startButtonColor = value;
                OnPropertyChanged("StartButtonColor");
            }
        }

        public MainViewModel(WebViewer TestClient, INavigation navigation)
        {
            this.SpeedTestClientView = TestClient;
            this.Navigation = navigation;

            IsEnabledButtons = true;

            WebNavigatingCommand = new Command(WebPageNavigating);
            WebNavigatedCommand = new Command(WebPageNavigated);
            ChangeHotelCommand = new Command(HotelNameButtonClicked);
            ResultButtonCommand = new Command(ShowResultsClicked);
            RefreshButtonCommand = new Command(RefreshWebView);
            LogoutClickedCommand = new Command(LogoutUser);
            APClickedCommand = new Command(NavigateToApList);
            StartButtonCommand = new Command(startTest);
            PageOnAppearingCommand = new Command(OnPageApearing);

            PingSpeed = "0 ms";
            DownloadSpeed = "0 Mbps";
            UploadSpeed = "0 Mbps";


            RequestLocationAccess();
        }

        private async void NavigateToApList()
        {
            await Navigation.PushAsync(new APListView());
        }

        private void OnPageApearing()
        {
            IsVisibleButtons = false;
        }

        private async void startTest()
        {

            await SpeedTestClientView.EvaluateJavaScriptAsync(JavaScriptInvoker.ExecuteClickEventById("startStopBtn"));

            if(StartButtonColor == "#086100" && StartButtonText == "Start")
            {
                StartButtonText = "Abort";
                StartButtonColor = "#70000d";
                RepeatTimer = true;
                RunTimer();
            }
            else
            {
                StartButtonText = "Start";
                StartButtonColor = "#086100";
                RepeatTimer = false;
            }
        }

        private async void CheckForFinish()
        {
            var className = await SpeedTestClientView.EvaluateJavaScriptAsync(JavaScriptInvoker.GetClassById("startStopBtn"));
            var ulValue = await SpeedTestClientView.EvaluateJavaScriptAsync(JavaScriptInvoker.GetElementDataByID("ulText"));

            if(string.IsNullOrEmpty(className) && string.IsNullOrEmpty(ulValue))
            {
                IsVisibleButtons = false;
            }
            else if(className == "running")
            {
                IsVisibleButtons = false;
            }
            else
            {
                IsVisibleButtons = true;
                RepeatTimer = false;
                StartButtonText = "Start";
                StartButtonColor = "#086100";
            }

            Debug.WriteLine(className);
        }

        private async void LogoutUser()
        {
            await AzureADAuthenticator.LogoutUser();
            Settings.UserName = string.Empty;
            await Navigation.PopToRootAsync();
        }

        private void RefreshWebView(object obj)
        {
            SpeedTestClientView.RefreshCommand();
            IsVisibleButtons = false;
            IsLoading = true;
            IndicatorVisible = true;
            SpeedTestClientView.IsVisible = false;
            IsVisibleStart = false;
        }

        

        private async void ShowResultsClicked()
        {
            IsEnabledButtons = false;

            await ExtractDataFromWebView();
            await Navigation.PushAsync(new RoomNumberView());
            //await Navigation.PushAsync(new ResultView());

            SpeedTestClientView.Reload();

        }

        private async void HotelNameButtonClicked()
        {
            string[] hotelArray = Constants.HotelArray;

            var res = await Application.Current.MainPage.DisplayActionSheet("Select your Location", "OK", "Cancel", hotelArray);

            if (res != "Cancel" && res != "OK")
            {
                HotelName = res;
                Constants.HotelCode = Constants.HotelDictionary[HotelName];
            }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task ExtractDataFromWebView()
        {
            try
            {
                var pingSpeed = await SpeedTestClientView.EvaluateJavaScriptAsync(JavaScriptInvoker.GetElementDataByID("pingText")).ConfigureAwait(true);
                var pingUnit = "ms";

                var downLinkSpeed = await SpeedTestClientView.EvaluateJavaScriptAsync(JavaScriptInvoker.GetElementDataByID("dlText")).ConfigureAwait(true);
                var downloadUnit = "Mbps";

                var uplinkSpeed = await SpeedTestClientView.EvaluateJavaScriptAsync(JavaScriptInvoker.GetElementDataByID("ulText")).ConfigureAwait(true);
                var uplinkUnit = "Mbps";


                PingSpeed = pingSpeed + $" {pingUnit}";
                DownloadSpeed = downLinkSpeed + $" {downloadUnit}";
                UploadSpeed = uplinkSpeed + $" {uplinkUnit}";

                Debug.WriteLine($"Ping : {pingSpeed} {pingUnit}");
                Debug.WriteLine($"Download : {downLinkSpeed}{downloadUnit}");
                Debug.WriteLine($"Upload : {uplinkSpeed}{uplinkUnit}");
                Debug.WriteLine($"=================================");

                Constants.CurrentSpeedModel = new SpeedModel()
                {
                    PingSpeed = this.PingSpeed,
                    DownloadSpeed = this.DownloadSpeed,
                    UploadSpeed = this.UploadSpeed
                };


            }
            catch (Exception)
            {
                Console.WriteLine("Extract Exception");
            }
        }

        private async void WebPageNavigated()
        {
            
            IsLoading = false;
            IndicatorVisible = false;
            SpeedTestClientView.IsVisible = true;
            IsVisibleStart = true;
        }

        private void WebPageNavigating()
        {

        }

        private async void RequestLocationAccess()
        {
            var res = await PermissionsHandler.CheckPermissions(Permission.Location);
            Debug.WriteLine("Location Permission : " + res);
        }

        private void RunTimer()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
                CheckForFinish();
                return RepeatTimer; // True = Repeat again, False = Stop the timer
            });
        }

    }
}
