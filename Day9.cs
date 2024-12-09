using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2024
{
    internal class Day9 : AoC2024
    {
        public override void Solve()
        {
            //string[] puzzleInput = this.ReadPuzzleInput("C:\\projects\\AoC2024\\input\\Day9.txt");
            string[] puzzleInput = this.ReadPuzzleInput("C:\\Users\\pnl14s5t\\source\\repos\\AoC2024\\input\\Day9_test.txt");
            LinkedList<Storage> totalStorage = new LinkedList<Storage>();
            Console.WriteLine(puzzleInput[0]);
            bool flag_file = true;
            Int64 storageId = 0;
            Int64 start_id = 0;
            Int64 end_id = 0;
            foreach (char letter in puzzleInput[0])
            {
                if (flag_file)
                {
                    end_id = start_id + Int64.Parse(letter.ToString());
                    totalStorage.AddLast(new Storage(start_id, end_id, storageId, end_id - start_id, !flag_file));
                    start_id = end_id;
                    flag_file = false;
                    storageId++;
                }
                else
                {
                    end_id = start_id + Int64.Parse(letter.ToString());
                    totalStorage.AddLast(new Storage(start_id, end_id, -1, end_id - start_id, !flag_file));
                    start_id = end_id;
                    flag_file = true;
                }
            }
            LinkedListNode<Storage> storageBlock = totalStorage.First;
            while (storageBlock != null)
            {
                if (storageBlock.Value.empty)
                {
                    while (storageBlock.Value.length > 0)
                    {
                        while (totalStorage.Last.Value.empty)
                        {
                            totalStorage.RemoveLast();
                        }
                        Storage tempStorage = totalStorage.Last.Value;
                        
                        if (tempStorage.length <= storageBlock.Value.length)
                        {
                            totalStorage.AddBefore(storageBlock, new Storage(storageBlock.Value.startId, storageBlock.Value.startId + tempStorage.length, tempStorage.storageId, tempStorage.length, false));
                            storageBlock.Value.length -= tempStorage.length;
                            storageBlock.Value.startId += tempStorage.length;
                            totalStorage.RemoveLast();
                        }
                        else
                        {
                            totalStorage.AddBefore(storageBlock, new Storage(storageBlock.Value.startId, storageBlock.Value.endId, tempStorage.storageId, storageBlock.Value.length, false));
                            tempStorage.length -= storageBlock.Value.length;
                            tempStorage.endId -= storageBlock.Value.length;
                            storageBlock.Value.length = 0;
                        }
                    }
                    LinkedListNode<Storage> oldStorageBlock = storageBlock;
                    storageBlock = storageBlock.Next;
                    totalStorage.Remove(oldStorageBlock);

                }
                else
                {
                    storageBlock = storageBlock.Next;
                }
            }

            //foreach(Storage storage in totalStorage)
            //{
            //    Console.WriteLine(storage.startId + " " + storage.endId + " " + storage.storageId + " " + storage.empty);
            //}
            Int64 total = 0;
            foreach (Storage storage in totalStorage)
            {
                if (storage.storageId > 0)
                {
                    for (Int64 i = storage.startId; i < storage.endId; i++)
                    {
                        total += i * storage.storageId;
                        //Console.WriteLine(total);
                    }
                }
            }
            //Console.WriteLine(total);
            LinkedList<Storage> storagesPart2 = new LinkedList<Storage>();
            flag_file = true;
            storageId = 0;
            start_id = 0;
            end_id = 0;
            foreach (char letter in puzzleInput[0])
            {
                if (flag_file)
                {
                    end_id = start_id + Int64.Parse(letter.ToString());
                    storagesPart2.AddLast(new Storage(start_id, end_id, storageId, end_id - start_id, !flag_file));
                    start_id = end_id;
                    flag_file = false;
                    storageId++;
                }
                else
                {
                    end_id = start_id + Int64.Parse(letter.ToString());
                    storagesPart2.AddLast(new Storage(start_id, end_id, -1, end_id - start_id, !flag_file));
                    start_id = end_id;
                    flag_file = true;
                }
            }
            

            storageBlock = storagesPart2.Last;
            while (storageBlock != storagesPart2.First)
            {
                if (!storageBlock.Value.empty)
                {
                    Int64 lengthNeeded = storageBlock.Value.length;
                    LinkedListNode<Storage> possibleOption = storagesPart2.First;
                    bool looking = true;
                    bool moved = false;
                    while (looking && possibleOption != null)
                    {
                        if (possibleOption.Value.empty && possibleOption.Value.length >= lengthNeeded)
                        {
                            moved = true;
                            looking = false;
                            storagesPart2.AddBefore(possibleOption, new Storage(possibleOption.Value.startId, possibleOption.Value.startId + lengthNeeded, storageBlock.Value.storageId, lengthNeeded, false));
                            possibleOption.Value.length -= lengthNeeded;
                            possibleOption.Value.startId += lengthNeeded;
                            if (possibleOption.Value.length == 0)
                            {
                                storagesPart2.Remove(possibleOption.Value);
                            }
                        }
                        else
                        {
                            possibleOption = possibleOption.Next;
                        }
                    }
                    if (moved)
                    {
                        
                        bool emptyAfter = false;
                        bool emptyBefore = false;
                        if (storageBlock.Next != null && storageBlock.Next.Value.empty)
                        {
                            emptyAfter = true;
                        }
                        
                        if (storageBlock.Next != null && storageBlock.Previous.Value.empty)
                        {
                            emptyBefore = true;
                        }
                        if (emptyAfter && emptyBefore) {
                            LinkedListNode<Storage> newEmpty = storageBlock.Previous;
                            newEmpty.Value.endId = storageBlock.Next.Value.endId;
                            newEmpty.Value.length += storageBlock.Value.length + storageBlock.Next.Value.length;
                            storagesPart2.Remove(storageBlock.Next);

                        }

                        else if (emptyAfter) {
                            storageBlock.Next.Value.startId = storageBlock.Value.startId;
                            storageBlock.Next.Value.length += storageBlock.Value.length;
                        }
                        else if (emptyBefore) {
                            storageBlock.Previous.Value.endId = storageBlock.Value.endId;
                            storageBlock.Previous.Value.length += storageBlock.Value.length;
                        }

                        storagesPart2.Remove(storageBlock);
                        
                    }
                }
                if (storageBlock.Previous != null)
                {
                    storageBlock = storageBlock.Previous;
                }
                else
                {
                    storageBlock = storagesPart2.Last;
                }
            }
            foreach(Storage storage in storagesPart2)
            {
                Console.WriteLine(storage.startId + " " + storage.endId + " " + storage.storageId + " " + storage.empty);
            }

        }

        
    internal class Storage
        {
            public Int64 startId;
            public Int64 endId;
            public Int64 storageId;
            public Int64 length;
            public bool empty;

            public Storage(Int64 start_id, Int64 end_id, Int64 storage_id, Int64 length, bool empty)
            {
                this.startId = start_id;
                this.endId = end_id;
                this.storageId = storage_id;
                this.empty = empty;
                this.length = length;
            }
        }
    }
}
