using System;

namespace _02.ArrayModifier
{
    class Program
    {
        static void Main(string[] args)
        {
             int[] array = Console.ReadLine().Split().Select(int.Parse).ToArray();

            string command = string.Empty;

            while ((command=Console.ReadLine())!="end")
            {
                string[] input = command.Split();
                string commandOne = input[0];

                if (commandOne == "swap")
                {
                    int indexOne = int.Parse(input[1]);
                    int indexTwo = int.Parse(input[2]);

                    int temp = array[indexOne];
                    array[indexOne] = array[indexTwo];
                    array[indexTwo] = temp;
                }
                else if (commandOne == "multiply")
                {
                    int indexOne = int.Parse(input[1]);
                    int indexTwo = int.Parse(input[2]);
                    if (indexOne >= 0 && indexOne <array.Length && indexTwo>=0 && indexTwo<array.Length)
                    {
                    array[indexOne] = array[indexOne] * array[indexTwo];
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (commandOne=="decrease")
                {
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i] = array[i] - 1;
                    }
                }
            }

            Console.WriteLine(string.Join(", ",array));
        }
    }
}
