using System;
using System.Collections.Generic;
using SpeedTest.ViewModels;
using Xamarin.Forms;

namespace SpeedTest.Views
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(Navigation);
        }

        protected override void OnAppearing()
        {
            //loginButton.IsVisible = true;

            base.OnAppearing();
        }
    }
}
