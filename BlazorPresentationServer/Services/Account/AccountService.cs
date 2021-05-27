using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorPresentationServer.Model;

namespace BlazorPresentationServer.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient client;

        public AccountService(HttpClient client)
        {
            this.client = client;
        }

        public async Task AddAccountAsync(Account account)
        {
            var accountJson = new StringContent(
                JsonSerializer.Serialize(account, typeof(Account),
                    new JsonSerializerOptions(JsonSerializerDefaults.Web)), Encoding.UTF8, "application/json");

            using var httpResponse = await client.PostAsync("/account", accountJson);
            if (!httpResponse.IsSuccessStatusCode) throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
        }

        public async Task EditAccountAsync(Account account)
        {
            var accountJson = new StringContent(
                JsonSerializer.Serialize(account, typeof(Account),
                    new JsonSerializerOptions(JsonSerializerDefaults.Web)), Encoding.UTF8, "application/json");

            using var httpResponse = await client.PutAsync($"/account/{account.Id}", accountJson);
            if (!httpResponse.IsSuccessStatusCode) throw new Exception(httpResponse.Content.ReadAsStringAsync().Result);
        }

        public async Task DeleteAccountAsync(Account account)
        {
            using var response = await client.DeleteAsync($"/account/{account.Id}");
            if (!response.IsSuccessStatusCode) throw new Exception(response.Content.ReadAsStringAsync().Result);
        }


        public async Task<List<Account>> GetAccountsAsync()
        {
            var stringAsync = client.GetStringAsync("/account");
            var message = await stringAsync;
            var accounts = JsonSerializer.Deserialize<List<Account>>(message, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return accounts;
        }

        public async Task<Account> GetAccountAsyncById(int id)
        {
            var stringAsync = client.GetStringAsync($"/account/{id}");
            var message = await stringAsync;
            var account = JsonSerializer.Deserialize<Account>(message, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return account;
        }
    }
}