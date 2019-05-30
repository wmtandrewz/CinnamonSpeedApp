using System;
using System.Collections.Generic;
using SpeedTest.ViewModels;
using Xamarin.Forms;

namespace SpeedTest.Views
{
    public partial class RoomNumberView : ContentPage
    {
        public RoomNumberView()
        {
            InitializeComponent();

            publicAreaSwitch.Toggled += delegate
            {
                if (publicAreaSwitch.IsToggled)
                {
                    titleLabel.Text = "Enter the public area name";
                    roomNumberText.WidthRequest = 300;
                    roomNumberText.Placeholder = "Ex : Main Restaurant";
                    roomNumberText.Keyboard = Keyboard.Default;
                    roomNumberText.Focus();
                }
                else
                {
                    titleLabel.Text = "Enter the Room Number";
                    roomNumberText.WidthRequest = 200;
                    roomNumberText.Placeholder = "Ex : 001";
                    roomNumberText.Keyboard = Keyboard.Numeric;
                    roomNumberText.Focus();
                }
            };


        }

        protected override void OnAppearing()
        {
            if (BindingContext == null)
            {
                BindingContext = new RoomNumberViewModel(Navigation);
            }

            if(string.IsNullOrEmpty(roomNumberText.Text))
            {
                roomNumberText.Focus();
            }

            base.OnAppearing();

        }
    }
}
