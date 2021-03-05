using System;

namespace _03.Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> sequence = Console.ReadLine().Split().Select(int.Parse).ToList();

            double average = Math.Round(sequence.Average(), 2);

            if (sequence.Count == 1 || average == sequence[sequence.IndexOf(sequence[0])])
            {
                Console.WriteLine("No");
                return;
            }

            for (int i = 0; i < sequence.Count; i++)
            {
                if (sequence[i] <= average)
                {
                    sequence.RemoveAt(i);
                    i--;
                }
            }

            sequence.Sort();
            sequence.Reverse();
           
            for (int i = 0; i < sequence.Count; i++)
            {
                if (i == 5)
                {
                    break;
                }
                Console.Write(sequence[i] + " ");
            }
        }
    }
}
