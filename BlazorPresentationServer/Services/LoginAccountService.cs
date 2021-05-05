using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorPresentationServer.Model;

namespace BlazorPresentationServer.Services
{
    public class LoginAccountService : ILoginAccountService
    {
        private readonly HttpClient client;

        public LoginAccountService (HttpClient client)
        {
            this.client = client;
        }
        
        public async Task LoginAccountAsync(Account account)
        {
            var accountJson = new StringContent(
                JsonSerializer.Serialize(account, typeof(Account), new JsonSerializerOptions(JsonSerializerDefaults.Web)), Encoding.UTF8, "application/json");

            using var httpResponse = await client.PostAsync("/login", accountJson);

            httpResponse.EnsureSuccessStatusCode();
        }
    }
}