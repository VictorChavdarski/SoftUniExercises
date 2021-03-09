using System;
using System.Linq;
using System.Collections.Generic;

namespace ProblemTwo
{
    class Program
    {
        static void Main(string[] args)
        {
            var dimensions = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int dimensionRow = dimensions[0];
            int dimensionCol = dimensions[1];

            var dictionary = new Dictionary<int, int>();

            int[,] garden = new int[dimensionRow, dimensionCol];

            int count = 0;


            string command;
            while ((command = Console.ReadLine()) != "Bloom Bloom Plow")
            {
                string[] input = command.Split();
                int positionOne = int.Parse(input[0]);
                int positionTwo = int.Parse(input[1]);

                if (!IsSafe(garden, positionOne, positionTwo))
                {
                    Console.WriteLine("Invalid coordinates.");
                    continue;
                }
                else
                {
                    garden[positionOne, positionTwo] = 6;

                }
            }
            for (int row = 0; row < garden.GetLength(0); row++)
            {
                for (int col = 0; col < garden.GetLength(1); col++)
                {
                    if (garden[row, col] == 6)
                    {
                        count++;
                        for (int rowTwo = 0; rowTwo < garden.GetLength(0); rowTwo++)
                        {
                            garden[rowTwo, col]++;
                        }
                        for (int colTwo = 0; colTwo < garden.GetLength(1); colTwo++)
                        {
                            garden[row, colTwo]++;
                        }
                    }
                }
            }
            for (int row = 0; row < garden.GetLength(0); row++)
            {
                for (int col = 0; col < garden.GetLength(1); col++)
                {
                    if (garden[row, col] > 5)
                    {
                        garden[row, col] -= 7;
                    }
                }
            }

            PrintMatric(garden);
        }

        static bool IsSafe(int[,] matrix, int row, int col)
        {
            if (row < 0 || col < 0)
            {
                return false;
            }
            if (row >= matrix.GetLength(0) || col >= matrix.GetLength(1))
            {
                return false;
            }
            return true;
        }

        static void PrintMatric(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
