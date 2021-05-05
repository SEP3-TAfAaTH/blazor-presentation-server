using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorPresentationServer.Model;

namespace BlazorPresentationServer.Services
{
    public class EditAccountService : IEditAccountService
    {
        private readonly HttpClient client;

        public EditAccountService(HttpClient client)
        {
            this.client = client;
        }
        
        public async Task EditAccountSync(Account account)
        {
            var accountJson = new StringContent(
                JsonSerializer.Serialize(account, typeof(Account), new JsonSerializerOptions(JsonSerializerDefaults.Web)), Encoding.UTF8, "application/json");

            using var httpResponse = await client.PostAsync("/account", accountJson);

            httpResponse.EnsureSuccessStatusCode();
        }
    }
}