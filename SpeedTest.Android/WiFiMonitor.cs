using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Android.Content;
using Android.Net.Wifi;

namespace SpeedTest.Droid
{
    public class WiFiMonitor : BroadcastReceiver
    {
        
        public override async void OnReceive(Context context, Intent intent)
        {
            var mainActivity = (MainActivity)context;
            var wifiManager = (WifiManager)mainActivity.GetSystemService(Context.WifiService);
            var message = string.Join("\r\n", wifiManager.ScanResults.Select(r => $"{r.Bssid} - {r.Level} dB"));
            await Task.Delay(TimeSpan.FromSeconds(1));
            
            Debug.WriteLine(message);
            Debug.WriteLine("*******************");
        }
    }
}
