using System;
using System.Collections.Generic;

namespace JsonStringValidator
{
    public class Program
    {
        const int ControlCharUpperLimit = 31;
        const int QuotationMark = 34;
        const int Slash = 47;
        const int Backslash = 92;
        const int MinimumQuotationMarks = 2;

        public static void Main()
        {
            Console.WriteLine(IsValidJsonString(Console.ReadLine()));
            Console.Read();
        }

        public static string IsValidJsonString(string inputData)
        {
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

                if (unquotedInputData[index] == Convert.ToChar(Backslash) && (index == unquotedInputData.Length - 1 || !IsEscapableCharacter(unquotedInputData, ++index)))
                {
                    return "Invalid";
                }

                index++;
            }

            return "Valid";
        }

        public static bool IsQuoted(string inputData)
        {
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

        public static bool IsEscapableCharacter(string unquotedInputData, int index)
        {
            const char Backspace = 'b';
            const char Formfeed = 'f';
            const char Newline = 'n';
            const char CarriageReturn = 'r';
            const char HorizontalTab = 't';
            const char Unicode = 'u';
            const int NumberOfHexCharacters = 4;

            char[] escapableCharacters = { Convert.ToChar(QuotationMark), Convert.ToChar(Backslash), Convert.ToChar(Slash), Backspace, Formfeed, Newline, CarriageReturn, HorizontalTab };
            char[] hexCharacters = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'A', 'B', 'C', 'D', 'E', 'F' };

            if (unquotedInputData == null)
            {
                return false;
            }

            if (unquotedInputData[index] == Unicode && unquotedInputData.Length > index + NumberOfHexCharacters)
            {
                for (int i = 1; i <= NumberOfHexCharacters; i++)
                {
                    if (Array.IndexOf(hexCharacters, unquotedInputData[index + i]) == -1)
                    {
                        return false;
                    }
                }

                return true;
            }

            return Array.IndexOf(escapableCharacters, unquotedInputData[index]) >= 0;
        }
    }
}