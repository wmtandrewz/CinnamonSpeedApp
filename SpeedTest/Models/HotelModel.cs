using System;
using Newtonsoft.Json;

namespace SpeedTest.Models
{
    public class HotelModel
    {

        [JsonProperty("HtlCode")]
        public string HotelCode { get; set; }

        [JsonProperty("TmsId")]
        public int HotelNumber { get; set; }

        [JsonProperty("Sector")]
        public string Sector { get; set; }

        [JsonProperty("HtlName")]
        public string HotelName { get; set; }

        [JsonProperty("HtlAddr")]
        public string HtlAddr { get; set; }

        [JsonProperty("Tel")]
        public string Tel { get; set; }

        [JsonProperty("Fax")]
        public string Fax { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("URL")]
        public string Url { get; set; }

        [JsonProperty("Company")]
        public string Company { get; set; }

        [JsonProperty("HtlType")]
        public string HtlType { get; set; }

        [JsonProperty("Active")]
        public bool Active { get; set; }

        [JsonProperty("LogoName")]
        public string LogoName { get; set; }

        [JsonProperty("HtlOperCont")]
        public object HtlOperCont { get; set; }

        [JsonProperty("HtlOperEmail")]
        public object HtlOperEmail { get; set; }

        [JsonProperty("HtlOperExtn")]
        public object HtlOperExtn { get; set; }

        [JsonProperty("SeqNo")]
        public long SeqNo { get; set; }

        [JsonProperty("CreditDeptNo")]
        public string CreditDeptNo { get; set; }

        [JsonProperty("ReservationDeptNo")]
        public string ReservationDeptNo { get; set; }

        [JsonProperty("BanquetDeptNo")]
        public string BanquetDeptNo { get; set; }

        [JsonProperty("WorkingHrsWeekDays")]
        public string WorkingHrsWeekDays { get; set; }

        [JsonProperty("WorkingHrsWeekends")]
        public string WorkingHrsWeekends { get; set; }

        [JsonProperty("CODateAddDays")]
        public long CoDateAddDays { get; set; }

        [JsonProperty("GuestFBEmailAddDays")]
        public long GuestFbEmailAddDays { get; set; }

        [JsonProperty("ResConfAddDays")]
        public long ResConfAddDays { get; set; }

        [JsonProperty("CategoryId")]
        public long CategoryId { get; set; }

        [JsonProperty("GMname")]
        public string GMname { get; set; }

        [JsonProperty("RevinateId")]
        public long RevinateId { get; set; }

        [JsonProperty("SpeedResults")]
        public object[] SpeedResults { get; set; }

        [JsonProperty("UserAccesses")]
        public object[] UserAccesses { get; set; }
    }
}
