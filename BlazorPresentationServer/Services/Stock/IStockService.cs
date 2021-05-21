using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorPresentationServer.Model;
using Newtonsoft.Json.Linq;

namespace BlazorPresentationServer.Services
{
    public interface IStockService
    {
        Task<JObject> Test();

        Task<Stock> GetStockAsync(string symbol);

        Task<double> GetStockPriceAsync(string symbol);

        Task<Stock[]> GetStockPriceListAsync(string symbol);
    }
}