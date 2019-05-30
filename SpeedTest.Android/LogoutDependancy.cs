using System;
using System.Threading.Tasks;
using Android.Webkit;
using SpeedTest.Droid;
using SpeedTest.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(LogoutDependancy))]
namespace SpeedTest.Droid
{
    public class LogoutDependancy : ILogout
    {
        public async Task<bool> Logout()
        {
            await Task.Run(() =>
            {
                CookieManager.Instance.RemoveAllCookie();

            });
            return true;

        }
    }
}
