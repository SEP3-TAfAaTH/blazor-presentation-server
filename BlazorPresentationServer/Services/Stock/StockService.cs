using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorPresentationServer.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlazorPresentationServer.Services
{
    public class StockService : IStockService
    {
        private const string ApiKey = "8b66c2d14f4643e7b19076c0de11b861";
        private const string ApiKey1 = "ced0265304804736b7cd16bb6c5e4332";
        private const string ApiKey2 = "f04e879f0da545829306ede67e813e27";
        private const string ApiKey3 = "fe7fb827aab34438b8ce632f7f6ccacb";
        private const string ApiKey4 = "c523a60be5c54c4f952f933136b8b73f";
        private const string ApiKey5 = "e9fc5f0530944e17b6de5bd26380eea1";

        private readonly List<string> apikeys;


        private readonly HttpClient client;

        private readonly Random generator = new();

        private string Key;

        public StockService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://api.twelvedata.com")
            };
            apikeys = new List<string> {ApiKey, ApiKey1, ApiKey2, ApiKey3, ApiKey4, ApiKey5};
        }
        
        public async Task<Stock> GetStockAsync(string symbol)
        {
            GetAPIKey();
            var response =
                await client.GetAsync($"/quote?symbol={symbol}&interval=1day&apikey={Key}");
            var responseContent = await response.Content.ReadAsStringAsync();
            var jobject = JObject.Parse(responseContent);
            var stock = JsonConvert.DeserializeObject<Stock>(jobject.ToString());
            stock.Price = await GetStockPriceAsync(symbol);
            return stock;
        }

        public async Task<double> GetStockPriceAsync(string symbol)
        {
            GetAPIKey();
            var response =
                await client.GetAsync($"/price?symbol={symbol}&interval=1day&apikey={Key}");
            var responseContent = await response.Content.ReadAsStringAsync();
            var jObject = JObject.Parse(responseContent);
            var price = JsonConvert.DeserializeObject<Stock>(jObject.ToString());
            return price.Price;
        }

        public async Task<Stock[]> GetStockPriceListAsync(string symbol)
        {
            GetAPIKey();
            var response =
                await client.GetAsync($"/time_series?symbol={symbol}&interval=1day&apikey={Key}");
            if (!response.IsSuccessStatusCode)
                throw new Exception(
                    $"{response.StatusCode}, {response.Content.ReadAsStringAsync().Result}"); //returns exception but not custom errormessage
            var responseContent = await response.Content.ReadAsStringAsync();
            var jobject = JObject.Parse(responseContent);
            var list = JsonConvert.DeserializeObject<List<Stock>>(jobject["values"]?.ToString());
            return list.ToArray();
        }

        private void GetAPIKey()
        {
            var num = generator.Next(0, 1000) % 6;
            Key = apikeys[num];
        }
    }
}