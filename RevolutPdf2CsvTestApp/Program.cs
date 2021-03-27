using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using RevolutPdf2Csv.Parsers;

namespace RevolutPdf2CsvTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Any())
                ProgramWithArgs(args);
            else
                ProgramWithoutArgs();
        }

        /// <summary>
        /// Parse all pdf files in the current directory
        /// </summary>
        private static void ProgramWithoutArgs()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var documentParser = new DocumentParser();
            var tradeActivities = documentParser.ParsePdfDirectory(currentDirectory);
            Debug.WriteLine("Total trade activities: {0}", tradeActivities.Count);
            RevolutPdf2Csv.PdfHelpers.Write2Csv(tradeActivities, "Results.csv", "\t");
        }

        /// <summary>
        /// Parse single pdf file
        /// </summary>
        /// <param name="args"></param>
        private static void ProgramWithArgs(string[] args)
        {
            string exceptionMessage = string.Format("argument cannot be null\r\nUse Syntax: \"RevolutPdf2CsvTestApp.exe Document.pdf Result.csv\"");

            if (!args.Any() || string.IsNullOrEmpty(args[0]) || string.IsNullOrEmpty(args[1]))
            {
                throw new ArgumentNullException(exceptionMessage);
            }
            string pdfPath = args[0];
            string csvPath = args[1];
            var rawText = RevolutPdf2Csv.PdfHelpers.ReadPdf2Text(pdfPath);
            var documentParser = new DocumentParser();
            var tradeActivities = documentParser.ParsePdfText(rawText);
            Debug.WriteLine("Total trade activities: {0}", tradeActivities.Count);
            RevolutPdf2Csv.PdfHelpers.Write2Csv(tradeActivities, csvPath, "\t");
        }
    }
}
