namespace BlazorPresentationServer.Model
{
    public class GeneralStock
    {
        public string Symbol { get; set; }
        public double Price { get; set; }
        public double PIncrease { get; set; }
        public double DailyHigh { get; set; }
        public double DailyLow { get; set; }
        public double PreviousClose { get; set; }
        public double AvgInPast { get; set; }
        public double HighLowPrice { get; set; }
        
    }
}