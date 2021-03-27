using System.Globalization;
using RevolutPdf2Csv.Helpers;

namespace RevolutPdf2Csv.Commands
{
    public class CSDCommand : IParseCommand
    {
        public string TradeType => "CSD";
        public TradeActivity Parse(string value)
        {
            var tradeActivity = new TradeActivity();

            var start = StringHelpers.ParseFromBeginning(value, 4, ' ');
            var end = StringHelpers.ParseFromEnd(value, 1, ' ');

            tradeActivity.TradeDate = ParserHelpers.ParseDataTime(start[0]);
            tradeActivity.SettleDate = ParserHelpers.ParseDataTime(start[1]);
            tradeActivity.Currency = start[2];
            tradeActivity.ActivityType = start[3];
            
            var amount = StringHelpers.RemoveParentheses(end[0]);
            tradeActivity.Amount = double.Parse(amount, CultureInfo.InvariantCulture);
            var description = value.Substring(value.IndexOf(start[3]));
            int length = StringHelpers.GetTotalLength(end);
            description = StringHelpers.RemoveCharactersFromEnd(description, length + 1);
            tradeActivity.Description = description;
            return tradeActivity;

        }
    }
}
