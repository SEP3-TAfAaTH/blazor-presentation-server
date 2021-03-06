using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorPresentationServer.Model;

namespace BlazorPresentationServer.Services
{
    public interface ITransactionService
    {
        Task CreateTransactionAsync(Transaction transaction);

        Task<List<Transaction>> GetAllTransactionsByAccountId(long id);

        Task DeleteAllTransactionsAsync(long id);
        Task<List<OwnedStock>> GetOwnedStocksByAccountIdAsync(long id);
    }
}