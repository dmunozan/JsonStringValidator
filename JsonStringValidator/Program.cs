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
            const int MinimumQuotationMarks = 2;

            if (inputData == null || !IsQuoted(inputData))
            {
                return "Invalid";
            }

            string unquotedInputData = inputData.Substring(1, inputData.Length - MinimumQuotationMarks);

            int index = 0;

            while (index < unquotedInputData.Length)
            {
                if (unquotedInputData[index] <= Convert.ToChar(ControlCharUpperLimit) || unquotedInputData[index] == '\"')
                {
                    return "Invalid";
                }

                if (unquotedInputData[index] == '\\' && (index == unquotedInputData.Length - 1 || !IsEscapableCharacter(unquotedInputData, ++index)))
                {
                    return "Invalid";
                }

                index++;
            }

            return "Valid";
        }

        public static bool IsQuoted(string inputData)
        {
            const int MinimumQuotationMarks = 2;

            if (inputData == null)
            {
                return false;
            }

            if (inputData.Length < MinimumQuotationMarks)
            {
                return false;
            }

            return inputData[0] == '\"' && inputData[inputData.Length - 1] == '\"';
        }

        public static bool IsEscapableCharacter(string unquotedInputData, int index)
        {
            const int NumberOfHexCharacters = 4;

            char[] escapableCharacters = { '\"', '\\', '/', 'b', 'f', 'n', 'r', 't' };
            char[] hexCharacters = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'A', 'B', 'C', 'D', 'E', 'F' };

            if (unquotedInputData == null)
            {
                return false;
            }

            if (unquotedInputData[index] == 'u' && unquotedInputData.Length > index + NumberOfHexCharacters)
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