using System;

using Xamarin.Forms;

namespace SpeedTest.CustomRenders
{
    public class WebViewer : WebView
    {
        public static BindableProperty RefreshCommandProperty =
        BindableProperty.Create(nameof(RefreshCommand), typeof(Action), typeof(WebViewer), null, BindingMode.OneWayToSource);

        public Action RefreshCommand
        {
            get { return (Action)GetValue(RefreshCommandProperty); }
            set { SetValue(RefreshCommandProperty, value); }
        }
    }
}

