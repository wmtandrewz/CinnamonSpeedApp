using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpeedTest.Models;

namespace SpeedTest.Services
{
    public static class ApiPOSTservices
    {
        public static async Task<bool> LDAPAuthenticateUser(string userName, string password)
        {
            try
            {

                string userJson = "{\"UserName\":\"" + "KEELLS\\\\" + userName + "\"," +
                                  "\"Password\":\"" + password + "\"," +
                                  "\"ADGroup\":\"" + "BI_APP_Discount" + "\"" +
                                  "}";

                using (var client = new WebClient())
                {

                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string responce = await client.UploadStringTaskAsync(new Uri("http://chml.keells.lk/CinnamonDirectoryServices/api/Directory/AuthGroup"), "POST", userJson);
                    Console.WriteLine(responce);

                    if (responce.Contains("BI_APP_Discount"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static async Task<bool> POSTResultData(ResultModel resultModel)
        {
            try
            {

                string resultJson = JsonConvert.SerializeObject(resultModel);

                using (var client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //string responce = await client.UploadStringTaskAsync(new Uri("http://chml.keells.lk/speedtest/api/TestResult/Insert"), "POST", resultJson);
                    var responce = await client.PostAsync("https://cinnamonspeedtestapi.azurewebsites.net/api/TestResult/Insert", new StringContent(resultJson, Encoding.UTF8, "application/json"));

                    if (responce.IsSuccessStatusCode)
                    {
                        var res =  await responce.Content.ReadAsStringAsync();
                        if (res.Contains("Speed Data saved"))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
