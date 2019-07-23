using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using SpeedTest.Globals;
using SpeedTest.Interfaces;
using SpeedTest.Services;
using SpeedTest.Views;
using Xamarin.Forms;
using SpeedTest.Helpers;

namespace SpeedTest.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Binding Properties
        string _userName = string.Empty;
        string _password = string.Empty;
        string _message;
        bool _isRunningIndicator = false;
        private bool _isLoginBtnEnabled = true;

        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                _userName = value;
                Constants.UserName = value;

                if (string.IsNullOrEmpty(Settings.UserName))
                {
                    Settings.UserName = value;
                }

                OnPropertyChanged("UserName");
            }
        }


        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                _message = value;
                OnPropertyChanged("Message");
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

        public bool IsLoginBtnEnabled
        {
            get
            {
                return _isLoginBtnEnabled;
            }

            set
            {
                _isLoginBtnEnabled = value;
                OnPropertyChanged("IsLoginBtnEnabled");
            }
        }


        private INavigation Navigation;


        #endregion Binding Properties

        public ICommand LoginButtonTappedCommand { get; }

        public LoginViewModel(INavigation navigation)
        {
            this.Navigation = navigation;

            LoginButtonTappedCommand = new Command(LoginEvent);
        }


        private async void LoginEvent()
        {
            try
            {

                Console.WriteLine("Tapped");
                Message = string.Empty;
                IsRunningIndicator = true;
                IsLoginBtnEnabled = false;


                var responce = await DependencyService.Get<ILogin>().LoginUser();

                if (responce != null)
                {
                    
                    //Settings.Password = Password;
                    UserName = responce.Username;

                    if (string.IsNullOrEmpty(Settings.FullName))
                    {
                        Settings.FullName = responce.FullName;
                    }

                    await System.Threading.Tasks.Task.Delay(1000);
                    await Navigation.PopAsync(true);
                    await Navigation.PushAsync(new HomeView());
                    IsRunningIndicator = false;
                    IsLoginBtnEnabled = true;

                    Message = string.Empty;
                }
                else
                {
                    Message = "Username or password is invalid";
                    IsRunningIndicator = false;
                    UserName = string.Empty;
                    Password = string.Empty;
                    Settings.FullName = string.Empty;

                    await System.Threading.Tasks.Task.Delay(1000);
                    IsLoginBtnEnabled = true;
                }
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("User canceled authentication"))
                {
                    Message = "Authentication Failed";
                    IsRunningIndicator = false;
                    UserName = string.Empty;
                    Password = string.Empty;
                    Settings.FullName = string.Empty;

                    await System.Threading.Tasks.Task.Delay(1000);
                    IsLoginBtnEnabled = true;
                }
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
