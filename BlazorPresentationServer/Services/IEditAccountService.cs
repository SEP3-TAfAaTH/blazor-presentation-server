using System.Threading.Tasks;
using BlazorPresentationServer.Model;

namespace BlazorPresentationServer.Services
{
    public interface IEditAccountService
    {
        Task EditAccountSync(Account account);
    }
}