using System;
using System.Collections;
using System.Net.Http;
using System.Text.Json;
using BlazorPresentationServer.Components;
using BlazorPresentationServer.Model;
using BlazorPresentationServer.Services;
using Bunit;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace UnitTests
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _helper;
        private IStockService StockService;

        public UnitTest1(ITestOutputHelper helper)
        {
            
            // _helper = helper;
            // StockService = new StockService(new HttpClient
            // {
            //     BaseAddress = new Uri("https://api.twelvedata.com")
            // });
        }
        
        
        
        
        [Fact]
        public async void GetStockPriceTest()
        {
            double price = await StockService.GetStockPriceAsync("AAPL");
            _helper.WriteLine(price.ToString());
        }
        
        [Fact]
        public async void GetStockListTest()
        {
            IList list = await StockService.GetStockPriceListAsync("AAPL");
            _helper.WriteLine(list.Count.ToString());
            foreach (var stock in list)
            {
                _helper.WriteLine(stock.ToString());
            }
        }


        [Fact]
        public async void StockComponentDisplayTest()
        {
            Stock testObject = new Stock()
            {
                Name = "Stock1",
                Close = 100,
                High = 200,
                Low = 50,
                Price = 200,
                Percent_Change = 300,
                Symbol = "Stck"

            };
            
            using var ctx = new TestContext();
            var renderedComponent = ctx.RenderComponent<StockComponent>(
                ComponentParameterFactory.Parameter(
                    nameof(StockComponent.GnrlStock), testObject));
            Assert.Equal(testObject.High.ToString(), renderedComponent.Find($"#{ElementIds.HighId}").TextContent);
        }
        
        // [Fact]
        // public void BuyDropDownTest()
        // {
        //     using var ctx = new TestContext();
        //     var cut = ctx.RenderComponent<Component1>(
        //         ComponentParameterFactory.Parameter(
        //             nameof(Component1.ParentName),
        //             someValue));
        // }
        
        //[Fact]
        // public async void Test1()
        // {
        //     var service = new StockService();
        //     var jObject = await service.Test();
        //     foreach (var prop in jObject.Properties())
        //     {
        //         _helper.WriteLine(prop.Name + ": "+prop.Value);
        //     }
        //     
        //     var stock = JsonConvert.DeserializeObject<Stock>(jObject.ToString());
        //     _helper.WriteLine("JsonConvert: "+stock);
        //     var stockb = JsonSerializer.Deserialize<Stock>(jObject.ToString(), new JsonSerializerOptions
        //     {
        //         PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        //     });
        //     _helper.WriteLine(stockb.ToString());
        //
        // }
    }
}