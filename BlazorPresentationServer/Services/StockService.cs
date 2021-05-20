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
        private string Key;

        private readonly List<string> apikeys;
        private int counter;
        private int keyIndex;
        
        
        private HttpClient client;

        public StockService(HttpClient client)
        {
            this.client = client;
            apikeys = new List<string> {ApiKey, ApiKey1, ApiKey2, ApiKey3};
        }
        public async Task<JObject> Test()
        {
            GetAPIKey();
            HttpResponseMessage response =
                await client.GetAsync($"/quote?symbol=AAPL&interval=1day&apikey={Key}");
            string responseContent = await response.Content.ReadAsStringAsync();
            var jobject = JObject.Parse(responseContent);
            return jobject;
        }

        public async Task<Stock> GetStockAsync(string symbol)
        {
            GetAPIKey();
            HttpResponseMessage response =
                await client.GetAsync($"/quote?symbol={symbol}&interval=1day&apikey={Key}");
            string responseContent = await response.Content.ReadAsStringAsync();
            var jobject = JObject.Parse(responseContent);
            Stock stock = JsonConvert.DeserializeObject<Stock>(jobject.ToString());
            stock.Price = await GetStockPriceAsync(symbol);
            return stock;
        }

        public async Task<double> GetStockPriceAsync(string symbol)
        {
            GetAPIKey();
            HttpResponseMessage response =
                await client.GetAsync($"/price?symbol={symbol}&interval=1day&apikey={Key}");
            string responseContent = await response.Content.ReadAsStringAsync();
            var jObject = JObject.Parse(responseContent);
            Stock price = JsonConvert.DeserializeObject<Stock>(jObject.ToString());
            return price.Price;
        }

        public async Task<Stock[]> GetStockPriceListAsync(string symbol)
        {
            GetAPIKey();
            HttpResponseMessage message =
                await client.GetAsync($"/time_series?symbol={symbol}&interval=1day&apikey={Key}");
            string responseContent = await message.Content.ReadAsStringAsync();
            var jobject = JObject.Parse(responseContent);
            var list = JsonConvert.DeserializeObject<List<Stock>>(jobject["values"]?.ToString());
            return list.ToArray();
        }

        private void GetAPIKey()
        {
            if (counter < 8)
            {
                counter++;
                Key = apikeys[keyIndex];
            }
            
            counter = 0;
            if (keyIndex == apikeys.Count - 1) 
            { 
                keyIndex = 0;
            }
            else
            {
                keyIndex++;
            }
            Key = apikeys[keyIndex];
        }
    }
}