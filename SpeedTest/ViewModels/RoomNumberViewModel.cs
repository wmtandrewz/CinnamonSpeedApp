using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using SpeedTest.Globals;
using SpeedTest.Views;
using Xamarin.Forms;

namespace SpeedTest.ViewModels
{
    public class RoomNumberViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ContinueButtonCommand { get; }

        private INavigation Navigation;
        private bool IsPressed = true;

        private string _roomNumber = Constants.RoomNumber;
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

        public RoomNumberViewModel(INavigation navigation)
        {
            this.Navigation = navigation;

            ContinueButtonCommand = new Command(async () => await ContinueButtonClicked());

            MessagingCenter.Subscribe<ResultViewModel>(this, "close", async (obj) =>
              {
                  MessagingCenter.Unsubscribe<ResultViewModel>(this, "cancel");
                  await Navigation.PopAsync();
              });
        }

        private async Task ContinueButtonClicked()
        {
            if (!string.IsNullOrEmpty(RoomNumber))
            {
                if (IsPressed)
                {
                    IsPressed = false;
                    Constants.RoomNumber = RoomNumber;
                    await Navigation.PushAsync(new ResultView());
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Empty Field!", "Please enter the room number", "OK");
            }
            IsPressed = true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
