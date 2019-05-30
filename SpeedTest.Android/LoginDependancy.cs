using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using SpeedTest.Droid;
using SpeedTest.Interfaces;
using SpeedTest.Models;
using SpeedTest.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(LoginDependancy))]
namespace SpeedTest.Droid
{
	public class LoginDependancy : ILogin
    {

        public async Task<UserModel> LoginUser()
        {
            return await AzureADAuthenticator.Login(new PlatformParameters(MainActivity.GetActivity));
        }
    }
}
