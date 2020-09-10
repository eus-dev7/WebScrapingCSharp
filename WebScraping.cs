using HtmlAgilityPack;
using Newtonsoft.Json;
using ScrapySharp.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapingCSharp
{
    public class WebScraping
    {
        
        public DataTable GetDataNoaa(string IdAddress = "BC60A6B6",string hours="5")
        {
            DataTable table = new DataTable();
            
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("https://dcs1.noaa.gov/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string Login_Data1 = "addr="+IdAddress+"&hours="+hours;
                HttpContent data = new StringContent(Login_Data1, Encoding.UTF8, "application/x-www-form-urlencoded");
                var postTask = client.PostAsync("__URL__", data);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    string responseBody = result.Content.ReadAsStringAsync().Result;

                    var datas = JsonConvert.DeserializeObject(responseBody);
                    table = (DataTable)JsonConvert.DeserializeObject(responseBody, (typeof(DataTable)));
                }
            }


            return table;
        }
    }
}