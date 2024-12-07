using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AoC2024
{
    internal class Day5 : AoC2024
    {

        public override void Solve()
        {
            bool manual = false;
            Dictionary<int, HashSet<int>> hetGroteBoek = new Dictionary<int, HashSet<int>>();
            List<int[]> deGroteManual = new List<int[]>();
            int totaal = 0;
            int totaalCorrected = 0;


            string[] puzzleInput = this.ReadPuzzleInput("C:\\Users\\pnl14s5t\\source\\repos\\AoC2024\\input\\Day5.txt");
            foreach (string line in puzzleInput)
            {
                if (line.Equals(""))
                {
                    manual = true;
                    continue;
                }
                if (!manual)
                {
                    string[] partString = line.Split('|');
                    int[] parts = Array.ConvertAll(partString, s => int.Parse(s));
                    if (hetGroteBoek.ContainsKey(parts[0]))
                    {
                        hetGroteBoek[parts[0]].Add(parts[1]);
                    }
                    else
                    {
                        HashSet<int> temp = new HashSet<int>();
                        temp.Add(parts[1]);
                        hetGroteBoek.Add(parts[0], temp);

                    }
                }
                if (manual)
                {
                    string[] manualString = line.Split(',');
                    int[] manualPages = Array.ConvertAll(manualString, s => int.Parse(s));
                    deGroteManual.Add(manualPages);
                }
            }
            foreach (int[] manualPage in deGroteManual)
            {
                if (CorrectManualOrder(manualPage, hetGroteBoek))
                {
                    totaal += manualPage[manualPage.Length / 2];
                }
                else
                {
                    totaalCorrected += CorrectedManualOrder(manualPage, hetGroteBoek)[manualPage.Length / 2];
                }


            }
            Console.WriteLine(totaal);
            Console.WriteLine(totaalCorrected);
        }
        private bool CorrectManualOrder(int[] manual, Dictionary<int, HashSet<int>> hetGroteBoek)
        {
            for (int i = 0; i < manual.Length; i++)
            {
                int currentPage = manual[i];
                if (hetGroteBoek.ContainsKey(currentPage))
                {
                    if (manual.Take(i).Intersect(hetGroteBoek[currentPage]).Any())
                    {
                        return false;
                    }

                }
                else
                {
                    continue;
                }
            }
            return true;
        }
        private int[] CorrectedManualOrder(int[] manual, Dictionary<int, HashSet<int>> hetGroteBoek)
        {
            for (int i = 0; i < manual.Length; i++)
            {
                int currentPage = manual[i];
                if (hetGroteBoek.ContainsKey(currentPage))
                {
                    if (manual.Take(i).Intersect(hetGroteBoek[currentPage]).Any())
                    {
                        int incorrectPage = manual.Take(i).Intersect(hetGroteBoek[currentPage]).First();
                        int indexIncorrect = Array.IndexOf(manual, incorrectPage);
                        manual[i] = incorrectPage;
                        manual[indexIncorrect] = currentPage;
                        i = indexIncorrect;
                    }

                }
                else
                {
                    continue;
                }
            }
            return manual;
        }
    }
}
