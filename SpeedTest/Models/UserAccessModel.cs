using System;
using Newtonsoft.Json;

namespace SpeedTest.Models
{
    public class UserAccessModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("User_userID")]
        public string UserID { get; set; }

        [JsonProperty("Hotel_htlCode")]
        public string HotelCode { get; set; }

        [JsonProperty("AccessLevels_level")]
        public int AccessLevel { get; set; }

        [JsonProperty("GrantDate")]
        public DateTime GrantDate { get; set; }

        [JsonProperty("Target")]
        public int Target { get; set; }

        [JsonProperty("IsActive")]
        public bool IsActive { get; set; }

        [JsonProperty("AccessLevel")]
        public object AccLevel { get; set; }

        [JsonProperty("Hotel")]
        public object Hotel { get; set; }

        [JsonProperty("User")]
        public object User { get; set; }
    
    }
}
