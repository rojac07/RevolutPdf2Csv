using System.Collections.Generic;

namespace RevolutPdf2Csv.Helpers
{
    public static class StringHelpers
    {
        public static List<string> ParseFromBeginning(string value, int nodes, char separator)
        {
            var result = new List<string>();
            var values = value.Split(separator);
            for (int i = 0; i < values.Length; i++)
            {
                if (i >= nodes)
                    return result;

                result.Add(values[i]);
            }
            return result;
        }

        public static List<string> ParseFromEnd(string value, int nodes, char seperator)
        {
            var result = new List<string>();
            var values = value.Split(seperator);
            int count = 0;
            for (int i = values.Length - 1; i >= 0; i--)
            {
                if (count >= nodes)
                {
                    result.Reverse();
                    return result;
                }

                result.Add(values[i]);
                count++;
            }
            result.Reverse();
            return result;
        }
        
        public static int GetTotalLength(List<string> str)
        {
            int length = 0;
            foreach (var s in str)
            {
                length += s.Length;
            }

            return length;
        }
        
        public static string RemoveParentheses(string str)
        {
            str = str.Replace("(", "");
            str = str.Replace(")", "");
            return str;
        }

        public static string RemoveCharactersFromEnd(string value, int length)
        {
            value = value.Substring(0, value.Length - length);
            return value;
        }

        public static string TrimStartEnd(string str)
        {
            str = str.TrimStart(' ');
            str = str.TrimEnd(' ');
            return str;
        }
    }
}
