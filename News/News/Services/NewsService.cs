﻿using System;
using System.Net;
using System.Threading.Tasks;
using News.Models;
using Xamarin.Forms;
using static System.Net.WebRequestMethods;

namespace News.Services
{
    public class NewsService
    {
        public async Task<NewsResult> GetNews(NewsScope scope)
        {
            string url = GetUrl(scope);

            var webclient = new WebClient();
            webclient.Headers["User-Agent"] = 
                "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            string ret = string.Empty;

            try
            {
                ret = await webclient.DownloadStringTaskAsync(url);                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<NewsResult>(ret);

            //var json = await webclient.DownloadStringTaskAsync(url);
            //return Newtonsoft.Json.JsonConvert.DeserializeObject<NewsResult>(json);
        }

        private string GetUrl(NewsScope scope)
        {
            return scope switch
            {
                NewsScope.Headlines => Headlines,
                NewsScope.Global => Global,
                NewsScope.Local => Local,
                NewsScope.Weather => Weather,
                _ => throw new Exception("Undefined scope")
            };
        }

        private string Headlines => "https://newsapi.org/v2/top-headlines?" +
                                    "country=us&" +
                                    $"apiKey={Settings.NewsApiKey}";

        private string Local => "https://newsapi.org/v2/everything?q=local&" +
                                $"apiKey={Settings.NewsApiKey}";

        private string Global => "https://newsapi.org/v2/everything?q=global&" +
                                 $"apiKey={Settings.NewsApiKey}";

        private string Weather => "https://api.agromonitoring.com/agro/1.0/weather/history" +
            "https://api.agromonitoring.com/agro/1.0/weather/history?lat=37.75&lon=-122.37&start=1620136623&end=1621864623&appid={091d83f20924f2f62d787e9b2a23c0d1}" + 
                                $"apiKey={Settings.NewsApiKey}";
    }
                                
}