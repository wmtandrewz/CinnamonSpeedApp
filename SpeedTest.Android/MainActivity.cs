
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace SpeedTest.Droid
{
    [Activity(Label = "SpeedTest", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static Activity GetActivity { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this,savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            //AnimationViewRenderer.Init();

            GetActivity = this;

            LoadApplication(new App());

        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AuthenticationAgentContinuationHelper.SetAuthenticationAgentContinuationEventArgs(requestCode, resultCode, data);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

}