using System;
using Foundation;
using SpeedTest.Interfaces;
using SpeedTest.iOS;
using SystemConfiguration;

[assembly: Xamarin.Forms.Dependency(typeof(WiFiDetails))]
namespace SpeedTest.iOS
{
    public class WiFiDetails : IWiFiStat
    {
        public WiFiDetails()
        {
        }

        public string GetBSSID()
        {
            var status = CaptiveNetwork.TryCopyCurrentNetworkInfo("en0", out NSDictionary dict);
            return status == StatusCode.NoKey ? "NULL" : dict[CaptiveNetwork.NetworkInfoKeyBSSID].ToString();
        }

        public int GetSignalStrength()
        {
            return 0;
        }

        public string GetSSIDname()
        {
            var status = CaptiveNetwork.TryCopyCurrentNetworkInfo("en0", out NSDictionary dict);
            return status == StatusCode.NoKey ? "NULL" : dict[CaptiveNetwork.NetworkInfoKeySSID].ToString();
        }
    }
}
