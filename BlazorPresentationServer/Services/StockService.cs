using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BlazorPresentationServer.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace BlazorPresentationServer.Services
{
    public class StockService : IStockService
    { 
        private const string ApiKey = "8b66c2d14f4643e7b19076c0de11b861";
        private HttpClient client;

        public StockService(HttpClient client)
        {
            this.client = client;
        }
        public async Task<JObject> Test()
        {
            HttpResponseMessage response =
                await client.GetAsync($"/quote?symbol=AAPL&interval=1day&apikey={ApiKey}");
            string responseContent = await response.Content.ReadAsStringAsync();
            var jobject = JObject.Parse(responseContent);
            return jobject;
        }

        public async Task<Stock> GetStockAsync(string symbol)
        {
            HttpResponseMessage response =
                await client.GetAsync($"/quote?symbol={symbol}&interval=1day&apikey={ApiKey}");
            string responseContent = await response.Content.ReadAsStringAsync();
            var jobject = JObject.Parse(responseContent);
            Stock stock = JsonConvert.DeserializeObject<Stock>(jobject.ToString());
            stock.Price = await GetStockPriceAsync(symbol);
            return stock;
        }

        public async Task<double> GetStockPriceAsync(string symbol)
        {
            HttpResponseMessage response =
                await client.GetAsync($"/price?symbol={symbol}&interval=1day&apikey={ApiKey}");
            string responseContent = await response.Content.ReadAsStringAsync();
            var jObject = JObject.Parse(responseContent);
            Stock price = JsonConvert.DeserializeObject<Stock>(jObject.ToString());
            return price.Price;
        }

        public async Task<Stock[]> GetStockPriceListAsync(string symbol)
        {
            HttpResponseMessage message =
                await client.GetAsync($"/time_series?symbol={symbol}&interval=1day&apikey={ApiKey}");
            string responseContent = await message.Content.ReadAsStringAsync();
            var jobject = JObject.Parse(responseContent);
            var list = JsonConvert.DeserializeObject<List<Stock>>(jobject["values"]?.ToString());
            return list.ToArray();
        }
    }
}