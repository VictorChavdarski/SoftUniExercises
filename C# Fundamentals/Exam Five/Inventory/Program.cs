using System;
using System.Collections.Generic;
using System.Linq;

namespace _05.BonusScoringSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> journal = Console.ReadLine().Split(", ").ToList();


            string input = Console.ReadLine();

            while ("Craft!" != input)
            {
                string[] tokens = input.Split(" - ");
                string command = tokens[0];
                string item = tokens[1];

                if ("Collect" == command)
                {
                    if (!journal.Contains(item))
                    {
                        journal.Add(item);
                    }
                }
                else if ("Drop" == command)
                {
                    if (journal.Contains(item))
                    {
                        journal.Remove(item);
                    }
                }
                else if ("Combine Items" == command)
                {
                    string[] items = item.Split(":");
                    string oldItem = items[0];
                    string newitem = items[1];

                    int index = journal.IndexOf(oldItem);

                    if (index >= 0)
                    {
                        journal.Insert(index + 1, newitem);
                    }

                }
                else if ("Renew" == command)
                {
                    int index = journal.IndexOf(item);
                    if (index >= 0)
                    {
                        string element = journal[index];
                        journal.Remove(element);
                        journal.Add(element);
                    }
                }

                input = Console.ReadLine();
            }

            Console.WriteLine(string.Join(", ", journal));

        }
    }
}
