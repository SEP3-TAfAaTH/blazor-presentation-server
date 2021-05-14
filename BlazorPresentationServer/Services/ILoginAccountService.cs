using System.Threading.Tasks;
using BlazorPresentationServer.Model;

namespace BlazorPresentationServer.Services
{
    public interface ILoginAccountService
    {
        Task LoginAccountAsync(Account account);
        
    }
}