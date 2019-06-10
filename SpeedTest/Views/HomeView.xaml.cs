using System;
using SpeedTest.ViewModels;
using Xamarin.Forms;

namespace SpeedTest.Views
{
    public partial class HomeView : CarouselPage
    {
        private HomeViewModel homeViewModel;
        public HomeView()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);
            //NavigationPage.SetHasNavigationBar(this, false);

            homeViewModel = new HomeViewModel(Navigation);
            BindingContext = homeViewModel;

            Device.StartTimer(TimeSpan.FromSeconds(2), () => 
            {
                homeViewModel.UpdateResultsCommand.Execute(null);
                return true;
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            homeViewModel.PageOnLoadCommand.Execute(null);
        }

    }
}
