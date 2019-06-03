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


        public static int[] WiFiChannels = {2412,2417,2422,2427,2432,2437,2442,2447,2452,2457,2462,2467,2472,2484};
        public static string[] HotelArray =
            {
                "Cinnamon Grand",
                "Cinnamon Lakeside",
                "Cinnamon Red",
                "Cinnamon Bey",
                "Hikka Tranz",
                "Cinnamon Wild",
                "Cinnamon Citadel",
                "Cinnamon Blu",
                "Cinnamon Lodge",
                "Habarana Village by Cinnamon",
                "Cinnamon Lodge",
                "Cinnamon Dhonveli",
                "Ellaidhoo Maldives by Cinnamon"

            };

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

        public static List<HotelModel> HotelModelList = new List<HotelModel> {

            new HotelModel
            {
                HotelNumber = "3000",
                HotelCode = "CNG",
                HotelName = "Cinnamon Grand"
            },
            new HotelModel
            {
                HotelNumber = "3005",
                HotelCode = "CNL",
                HotelName = "Cinnamon Lakeside"
            },
            new HotelModel
            {
                HotelNumber = "3015",
                HotelCode = "RED",
                HotelName = "Cinnamon Red"
            },
            new HotelModel
            {
                HotelNumber = "3120",
                HotelCode = "VIL",
                HotelName = "Habarana Village By Cinnamon"
            },
            new HotelModel
            {
                HotelNumber = "3150",
                HotelCode = "WLD",
                HotelName = "Cinnamon Wild"
            },
            new HotelModel
            {
                HotelNumber = "3170",
                HotelCode = "TRA",
                HotelName = "Hikka Tranz By Cinnamon"
            },
            new HotelModel
            {
                HotelNumber = "3165",
                HotelCode = "BLU",
                HotelName = "Trinco Blu By Cinnamon"
            },
            new HotelModel
            {
                HotelNumber = "3160",
                HotelCode = "BEY",
                HotelName = "Cinnamon Bey"
            },
            new HotelModel
            {
                HotelNumber = "3115",
                HotelCode = "LOD",
                HotelName = "Cinnamon Lodge"
            },
            new HotelModel
            {
                HotelNumber = "3110",
                HotelCode = "CIT",
                HotelName = "Cinnamon Citadel"
            },
            new HotelModel
            {
                HotelNumber = "3300",
                HotelCode = "ELL",
                HotelName = "Ellaidhoo Maldives By Cinnamon"
            },
            new HotelModel
            {
                HotelNumber = "3310",
                HotelCode = "DHO",
                HotelName = "Cinnamon Dhonveli"
            },
            new HotelModel
            {
                HotelNumber = "3305",
                HotelCode = "HAK",
                HotelName = "Cinnamon Hakuraa Huraa"
            },
            new HotelModel
            {
                HotelNumber = "3100",
                HotelCode = "BBH",
                HotelName = "Bentota Beach By Cinnamon"
            }
        };

        public static string HotelName = "Cinnamon Bey";
        public static string HotelCode = "BEY";

        public static Dictionary<string, string> HotelDictionary = new Dictionary<string, string>();

        static Constants()
        {
            HotelDictionary.Add("Cinnamon Grand", "CNG");
            HotelDictionary.Add("Cinnamon Lakeside", "CNL");
            HotelDictionary.Add("Cinnamon Red", "RED");
            HotelDictionary.Add("Cinnamon Bey", "BEY");
            HotelDictionary.Add("Hikka Tranz", "TRA");
            HotelDictionary.Add("Cinnamon Wild", "WLD");
            HotelDictionary.Add("Cinnamon Citadel", "CIT");
            HotelDictionary.Add("Cinnamon Lodge", "LOD");
            HotelDictionary.Add("Cinnamon Blu", "BLU");
            HotelDictionary.Add("Habarana Village by Cinnamon", "VIL");
            HotelDictionary.Add("Cinnamon Dhonveli", "DHO");
            HotelDictionary.Add("Ellaidhoo Maldives by Cinnamon", "ELL");

            UserName = string.Empty;
        }
    }
}
