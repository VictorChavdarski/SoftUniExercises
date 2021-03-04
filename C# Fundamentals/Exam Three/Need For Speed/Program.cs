using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.Nfs
{
    class Program
    {
        static void Main(string[] args)
        {
            int carNumbers = int.Parse(Console.ReadLine());

            var carsMileage = new Dictionary<string, int>();
            var carsFuel = new Dictionary<string, int>();
            for (int i = 0; i < carNumbers; i++)
            {
                string[] input = Console.ReadLine().Split("|");
                string carName = input[0];
                int mileage = int.Parse(input[1]);
                int fuel = int.Parse(input[2]);
                carsMileage.Add(carName, mileage);
                carsFuel.Add(carName, fuel);
            }

            string command = string.Empty;

            while ((command = Console.ReadLine()) != "Stop")
            {
                string[] splitted = command.Split(" : ");
                string commOne = splitted[0];
                if (commOne == "Drive")
                {
                    string currentCar = splitted[1];
                    int distance = int.Parse(splitted[2]);
                    int fuel = int.Parse(splitted[3]);
                    if (carsFuel[currentCar] < fuel)
                    {
                        Console.WriteLine("Not enough fuel to make that ride");
                    }
                    else
                    {
                        carsMileage[currentCar] += distance;
                        carsFuel[currentCar] -= fuel;
                        Console.WriteLine($"{currentCar} driven for {distance} kilometers. {fuel} liters of fuel consumed.");
                    }
                    if (carsMileage[currentCar] >= 100000)
                    {
                        Console.WriteLine($"Time to sell the {currentCar}!");
                        carsFuel.Remove(currentCar);
                        carsMileage.Remove(currentCar);
                    }
                }

                if (commOne == "Refuel")
                {
                    string currentCar = splitted[1];
                    int fuel = int.Parse(splitted[2]);
                    int currentFuel = carsFuel[currentCar];
                    carsFuel[currentCar] += fuel;
                    if (carsFuel[currentCar] >= 75) //true 90 > 75
                    {
                        carsFuel[currentCar] = 75;
                        Console.WriteLine($"{currentCar} refueled with {75 - currentFuel} liters");
                    }
                    else
                    {
                        Console.WriteLine($"{currentCar} refueled with {fuel} liters");
                    }
                }

                if (commOne == "Revert")
                {
                    string currentCar = splitted[1];
                    int kilometers = int.Parse(splitted[2]);
                    carsMileage[currentCar] -= kilometers;
                    if (carsMileage[currentCar] < 10000)
                    {
                        carsMileage[currentCar] = 10000;
                    }
                    else
                    {
                        Console.WriteLine($"{currentCar} mileage decreased by {kilometers} kilometers");
                    }
                }
            }

            var printingCars = carsMileage.OrderByDescending(x => x.Value)
                .ThenBy(x => x);
            foreach (var item in printingCars)
            {
                Console.WriteLine($"{item.Key} -> Mileage: {item.Value} kms, Fuel in the tank: {carsFuel[item.Key]} lt.");
            }

        }
    }
}
