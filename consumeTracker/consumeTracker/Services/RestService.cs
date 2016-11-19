using consumeTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Json;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace consumeTracker.Services
{
    public class RestService
    {
        private HttpClient client;
        private bool clientInitialized;

        private string REST_URL = "";

        public RestService()
        {
            clientInitialized = false;
        }

        public void InitializeLogin(string username, string password)
        {
            if (!clientInitialized)
            {
                var authData = string.Format("{0}:{1}", username, password);
                var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

                client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
                clientInitialized = true;
            }
        }
             

        //public async Task<bool> Login(string username, string password)
        //{
        //    var authData = string.Format("{0}:{1}", username, password);
        //    var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

        //    var uri = new Uri(string.Format(REST_URL, string.Empty));
        //    var response = await client.GetAsync(uri);   
        //    return response.IsSuccessStatusCode;
        //}

        public async Task<List<ConsumeItem>> GetData()
        {
            List<ConsumeItem> consumeList = new List<ConsumeItem>();

            var uri = new Uri(string.Format(REST_URL, string.Empty));

            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                consumeList = JsonConvert.DeserializeObject<ServiceResponse<List<ConsumeItem>>>(content).Data;
            }
            return consumeList;
        }

        public async Task<bool> CreateItem(ConsumeItem item)
        {
            var uri = new Uri(string.Format(REST_URL, item.Id));

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Item created");
            }
            return response.IsSuccessStatusCode;
        }


        //private async Task<JsonValue> GetDataJson(string url)
        //{

        //    //var uri = new Uri(string.Format(url, string.Empty));

        //    //var response = await client.GetAsync(uri);
        //    //if (response.IsSuccessStatusCode)
        //    //{
        //    //   var content = await response.Content.ReadAsStringAsync();
        //    //    //consumeList = JsonConvert.DeserializeObject<List<ConsumeItem>>(content);
        //    //}

        //    //// Create an HTTP web request using the URL:
        //    //string credentials = "username:password";
        //    //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
        //    //request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));
        //    //request.PreAuthenticate = true;
        //    //request.ContentType = "application/json";
        //    //request.Method = "GET";

        //    //// Send the request to the server and wait for the response:
        //    //using (WebResponse response = await request.GetResponseAsync())
        //    //{
        //    //    // Get a stream representation of the HTTP web response:
        //    //    using (Stream stream = response.GetResponseStream())
        //    //    {
        //    //        // Use this stream to build a JSON document object:
        //    //        JsonValue jsonDoc = await Task.Run(() => JsonValue.Load(stream));
        //    //        Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

        //    //        // Return the JSON document:
        //    //        return jsonDoc;
        //    //    }
        //    //}
        //}




    }
}
