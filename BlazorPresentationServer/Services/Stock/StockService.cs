using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorPresentationServer.Model;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BlazorPresentationServer.Services
{
    public class StockService : IStockService
    {
        private readonly HttpClient client;
        
        private string Key;

        public StockService(HttpClient client)
        {
            this.client = client;
        }
        
        public async Task<Stock> GetStockAsync(string symbol)
        {
            var response =
                await client.GetStringAsync($"/stock/{symbol}");
            var stock = JsonSerializer.Deserialize<Stock>(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            Console.WriteLine("Stock price: "+stock);
            return stock;
        }

        public async Task<double> GetStockPriceAsync(string symbol)
        {
            var response =
                await client.GetStringAsync($"/stock/price/{symbol}");
            var stock = JsonSerializer.Deserialize<double>(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            Console.WriteLine("Stock price: "+stock);
            return stock;
        }

        public async Task<Stock[]> GetStockPriceListAsync(string symbol)
        {
            var response =
                await client.GetStringAsync($"/stock/list/{symbol}");
            var stock = JsonSerializer.Deserialize<Stock[]>(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            Console.WriteLine("Stock price: "+stock);
            foreach (var s in stock)
            {
                Console.WriteLine(s);
            }
            return stock;
        }
    }
}