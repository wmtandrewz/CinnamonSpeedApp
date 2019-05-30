using System;
using Android.App;
using Android.Content;
using Android.Net.Wifi;
using SpeedTest.Droid;
using SpeedTest.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(WiFiDetails))]
namespace SpeedTest.Droid
{
    public class WiFiDetails : IWiFiStat
    {
        public string GetBSSID()
        {
            WifiManager wifiManager = (WifiManager)(Application.Context.GetSystemService(Context.WifiService));

            return wifiManager != null ? wifiManager.ConnectionInfo.BSSID : "NULL";
        }

        public int GetSignalStrength()
        {
            WifiManager wifiManager = (WifiManager)(Application.Context.GetSystemService(Context.WifiService));
            var RSSI = wifiManager.ConnectionInfo.Rssi;
            return wifiManager != null ? RSSI : 0 ;
            //return wifiManager != null ? WifiManager.CalculateSignalLevel(RSSI, 5).ToString() : "NULL";

        }

        public string GetSSIDname()
        {
            WifiManager wifiManager = (WifiManager)(Application.Context.GetSystemService(Context.WifiService));

            return wifiManager != null ? wifiManager.ConnectionInfo.SSID : "NULL";
        }
    }
}
    
