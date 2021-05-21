using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorPresentationServer.Model;

namespace BlazorPresentationServer.Services
{
    public interface IAccountService
    {
        Task AddAccountAsync(Account account);

        Task EditAccountAsync(Account account);
        
        Task<List<Account>> GetAccountsAsync();
        Task<Account> GetAccountAsyncById(int id);
        
    }
}