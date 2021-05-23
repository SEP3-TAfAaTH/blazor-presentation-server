using System;
using System.ComponentModel;

namespace BlazorPresentationServer.Model
{
    public class Transaction
    {
        public long Id { get; set; }
        public string StockSymbol { get; set; }
        public Account Account { get; set; } //TODO Change to ID only
        [DefaultValue(1)]
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        public Decimal Price { get; set; }
        public bool IsBuy { get; set; }

        public Decimal GetTotal()
        {
            return Quantity * Price;
        }
        public override string ToString()
        {
            return $"Id: {Id}, Stock Symbol: {StockSymbol}, Price: {Price}, Quantity: {Quantity} IsBuy: {IsBuy}";
        }
    }
}