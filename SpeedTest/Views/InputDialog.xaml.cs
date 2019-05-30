using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace SpeedTest.Views
{
    public partial class InputDialog : Rg.Plugins.Popup.Pages.PopupPage
    {
        public InputDialog()
        {
            InitializeComponent();
        }

        async void OKClicked(object sender, EventArgs e)
        {
            var text = popupEntry.Text;
            await Navigation.PopPopupAsync(true);
            MessagingCenter.Send<InputDialog, string>(this, "inputData", text);

        }

        void ClearClicked(object sender, EventArgs e)
        {
            popupEntry.Text = "";
            popupEntry.Focus();
        }

        async void CancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync(true);
            MessagingCenter.Send<InputDialog>(this, "cancel");
        }

        protected override bool OnBackgroundClicked()
        {
            return false;
        }

        protected override void OnAppearing()
        {
            popupEntry.Focus();
            base.OnAppearing();
        }
    }
}
