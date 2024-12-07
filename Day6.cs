using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024
{
    // Op route die hij loopt spul gooien.
    // bij checken, check of zelfde locatie + richting is geweest. Richting moet herleidbaar zijn voor meerdere richtingen op zelfde locatie
    internal class Day6 : AoC2024
    {
        public override void Solve()
        {
            int[] start_positie = new int[2] { 0, 0 };
            string[] puzzleInput = this.ReadPuzzleInput("C:\\projects\\AoC2024\\input\\Dag_6.txt");
            //string[] puzzleInput = this.ReadPuzzleInput("C:\\Users\\pnl14s5t\\source\\repos\\AoC2024\\input\\Day6.txt");
            int[,] play_board = new int[puzzleInput.Length, puzzleInput[0].Length];
            for (int i = 0; i < puzzleInput.Length; i++)
            {
                for (int j = 0; j < puzzleInput[0].Length; j++)
                {
                    if (puzzleInput[i][j] == '.')
                    {
                        play_board[i, j] = 0;
                    }
                    else if (puzzleInput[i][j] == '#')
                    {
                        play_board[i, j] = 1;
                    }
                    else if (puzzleInput[i][j] == '^')
                    {
                        play_board[i, j] = 2;
                        start_positie[0] = i;
                        start_positie[1] = j;
                    }
                }

            }
            int[,] original_play_board = play_board.Clone() as int[,];
            Guard guard = new Guard(start_positie[0], start_positie[1], play_board);

            while (!guard.Walk())
            { }
            Console.WriteLine(guard.GetPlacesVisited());
            //Console.ReadLine();
            List<int[]> potentialBlock = new List<int[]>();
            for (int i = 0; i < puzzleInput.Length; i++)
            {
                for (int j = 0; j < puzzleInput[0].Length; j++)
                {
                    if (play_board[i, j] >= 2)
                    {
                        potentialBlock.Add(new int[2] { i, j });
                    }
                }
            }
            int loopsFound = 0;

            foreach (int[] block in potentialBlock)
            {
                int[,] new_play_board = original_play_board.Clone() as int[,];
                new_play_board[block[0], block[1]] = 1;
                Console.Write(block[0]);
                Console.Write(", ");
                Console.WriteLine(block[1]);
                Guard new_guard = new Guard(start_positie[0], start_positie[1], new_play_board);
                while (new_guard.WalkLoop() == 0)
                { }
                if (new_guard.GetLoop())
                {
                    loopsFound++;
                    Console.Write(block[0]);
                    Console.Write(", ");
                    Console.WriteLine(block[1]);
                }

            }
            Console.WriteLine(loopsFound);
            Console.ReadLine();
        }
    } 
    internal class Guard
    {
        int i;
        int j;
        int[,] play_board;
        int places_visited = 1;
        int[] direction = new int[2] { -1, 0 };
        bool loop = false;
        public Guard(int i, int j, int[,] play_board)
        {
            this.i = i;
            this.j = j;
            this.play_board = play_board;
        }
        public bool Walk()
        {
            return this.CheckLocation();
        }
        public int WalkLoop()
        {
            int uitkomst = CheckLoop();
            if (uitkomst == 1)
            {
                this.loop = true;
            }
            return uitkomst;
        }
        public bool GetLoop()
        {
            return this.loop;
        }
        public int GetPlacesVisited()
        {
            return this.places_visited;
        }
        private bool CheckLocation()
        {
            //Console.Write(this.i);
            //Console.Write(", ");
            //Console.WriteLine(this.j);
            if (this.i + this.direction[0] < 0 
                || this.i + this.direction[0] >= this.play_board.GetLength(0)
                || this.j + this.direction[1] < 0 
                || this.j + this.direction[1] >= this.play_board.GetLength(1))
            {
                return true;
            }
            int board_value = this.play_board[this.i + this.direction[0], this.j + this.direction[1]];
                if (board_value == 0)
            {
                int multiplier = 1;
                if (this.direction[0] < 0) { multiplier = 2; }
                else if (this.direction[1] > 0) { multiplier = 3; }
                else if (this.direction[0] > 0) { multiplier = 5; }
                else if (this.direction[1] < 0) { multiplier = 7; }
                else { }
                this.play_board[this.i + this.direction[0], this.j + this.direction[1]] = multiplier;
                //this.play_board[this.i + this.direction[0], this.j + this.direction[1]] = 2;
                this.places_visited++;
                this.i += this.direction[0];
                this.j += this.direction[1];
            }
            else if (board_value >= 2)
            {
                int multiplier = 1;
                if (this.direction[0] < 0) { multiplier = 2; }
                else if (this.direction[1] > 0) { multiplier = 3; }
                else if (this.direction[0] > 0) { multiplier = 5; }
                else if (this.direction[1] < 0) { multiplier = 7; }
                if (this.play_board[this.i + this.direction[0], this.j + this.direction[1]] % multiplier == 0)
                {
                    return true;
                }
                else
                {
                    this.play_board[this.i + this.direction[0], this.j + this.direction[1]] *= multiplier;
                }
                this.i += this.direction[0];
                this.j += this.direction[1];
            }
            else if (board_value == 1)
            {
                int k = this.direction[0];
                int l = this.direction[1];
                this.direction[0] = l;
                this.direction[1] = -k;
            }
            return false;
        }

        private int CheckLoop()
        {
            //Console.Write(this.i);
            //Console.Write(", ");
            //Console.WriteLine(this.j);
            if (this.i + this.direction[0] < 0
                || this.i + this.direction[0] >= this.play_board.GetLength(0)
                || this.j + this.direction[1] < 0
                || this.j + this.direction[1] >= this.play_board.GetLength(1))
            {

                return -1;
                
            }
            int board_value = this.play_board[this.i + this.direction[0], this.j + this.direction[1]];
            if (board_value == 0)
            {
                int multiplier = 1;
                if (this.direction[0] < 0) { multiplier = 2; }
                else if (this.direction[1] > 0) { multiplier = 3; }
                else if (this.direction[0] > 0) { multiplier = 5; }
                else if (this.direction[1] < 0) { multiplier = 7; }
                else { }
                this.play_board[this.i + this.direction[0], this.j + this.direction[1]] = multiplier;
                //this.play_board[this.i + this.direction[0], this.j + this.direction[1]] = 2;
                this.places_visited++;
                this.i += this.direction[0];
                this.j += this.direction[1];
            }
            else if (board_value >= 2)
            {
                int multiplier = 1;
                if (this.direction[0] < 0) { multiplier = 2; }
                else if (this.direction[1] > 0) { multiplier = 3; }
                else if (this.direction[0] > 0) { multiplier = 5; }
                else if (this.direction[1] < 0) { multiplier = 7; }
                if (this.play_board[this.i + this.direction[0], this.j + this.direction[1]] % multiplier == 0)
                {

                    return 1;
                }
                else
                {
                    this.play_board[this.i + this.direction[0], this.j + this.direction[1]] *= multiplier;
                }
                this.i += this.direction[0];
                this.j += this.direction[1];
            }
            else if (board_value == 1)
            {
                int k = this.direction[0];
                int l = this.direction[1];
                this.direction[0] = l;
                this.direction[1] = -k;
            }
            return 0;
        }
    }
}

