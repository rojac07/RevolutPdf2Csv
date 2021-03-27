using System;

namespace RevolutPdf2Csv
{
    public class TradeActivity
    {
        public DateTime TradeDate { get; set; }

        public DateTime SettleDate { get; set; }
        
        public string Currency { get; set; }
        
        public string ActivityType { get; set; }
        
        public string Symbol { get; set; }

        public string Description { get; set; }

        public double Quantity { get; set; }
        
        public double Price { get; set; }
        
        public double Amount { get; set; }
    }
}
