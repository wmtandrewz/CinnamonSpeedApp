using System;
using System.Collections.Generic;

namespace SpeedTest.Interfaces
{
    public interface IWiFiStat
    {
        string GetSSIDname();
        int GetSignalStrength();
        string GetBSSID();
        int GetFrequenzy();
        List<Android.Net.Wifi.ScanResult> GetAvailableSSIDList();
    }
}
