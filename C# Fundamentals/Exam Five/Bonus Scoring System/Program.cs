using System;
using System.Collections.Generic;
using System.Linq;
namespace ListExercises
{
    class Program
    {
        static void Main(string[] args)
        {

            int studentsCount = int.Parse(Console.ReadLine());
            int lecturesCount = int.Parse(Console.ReadLine());
            int bonus = int.Parse(Console.ReadLine());

            int maxAtendances = 0;
            double studentWithMaxBonus = 0;
            for (int i = 0; i < studentsCount; i++)
            {
                int currentAtendances = int.Parse(Console.ReadLine());

                double currentBonus = ((1.0 * currentAtendances / lecturesCount) * (5 + bonus));

                if (currentBonus > studentWithMaxBonus)
                {
                    studentWithMaxBonus = currentBonus;
                }
                if (currentAtendances > maxAtendances)
                {
                    maxAtendances = currentAtendances;

                }

            }
            studentWithMaxBonus = Math.Ceiling(studentWithMaxBonus);
            Console.WriteLine($"Max Bonus: {studentWithMaxBonus}.");
            Console.WriteLine($"The student has attended {maxAtendances} lectures.");


        }
    }
}
