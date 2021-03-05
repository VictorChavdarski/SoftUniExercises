using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.Pirates
{
    class Program
    {
        class PirateCities
        {
            public int Population { get; set; }
            public int Gold { get; set; }
        }

        static void Main(string[] args)
        {
            var cities = new Dictionary<string, PirateCities>();



            string untilSail = string.Empty;

            while ((untilSail = Console.ReadLine()) != "Sail")
            {
                string[] input = untilSail.Split("||");
                string cityName = input[0];
                int population = int.Parse(input[1]);
                int gold = int.Parse(input[2]);
                PirateCities city = new PirateCities() { Population = population, Gold = gold };
                if (cities.ContainsKey(cityName))
                {
                    cities[cityName].Gold += gold;
                    cities[cityName].Population += population;
                }
                else
                {
                    cities.Add(cityName, city);
                }
            }

            string untilEnd = string.Empty;

            while ((untilEnd = Console.ReadLine()) != "End")
            {
                string[] input = untilEnd.Split("=>");
                string commOne = input[0];
                if (commOne == "Plunder")
                {
                    string town = input[1];
                    int kills = int.Parse(input[2]);
                    int goldEarned = int.Parse(input[3]);
                    cities[town].Population -= kills;
                    cities[town].Gold -= goldEarned;
                    Console.WriteLine($"{town} plundered! {goldEarned} gold stolen, {kills} citizens killed.");
                    if (cities[town].Population <= 0 || cities[town].Gold <= 0)
                    {
                        Console.WriteLine($"{town} has been wiped off the map!");
                        cities.Remove(town);
                    }

                }
                if (commOne == "Prosper")
                {
                    string town = input[1];
                    int gold = int.Parse(input[2]);
                    if (gold < 0)
                    {
                        Console.WriteLine("Gold added cannot be a negative number!");
                    }
                    else
                    {
                        cities[town].Gold += gold;
                        Console.WriteLine($"{gold} gold added to the city treasury. {town} now has {cities[town].Gold} gold.");
                    }
                }
            }
            if (cities.Count == 0)
            {
                Console.WriteLine("Ahoy, Captain! All targets have been plundered and destroyed!");
            }
            else
            {
                Console.WriteLine($"Ahoy, Captain! There are {cities.Count} wealthy settlements to go to:");
                var sortedCities = cities.OrderByDescending(h => h.Value.Gold).ThenBy(h => h.Key);
                foreach (var city in sortedCities)
                {
                    Console.WriteLine($"{city.Key} -> Population: {city.Value.Population} citizens, Gold: {city.Value.Gold} kg");
                }
            }
        }
    }
}
