using System;
using System.Collections.Generic;

namespace JsonStringValidator
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine(IsValidJsonString(Console.ReadLine()));
            Console.Read();
        }

        public static string IsValidJsonString(string inputData)
        {
            const int ControlCharUpperLimit = 31;
            const int QuotationMark = 34;
            const int Backslash = 92;
            const int MinimumQuotationMarks = 2;

            if (inputData == null || !IsQuoted(inputData))
            {
                return "Invalid";
            }

            string unquotedInputData = inputData.Substring(1, inputData.Length - MinimumQuotationMarks);

            int index = 0;

            while (index < unquotedInputData.Length)
            {
                if (unquotedInputData[index] <= Convert.ToChar(ControlCharUpperLimit) || unquotedInputData[index] == Convert.ToChar(QuotationMark))
                {
                    return "Invalid";
                }

                if (unquotedInputData[index] == Convert.ToChar(Backslash) && (index == unquotedInputData.Length - 1 || unquotedInputData[++index] != Convert.ToChar(QuotationMark)))
                {
                    return "Invalid";
                }

                index++;
            }

            return "Valid";
        }

        public static bool IsQuoted(string inputData)
        {
            const int QuotationMark = 34;
            const int MinimumQuotationMarks = 2;

            if (inputData == null)
            {
                return false;
            }

            if (inputData.Length < MinimumQuotationMarks)
            {
                return false;
            }

            return inputData[0] == Convert.ToChar(QuotationMark) && inputData[inputData.Length - 1] == Convert.ToChar(QuotationMark);
        }
    }
}