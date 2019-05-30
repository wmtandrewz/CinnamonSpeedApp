using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SpeedTest.Services
{
    public static class ApiGETservices
    {
        public static async Task<string> GetIPGeoLocationDetails()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://ip-api.com/json");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync("http://ip-api.com/json");
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        return result;
                    }
                }
                catch (Exception)
                {
                    return "Error GeoIP";
                }
            }
        }

        public static async Task<string> GetTestResults(string startDate,string endDate)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://cinnamonspeedtestapi.azurewebsites.net/api/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync($"TestResult/GetTestResults?startDate={startDate}&endDate={endDate}");
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        return result;
                    }
                }
                catch (Exception)
                {
                    return "Error Test Results";
                }
            }
        }

        public static async Task<string> GetRoomHistory(string hotelCode,int roomNumber)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://cinnamonspeedtestapi.azurewebsites.net/api/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync($"TestResult/GetRoomHistory?roomNumber={roomNumber}&hotelCode={hotelCode}");
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        return result;
                    }
                }
                catch (Exception)
                {
                    return "Error Test Results";
                }
            }
        }
    }
}
