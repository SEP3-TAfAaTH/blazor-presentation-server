using System;
using System.Collections;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using BlazorPresentationServer.Model;
using BlazorPresentationServer.Services;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace UnitTests
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _helper;
        private IStockService StockService;

        // public UnitTest1(ITestOutputHelper helper)
        // {
        //     _helper = helper;
        //     StockService = new StockService(new HttpClient
        //     {
        //         BaseAddress = new Uri("https://api.twelvedata.com")
        //     });
        // }
        //
        // [Fact]
        // public async void GetStockPriceTest()
        // {
        //     double price = await StockService.GetStockPriceAsync("AAPL");
        //     _helper.WriteLine(price.ToString());
        // }
        //
        // [Fact]
        // public async void GetStockListTest()
        // {
        //     IList list = await StockService.GetStockPriceListAsync("AAPL");
        //     _helper.WriteLine(list.Count.ToString());
        //     foreach (var stock in list)
        //     {
        //         _helper.WriteLine(stock.ToString());
        //     }
        // }
        //
        // [Fact]
        // public async void Test1()
        // {
        //     // var service = new StockService();
        //     // var jObject = await service.Test();
        //     // // foreach (var prop in jObject.Properties())
        //     // // {
        //     // //     _helper.WriteLine(prop.Name + ": "+prop.Value);
        //     // // }
        //     //
        //     // var stock = JsonConvert.DeserializeObject<Stock>(jObject.ToString());
        //     // _helper.WriteLine("JsonConvert: "+stock);
        //     // // var stockb = JsonSerializer.Deserialize<Stock>(jObject.ToString(), new JsonSerializerOptions
        //     // // {
        //     // //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        //     // // });
        //     // // _helper.WriteLine(stockb.ToString());
        //
        // }
    }
}