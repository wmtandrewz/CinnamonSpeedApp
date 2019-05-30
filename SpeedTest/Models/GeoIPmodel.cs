using System;
using Newtonsoft.Json;

namespace SpeedTest.Models
{
    public class GeoIPmodel
    {

        [JsonProperty("as")]
        public string ASN { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("isp")]
        public string ISP { get; set; }

        [JsonProperty("lat")]
        public string Latitude { get; set; }

        [JsonProperty("lon")]
        public string Longtitude { get; set; }

        [JsonProperty("org")]
        public string Organization { get; set; }

        [JsonProperty("query")]
        public string IP { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("regionName")]
        public string RegionName { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("timezone")]
        public string TimeZone { get; set; }

        [JsonProperty("zip")]
        public string ZipCode { get; set; }
    }
}
