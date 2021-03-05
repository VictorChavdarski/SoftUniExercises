using System;
using System.Text;
using System.Threading;

namespace ActivationKeys
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            string command = string.Empty;

            while ((command = Console.ReadLine()) != "Generate")
            {
                string[] splitted = command.Split(">>>");
                string commOne = splitted[0];
                if (commOne == "Contains")
                {
                    string substring = splitted[1];
                    if (input.Contains(substring))
                    {
                        Console.WriteLine($"{input} contains {substring}");
                    }
                    else
                    {
                        Console.WriteLine("Substring not found!");
                    }
                }
                else if (commOne == "Flip")
                {
                    string action = splitted[1];
                    int startIndex = int.Parse(splitted[2]);
                    int endIndex = int.Parse(splitted[3]);
                    StringBuilder sb = new StringBuilder();
                    if (action == "Upper")
                    {
                        for (int i = startIndex; i < endIndex; i++)
                        {
                            sb.Append(input[i]);
                        }
                        string cut = sb.ToString();
                        string newCut = sb.ToString().ToUpper();
                        input = input.Replace(cut, newCut);
                        Console.WriteLine(input);
                    }
                    else if (action == "Lower")
                    {
                        for (int i = startIndex; i < endIndex; i++)
                        {
                            sb.Append(input[i]);
                        }
                        string cut = sb.ToString();
                        string newCut = sb.ToString().ToLower();
                        input = input.Replace(cut, newCut);
                        Console.WriteLine(input);
                    }
                }
                else if (commOne == "Slice")
                {
                    int startIndex = int.Parse(splitted[1]);
                    int endIndex = int.Parse(splitted[2]);
                    string cut = input.Substring(startIndex, endIndex - 2);
                    input = input.Replace(cut, string.Empty);

                    Console.WriteLine(input);
                }
            }
            Console.WriteLine($"Your activation key is: {input}");
        }
    }
}
