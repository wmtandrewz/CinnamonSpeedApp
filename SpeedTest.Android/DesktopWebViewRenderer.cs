using System;
using Android.Content;
using Android.Webkit;
using SpeedTest.CustomRenders;
using SpeedTest.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(WebViewer), typeof(DesktopWebViewRenderer))]
namespace SpeedTest.Droid
{
    public class DesktopWebViewRenderer : WebViewRenderer
    {
        public DesktopWebViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);

            //Control.Settings.UserAgentString = "Mozilla/5.0 (X11; U; Linux i686; en-US; rv:1.9.0.4) Gecko/20100101 Firefox/4.0";
            //Control.SetInitialScale(200);

            Control.Settings.JavaScriptEnabled = true;

            //WebSettings websettings = Control.Settings;
            //websettings.UseWideViewPort = true;
            //websettings.LoadWithOverviewMode = true;

            if (Control != null && e.NewElement != null)
            {
                InitializeCommands((WebViewer)e.NewElement);
            }

        }

        private void InitializeCommands(WebViewer element)
        {
            element.RefreshCommand = () =>
            {
                Control?.Reload();
            };

        }

    }

}
