namespace BlazorPresentationServer.Model
{
    public class OwnedStock : GeneralStock
    {
        public string Name { get; set; }
        public int ShareId { get; set; }
        public double PurchasePrice { get; set; }
        public string Date { get; set; }
        public int Quantity { get; set; }
        
        public double TotalValue { get; set; }
        
        public double TodaysChange { get; set; }
        
        public double TotalGainLoss { get; set; }


    }
}