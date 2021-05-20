using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorPresentationServer.Model;

namespace BlazorPresentationServer.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly HttpClient client;

        public TransactionService(HttpClient client)
        {
            this.client = client;
        }
        
        
        public async Task BuyStock(Transaction transaction)
        {
            var accountJson = new StringContent(
                JsonSerializer.Serialize(transaction, typeof(Transaction), new JsonSerializerOptions(JsonSerializerDefaults.Web)), Encoding.UTF8, "application/json");

            using var httpResponse = await client.PostAsync("/account", accountJson);
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
            }
        }

        public Task SellStock(Transaction transaction)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Transaction>> GetAllTransactionsByAccountId(long id)
        {
            Task<string> stringAsync = client.GetStringAsync($"/transaction/all/{id}");
            string message = await stringAsync;
            List<Transaction> transactions = JsonSerializer.Deserialize<List<Transaction>>(message, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return transactions;
        }
    }
}