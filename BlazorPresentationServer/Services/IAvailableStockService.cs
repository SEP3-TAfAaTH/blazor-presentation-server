using System.Threading.Tasks;
using BlazorPresentationServer.Model;

namespace BlazorPresentationServer.Services
{
    public interface IAvailableStockService
    {
        Task<GeneralStock> GatherInformations();
    }
}