using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorPresentationServer.Model;

namespace BlazorPresentationServer.Services
{
    public class FileAdapter : IFileAdapter
    {
        private readonly HttpClient client;

        public FileAdapter(HttpClient client)
        {
            this.client = client;
        }
        
        public async Task AddAccountAsync(Account account)
        {
            string accountJson = JsonSerializer.Serialize(account);
            HttpContent content = new StringContent(accountJson, Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await client.PostAsync("/account", content);

            Console.WriteLine($"I got here: + {response.StatusCode} + {accountJson.ToString()}");
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        
        public async Task<List<Account>> GetAccountsAsync()
        {
            Task<string> stringAsync = client.GetStringAsync("/account");
            string message = await stringAsync;
            List<Account> accounts = JsonSerializer.Deserialize<List<Account>>(message, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return accounts;
        }

        public async Task<Account> GetAccountAsyncById(int id)
        {
            Task<string> stringAsync = client.GetStringAsync($"/account/{id}");
            string message = await stringAsync;
            Account account = JsonSerializer.Deserialize<Account>(message, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return account;
        }

    }
}