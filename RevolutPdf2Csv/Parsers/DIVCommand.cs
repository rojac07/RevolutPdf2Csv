using System.Globalization;
using RevolutPdf2Csv.Helpers;

namespace RevolutPdf2Csv.Commands
{
    public class DIVCommand : IParseCommand
    {
        public string TradeType => "DIV";
        public TradeActivity Parse(string str)
        {
            var tradeActivity = new TradeActivity();

            var start = StringHelpers.ParseFromBeginning(str, 4, ' ');
            var end = StringHelpers.ParseFromEnd(str, 3, ' ');

            tradeActivity.TradeDate = ParserHelpers.ParseDataTime(start[0]);
            tradeActivity.SettleDate = ParserHelpers.ParseDataTime(start[1]);
            tradeActivity.Currency = start[2];
            tradeActivity.ActivityType = start[3];
            tradeActivity.Quantity = double.Parse(end[0], CultureInfo.InvariantCulture);
            tradeActivity.Price = double.Parse(end[1], CultureInfo.InvariantCulture);
            var amount = StringHelpers.RemoveParentheses(end[2]);
            tradeActivity.Amount = double.Parse(amount, CultureInfo.InvariantCulture);
            var description = str.Substring(str.IndexOf(start[3]));

            int length = StringHelpers.GetTotalLength(end);
            description = StringHelpers.RemoveCharactersFromEnd(description, length + 3);
            tradeActivity.Description = description;
            return tradeActivity;
        }
    }
}
