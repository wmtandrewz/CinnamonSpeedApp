using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpeedTest.ViewModels;
using Xamarin.Forms;

namespace SpeedTest.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, true);
        }

        protected override void OnAppearing()
        {
            if (BindingContext == null)
            {
                BindingContext = new MainViewModel(webView, Navigation);
            }

            if(!resultButton.IsEnabled)
            {
                resultButton.IsEnabled = true;
            }

            base.OnAppearing();

        }

    }
}
