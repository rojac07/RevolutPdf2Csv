using System.Globalization;
using RevolutPdf2Csv.Helpers;

namespace RevolutPdf2Csv.Commands
{
    public class SellCommand : IParseCommand
    {
        public string TradeType => "SELL";
       
        public TradeActivity Parse(string value)
        {
            var tradeActivity = new TradeActivity();

            var startNodes = StringHelpers.ParseFromBeginning(value, 4, ' ');
            var endNodes = StringHelpers.ParseFromEnd(value, 3, ' ');

            tradeActivity.TradeDate = ParserHelpers.ParseDataTime(startNodes[0]);
            tradeActivity.SettleDate = ParserHelpers.ParseDataTime(startNodes[1]);
            tradeActivity.Currency = startNodes[2];
            tradeActivity.ActivityType = startNodes[3];
            tradeActivity.Quantity = double.Parse(endNodes[0], CultureInfo.InvariantCulture);
            tradeActivity.Price = double.Parse(endNodes[1], CultureInfo.InvariantCulture);
            var amount = StringHelpers.RemoveParentheses(endNodes[2]);
            tradeActivity.Amount = double.Parse(amount, CultureInfo.InvariantCulture);
            var symbolDescription = value.Substring(value.IndexOf(startNodes[3]) + startNodes[3].Length + 1);
            int length = StringHelpers.GetTotalLength(endNodes);
            symbolDescription = StringHelpers.RemoveCharactersFromEnd(symbolDescription, length + 3);
            var array = symbolDescription.Split('-');
            tradeActivity.Symbol = StringHelpers.TrimStartEnd(array[0]);
            tradeActivity.Description = StringHelpers.TrimStartEnd(array[1]);
            return tradeActivity;
        }
    }
}
