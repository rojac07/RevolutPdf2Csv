namespace RevolutPdf2Csv
{
    public interface IParseCommand
    {
        string TradeType { get; }
        TradeActivity Parse(string value);
    }
}
