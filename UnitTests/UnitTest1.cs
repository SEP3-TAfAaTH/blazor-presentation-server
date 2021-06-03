using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using BlazorPresentationServer.Components;
using BlazorPresentationServer.Model;
using BlazorPresentationServer.Services;
using Bunit;
using Moq;
using Moq.Protected;
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

        private ITransactionService TransactionService;

        
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
        public void CreateTransactionTest()
        {
            
            // Transaction transaction2 = TransactionService.CreateTransactionAsync(TransactionService);
            using (var mock = AutoMock.GetLoose())
            {
                Transaction transaction = GetSampleTransactions()[0];
                var transactionJson = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(transaction, typeof(Transaction),
                        new JsonSerializerOptions(JsonSerializerDefaults.Web)), Encoding.UTF8, "application/json");
                
                mock.Mock<HttpClient>()
                    .Setup(x => x.PostAsync("/transaction",transactionJson));

                var cls = mock.Create<TransactionService>();

                cls.CreateTransactionAsync(transaction);
                
                mock.Mock<HttpClient>()
                    .Verify(x => x.PostAsync("/transaction", transactionJson),Times.Exactly(1));
                
            }
        }
        
        [Fact]
        public async void GetAllTransactionsByAccountIdTest()
        {
            // //ARRANGE
            // var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            // handlerMock.Protected()
            //     // Setup the PROTECTED method to mock
            //     .Setup<Task<HttpResponseMessage>>(
            //         "SendAsync",
            //         ItExpr.IsAny<HttpRequestMessage>(),
            //         ItExpr.IsAny<CancellationToken>()
            //     )
            //     // prepare the expected response of the mocked http call
            //     .ReturnsAsync(new HttpResponseMessage()
            //     {
            //         StatusCode = HttpStatusCode.OK,
            //         Content = new StringContent("[{'id':1,'value':'1'}]"),
            //     })
            //     .Verifiable();
            //
            //
            // // use real http client with mocked handler here
            // var httpClient = new HttpClient(handlerMock.Object)
            // {
            //     BaseAddress = new Uri("http://test.com/"),
            // };
            //
            // var subjectUnderTest = new TransactionService(httpClient);
            //
            // // ACT
            // var result = await subjectUnderTest
            //     .CreateTransactionAsync();
            //
            // // ASSERT
            // result.Should().NotBeNull(); // this is fluent assertions here...
            // result.Id.Should().Be(1);
            //
            // // also check the 'http' call was like we expected it
            // var expectedUri = new Uri("http://test.com/api/test/whatever");
            //
            // handlerMock.Protected().Verify(
            //     "SendAsync",
            //     Times.Exactly(1), // we expected a single external request
            //     ItExpr.Is<HttpRequestMessage>(req =>
            //             req.Method == HttpMethod.Get  // we expected a GET request
            //             && req.RequestUri == expectedUri // to this uri
            //     ),
            //     ItExpr.IsAny<CancellationToken>()
            // );
            
            
            
            // using (var mock = AutoMock.GetLoose())
            // {
            //     long id = 1;
            //
            //     mock.Mock<HttpClient>()
            //         .Setup(x => x.GetStringAsync($"/transaction/all/{id}"))
            //         .Returns(GetSampleTransactions2());
            //
            //     var cls = mock.Create<TransactionService>();
            //     var expected = GetSampleTransactions2();
            //
            //     var actual = cls.GetAllTransactionsByAccountId(1);
            //     //Console.WriteLine(actual);
            //     Assert.True(actual != null);
            //     Assert.Equal(expected.Result.Length, actual.Result.Count );
            // }
            //
            // throw new NotImplementedException();
        }

        

        private List<Transaction> GetSampleTransactions()
        {
            List<Transaction> output = new List<Transaction>
            {
                new Transaction()
                {
                    Id = 1,
                    IsBuy = true,
                    Quantity = 3,
                },
                new Transaction()
                {
                    Id = 2,
                    IsBuy = false,
                    Quantity = 4,
                }
            };
           
            return output;
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
                //Percent_Change = 300,
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