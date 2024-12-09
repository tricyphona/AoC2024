using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024
{
    // List aanmaken met alle beacons
    // 
    internal class Day8 : AoC2024
    {
        public override void Solve()
        {
            string[] puzzleInput = this.ReadPuzzleInput("C:\\projects\\AoC2024\\input\\Dag_8.txt");
            //string[] puzzleInput = this.ReadPuzzleInput("C:\\Users\\pnl14s5t\\source\\repos\\AoC2024\\input\\Day8.txt");
            int[,] play_board = new int[puzzleInput.Length, puzzleInput[0].Length];
            HashSet<Coord> beaconOptions = new HashSet<Coord>();
            HashSet<Coord> beaconOptionsPart2 = new HashSet<Coord>();
            Dictionary<char, AntennaCollection> antennas = new Dictionary<char, AntennaCollection>();
            int lengthBoard = puzzleInput.Length;
            int widthBoard = puzzleInput[0].Length;

            for (int i = 0; i < lengthBoard; i++)
            {
                for (int j = 0; j < widthBoard; j++)
                {
                    if (puzzleInput[i][j] == '.')
                    {
                        play_board[i, j] = 0;
                    }
                    else
                    {
                        play_board[i, j] = 1;

                        if (antennas.ContainsKey(puzzleInput[i][j]))
                        {
                            antennas[puzzleInput[i][j]].AddLast(i, j);
                        }
                        else
                        {
                            antennas.Add(puzzleInput[i][j], new AntennaCollection(puzzleInput[i][j], i, j, lengthBoard, widthBoard, play_board));
                        }
                    }
                }
            }
            //foreach (KeyValuePair<char, AntennaCollection> antenna in antennas)
            //{
            //    LinkedList<Coord> antiNodes = antenna.Value.GetAntiNodes();
            //    foreach (Coord antiNode in antiNodes)
            //    {
            //        beaconOptions.Add(antiNode);
            //    }

            //}

            foreach (KeyValuePair<char, AntennaCollection> antenna in antennas)
            {
                LinkedList<Coord> antiNodes = antenna.Value.GetAntiNodesVector();
                foreach (Coord antiNode in antiNodes)
                {
                    beaconOptionsPart2.Add(antiNode);
                }

            }
            //foreach (Coord beaconOption in beaconOptions)
            //{
            //    Console.Write(beaconOption.I);
            //    Console.Write(", ");
            //    Console.WriteLine(beaconOption.J);
            //}
            //Console.WriteLine(beaconOptions.Count);

            foreach (Coord beaconOption in beaconOptionsPart2)
            {
                Console.Write(beaconOption.I);
                Console.Write(", ");
                Console.WriteLine(beaconOption.J);
            }
            Console.WriteLine(beaconOptionsPart2.Count);
        }
    }


    internal class Antenna
    {
        char typeAntenna;
        int i;
        int j;
        int lengthBoard;
        int widthBoard;

        public Antenna(char typeAntenna, int i, int j, int lengthBoard, int widthBoard)
        {
            this.typeAntenna = typeAntenna;
            this.i = i;
            this.j = j;
            this.lengthBoard = lengthBoard;
            this.widthBoard = widthBoard;
        }

        public int I { get => i; set => i = value; }
        public int J { get => j; set => j = value; }

        public Coord GetAntiNode(Antenna other)
        {
            int tempI = this.I - (other.I - this.I);
            int tempJ = this.J - (other.J - this.J);
            return new Coord(tempI, tempJ);
        }
        public LinkedList<Coord> getAntiNodeVector(Antenna other)
        {
            int vectorI = other.I - this.I;
            int vectorJ = other.J - this.J;
            int i = 0;
            LinkedList<Coord> antiNodes = new LinkedList<Coord>();
            while (this.I - i * vectorI >= 0 && this.I - i * vectorI < this.lengthBoard && this.J - i * vectorJ >= 0 && this.J - i * vectorJ < this.widthBoard)
            {
                antiNodes.AddLast(new Coord(this.I - i * vectorI, this.J - i * vectorJ));
                i++;
            }
            while (this.I + i * vectorI >= 0 && this.I + i * vectorI < this.lengthBoard && this.J + i * vectorJ >= 0 && this.J + i * vectorJ < this.widthBoard)
            {
                antiNodes.AddLast(new Coord(this.I + i * vectorI, this.J + i * vectorJ));
                i++;
            }
            return antiNodes;

            
        }
    }

    internal class AntennaCollection
    {
        char typeAntenna;
        LinkedList<Antenna> collectionAntennas = new LinkedList<Antenna>();
        int lengthBoard;
        int widthBoard;
        int[,] play_board;


        public AntennaCollection(char typeAntenna, int i, int j, int lengthBoard, int widthBoard, int[,] play_board)
        {
            this.typeAntenna = typeAntenna;
            this.collectionAntennas.AddLast(new Antenna(typeAntenna, i, j, lengthBoard, widthBoard));
            this.lengthBoard = lengthBoard;
            this.widthBoard = widthBoard;
            this.play_board = play_board;
        }

        public void AddLast(int i, int j)
        {
            this.collectionAntennas.AddLast(new Antenna(this.typeAntenna, i, j, lengthBoard, widthBoard));
        }
        public LinkedList<Antenna> GetAntennas()
        {
            return this.collectionAntennas;
        }

        public LinkedList<Coord> GetAntiNodes()
        {
            LinkedList<Coord> antiNodes = new LinkedList<Coord>();
            foreach (Antenna ant in this.collectionAntennas)
            {
                LinkedListNode<Antenna> antenna = this.collectionAntennas.First;
                while (antenna != null)
                {
                    if (antenna.Value != ant)
                    {
                        Coord temp = ant.GetAntiNode(antenna.Value);
                        if (temp.I >= 0 && temp.I < this.lengthBoard && temp.J >= 0 && temp.J < this.widthBoard)
                        {
                            Console.WriteLine(play_board[temp.I, temp.J]);
                            if (true || play_board[temp.I, temp.J] == 0)
                            {

                                antiNodes.AddLast(temp);
                            }
                        }
                    }
                    antenna = antenna.Next;
                }

            }
            return antiNodes;
        }
        public LinkedList<Coord> GetAntiNodesVector()
        {
            LinkedList<Coord> antiNodes = new LinkedList<Coord>();
            foreach (Antenna ant in this.collectionAntennas)
            {
                LinkedListNode<Antenna> antenna = this.collectionAntennas.First;
                while (antenna != null)
                {
                    if (antenna.Value != ant)
                    {
                        LinkedList<Coord> temp = ant.getAntiNodeVector(antenna.Value);
                        foreach (Coord temp2 in temp)
                        {
                            antiNodes.AddLast(temp2);
                        }
                    }
                    antenna = antenna.Next;
                }

            }
            return antiNodes;
        }
    }

    internal class Coord
    {
        int i;
        int j;

        public int I { get => i; set => i = value; }
        public int J { get => j; set => j = value; }

        public Coord(int i, int j)
        {
            this.i = i;
            this.j = j;
        }

        public override int GetHashCode()
        {
            return i * 1000 + j;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            Coord other = (Coord)obj;
            return this.i == other.i && this.j == other.j;
        }

    }
}

