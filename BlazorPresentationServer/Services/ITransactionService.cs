using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorPresentationServer.Model;

namespace BlazorPresentationServer.Services
{
    public interface ITransactionService
    {
        Task BuyStock(Transaction transaction);
        Task SellStock(Transaction transaction);

        Task<List<Transaction>>GetAllTransactionsByAccountId(long id);
    }
}