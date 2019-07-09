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
        MainViewModel bindingViewModel;
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
                bindingViewModel = new MainViewModel(webView, Navigation);
                BindingContext = bindingViewModel;
            }

            if(!resultButton.IsEnabled)
            {
                resultButton.IsEnabled = true;
            }

            base.OnAppearing();

            bindingViewModel.PageOnAppearingCommand.Execute(null);

        }

    }
}
