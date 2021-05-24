using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorPresentationServer.Authentication;
using BlazorPresentationServer.Model;

namespace BlazorPresentationServer.Services
{
    public class LoginAccountService : ILoginAccountService
    {
        private readonly HttpClient client;

        private readonly ICachedAccount CachedAccount;

        public LoginAccountService(HttpClient client, ICachedAccount cachedAccount)
        {
            this.client = client;
            CachedAccount = cachedAccount;
        }

        public async Task LoginAccountAsync(Account account)
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
                CachedAccount.SetCachedAccount(acc);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}