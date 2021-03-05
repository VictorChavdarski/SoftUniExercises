using System;

namespace MirrorWords
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();

            var pairs = new List<string>();

            string pattern = @"([@,#])(?<wordOne>[a-zA-Z]{3,})(\1)(\1)(?<wordTwo>[a-zA-Z]{3,})(\1)";

            MatchCollection matches = Regex.Matches(input, pattern);

            foreach (Match match in matches)
            {
                string word1 = match.Groups["wordOne"].Value;
                string word2 = match.Groups["wordTwo"].Value;

                string reversed =String.Concat(word1.Reverse());

                if (reversed == word2)
                {
                    pairs.Add($"{word1} <=> {word2}");
                }
            }

            if (matches.Count==0)
            {
                Console.WriteLine("No word pairs found!");
            }
            else
            {
                Console.WriteLine($"{matches.Count} word pairs found!");
            }

            if (pairs.Count==0)
            {
                Console.WriteLine("No mirror words!");
            }
            else
            {
                Console.WriteLine("The mirror words are: ");
                Console.WriteLine(string.Join(", ",pairs));
            }

        }
    }
}
