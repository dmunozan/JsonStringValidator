﻿using System;
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

            int currentIndex = 0;

            while (currentIndex < unquotedInputData.Length)
            {
                int incrementIndex = 1;

                if (unquotedInputData[currentIndex] <= Convert.ToChar(ControlCharUpperLimit) || unquotedInputData[currentIndex] == '\"')
                {
                    return "Invalid";
                }

                if (unquotedInputData[currentIndex] == '\\' && (currentIndex == unquotedInputData.Length - 1 || !IsEscapableCharacter(unquotedInputData, currentIndex, out incrementIndex)))
                {
                    return "Invalid";
                }

                currentIndex += incrementIndex;
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

        public static bool IsEscapableCharacter(string unquotedInputData, int currentIndex, out int incrementIndex)
        {
            const int NumberOfHexCharacters = 4;
            incrementIndex = 1;
            int nextCharacter = currentIndex + incrementIndex;

            const string EscapableCharacters = "\"\\/bfnrt";
            const string HexCharacters = "0123456789abcdef";

            if (unquotedInputData == null)
            {
                return false;
            }

            if (unquotedInputData[nextCharacter] == 'u' && unquotedInputData.Length > nextCharacter + NumberOfHexCharacters)
            {
                string lowerCaseUnquotedInputData = unquotedInputData.ToLower();

                for (int i = 1; i <= NumberOfHexCharacters; i++)
                {
                    if (HexCharacters.IndexOf(lowerCaseUnquotedInputData[nextCharacter + i]) == -1)
                    {
                        return false;
                    }
                }

                incrementIndex += NumberOfHexCharacters;
                return true;
            }

            incrementIndex++;
            return EscapableCharacters.IndexOf(unquotedInputData[nextCharacter]) >= 0;
        }
    }
}