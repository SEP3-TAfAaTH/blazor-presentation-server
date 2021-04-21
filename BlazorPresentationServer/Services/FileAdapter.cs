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
            await client.PostAsync("/account", content);
        }

        public async Task<Account> GetAccountAsync(int id)
        {
            Task<string> stringAsync = client.GetStringAsync($"/account/{id}");
            string message = await stringAsync;
            Account account = JsonSerializer.Deserialize<Account>(message, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return account;
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

        public async Task RemoveAccountAsync(int id)
        {
            await client.DeleteAsync($"/account/{id}");
        }

        public async Task UpdateAccountAsync(Account account)
        {
            string accountJson = JsonSerializer.Serialize(account);
            HttpContent content = new StringContent(accountJson, Encoding.UTF8, "application/json");
            await client.PatchAsync("/account", content);
            
        }

    }
}