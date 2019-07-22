using System;
using System.Collections.Generic;
using SpeedTest.Models;

namespace SpeedTest.Globals
{
    public static class Constants
    {
        public static SpeedModel CurrentSpeedModel { get; set; }
        public static string RoomNumber { get; set; }
        public static string UserName { get; set; }

        public static List<HotelModel> HotelList { get; set; }

        public static int[] WiFiChannels = {2412,2417,2422,2427,2432,2437,2442,2447,2452,2457,2462,2467,2472,2484};

        public static string[] ColorList =
        {
            "#DC143C",//Crimson
            "#008000",//Green
            "#008B8B",//DarkCyan
            "#1E90FF",//dodgerblue
            "#8A2BE2",//blueviolet
            "#FF8C00",//darkorange
            "#DC143C",//Crimson
            "#008000",//Green
            "#008B8B",//DarkCyan
            "#1E90FF",//dodgerblue
            "#8A2BE2",//blueviolet
            "#FF8C00",//darkorange
            "#DC143C",//Crimson
            "#008000",//Green
            "#008B8B",//DarkCyan
            "#1E90FF",//dodgerblue
            "#8A2BE2",//blueviolet
            "#FF8C00",//darkorange
            "#DC143C",//Crimson
            "#008000",//Green
            "#008B8B",//DarkCyan
            "#1E90FF",//dodgerblue
            "#8A2BE2",//blueviolet
            "#FF8C00",//darkorange
            "#DC143C",//Crimson
            "#008000",//Green
            "#008B8B",//DarkCyan
            "#1E90FF",//dodgerblue
            "#8A2BE2",//blueviolet
            "#FF8C00"//darkorange
        };

        public static string HotelName = "Cinnamon Bey";
        public static string HotelCode = "BEY";

        static Constants()
        {
            UserName = string.Empty;
        }
    }
}
