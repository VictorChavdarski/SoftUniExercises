using System;
using System.Collections.Generic;
using System.Linq;

namespace _05._01Bombs
{
    class Program
    {
        static void Main(string[] args)
        {
            var bombsEffects = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
            var bombsCasing = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();

            var bombEffectQueue = new Queue<int>(bombsEffects);
            var bombsCasingStack = new Stack<int>(bombsCasing);

            bool isTrue = true;
            var createdBombsCount = new Dictionary<string, int>()
            {
                {"Datura Bombs",0},
                {"Cherry Bombs",0 },
                {"Smoke Decoy Bombs",0}
            };

            var materialsTable = new Dictionary<int, string>()
            {
                {40,"Datura bombs"},
                {60, "Cherry bombs"},
                {120, "Smoke decoy bombs"}
            };

            while (bombsCasingStack.Count > 0 && bombEffectQueue.Count > 0)
            {
                if (createdBombsCount["Datura Bombs"] >= 3 && createdBombsCount["Cherry Bombs"] >= 3 && createdBombsCount["Smoke Decoy Bombs"] >= 3)
                {
                    Console.WriteLine("Bene! You have successfully filled the bomb pouch!");
                    isTrue = false;
                    break;
                }
                var bombEffect = bombEffectQueue.Peek();
                var bombCasing = bombsCasingStack.Peek();
                var result = bombEffect + bombCasing;

                if (materialsTable.ContainsKey(result))
                {
                    if (result == 40)
                    {
                        createdBombsCount["Datura Bombs"]++;
                    }
                    else if (result == 60)
                    {
                        createdBombsCount["Cherry Bombs"]++;
                    }
                    else
                    {
                        createdBombsCount["Smoke Decoy Bombs"]++;
                    }
                    bombEffectQueue.Dequeue();
                    bombsCasingStack.Pop();
                }
                else
                {
                    int caseDecrease = bombCasing - 5;
                    bombsCasingStack.Pop();
                    bombsCasingStack.Push(caseDecrease);
                }
            }
            if (isTrue)
            {
                Console.WriteLine("You don't have enough materials to fill the bomb pouch.");
            }


            if (bombEffectQueue.Count > 0)
            {
                Console.WriteLine($"Bomb Effects: { string.Join(", ", bombEffectQueue)}");
            }
            else
            {
                Console.WriteLine("Bomb Effects: empty");
            }
            if (bombsCasingStack.Count > 0)
            {
                Console.WriteLine($"Bomb Casings: {string.Join(", ", bombsCasingStack)}");
            }
            else
            {
                Console.WriteLine("Bomb Casings: empty");
            }

            foreach (var bomb in createdBombsCount.OrderBy(x => x.Key))
            {
                Console.WriteLine($"{bomb.Key}: {bomb.Value}");
            }

        }
    }
}
