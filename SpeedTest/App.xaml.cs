using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SpeedTest.Views;
using SpeedTest.Interfaces;
using SpeedTest.Helpers;
using System.Threading.Tasks;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using SpeedTest.Services;
using System.Linq;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SpeedTest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;

            NavigationPage navigationPage = new NavigationPage(new LoginView())
            {
                BarTextColor = Color.White,
                BackgroundColor = Color.White,
                BarBackgroundColor = Color.FromHex("#211261"),
            };

            MainPage = navigationPage;
            //MainPage = new MasterBasePage() ;

        }

        private async void Current_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {

            var wifi = ConnectionType.WiFi;
            var connectionTypes = CrossConnectivity.Current.ConnectionTypes;
            if (!connectionTypes.Contains(wifi))
            {
                PopToLoginPage();
            }

            if (!e.IsConnected)
            {
                PopToLoginPage();
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app starts
        }

        private async void PopToLoginPage()
        {
            await Application.Current.MainPage.DisplayAlert("Error Connection", "Please check your Wi-Fi connection", "OK");

            NavigationPage navigationPage = new NavigationPage(new LoginView())
            {
                BarTextColor = Color.White,
                BackgroundColor = Color.White,
                BarBackgroundColor = Color.FromHex("#211261")
            };

            MainPage = navigationPage;
        }

    }
}
