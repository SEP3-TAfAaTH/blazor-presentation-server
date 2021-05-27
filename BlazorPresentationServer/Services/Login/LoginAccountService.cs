using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorPresentationServer.Authentication;
using BlazorPresentationServer.Model;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorPresentationServer.Services
{
    public class LoginAccountService : ILoginAccountService
    {
        private readonly ICachedAccount CachedAccount;
        //private readonly AuthenticationStateProvider CachedAccount;
        private readonly HttpClient client;

        public LoginAccountService(HttpClient client, ICachedAccount cachedAccount)
        {
            this.client = client;
            CachedAccount = cachedAccount;
        }

        public async Task<Account> LoginAccountAsync(Account account)
        {
            try
            {
                var response =
                    await client.GetAsync($"/login?username={account.Username}&password={account.Password}");
                if (!response.IsSuccessStatusCode)
                    throw new Exception(
                        $"{response.StatusCode}, {response.Content.ReadAsStringAsync().Result}"); //returns exception but not custom errormessage

                var responseContent = await response.Content.ReadAsStringAsync();

                var acc = JsonSerializer.Deserialize<Account>(responseContent, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                acc.Login = "true";
                CachedAccount.SetCachedAccount(acc);
                return acc;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}