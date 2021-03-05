using System;

namespace _02.AdAstra
{
    class Program
    {
        static void Main(string[] args)
        {
             string input = Console.ReadLine();
            // string pattern = @"([\|#])(?<food>[a-zA-Z {1,}]+)([\|#])(?<date>[0-9]+\/[0-9]+\/[0-9]{2})([\|#])(?<calories>[0-9]+)([\|#])";
            string pattern = @"([#|\|])(?<food>[a-zA-Z ]+)(\1)(?<date>[0-9]{2}\/[0-9]{2}\/[0-9]{2})(\1)(?<calories>[0-9]+)(\1)";
            MatchCollection matches = Regex.Matches(input,pattern);

            int totalCalories = 0;

            var calories = new List<int>();
            for (int i = 0; i < matches.Count; i++)
            {
                int matchCalories = int.Parse(matches[i].Groups["calories"].Value);
                calories.Add(matchCalories);
            }

            totalCalories = calories.Sum();

            int days = totalCalories / 2000;

            Console.WriteLine($"You have food to last you for: {days} days!");

            for (int i = 0; i < matches.Count; i++)
            {
                string food = matches[i].Groups["food"].Value;
                string date = matches[i].Groups["date"].Value;
                string caloriesCount = matches[i].Groups["calories"].Value;
                Console.WriteLine($"Item: {food}, Best before: {date}, Nutrition: {caloriesCount}");
            }
            
            
        }
    }
}
