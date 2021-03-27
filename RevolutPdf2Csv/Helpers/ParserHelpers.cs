using System;
using System.Diagnostics;
using System.Globalization;

namespace RevolutPdf2Csv.Helpers
{
    public static class ParserHelpers
    {
        public static DateTime ParseDataTime(string dateString)
        {
            string[] formats = {
                "MM/dd/yyyy"
            };

            try
            {
                var dateTime = DateTime.ParseExact(dateString, formats,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None);
                return dateTime;
            }
            catch (FormatException ex)
            {
                Debug.WriteLine("Exception in {0}, Message: {1}", nameof(Helpers), ex?.Message);
                return DateTime.MinValue;
            }
        }
    }
}
