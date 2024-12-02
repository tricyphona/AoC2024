using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024
{
    abstract internal class AoC2024
    {
        public string[] ReadPuzzleInput(string path)
        {
            return System.IO.File.ReadAllLines(path);
            
        }
        abstract public void Solve();
    }
}
