using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024
{
    internal class Day7 : AoC2024
    {
        public override void Solve()
        {
            Int64 sumSolutions = 0;
            string[] puzzleInput = this.ReadPuzzleInput("C:\\projects\\AoC2024\\input\\dag_7.txt");
            foreach (string line in puzzleInput)
            {
                Int64 number;
                Int64 solution;

                string[] stuff = line.Split(':');
                Int64.TryParse(stuff[0].Trim(), out solution);
                Console.WriteLine(solution);
                string[] options = stuff[1].Split(' ');
                LinkedList<Int64> values = new LinkedList<Int64>();
                foreach (string option in options)
                {
                    Int64.TryParse(option, out number);
                    if (number != 0)
                    {
                        values.AddLast(number);
                    }

                }
                LinkedList<Int64> possibleValues = new LinkedList<Int64>();
                foreach (Int64 value in values)
                {
                    if (possibleValues.First == null)
                    {
                        possibleValues.AddLast(value);
                    }
                    else
                    {
                        LinkedList<Int64> tempList = new LinkedList<Int64>();
                        foreach (Int64 possibleValue in possibleValues)
                        {
                            tempList.AddLast(possibleValue + value);
                            tempList.AddLast(possibleValue * value);
                            tempList.AddLast(Int64.Parse(possibleValue.ToString() + value.ToString()));

                        }
                        possibleValues = tempList;
                    }
                }
                if (possibleValues.Contains(solution))
                {
                    Console.WriteLine("Solution found");
                    sumSolutions += solution;
                }
            }
            Console.WriteLine(sumSolutions);
            Console.ReadLine();
        }
    }
}
