using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024
{
    internal class Day4 : AoC2024
    {

        public override void Solve()
        {

            string[] puzzleInput = this.ReadPuzzleInput("C:\\Users\\pnl14s5t\\source\\repos\\AoC2024\\input\\Day4.txt");
            char[,] charArrayPuzzle = new char[puzzleInput.Length, puzzleInput[0].Length];
            for (int i = 0; i < puzzleInput.Length; i++)
            {

                for (int j = 0; j < puzzleInput[0].Length; j++)
                {
                    charArrayPuzzle[i, j] = puzzleInput[i][j];
                }
            }

            int totalXmas = 0;
            int totalXmasDiag = 0;
            for (int i = 0; i < puzzleInput.Length; i++)
            {

                for (int j = 0; j < puzzleInput[0].Length; j++)
                {
                    if (charArrayPuzzle[i, j] == 'X')
                    {
                        totalXmas += startCheckXmas(i, j, charArrayPuzzle);
                    }
                    if (charArrayPuzzle[i,j] == 'A')
                    {
                        if (Xmas(i, j, charArrayPuzzle)){
                            totalXmasDiag++;
                        }
                        
                    }
                }
            }
            Console.Write("Aantal keer Xmas: ");
            Console.WriteLine(totalXmas);
            Console.Write("Aantal keer X-mas: ");
            Console.WriteLine(totalXmasDiag);
        }
        private int startCheckXmas(int i, int j, char[,] arrayPuzzle)
        {
            int xmasFound = 0;
            int[] direction;

            for (int k = -1; k < 2; k++)
            {
                for (int l = -1; l < 2; l++)
                {
                    direction = new int[] { k, l };
                    if (CheckXmas(i + k, j + l, arrayPuzzle, 'M', direction))
                    {
                        xmasFound++;
                    }
                }
            }
            return xmasFound;
        }
        private bool CheckXmas(int i, int j, char[,] arrayPuzzle, char target, int[] direction = null)
        {
                       
            if (target == 'M')
            {
                if(i < arrayPuzzle.GetLength(0) && i >= 0 && j >= 0 && j < arrayPuzzle.GetLength(1))
                {
                    if (arrayPuzzle[i, j] == 'M')
                    {
                        return CheckXmas(i + direction[0], j + direction[1], arrayPuzzle, 'A', direction);
                    }
                    
                }
            }
            else if (target == 'A')
            {
                if (i < arrayPuzzle.GetLength(0) && i >= 0 && j >= 0 && j < arrayPuzzle.GetLength(1))
                {
                    if (arrayPuzzle[i, j] == 'A')
                    {
                        return CheckXmas(i + direction[0], j + direction[1], arrayPuzzle, 'S', direction);
                    }
                }
            }
            else if (target == 'S')
            {
                if (i < arrayPuzzle.GetLength(0) && i >= 0 && j >= 0 && j < arrayPuzzle.GetLength(1))
                {if (arrayPuzzle[i, j] == 'S') { return true; }
                    
                }
            }

            // Add logic here to ensure all code paths return a value
            // For now, returning false as a placeholder
            return false;
        }

        private bool Xmas(int i, int j, char[,] arrayPuzzle)
        {
            bool diagonalLeft = false;
            bool diagonalRight = false;
            if (i+1 < arrayPuzzle.GetLength(0) && i-1 >= 0 && j-1 >= 0 && j+1 < arrayPuzzle.GetLength(1))
            {
                if(arrayPuzzle[i-1, j-1] == 'M')
                {
                    diagonalLeft = arrayPuzzle[i + 1, j + 1] == 'S';
                }
                else if (arrayPuzzle[i-1, j-1] == 'S') {
                    diagonalLeft = arrayPuzzle[i + 1, j + 1] == 'M';
                }

                if (arrayPuzzle[i+1, j-1] == 'M') {
                    diagonalRight = arrayPuzzle[i - 1, j + 1] == 'S';
                }

                else if (arrayPuzzle[i+1, j-1] == 'S') {
                    diagonalRight = arrayPuzzle[i - 1, j + 1] == 'M';
                }
            }
            return diagonalLeft && diagonalRight;

        }
    }
}
