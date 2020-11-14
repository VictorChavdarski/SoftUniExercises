using System;
using System.Collections.Generic;
using Raiding.Models;

namespace Raiding
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            ICollection<BaseHero> heroes = new List<BaseHero>();

            BaseHero hero;

            for (int i = 0; i < n; i++)
            {
                string name = Console.ReadLine();
                string type = Console.ReadLine();
                if (type == typeof(Paladin).Name)
                {
                    hero = new Paladin(name);
                    heroes.Add(hero);
                }
                else if (type == typeof(Druid).Name)
                {
                    hero = new Druid(name);
                    heroes.Add(hero);
                }
                else if (type == typeof(Rogue).Name)
                {
                    hero = new Rogue(name);
                    heroes.Add(hero);
                }
                else if (type ==typeof(Warrior).Name)
                {
                    hero = new Warrior(name);
                    heroes.Add(hero);
                }
                else
                {
                    Console.WriteLine("Invalid hero!");
                    i--;
                }
            }

            int bossPower = int.Parse(Console.ReadLine());
            int heroesPowerSum =0;
            foreach (var player in heroes )
            {
                Console.WriteLine(player.CastAbility());
                heroesPowerSum += player.Power;
            }

            if (heroesPowerSum >= bossPower)
            {
                Console.WriteLine("Victory!");
            }
            else
            {
                Console.WriteLine("Defeat...");
            }
        }
    }
}
