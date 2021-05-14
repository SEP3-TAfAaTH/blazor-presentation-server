


using System.Threading.Tasks;
using System.Timers;
using BlazorPresentationServer.Model;


namespace BlazorPresentationServer.Services
{
    public class AvailableStockService : IAvailableStockService
    {
        private GeneralStock stock;
        
        public async Task<GeneralStock> GatherInformations()
        {
            stock = new GeneralStock();
            stock.Price = 1254.54642121;
            stock.DailyLow = 122212;
            return stock;
        }
        
    }
}