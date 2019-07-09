 using System;
using Newtonsoft.Json;

namespace SpeedTest.Models
{
    public class ResultModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("HodelCode")]
        public string HodelCode { get; set; }

        [JsonProperty("RoomNumber")]
        public string RoomNumber { get; set; }

        [JsonProperty("Date")]
        public DateTime Date { get; set; }

        [JsonProperty("Time")]
        public TimeSpan Time { get; set; }

        [JsonProperty("SSID")]
        public string Ssid { get; set; }

        [JsonProperty("IP")]
        public string Ip { get; set; }

        [JsonProperty("Longtitude")]
        public string Longtitude { get; set; }

        [JsonProperty("Latitude")]
        public string Latitude { get; set; }

        [JsonProperty("Username")]
        public string Username { get; set; }

        [JsonProperty("ApMACaddress")]
        public string ApMacAddress { get; set; }

        [JsonProperty("WiFiStrength")]
        public double WiFiStrength { get; set; }

        [JsonProperty("Ping")]
        public double Ping { get; set; }

        [JsonProperty("Download")]
        public double Download { get; set; }

        [JsonProperty("Upload")]
        public double Upload { get; set; }


    }
}

