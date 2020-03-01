using System;
using System.Collections.Generic;

namespace ChequeWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                
                try
                {
                    Console.WriteLine("Enter value:");
                    var input = Console.ReadLine();
                    var ans = new Solution().ConvertToWords(input);
                    Console.WriteLine(ans);
                    Console.WriteLine("Press Q to quit, other key to continue");
                    if (Console.ReadKey().Key == ConsoleKey.Q)
                    {
                        break;
                    } else
                    {
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Enter valid number (e.g: 123.45)");
                }
            }
            
        }
    }

    public class Solution
    {
        static string[] ONE_TO_NINETEEN = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

        static string[] TENTHS = { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        static string HUNDRED = "hundred";

        static string THOUSAND = "thousand";

        static string MILLION = "million";

        static string BILLION = "billion";

        public string ConvertToWords(string input)
        {
            var digits = int.Parse(input.Split('.')[0]);
            var decimalPlaces = int.Parse(input.Split('.')[1]);

            var strs = ConvertDigits(digits, new List<string>());
            
            strs.Add("DOLLARS");
            if (decimalPlaces != 0)
            {
                strs.Add("AND");
                strs.Add(ConvertTwoDigitsNumber(decimalPlaces));
                strs.Add("CENTS");
            }

            strs.RemoveAll(x => string.IsNullOrEmpty(x));
            return string.Join(' ', strs).Trim();
        }

        public List<string> ConvertDigits(int number, List<string> strs)
        {
            if (number < 100)
            {
                strs.Add(ConvertTwoDigitsNumber(number));
                return strs;
            }

            if (number < Math.Pow(10, 3))
            {
                var hundred = number / 100 % 10;
                var units = number % 100;

                if (units == 0)
                {
                    strs.Add($"{ONE_TO_NINETEEN[hundred]} {HUNDRED}");
                } else
                {
                    strs.Add($"{ONE_TO_NINETEEN[hundred]} {HUNDRED} and");
                }
                
                ConvertDigits(units, strs);
                return strs;
            }

            if (number < Math.Pow(10, 6))
            {
                var thousands = number / 1000;
                var hundreds = number % 1000;
                ConvertDigits(thousands, strs);

                if (hundreds == 0)
                {
                    strs.Add($"{THOUSAND}");
                } else
                {
                    strs.Add($"{THOUSAND},");
                }
                
                ConvertDigits(hundreds, strs);
                return strs;
            }

            if (number < Math.Pow(10, 9))
            {
                var millions = number / Math.Pow(10, 6);
                var thousands = number % Math.Pow(10, 6);
                ConvertDigits((int)millions, strs);

                if (thousands == 0)
                {
                    strs.Add($"{MILLION}");
                } else
                {
                    strs.Add($"{MILLION},");
                }
                
                ConvertDigits((int)thousands, strs);
                return strs;
            }

            if (number < Math.Pow(10, 12))
            {
                var billions = number / Math.Pow(10, 9);
                var millions = number % Math.Pow(10, 9);
                ConvertDigits((int)billions, strs);

                if (millions == 0)
                {
                    strs.Add($"{BILLION}");
                } else
                {
                    strs.Add($"{BILLION},");
                }
                
                ConvertDigits((int)millions, strs);
                return strs;
            }
            return strs;
        }

        public string ConvertTwoDigitsNumber(int number)
        {
            var ten = number / 10 % 10;
            var unit = number % 10;

            if (number == 10) return ONE_TO_NINETEEN[number];
            if (number % 10 == 0) return TENTHS[ten];

            if (number < 20)
            {
                return ONE_TO_NINETEEN[number];
            }
            
            return $"{TENTHS[ten]} {ONE_TO_NINETEEN[unit]}";
        }
    }
}
