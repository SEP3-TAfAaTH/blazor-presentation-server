namespace BlazorPresentationServer.Model
{
    public class OwnedStock
    {
        public string Symbol { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public double TotalCost { get; set; }
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