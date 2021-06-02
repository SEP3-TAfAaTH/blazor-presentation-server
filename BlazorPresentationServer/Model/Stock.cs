using System;


namespace BlazorPresentationServer.Model
{
    public class Stock
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public DateTime Datetime { get; set; }
        public double Price { get; set; }
        public double Open { get; set; }

        public double High { get; set; }

        public double Low { get; set; }

        public double Close { get; set; }

        public double percent_change { get; set; }
        
        public long Volume { get; set; }

        public override string ToString()
        {
            return
                $"Symbol: {Symbol}\nName: {Name}\nDateTime: {Datetime}\nPrice: {Price}\nOpen: {Open}\nHigh: {High}\nLow: {Low}\nClose: {Close}\nPercent change: {percent_change}";
        }
    }
}