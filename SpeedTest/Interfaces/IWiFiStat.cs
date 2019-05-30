using System;
namespace SpeedTest.Interfaces
{
    public interface IWiFiStat
    {
        string GetSSIDname();
        int GetSignalStrength();
        string GetBSSID();
    }
}
