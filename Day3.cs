using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AoC2024
{
    internal class Day3 : AoC2024
    {
        int total_value = 0;
        bool do_mul = true;
        string regexPattern = @"(mul\(\d{1,3},\d{1,3}\)|(do\(\))|(don\'t\(\)))";
        public override void Solve()
        {
            Day3 day3 = new Day3();
            string[] puzzleInput = day3.ReadPuzzleInput("C:\\Users\\pnl14s5t\\source\\repos\\AoC2024\\input\\Day3.txt");
            int[] numbers = new int[2];
            foreach (string line in puzzleInput)
            {
                //Console.WriteLine("");
                //Console.WriteLine(line);

                foreach (Match match in Regex.Matches(line, regexPattern))
                {
                    if (match.Value.Equals("do()"))
                    {
                        this.do_mul = true;
                    }
                    else if (match.Value.Equals("don't()"))
                    {
                        this.do_mul = false;
                    }
                    else if(this.do_mul)
                    {
                        Console.WriteLine(match.Value);
                        MatchCollection number_matches = Regex.Matches(match.Value, @"\d{1,3}");

                        for (int i = 0; i < number_matches.Count; i++)
                        {
                            Int32.TryParse(number_matches[i].Value, out numbers[i]);
                        }
                        total_value += numbers[0] * numbers[1];
                    }
                }
            }
            Console.WriteLine(total_value);
        }

    }
}
