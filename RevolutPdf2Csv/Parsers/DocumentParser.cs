using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RevolutPdf2Csv.Commands;

namespace RevolutPdf2Csv.Parsers
{
    public class DocumentParser
    {
        private readonly List<IParseCommand> parseCommands;
        public DocumentParser()
        {
             this.parseCommands = new List<IParseCommand>();
             InitializeCommands();
        }

        private void InitializeCommands()
        {
            parseCommands.Add(new BuyCommand());
            parseCommands.Add(new SellCommand());
            parseCommands.Add(new CSDCommand());
            parseCommands.Add(new DIVCommand());
        }

        public List<TradeActivity> ParsePdfDirectory(string directoryPath)
        {
            var pdfFilePaths = Directory.GetFiles(directoryPath, "*.pdf");
            var accountStatements = new List<TradeActivity>();
            foreach (var pdfPath in pdfFilePaths)
            {
                var tradeActivities = ParsePdfFile(pdfPath);
                accountStatements.AddRange(tradeActivities);
            }

            return accountStatements;
        }

        public List<TradeActivity> ParsePdfFile(string pdfPath)
        {
            var pdfText = PdfHelpers.ReadPdf2Text(pdfPath);
            var tradeActivities = ParsePdfText(pdfText);
            return tradeActivities;
        }

        public List<TradeActivity> ParsePdfText(string pdfText)
        {
            string str = pdfText.Substring(pdfText.IndexOf("ACTIVITY"));
            str = str.Replace("Trade Date", "TradeDate");
            str = str.Replace("Settle Date", "SettleDate");
            str = str.Replace("Symbol / Description", "Symbol/Description");
            
            string[] lines = str.Split(new []{ "\n" }, StringSplitOptions.None);

            var tradeActivities = ParseTradeActivities(lines);
            return tradeActivities;
        }

        private List<TradeActivity> ParseTradeActivities(string[] lines)
        {
            var tradeActivities = new List<TradeActivity>();
            foreach (var line in lines)
            {
                var commands = parseCommands.Select(x => x).Where(x => line.Contains(x.TradeType)).ToList();
                if (commands.Any())
                {
                    var command = commands.First();
                    var tradeActivity = command.Parse(line);
                    tradeActivities.Add(tradeActivity);
                }
            }
            return tradeActivities;
        }
    }
}
