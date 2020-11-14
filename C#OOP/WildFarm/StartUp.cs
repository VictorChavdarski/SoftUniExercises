using System;
using System.Collections.Generic;
using WildFarm.Models;
using WildFarm.Models.Animals;
using WildFarm.Models.Foods;

namespace WildFarm
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            ICollection<Animal> animals = new List<Animal>();

            string command;

            Animal animal;
            while ((command = Console.ReadLine()) != "End")
            {
                string[] animalInput = command.Split();
                string[] foodInput = Console.ReadLine().Split();
                string animalType = animalInput[0];
                string animalName = animalInput[1];
                double animalWeight =double.Parse(animalInput[2]);
                string foodType = foodInput[0];
                int foodQuantity = int.Parse(foodInput[1]);
                if (animalType == typeof(Cat).Name)
                {
                    string livingRegion = animalInput[3];
                    string breed = animalInput[4];
                    animal = new Cat(animalName,animalWeight,livingRegion,breed);
                    animals.Add(animal);
                    Console.WriteLine(animal.ProduceSound());
                    if (foodType != typeof(Vegetable).Name && foodType != typeof(Meat).Name)
                    {
                        Console.WriteLine($"{animalType} does not eat {foodType}!");
                    }
                    else
                    {
                        animal.Weight += (0.30 * foodQuantity);
                        animal.FoodEaten += foodQuantity;
                    }
                }
                else if (animalType==typeof(Tiger).Name)
                {
                    string livingRegion = animalInput[3];
                    string breed = animalInput[4];
                    animal = new Tiger(animalName, animalWeight, livingRegion, breed);
                    animals.Add(animal);
                    Console.WriteLine(animal.ProduceSound());
                    if (foodType != typeof(Meat).Name)
                    {
                        Console.WriteLine($"{animalType} does not eat {foodType}!");
                    }
                    else
                    {
                        animal.Weight += (1.0 * foodQuantity);
                        animal.FoodEaten += foodQuantity;
                    }
                }
                else if (animalType == typeof(Dog).Name)
                {
                    string livingRegion = animalInput[3];
                    animal = new Dog(animalName, animalWeight, livingRegion);
                    animals.Add(animal);
                    Console.WriteLine(animal.ProduceSound());
                    if (foodType != typeof(Meat).Name)
                    {
                        Console.WriteLine($"{animalType} does not eat {foodType}!");
                    }
                    else
                    {
                        animal.Weight += (0.40 * foodQuantity);
                        animal.FoodEaten += foodQuantity;
                    }
                }
                else if (animalType == typeof(Mouse).Name)
                {
                    string livingRegion = animalInput[3];
                    animal = new Mouse(animalName, animalWeight, livingRegion);
                    animals.Add(animal);
                    Console.WriteLine(animal.ProduceSound());
                    if (foodType != typeof(Vegetable).Name && foodType != typeof(Fruit).Name)
                    {
                        Console.WriteLine($"{animalType} does not eat {foodType}!");
                    }
                    else
                    {
                        animal.Weight += (0.10 * foodQuantity);
                        animal.FoodEaten += foodQuantity;
                    }

                }
                else if (animalType == typeof(Hen).Name)
                {
                    double wingSize = double.Parse(animalInput[3]);
                    animal = new Hen(animalName, animalWeight, wingSize);
                    animals.Add(animal);
                    Console.WriteLine(animal.ProduceSound());
                    animal.Weight += (0.35 * foodQuantity);
                    animal.FoodEaten += foodQuantity;
                }
                else if (animalType == typeof(Owl).Name)
                {
                    double wingSize = double.Parse(animalInput[3]);
                    animal = new Owl(animalName, animalWeight, wingSize);
                    animals.Add(animal);
                    Console.WriteLine(animal.ProduceSound());
                    if (foodType != typeof(Meat).Name)
                    {
                        Console.WriteLine($"{animalType} does not eat {foodType}!");
                    }
                    else
                    {
                        animal.Weight += (0.25 * foodQuantity);
                        animal.FoodEaten += foodQuantity;
                    }
                }
            }

            foreach (Animal currAnimal in animals)
            {
                Console.WriteLine(currAnimal);
            }



        }
    }
}
