using System;
using System.Linq;
using System.Collections.Generic;

namespace _07._01LootBox
{
    class Program
    {
        static void Main(string[] args)
        {
            var firstLootBox = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var secondLootBox = Console.ReadLine().Split().Select(int.Parse).ToArray();

            var firtsQueue = new Queue<int>(firstLootBox);
            var secondStack = new Stack<int>(secondLootBox);

            int loot = 0;

            while (firtsQueue.Count > 0 || secondStack.Count > 0)
            {
                if (firtsQueue.Count == 0)
                {
                    Console.WriteLine("First lootbox is empty");
                    break;
                }
                else if (secondStack.Count == 0)
                {
                    Console.WriteLine("Second lootbox is empty");
                    break;
                }
                var sum = firtsQueue.Peek() + secondStack.Peek();
                if (sum % 2 == 0)
                {
                    loot += sum;
                    firtsQueue.Dequeue();
                    secondStack.Pop();
                }
                else
                {
                    var movedItem = secondStack.Peek();
                    secondStack.Pop();
                    firtsQueue.Enqueue(movedItem);
                }
            }

            if (loot >= 100)
            {
                Console.WriteLine($"Your loot was epic! Value: {loot}");
            }
            else
            {
                Console.WriteLine($"Your loot was poor... Value: {loot}");
            }




        }
    }
}
