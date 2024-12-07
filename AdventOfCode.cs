using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024
{
    internal class AdventOfCode
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            //Day1 day1 = new Day1();
            //day1.Solve();
            //Day2 day2 = new Day2();
            //day2.Solve();
            //Day3 day3 = new Day3();
            //day3.Solve();
            //Day4 day4 = new Day4();
            //day4.Solve();
            //Console.ReadLine();
            //Day5 day5 = new Day5();
            //day5.Solve();
            Day6 day6 = new Day6();
            day6.Solve();
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
    }
}
