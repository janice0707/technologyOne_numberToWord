using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Helpers
{
    public static class NumberHelper
    {
        public static string ConvertNumberToWords(decimal inputValue)
        {
            String[] multiplier = { "", "Thousand", "Million",
                                "Billion", "Trillion" };


            string s = inputValue.ToString("0.00", CultureInfo.InvariantCulture);
            string[] parts = s.Split('.');

            string parts1 = parts[0];
            string parts2 = parts[1];

            int loopNumberCount = parts1.Length;
            int loopCentsCount = parts2.Length;

            int timesLoop = 0;
            int multiplierCount = 0;
            string currentMultiplier = "";

            string appendPart1 = "";
            string outputString = "";
            int appendNumber = 0;

            if (loopNumberCount == 1)
            {
                int singleNum = Convert.ToInt32(parts1);

                if (singleNum == 0)
                {
                    outputString = "Zero";
                }
                else
                {
                    outputString = SingleControl(singleNum);
                }
            }
            else
            {
                for (int i = loopNumberCount - 1; i >= 0; i = --i)
                {
                    timesLoop++;

                    appendPart1 = parts1[i].ToString() + appendPart1;
                    appendNumber = Convert.ToInt32(appendPart1);
                    //currentNumber = Convert.ToInt32(parts1[i].ToString());

                    int prevNumber = 0;

                    if (i > 0)
                    {
                        prevNumber = Convert.ToInt32(parts1[i - 1].ToString());
                    }

                    if (timesLoop == 1)
                    {
                        if (prevNumber != 1)
                        {
                            outputString = SingleControl(appendNumber) + outputString;
                        }
                    }

                    if (timesLoop == 2)
                    {
                        outputString = TensControl(appendNumber) + outputString;
                    }

                    if (timesLoop == 3)
                    {
                        string hundredOutput = HundredsControl(appendNumber);

                        int numberBeforeCurrentIndexInt = GetNumbersBeforeIndexFromString(parts1, i);
                        int numberAfterCurrentIndexInt = GetNumbersAfterIndexFromString(parts1, i + 1);
                        string andString = "";

                        if (numberAfterCurrentIndexInt > 0)
                        {
                            andString = "And";
                        }


                        outputString = !string.IsNullOrEmpty(hundredOutput) ? HundredsControl(appendNumber) + "Hundred" + andString + outputString : outputString;

                        timesLoop = 0;
                        multiplierCount++;
                        currentMultiplier = multiplier[multiplierCount];
                        appendPart1 = "";


                        if (i > 0)
                        {

                            if (currentMultiplier == "Thousand") // check if thousand part have value
                            {

                                if (numberBeforeCurrentIndexInt > 0)
                                {
                                    outputString = currentMultiplier + andString + outputString;
                                }
                            }
                            else
                            {
                                outputString = currentMultiplier + andString + outputString;
                            }
                        }


                    }

                }

            }

            string val = string.Concat(outputString.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ') + " Dollars";


            int centsNumber = Convert.ToInt32(parts2);

            if (centsNumber > 0)
            {
                val = val + " And " + ConvertDecimalToWords(parts2);
            }

            return val.ToUpper();
        }

        public static string ConvertDecimalToWords(string inputValue)
        {
            int loopCentsCount = inputValue.Length;

            int timesLoop = 0;
            int multiplierCount = 0;
            string currentMultiplier = "";

            string appendPart1 = "";
            string outputString = "";
            int appendNumber = 0;

            if (loopCentsCount == 1)
            {
                int singleNum = Convert.ToInt32(inputValue);

                if (singleNum == 0)
                {
                    outputString = "Zero";
                }
                else
                {
                    outputString = SingleControl(singleNum);
                }
            }
            else
            {
                for (int i = loopCentsCount - 1; i >= 0; i = --i)
                {
                    timesLoop++;

                    appendPart1 = inputValue[i].ToString() + appendPart1;
                    appendNumber = Convert.ToInt32(appendPart1);
                    //currentNumber = Convert.ToInt32(parts1[i].ToString());

                    int prevNumber = 0;

                    if (i > 0)
                    {
                        prevNumber = Convert.ToInt32(inputValue[i - 1].ToString());
                    }

                    if (timesLoop == 1)
                    {
                        if (prevNumber != 1)
                        {
                            outputString = SingleControl(appendNumber) + outputString;
                        }
                    }

                    if (timesLoop == 2)
                    {
                        outputString = TensControl(appendNumber) + outputString;
                    }

                }

            }

            string val = string.Concat(outputString.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ') + " Cents";

            return val.ToUpper();
        }

        public static int GetNumbersAfterIndexFromString(string parts1, int i)
        {
            //check if 3 digits after got numbers

            int numberAfterCurrentIndexCount = parts1.Substring(i).Length;
            string numberAfterCurrentIndex = "";

            if (numberAfterCurrentIndexCount > 2)
            {
                numberAfterCurrentIndex = parts1.Substring(i, 2);
            }
            else
            {
                numberAfterCurrentIndex = parts1.Substring(i);
            }

            int numberAfterCurrentIndexInt = !string.IsNullOrEmpty(numberAfterCurrentIndex) ? Convert.ToInt32(numberAfterCurrentIndex) : 0;

            return numberAfterCurrentIndexInt;
        }

        public static int GetNumbersBeforeIndexFromString(string parts1, int i)
        {
            //check if 3 digits before got numbers
            int numberBeforeCurrentIndexCount = parts1.Substring(0, i).Length;
            string numberBeforeCurrentIndex = "";

            if (numberBeforeCurrentIndexCount > 3)
            {
                numberBeforeCurrentIndex = parts1.Substring((i - 3), 3);
            }
            else
            {
                numberBeforeCurrentIndex = parts1.Substring(0, i);
            }

            int numberBeforeCurrentIndexInt = !string.IsNullOrEmpty(numberBeforeCurrentIndex) ? Convert.ToInt32(numberBeforeCurrentIndex) : 0;

            return numberBeforeCurrentIndexInt;
        }

        public static string SingleControl(int singeValueInt)
        {
            String[] first_twenty = {
                "",        "One",       "Two",      "Three",
                "Four",    "Five",      "Six",      "Seven",
                "Eight",   "Nine",      "Ten",      "Eleven",
                "Twelve",  "Thirteen",  "Fourteen", "Fifteen",
                "Sixteen", "Seventeen", "Eighteen", "Nineteen"
            };

            string output = "";

            if (singeValueInt > 0)
            {
                output = first_twenty[singeValueInt];
            }

            return output;
        }

        public static string TensControl(int tenValueInt)
        {
            String[] first_twenty = {
                "",        "One",       "Two",      "Three",
                "Four",    "Five",      "Six",      "Seven",
                "Eight",   "Nine",      "Ten",      "Eleven",
                "Twelve",  "Thirteen",  "Fourteen", "Fifteen",
                "Sixteen", "Seventeen", "Eighteen", "Nineteen"
            };

            String[] tens = { "", "Twenty", "Thirty",
                          "Forty",   "Fifty",  "Sixty",
                          "Seventy", "Eighty", "Ninety" };

            string output = "";

            if (tenValueInt >= 10)
            {
                if (tenValueInt < 20)
                {
                    output = first_twenty[tenValueInt];
                }

                if (tenValueInt >= 20)
                {
                    int ten_index = (tenValueInt / 10) - 1;
                    //int ten_remaining_index = (tenValueInt % 10);

                    output = tens[ten_index];

                    //if (ten_remaining_index > 0)
                    //{
                    //    output = output + first_twenty[ten_remaining_index];
                    //}
                }
            }

            return output;
        }

        public static string HundredsControl(int hundredValueInt)
        {
            String[] first_twenty = {
                "",        "One",       "Two",      "Three",
                "Four",    "Five",      "Six",      "Seven",
                "Eight",   "Nine",      "Ten",      "Eleven",
                "Twelve",  "Thirteen",  "Fourteen", "Fifteen",
                "Sixteen", "Seventeen", "Eighteen", "Nineteen"
            };

            String[] tens = { "", "Twenty", "Thirty",
                          "Forty",   "Fifty",  "Sixty",
                          "Seventy", "Eighty", "Ninety" };

            string output = "";

            if (hundredValueInt > 0 && hundredValueInt >= 100)
            {
                int hundred_index = (hundredValueInt / 100);

                output = first_twenty[hundred_index];
            }

            return output;
        }
    }
}
