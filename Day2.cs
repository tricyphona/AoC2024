using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024
{
    internal class Day2 : AoC2024
    {
        public override void Solve()
        {
            Day2 day2 = new Day2();
            Console.WriteLine(day2.Part1(day2));
 //Console.WriteLine(day1.Part2(day1));
        }
        public int Part1(Day2 self)
        {
            int safe_reports = 0;
            string[] puzzle_input = self.ReadPuzzleInput("C:\\projects\\AoC2024\\input\\dag_2.txt");
            List<Report> reports = new List<Report>();
            foreach (string line in puzzle_input)
            {
                string[] row = line.Split(' ');
                List<int> reports_ints = new List<int>();
                foreach (string number in row)
                {
                    int.TryParse(number, out int parsed_number);
                    reports_ints.Add(parsed_number);
                }
                reports.Add(new Report(reports_ints));
            }

            foreach(Report report in reports)
            {
                if (report.IsSafe() == true)
                {
                    safe_reports++;
                }
            }
            return safe_reports;
        }

    }
}
