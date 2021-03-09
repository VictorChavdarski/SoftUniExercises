using System;

namespace _04._02BeeDescription
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            char[,] field = new char[n, n];

            int beeRow = -1;
            int beeCol = -1;

            int flowersCount = 0;


            for (int row = 0; row < n; row++)
            {
                string line = Console.ReadLine();
                for (int col = 0; col < line.Length; col++)
                {
                    field[row, col] = line[col];
                    if (field[row, col] == 'B')
                    {
                        beeRow = row;
                        beeCol = col;
                    }
                }
            }

            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                field[beeRow, beeCol] = '.';
                if (command == "up")
                {

                    beeRow--;
                }
                else if (command == "down")
                {

                    beeRow++;
                }
                else if (command == "left")
                {

                    beeCol--;
                }
                else if (command == "right")
                {

                    beeCol++;
                }
                if (!IsSafe(field, beeRow, beeCol))
                {
                    Console.WriteLine("The bee got lost!");
                    break;
                }

                if (field[beeRow, beeCol] == 'O')
                {
                    field[beeRow, beeCol] = '.';
                    if (command == "up")
                    {
                        beeRow--;
                    }
                    else if (command == "down")
                    {
                        beeRow++;
                    }
                    else if (command == "left")
                    {
                        beeCol--;
                    }
                    else if (command == "right")
                    {
                        beeCol++;
                    }
                    if (field[beeRow, beeCol] == 'f')
                    {
                        field[beeRow, beeCol] = '.';
                        flowersCount++;
                    }
                }

                if (field[beeRow, beeCol] == 'f')
                {
                    flowersCount++;
                }

                field[beeRow, beeCol] = 'B';
            }
            if (flowersCount < 5)
            {
                Console.WriteLine($"The bee couldn't pollinate the flowers, she needed {5 - flowersCount} flowers more");
            }
            else
            {
                Console.WriteLine($"Great job, the bee managed to pollinate {flowersCount} flowers!");
            }
            PrintField(field);
        }

        static bool IsSafe(char[,] field, int row, int col)
        {
            if (row < 0 || col < 0)
            {
                return false;
            }
            if (row >= field.GetLength(0) || col >= field.GetLength(1))
            {
                return false;
            }
            return true;
        }
        static void PrintField(char[,] field)
        {
            for (int row = 0; row < field.GetLength(0); row++)
            {
                for (int col = 0; col < field.GetLength(1); col++)
                {
                    Console.Write(field[row, col]);
                }
                Console.WriteLine();
            }
        }
    }
}
