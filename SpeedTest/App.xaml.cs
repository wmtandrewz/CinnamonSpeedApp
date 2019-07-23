using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SpeedTest.Views;
using SpeedTest.Interfaces;
using SpeedTest.Helpers;
using System.Threading.Tasks;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SpeedTest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            NavigationPage navigationPage = new NavigationPage(new LoginView())
            {
                BarTextColor = Color.White,
                BackgroundColor = Color.White,
                BarBackgroundColor = Color.FromHex("#211261")
            };


            MainPage = navigationPage;
            
        }

        protected override void OnStart()
        {
            // Handle when your app sleeps
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app starts
        }

    }
}
