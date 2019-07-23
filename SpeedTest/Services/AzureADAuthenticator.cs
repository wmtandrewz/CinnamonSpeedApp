using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using SpeedTest.Interfaces;
using SpeedTest.Models;
using Xamarin.Forms;

namespace SpeedTest.Services
{
    public static class AzureADAuthenticator
    {
        public static string clientId = "8a0624bd-2039-4d52-afe9-e99d3b1dd115";
        public static string commonAuthority = "https://login.microsoftonline.com/common/";
        public static Uri returnUri = new Uri("https://keells.onmicrosoft.com/CHML-MobileADAuthenticator");
        const string graphResourceUri = "https://graph.microsoft.com";

        private static AuthenticationContext authContext;

        public static async Task<UserModel> Login( IPlatformParameters parent)
        {
            AuthenticationResult authResult = null;
            authContext = new AuthenticationContext(commonAuthority);

            try
            {
      
                if (authContext.TokenCache.ReadItems().Any())
                {
                    authContext = new AuthenticationContext(authContext.TokenCache.ReadItems().First().Authority);
                    
                }
                else
                {
                    authResult = await authContext.AcquireTokenSilentAsync(graphResourceUri, clientId);
                }

            }
            catch (AdalSilentTokenAcquisitionException)
            {
                authResult = await authContext.AcquireTokenAsync(graphResourceUri, clientId, returnUri, parent);
            }
            catch (Exception)
            {

            }

            return new UserModel
            {
                Username = authResult?.UserInfo.DisplayableId.Split('@')[0],
                FullName = authResult?.UserInfo.GivenName
            };
        }

        public static async Task<bool> LogoutUser()
        {
            authContext.TokenCache.Clear();
            return await DependencyService.Get<ILogout>().Logout();
        }
    }
}

