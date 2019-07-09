using System;
using System.Collections.Generic;
using SpeedTest.ViewModels;
using Xamarin.Forms;

namespace SpeedTest.Views
{
    public partial class APListView : ContentPage
    {
        APListViewModel aPListViewModel;
        public APListView()
        {
            InitializeComponent();
            aPListViewModel = new APListViewModel();
            BindingContext = aPListViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            aPListViewModel.UpdateResultsCommand.Execute(null);
        }
    }
}
