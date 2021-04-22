using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorPresentationServer.Model;

namespace BlazorPresentationServer.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient client;

        public UserService(HttpClient client)
        {
            this.client = client;
            Console.WriteLine($"###########BASE ADDRESS {client.BaseAddress}");
        }
        
        public async Task AddAccountAsync(Account account)
        {
            // Console.WriteLine($"FIRST LINE: {account.Id} {account.Username} {account.Password}");
            // string accountJson = JsonSerializer.Serialize(account);
            // HttpContent content = new StringContent(accountJson, Encoding.UTF8, "application/json");
            // await client.PostAsJsonAsync("/account", content);
            // Console.WriteLine($"_userService > AddAccountAsync: {accountJson.ToString()}");

            var accountJson = new StringContent(
                JsonSerializer.Serialize(account, typeof(Account), new JsonSerializerOptions(JsonSerializerDefaults.Web)), Encoding.UTF8, "application/json");

            using var httpResponse = await client.PostAsync("/account", accountJson);

            httpResponse.EnsureSuccessStatusCode();
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