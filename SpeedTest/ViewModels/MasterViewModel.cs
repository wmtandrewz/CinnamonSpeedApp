using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using SpeedTest.Globals;
using SpeedTest.Helpers;
using SpeedTest.Interfaces;
using SpeedTest.Services;
using SpeedTest.Views;
using Xamarin.Forms;

namespace SpeedTest.ViewModels
{
    public class MasterViewModel :INotifyPropertyChanged
    {
        private MasterBasePage BaseView { get; set; }
        public ICommand SignOutCommand { get; }
        public ICommand APListCommand { get; }

        public string UserName
        {
            get
            {
                return Settings.UserName;
            }
        }

        public string CurentDate
        {
            get
            {
                return DateTime.Now.ToString("yyyy MMM dd");
            }
        }


        public MasterViewModel(MasterBasePage basePage)
        {

            this.BaseView = basePage;

            SignOutCommand = new Command(SignOutButtonPressed);
            APListCommand = new Command(LoadAPList);

        }

        private async void LoadAPList()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                BaseView.IsPresented = false;
            });

            await Task.Delay(500);
            
            await Constants.MasterDetailNavigation.PushAsync(new APListView());
        }

        private async void SignOutButtonPressed()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                BaseView.IsPresented = false;
            });

            await AzureADAuthenticator.LogoutUser();
            Settings.UserName = string.Empty;
            Settings.FullName = string.Empty;
            //await Navigation.PopToRootAsync();

            NavigationPage navigationPage = new NavigationPage(new LoginView())
            {
                BarTextColor = Color.White,
                BackgroundColor = Color.White,
                BarBackgroundColor = Color.FromHex("#211261")
            };

            Application.Current.MainPage = navigationPage;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
