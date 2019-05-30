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
        public DateTime Time { get; set; }

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

        [JsonProperty("ASN")]
        public string Asn { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("CountryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("ISP")]
        public string Isp { get; set; }

        [JsonProperty("Organization")]
        public string Organization { get; set; }

        [JsonProperty("Region")]
        public string Region { get; set; }

        [JsonProperty("RegionName")]
        public string RegionName { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("TimeZone")]
        public string TimeZone { get; set; }

        [JsonProperty("ZipCode")]
        public string ZipCode { get; set; }
    }
}

