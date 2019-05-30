using System;
using System.Collections.Generic;
using SpeedTest.ViewModels;
using Xamarin.Forms;

namespace SpeedTest.Views
{
    public partial class ResultView : ContentPage
    {
        public ResultView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if(BindingContext == null)
            {
                BindingContext = new ResultViewModel(Navigation);
            }

            var viewModel = BindingContext as ResultViewModel;
            //viewModel.RequestRoomNumberCommand.Execute(null);

            base.OnAppearing();
        }
    }
}
