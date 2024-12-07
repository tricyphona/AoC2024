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
            Console.WriteLine(this.Part1());
        }
        public int Part1()
        {
            int safe_reports = 0;
            string[] puzzle_input = this.ReadPuzzleInput("C:\\Users\\pnl14s5t\\source\\repos\\AoC2024\\input\\Day2.txt");
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
