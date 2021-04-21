using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorPresentationServer.Model;

namespace BlazorPresentationServer.Services
{
    public interface IFileAdapter
    {
        Task<List<Account>> GetAccountsAsync();
        Task AddAccountAsync(Account account);
        Task<Account> GetAccountAsync(int id);
        Task RemoveAccountAsync(int id);
        Task UpdateAccountAsync(Account account);
    }
}