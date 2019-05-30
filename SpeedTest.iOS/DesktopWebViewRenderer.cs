using System;
using SpeedTest.CustomRenders;
using SpeedTest.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(WebViewer), typeof(DesktopWebViewRenderer))]
namespace SpeedTest.iOS
{
    public class DesktopWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            this.ScalesPageToFit = true;
            this.ScrollView.ScrollEnabled = false;

            if (this != null && e.NewElement != null)
            {
                InitializeCommands((WebViewer)e.NewElement);
            }
        }

        private void InitializeCommands(WebViewer element)
        {
            element.RefreshCommand = () =>
            {
                this?.Reload();
            };

        }
    }
}
