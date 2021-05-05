using System;
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
            try
            {
                HttpResponseMessage response =
                    await client.GetAsync($"/login?username={account.Username}&password={account.Password}");
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"{response.StatusCode}, {response.Content.ReadAsStringAsync().Result}"); //returns exception but not custom errormessage
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            
        }
    }
}