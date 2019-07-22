using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpeedTest.Models;

namespace SpeedTest.Services
{
    public static class ApiGETservices
    {
        public static async Task<string> GetIP()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://cinnamonspeedtestapi.azurewebsites.net/api/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync($"TestResult/GetIP");
                    using (HttpContent content = response.Content)
                    {
                        string result = await content.ReadAsStringAsync();
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    return "Error" + ex.Message;
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
                catch (Exception ex)
                {
                    return "Error" + ex.Message;
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
                catch (Exception ex)
                {
                    return "Error" + ex.Message;
                }
            }
        }

        public static async Task<List<HotelModel>> GetAuthHotels(string username)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://cinnamonspeedtestapi.azurewebsites.net/api/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = await client.GetAsync($"Access/GetAuthorizedHotels?username={username}");
                    using (HttpContent content = response.Content)
                    {
                        var result = await content.ReadAsStringAsync();

                        var obj = JsonConvert.DeserializeObject<List<HotelModel>>(result);

                        return obj;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
