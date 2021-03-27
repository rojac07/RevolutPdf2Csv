using System.Collections.Generic;
using System.IO;
using System.Linq;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace RevolutPdf2Csv
{
    public static class PdfHelpers
    {
        public static string ReadPdf2Text(string path)
        {
            var pdfReader = new PdfReader(path);
            string text = "";
            for (int page = 1; page <= pdfReader.NumberOfPages; page++)
            {
                text += PdfTextExtractor.GetTextFromPage(pdfReader, page);
            }
            pdfReader.Close();
            return text;
        }

        public static void Write2Csv(List<TradeActivity> tradeActivities, string path, string format)
        {
            if (!tradeActivities.Any()) return;

            using (StreamWriter outputFile = new StreamWriter(path))
            {
                outputFile.Write("Trade Date" + format);
                outputFile.Write("Settle Date" + format);
                outputFile.Write("Currency" + format);
                outputFile.Write("Activity TradeType" + format);
                outputFile.Write("Symbol" + format);
                outputFile.Write("Description" + format);
                outputFile.Write("Quantity" + format);
                outputFile.Write("Price" + format);
                outputFile.Write("Amount" + format);
                outputFile.WriteLine();
                foreach (var trade in tradeActivities)
                {
                    outputFile.Write(trade.TradeDate + format);
                    outputFile.Write(trade.SettleDate + format);
                    outputFile.Write(trade.Currency + format);
                    outputFile.Write(trade.ActivityType + format);
                    outputFile.Write(trade.Symbol + format);
                    outputFile.Write(trade.Description + format);
                    outputFile.Write(trade.Quantity + format);
                    outputFile.Write(trade.Price + format);
                    outputFile.Write(trade.Amount + format);
                    outputFile.WriteLine();
                }
            }
        }
    }
}
