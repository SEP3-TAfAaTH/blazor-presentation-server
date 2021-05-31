using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using BlazorPresentationServer.Model;

namespace BlazorPresentationServer.Services
{
    public class ProxyService: IStockService
    {
        private Dictionary<string, StockDateTime> cachedStocks;
        private Dictionary<string, PriceDateTime> cachedStockPrice;
        private Dictionary<string, ListDateTime> cachedStockPriceList;
        
        private IStockService stockService;

        public ProxyService()
        {
            stockService = new StockService();
            
            cachedStocks = new Dictionary<string, StockDateTime>();
            cachedStockPrice = new Dictionary<string, PriceDateTime>();
            cachedStockPriceList = new Dictionary<string, ListDateTime>();
        }
        
        public async Task<Stock> GetStockAsync(string symbol)
        {
            if (!cachedStocks.ContainsKey(symbol))
            {
                var stock = await stockService.GetStockAsync(symbol);
                StockDateTime stockDateTime = new StockDateTime
                {
                    Stock = stock,
                    DateTime = DateTime.Now
                };
                cachedStocks.Add(symbol,stockDateTime);
                Console.WriteLine("STOCK FROM API: "+stock);
                return stock;
            }
            if (DateTime.Now.Subtract(cachedStocks[symbol].DateTime).TotalSeconds > 60)
            {
                var stock = await stockService.GetStockAsync(symbol);
                cachedStocks[symbol] = new StockDateTime
                {
                    Stock = stock,
                    DateTime = DateTime.Now
                };
                Console.WriteLine("CACHED STOCK UPDATED FROM API: "+stock);
                return stock;
            }
            else
            {
                var stock = cachedStocks[symbol];
                Console.WriteLine("STOCK FROM CACHE: "+stock.Stock);
                return stock.Stock;
            }
        }

        public async Task<double> GetStockPriceAsync(string symbol)
        {
            if (!cachedStockPrice.ContainsKey(symbol))
            {
                var stock = await stockService.GetStockPriceAsync(symbol);
                PriceDateTime priceDateTime = new PriceDateTime()
                {
                    Price = stock,
                    DateTime = DateTime.Now
                };
                cachedStockPrice.Add(symbol, priceDateTime);
                Console.WriteLine("STOCK PRICE FROM API: "+stock);
                return stock;
            }
            if (DateTime.Now.Subtract(cachedStockPrice[symbol].DateTime).TotalSeconds > 60)
            {
                var stock = await stockService.GetStockPriceAsync(symbol);
                cachedStockPrice[symbol] = new PriceDateTime()
                {
                    Price = stock,
                    DateTime = DateTime.Now
                };
                Console.WriteLine("CACHED STOCK PRICE UPDATED FROM API: "+stock);
                return stock;
            }
            else
            {
                var stock = cachedStockPrice[symbol];
                Console.WriteLine("CACHED STOCK: "+stock.Price);
                return stock.Price;
            }
            
        }

        public async Task<Stock[]> GetStockPriceListAsync(string symbol)
        {
            if (!cachedStockPriceList.ContainsKey(symbol))
            {
                var stock = await stockService.GetStockPriceListAsync(symbol);
                ListDateTime listDateTime = new ListDateTime()
                {
                    Stocks = stock,
                    DateTime = DateTime.Now
                };
                cachedStockPriceList.Add(symbol,listDateTime);
                Console.WriteLine("STOCK LIST FROM API: "+stock);
                return stock;
            }
            if (DateTime.Now.Subtract(cachedStockPriceList[symbol].DateTime).TotalSeconds > 60)
            {
                var stock = await stockService.GetStockPriceListAsync(symbol);
                cachedStockPriceList[symbol] = new ListDateTime()
                {
                    Stocks = stock,
                    DateTime = DateTime.Now
                };
                Console.WriteLine("CACHED STOCK LIST UPDATED FROM API: "+stock);
                return stock;
            }
            else
            {
                var stock = cachedStockPriceList[symbol];
                Console.WriteLine("STOCK LIST FROM CACHE: "+stock.Stocks);
                return stock.Stocks;
            }
        }
        
        private class StockDateTime
        {
            public Stock Stock { get; set; }
            public DateTime DateTime { get; set; }
        }

        private class PriceDateTime
        {
            public double Price { get; set; }
            public DateTime DateTime { get; set; }
        }
        
        private class ListDateTime
        {
            public Stock[] Stocks { get; set; }
            public DateTime DateTime { get; set; }
        }
    }
    
}