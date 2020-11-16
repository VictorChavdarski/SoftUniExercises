using System;
using System.Linq;
using System.Collections.Generic;

namespace ProblemOne
{
    class Program
    {
        static void Main(string[] args)
        {
            var tasksInput = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
            var threadsInput = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int taskToKill = int.Parse(Console.ReadLine());

            var threadsQueue = new Queue<int>(threadsInput);
            var tasksStack = new Stack<int>(tasksInput);

            int taskKiller = 0;

            while (threadsQueue.Count > 0 || tasksStack.Count > 0)
            {
                int currentTask = tasksStack.Peek();
                int currentThread = threadsQueue.Peek();
                if (currentTask == taskToKill)
                {
                    taskKiller = currentThread;
                    break;
                }
                if (currentThread >= currentTask)
                {
                    tasksStack.Pop();
                    threadsQueue.Dequeue();
                }
                if (currentThread < currentTask)
                {
                    threadsQueue.Dequeue();
                }
            }
            Console.WriteLine($"Thread with value {taskKiller} killed task {taskToKill}");
            Console.WriteLine(string.Join(' ', threadsQueue));

        }
    }
}

