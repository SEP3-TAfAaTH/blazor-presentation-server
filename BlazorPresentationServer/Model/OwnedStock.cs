namespace BlazorPresentationServer.Model
{
    public class OwnedStock : GeneralStock
    {
        public string Name { get; set; }
        public int ShareId { get; set; }
        public double PurchasePrice { get; set; }
        
        public double TotalCost { get; set; }
        
        public string Date { get; set; }
        public int Quantity { get; set; }
        
        public double GetTotalValue()
        {
            return Quantity * Price;
        }

        public double GetGainLoss()
        {
            return GetTotalValue() - TotalCost;
        }

    }
}