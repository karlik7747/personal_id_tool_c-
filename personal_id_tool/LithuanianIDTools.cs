using System;
using System.Collections.Generic;
using System.Text;

namespace IDTools
{
    public static class LithuanianIDTools
    {
        private static Random rnd;
        // Public methods:
        static LithuanianIDTools()
        {
            rnd = new Random();
        }
        public static string GenerateValidID()
        {
            while (true)
            {
                List<int> generatedID = new List<int> { rnd.Next(1, 7) };
                generatedID
                    .UnpackAndAddValues(StringToIntList(rnd.Next(100).ToString("D2"))) /* Random year between 0 and 99 */
                    .UnpackAndAddValues(StringToIntList(rnd.Next(13).ToString("D2"))) /* Random month between 0 and 12 */
                    .UnpackAndAddValues(StringToIntList(rnd.Next(32).ToString("D2"))) /* Random day between 0 and 31 */
                    .UnpackAndAddValues(StringToIntList(rnd.Next(1000).ToString("D3"))); /* Random sequence number between 0 and 999 */
                if (IsDateCorrect(generatedID))
                {
                    generatedID.Add(GetCheckNumber(generatedID));
                    return String.Join("", generatedID);
                }
            }
        }
        public static List<string> GetCorrectCodes(string text)
        {
            List<string> correctCodes = new List<string>();
            List<string> possibleCodes = GetPossibleCodes(text);
            foreach (string possibleCode in possibleCodes)
            {
                if (IsCodeCorrect(possibleCode))
                    correctCodes.Add(possibleCode);
            }
            return correctCodes;
        }
        // ------------------------------------------------------------------------------------------------------------------------------------
        // Private methods:
        static string GetDigitsOnly(string word)
        {
            var filteredWord = new StringBuilder();
            foreach (char letter in word)
            {
                if (char.IsDigit(letter))
                {
                    filteredWord.Append(letter);
                }
            }
            return filteredWord.ToString();
        }

        static List<string> GetPossibleCodes(string text)
        {
            List<string> possibleCodes = new List<string>();
            List<string> words = new List<string>(text.Split(' '));
            foreach (string word in words)
            {
                string newWord = GetDigitsOnly(word);
                if (newWord.Length == 11)
                {
                    possibleCodes.Add(newWord);
                }
            }

            return possibleCodes;
        }
        static int IntFromDigits(int num1, int num2)
        {
            return num1 * 10 + num2;
        }
        static bool IsDateCorrect(List<int> code)
        {
            // If first digits is from 1 to 6:
            if (code[0] > 0 || code[0] < 7)
            {
                int year;
                switch (code[0])
                {
                    case 1:
                    case 2:
                        year = 1800;
                        break;
                    case 3:
                    case 4:
                        year = 1900;
                        break;
                    default:
                        year = 2000;
                        break;
                }
                year += IntFromDigits(code[1], code[2]);
                string[] dateElements =
                {
                    year.ToString(), /* Year */
                    IntFromDigits(code[3], code[4]).ToString(), /* Month */
                    IntFromDigits(code[5], code[6]).ToString() /* Day */
                };

                string dateString = String.Join("-", dateElements);

                // Try to parse date. This returns bool value if date can be parsed:
                return DateTime.TryParse(dateString, out _);
            }

            return false;
        }
        static int GetSumMod(List<int> code, List<int> s)
        {
            int checkSum = 0;
            for (int i = 0; i < 10; i++)
            {
                checkSum += code[i] * s[i];
            }
            return checkSum % 11;
        }
        static int GetCheckNumber(List<int> code)
        {
            List<int> valueSet1 = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1 };
            List<int> valueSet2 = new List<int> { 3, 4, 5, 6, 7, 8, 9, 1, 2, 3 };
            int k = GetSumMod(code, valueSet1);
            if (k < 10)
            {
                return k;
            }
            else
            {
                k = GetSumMod(code, valueSet2);
                if (k < 10)
                {
                    return k;
                }
                else
                {
                    return 0;
                }
            }

        }
        static List<int> StringToIntList(string word)
        {
            List<int> values = new List<int>(11);
            foreach (char letter in word)
            {
                values.Add(int.Parse(letter.ToString()));
            }
            return values;
        }

        static bool IsCodeCorrect(string code)
        {
            List<int> codeValues = StringToIntList(code);
            return IsDateCorrect(codeValues) && (GetCheckNumber(codeValues) == codeValues[10]);
        }

        static List<int> UnpackAndAddValues(this List<int> currList, List<int> additionalValues)
        {
            foreach (int additionalValue in additionalValues)
                currList.Add(additionalValue);
            return currList;
        }
    }
}