using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Net.Wifi;
using SpeedTest.Droid;
using SpeedTest.Interfaces;
using Xamarin.Forms;
using Application = Android.App.Application;

[assembly: Xamarin.Forms.Dependency(typeof(WiFiDetails))]
namespace SpeedTest.Droid
{
    public class WiFiDetails : IWiFiStat
    {
        WifiManager wifiManager = (WifiManager)(Application.Context.GetSystemService(Context.WifiService));

        public string GetBSSID()
        {
            return wifiManager != null ? wifiManager.ConnectionInfo.BSSID : "NULL";
        }

        public int GetSignalStrength()
        {
            var RSSI = wifiManager.ConnectionInfo.Rssi;
            return wifiManager != null ? RSSI : 0 ;
            //return wifiManager != null ? WifiManager.CalculateSignalLevel(RSSI, 5).ToString() : "NULL";

        }

        public string GetSSIDname()
        {
            return wifiManager != null ? wifiManager.ConnectionInfo.SSID : "NULL";
        }

        public int GetFrequenzy()
        {
            return wifiManager != null ? wifiManager.ConnectionInfo.Frequency : 0;
        }

        public List<ScanResult> GetAvailableSSIDList()
        {

            wifiManager = (WifiManager)(Application.Context.GetSystemService(Context.WifiService));
            wifiManager.StartScan();
            var Distance = "~ " + Math.Round(GetDistance(GetFrequenzy(), GetSignalStrength()), 2).ToString() + " m";
            Debug.WriteLine(GetFrequenzy()+ " < : > " + GetSignalStrength() + "< : >" + Distance);
            return wifiManager.ScanResults.ToList();
        }

        private double GetDistance(int freq, int rssi)
        {
            double exp = (27.55 - (20 * Math.Log10(freq)) + Math.Abs(rssi)) / 20.0;
            return Math.Pow(10.0, exp);
        }

    }
}

