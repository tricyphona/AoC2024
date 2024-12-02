using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024
{
    internal class Day1 : AoC2024
    {
        new public static void Solve()
        {
            Day1 day1 = new Day1();
            Console.WriteLine(day1.Part1(day1));
            Console.WriteLine(day1.Part2(day1));
            
        }
        public int Part1(Day1 self)
        {
            string[] puzzle_input = self.ReadPuzzleInput("C:\\projects\\AoC2024\\input\\dag_1.txt");
            (List<int>, List<int>) columns = self.DivideInTwoColumns(puzzle_input);
            List<int> left_column = columns.Item1;
            List<int> right_column = columns.Item2;

            List<int> SortedList_left = left_column.OrderBy(o => o).ToList();
            List<int> SortedList_right = right_column.OrderBy(o => o).ToList();
            int difference = 0;
            for (int i = 0; i < SortedList_left.Count; i++)
            {
                difference += Math.Abs(SortedList_left[i] - SortedList_right[i]);
            }
            return difference;
        }
        public int Part2(Day1 self)
        {
            string[] puzzle_input = self.ReadPuzzleInput("C:\\projects\\AoC2024\\input\\dag_1.txt");
            (List<int>, List<int>) columns = self.DivideInTwoColumns(puzzle_input);
            List<int> left_column = columns.Item1;
            List<int> right_column = columns.Item2;

            Dictionary<int, int> right_dict = new Dictionary<int, int>();
            foreach (int number in right_column)
            {
                if (right_dict.ContainsKey(number))
                {
                    right_dict[number] += 1;
                }
                else
                {
                    right_dict.Add(number, 1);
                }
            }
            int solution = 0;
            foreach (int number in left_column)
            {
                if (right_dict.ContainsKey(number))
                {
                    solution += number*right_dict[number];
                }
            }
            return solution;
        }

        public (List<int>, List<int>) DivideInTwoColumns(string[] puzzle_input)
        {
            List<int> left_column = new List<int>();
            List<int> right_column = new List<int>();
            foreach (string line in puzzle_input)
            {
                string[] row = line.Split(' ');
                left_column.Add(Int32.Parse(row[0].Trim()));
                right_column.Add(Int32.Parse(row[row.Length - 1].Trim()));
            }
            return (left_column, right_column);
        }
    }
}
