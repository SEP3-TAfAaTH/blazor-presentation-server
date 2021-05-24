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


        public async Task CreateTransactionAsync(Transaction transaction)
        {
            var transactionJson = new StringContent(
                JsonSerializer.Serialize(transaction, typeof(Transaction),
                    new JsonSerializerOptions(JsonSerializerDefaults.Web)), Encoding.UTF8, "application/json");

            using var httpResponse = await client.PostAsync("/transaction", transactionJson);
            if (!httpResponse.IsSuccessStatusCode) throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
        }


        public async Task<List<Transaction>> GetAllTransactionsByAccountId(long id)
        {
            var stringAsync = client.GetStringAsync($"/transaction/all/{id}");
            var message = await stringAsync;
            var transactions = JsonSerializer.Deserialize<List<Transaction>>(message, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return transactions;
        }

        public async Task DeleteAllTransactionsAsync(long id)
        {
            using var response = await client.DeleteAsync($"/transaction/{id}");
            if (!response.IsSuccessStatusCode) throw new Exception(response.Content.ReadAsStringAsync().Result);
        }

        public async Task<Transaction> GetTransactionById(long id)
        {
            var stringAsync = client.GetStringAsync($"transaction/{id}");
            var message = await stringAsync;
            var transaction = JsonSerializer.Deserialize<Transaction>(message, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return transaction;
        }
    }
}