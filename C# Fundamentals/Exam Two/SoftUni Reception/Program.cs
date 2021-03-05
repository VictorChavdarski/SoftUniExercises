using System;

namespace _01.SoftUniReception
{
    class Program
    {
        static void Main(string[] args)
        {
             int employeeOne = int.Parse(Console.ReadLine());
            int employeeTwo = int.Parse(Console.ReadLine());
            int employeeThree = int.Parse(Console.ReadLine());

            int studentsCount = int.Parse(Console.ReadLine());

            int studentsPerHour = employeeOne + employeeTwo + employeeThree;

            int hours = 0;

            while (studentsCount > 0)
            {
                hours++;
                if (hours%4==0)
                {
                    continue;
                }

                studentsCount -= studentsPerHour;
            }
            Console.WriteLine($"Time needed: {hours}h.");

        }
    }
}
