using System;
using System.Globalization;
using RevolutPdf2Csv.Helpers;

namespace RevolutPdf2Csv
{
    public class BuyCommand : IParseCommand
    {
        public string TradeType  => "BUY";

        public TradeActivity Parse(string value)
        {
            var tradeActivity = new TradeActivity();

            var start = StringHelpers.ParseFromBeginning(value, 4, ' ');
            var end = StringHelpers.ParseFromEnd(value, 3, ' ');

            tradeActivity.TradeDate = ParserHelpers.ParseDataTime(start[0]);
            tradeActivity.SettleDate = ParserHelpers.ParseDataTime(start[1]);
            tradeActivity.Currency = start[2];
            tradeActivity.ActivityType = start[3];
            tradeActivity.Quantity = double.Parse(end[0], CultureInfo.InvariantCulture);
            tradeActivity.Price = double.Parse(end[1], CultureInfo.InvariantCulture);
            var amount = StringHelpers.RemoveParentheses(end[2]);
            tradeActivity.Amount = double.Parse(amount, CultureInfo.InvariantCulture);
            var symbolDescription = value.Substring(value.IndexOf(start[3]) + start[3].Length + 1);

            int length = StringHelpers.GetTotalLength(end);
            symbolDescription = StringHelpers.RemoveCharactersFromEnd(symbolDescription, length  + 3);
            var array = symbolDescription.Split('-');
            tradeActivity.Symbol = StringHelpers.TrimStartEnd(array[0]);
            tradeActivity.Description = StringHelpers.TrimStartEnd(array[1]);
            return tradeActivity;
        }
    }
}
